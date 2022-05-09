using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            EndPoint iep = new IPEndPoint(IPAddress.Parse("192.168.1.3"), 9595);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(iep);

            bool End = true;

            while (End)
            {
                byte[] msgBytes = new byte[1024];

                int msgSize = socket.Receive(msgBytes);

                string msgStr = Encoding.ASCII.GetString(msgBytes, 0, msgSize);

                Console.WriteLine(msgStr);

                if (msgStr == "Bye")
                {
                    End = false;
                }

                string str = Console.ReadLine();

                byte[] msgByte = Encoding.ASCII.GetBytes(str);

                socket.Send(msgByte);
            }

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
