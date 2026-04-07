using System;
using System.Collections.Generic;
using System.Text;

namespace TMMCVerticalLineCounterApp
{
    internal class App
    {
        public App()
        {
            Console.WriteLine("Initializing App...");
        }

        public Task Run()
        {
            Console.WriteLine("Running App...");

            return Task.CompletedTask;
        }
    }
}
