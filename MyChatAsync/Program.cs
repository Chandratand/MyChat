using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyChatSocketAsync;

namespace MyChatAsync
{
    class Program
    {
        static MyChatServer mServer;
        static void Main(string[] args)
        {
            mServer = new MyChatServer();
            mServer.StartListeningForIncomingConnection();

            Console.ReadKey();
        }
    }
}
