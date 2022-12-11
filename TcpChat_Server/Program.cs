using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpChat_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Server";
            Console.WriteLine("[Server]");

            // socket TCP
            Socket socket = new Socket
                (AddressFamily.InterNetwork, SocketType.Stream,
                ProtocolType.Tcp);

            IPAddress address = IPAddress.Parse("127.0.0.1");

            //создаем endpoint = 127.0.0.1:7632
            IPEndPoint endPoint = new IPEndPoint(address, 7632);

            //привязываем сокет к endpoint
            socket.Bind(endPoint);          

            socket.Listen(1);   // переводим сокет в режим слушать

            Console.WriteLine("Ожидаем звонка от клиента...");

            Socket socker_client = socket.Accept();    // ожидаем звонка от клиента

            Console.WriteLine("Клиент на связи");

            while (true)
            {
                // получение sms
                byte[] bytes = new byte[1024];
                int num_bytes = socker_client.Receive(bytes);
                string textFromClient = Encoding.Unicode.GetString(bytes, 0, num_bytes);
                Console.WriteLine(textFromClient);

                // ответное sms
                string answer = "Server : Ok";
                byte[] bytes_answer = Encoding.Unicode.GetBytes(answer);    
                socker_client.Send(bytes_answer);
            }



            Console.ReadLine();
        }
    }
}
