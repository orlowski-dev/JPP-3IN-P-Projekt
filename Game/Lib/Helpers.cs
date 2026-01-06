using System.Reflection;
using System.Text.Json;
using Game.Core;

namespace Game.Lib;

public static class Helpers
{
    public static T GetInput<T>(string? message = null)
    {
        while (true)
        {
            try
            {
                var prompt = message ?? "> ";
                Console.Write(prompt);
                var buff =
                    Console.ReadLine()
                    ?? throw new Exception("Nie można pobrać danych ze standardowego wejścia!");
                buff = buff.Trim();
                return (T)Convert.ChangeType(buff, typeof(T));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Globals.ExitGame = true;
            }
        }
    }

    public static void InvokeStaticMethod(string className, string methodName)
    {
        try
        {
            var type =
                Type.GetType(className) ?? throw new Exception($"Klasa {className} nie istnieje!");
            var method =
                type.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public)
                ?? throw new Exception($"Metoda {className}.{methodName} nie istnieje!");
            method.Invoke(null, null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Globals.ExitGame = true;
        }
    }

    public static void ExecuteCommand(string command, ref List<MenuOption> options)
    {
        while (true)
        {
            var selectedCommand = options.Find((obj) => obj.Command == command);
            if (selectedCommand == null)
            {
                Console.WriteLine("Nieznane polecenie!");
                return;
            }
            selectedCommand.Action.Invoke();
            break;
        }
    }

    public static (string, string) GetViewClassAndMethod(string view)
    {
        var v = view.Trim();
        var splitted = view.Split(":");
        if (splitted.Length != 2)
        {
            throw new FormatException(
                "Nieprawidłowy format nazwy View. Oczekiwany format Class:Method"
            );
        }
        return (splitted[0], splitted[1]);
    }

    public static void ChangeView(string view)
    {
        Console.Clear();
        Globals.View = view.Trim();
    }

    public static void PrintMenuOptions(ref List<MenuOption> options)
    {
        foreach (var option in options)
        {
            Console.WriteLine($"{option.Command} - {option.Text}");
        }
    }

    public static T? GetObjectFromJsonFile<T>(string path)
        where T : class
    {
        try
        {
            var jsonContent = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(jsonContent);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public static bool LoadInitData()
    {
        var initDataFromFile = GetObjectFromJsonFile<InitialData>("Data/initData.json");
        if (initDataFromFile == null)
            return false;
        Globals.InitialData = initDataFromFile;
        return true;
    }

    public static Dictionary<string, string> ConcatTwoDicts(
        Dictionary<string, string> d1,
        Dictionary<string, string>? d2
    )
    {
        var finalDict = new Dictionary<string, string>();

        if (d2 != null)
        {
            finalDict = d1.Concat(d2).ToDictionary(x => x.Key, x => x.Value);
            return finalDict;
        }

        return d1;
    }

    public static int GetInputInRange(int start, int stop, string? message = null)
    {
        var prompt = message ?? $"[{start}-{stop}]> ";
        int input;
        do
        {
            input = GetInput<int>(prompt);
        } while (input < start || input > stop);
        return input;
    }
}
