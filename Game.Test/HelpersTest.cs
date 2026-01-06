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

    [Fact]
    public void TestConcatTwoDicts()
    {
        var d1 = new Dictionary<string, string> { { "a", "b" } };
        var d2 = new Dictionary<string, string> { { "c", "d" } };
        var d3 = new Dictionary<string, string> { { "a", "b" }, { "c", "d" } };
        var merged = Helpers.ConcatTwoDicts(d1, d2);

        Assert.Equal(d3, merged);
    }

    [Fact]
    public void TestConcatTwoDictsOneNull()
    {
        var d1 = new Dictionary<string, string> { { "a", "b" } };
        Dictionary<string, string>? d2 = null;
        var merged = Helpers.ConcatTwoDicts(d1, d2);

        Assert.Equal(d1, merged);
    }
}
