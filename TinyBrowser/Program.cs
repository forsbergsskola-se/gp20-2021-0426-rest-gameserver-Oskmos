using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;


namespace TinyBrowser
{
    class Program
    {

        private static Stream stream;
        
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient("acme.com", 80);
            stream = client.GetStream();

            var bytes = Encoding.ASCII.GetBytes("GET / HTTP/1.1\r\nHost:acme.com\r\n\r\n");
            
            stream.Write(bytes);

            var respons = new byte[9552];
            stream.Read(respons);
            var responsString = Encoding.ASCII.GetString(respons);

            Console.WriteLine(getBetween(responsString, "<title>", "</title>"));
            

            Console.ReadLine();
        }

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }
        
    }
}
