﻿using System;
using System.Linq;

namespace Server
{
    class Program
    {
        public static int Main(string[] args)
        {
            if (args.Contains("-h"))
            {
                Console.WriteLine("Ussage:\n  " + args[0]
                                  + "\n\t-h print this message"
                                  + "\n\t-d specify root directory(default: ./WWW_Root/)"
                                  + "\n\t-p specify listen port(default: 8090)"
                                  + "\n\t-l specify listen ip(default: 127.0.0.1)"
                                  + "\n");
                return 0;
            }

            string path = GetOption(args, "d");
            if (!string.IsNullOrEmpty(path) && System.IO.Directory.Exists(path))
                RequestHandler.WebRoot = path;
            string portstr = GetOption(args, "p");
            int port = 8090;
            if (!string.IsNullOrEmpty(portstr))
            {
                int.TryParse(portstr, out port);
            }
            string ip = GetOption(args, "l");
            ip = string.IsNullOrEmpty(ip) ? "127.0.0.1" : ip;

            Console.WriteLine("Listen on: " + ip + ":" + port.ToString());

            Console.WriteLine("Root: " + RequestHandler.WebRoot);

            Console.WriteLine("\nServer Started\n");
            SocketServer.Instance.InitSocket(ip, port);
            return 0;
        }

        private static string GetOption(string[] args, string opt)
        {
            var it = ((args) as System.Collections.Generic.IEnumerable<string>).GetEnumerator();
            while (it.MoveNext())
            {
                if (it.Current[0] == '-' && it.Current.Substring(1) == opt && it.MoveNext())
                {
                    return it.Current;
                }
            }

            return string.Empty;
        }
    }
}
