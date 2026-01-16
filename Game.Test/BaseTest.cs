using Game.Core;

namespace Game.Test;

public abstract class BaseTest : IDisposable
{
    public void Dispose()
    {
        Console.WriteLine("Reseting globals..");
        Globals.Reset();
    }
}
