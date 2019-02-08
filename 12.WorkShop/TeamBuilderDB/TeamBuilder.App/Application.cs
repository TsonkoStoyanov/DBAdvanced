using System;

namespace TeamBuilder.App
{
    class Application
    {
        static void Main()
        {
            Engine engine = new Engine(new CommandDispatcher());
            engine.Run();
        }
    }
}
