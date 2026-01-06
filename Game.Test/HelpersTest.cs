using Game.Lib;

namespace Game.Test;

public class HelpersTest
{
    [Fact]
    public void TestGetViewClassAndMethod()
    {
        var viewName = "abc";
        Assert.Throws<FormatException>(() => Helpers.GetViewClassAndMethod(viewName));
        viewName = "ab:cd";
        Assert.Equal(("ab", "cd"), Helpers.GetViewClassAndMethod(viewName));
    }

    [Fact]
    public void TestExecuteCommand()
    {
        static void func()
        {
            throw new NotImplementedException();
        }
        var command = "test";
        var options = new List<MenuOption> { new(command, "Opis", func) };
        Assert.Throws<NotImplementedException>(() => Helpers.ExecuteCommand(command, ref options));
    }
}
