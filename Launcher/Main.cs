using TCPServer.Startup;
/***
 * Base Launcher can be anything so long as you reference the C# Library
 */

/***
 *  TODO: Use command line arguments to specifically only run certain application as the launcher will provide different types of services.
 *  When no commands given run all services.
 */

namespace Launcher
{
    internal class Launcher
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CounterSharpO2 Project");
            Thread TCPThread = new Thread(new ThreadStart(Startup.Start));


            // Start TCP Server
            TCPThread.Start();


            TCPThread.Join();

        }
    }
}
