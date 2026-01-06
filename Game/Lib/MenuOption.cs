namespace Game.Lib;

public class MenuOption(string command, string text, Action action)
{
    public string Command { get; set; } = command;
    public string Text { get; set; } = text;
    public Action Action { get; set; } = action;
}
