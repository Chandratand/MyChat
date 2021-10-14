using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace MyChatSocketAsync
{
    public class MyChatServer
    {
        IPAddress mIP;
        int mPort;
        TcpListener mTCPListener;

        List<TcpClient> mClients;
        public bool KeepRunning { get; set; }

        //simple constraktor

        public MyChatServer()
        {
            mClients = new List<TcpClient>();
        }
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
            try
            {
                mTCPListener.Start();

                KeepRunning = true;
                while (KeepRunning)
                { 
                    var returnedByAccept =  await mTCPListener.AcceptTcpClientAsync();

                     mClients.Add(returnedByAccept);

                    Console.WriteLine($"Client Berhasil Terhubung, jumlah : {mClients.Count} - {returnedByAccept.Client.RemoteEndPoint}"); 
                   
                    TakeCareOfTCPClient(returnedByAccept);

                }
                
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }

            
        }

        private async void TakeCareOfTCPClient(TcpClient paramClient)
        {
            NetworkStream stream = null;
            StreamReader reader = null; // untuk membaca data dari netwrok stream yang terhubung dengan TCP Client sebagai parameter

            try
            {
                stream = paramClient.GetStream();
                reader = new StreamReader(stream);

                char[] buff = new char[64];
                while (KeepRunning)
                {
                    Console.WriteLine("*** Siap untuk membaca pesan Client");
                  int nRet = await reader.ReadAsync(buff, 0, buff.Length);

                    Console.WriteLine("Returened : " + nRet);

                    if (nRet == 0)
                    {
                        RemoveClient(paramClient);

                        Console.WriteLine("Socket Terputus");
                        break;
                    }

                    string receivedText = new string(buff);

                    Console.WriteLine("Pesan : " + receivedText);

                    Array.Clear(buff, 0, buff.Length);
                }

            }
            catch (Exception ex)
            {
                RemoveClient(paramClient);
                Console.WriteLine(ex.ToString());
            }


        }

        private void RemoveClient(TcpClient paramClient)
        {
            if (mClients.Contains(paramClient)
            {
                mClients.Remove(paramClient);
                Console.WriteLine("Client removed, jumalah : "+ mClients.Count);
            }
        }
    }
}
