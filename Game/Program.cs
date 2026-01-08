using Game.Core;
using Game.Lib;

namespace Game;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        var initDataLoaded = Helpers.LoadInitData();
        if (!initDataLoaded)
            return;

        while (!Globals.ExitGame)
        {
            try
            {
                var (className, methodName) = Helpers.GetViewClassAndMethod(Globals.View);
                Helpers.InvokeStaticMethod(Globals.ViewsNamespace + "." + className, methodName);
            }
            catch (Exception e)
            {
                Helpers.PrintException(e);
                Globals.ExitGame = true;
            }
        }
    }
}
