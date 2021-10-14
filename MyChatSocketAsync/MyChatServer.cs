using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace MyChatSocketAsync
{
    public class MyChatServer
    {
        IPAddress mIP;
        int mPort;
        TcpListener mTCPListener;

        public async void StartListeningForIncomingConnection(IPAddress ipaddr = null, int port = 23000)
        {
            if (ipaddr == null)
            {
                ipaddr = IPAddress.Any;
            }
            if (port <= 0)
            {
                port = 23000;
            }

            mIP = ipaddr;
            mPort = port;

            Console.WriteLine($"IP Address : {mIP} - Port : {mPort}");

            mTCPListener = new TcpListener(mIP, mPort);
            mTCPListener.Start();

           var returnedByAccept =  await mTCPListener.AcceptTcpClientAsync();
            Console.WriteLine("Client Berhasil Terhubung : " + returnedByAccept.ToString() );
        }

    }
}
