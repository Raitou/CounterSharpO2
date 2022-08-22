using System;
using Server;
/***
 * Base Launcher can be anything so long as you reference the C# Library
 */


namespace Launcher
{
    internal class Launcher
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("CounterSharpO2 Project");
            await TCPServer.Start();
            
        }
    }
}
