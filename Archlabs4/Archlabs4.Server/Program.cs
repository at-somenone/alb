using System;
using System.Collections;
using Nwc.XmlRpc;

namespace Archlabs4.Server
{
    class Server
    {
        private static void Main(string[] args) {
            var port = 8005;
            var server = new XmlRpcServer(port);
            server.Add("servese", new Server());
            server.Start();
            Console.ReadKey();
            server.Stop();
        }

        [XmlRpcExposed]
        public ArrayList DoStuff(ArrayList arrList, int size) {
            int min1 = int.MaxValue;
            int min2 = int.MaxValue;
            for (var i = 0; i < size; i++) {
                min1 = Math.Min(min1, (int) arrList[i * size + i]);
                min2 = Math.Min(min2, (int) arrList[i * size + size - i - 1]);
            }

            for (var i = 0; i < size; i++) {
                for (var j = 0; j < size; j++) {
                    if (min1 <= min2) {
                        if (i == j)
                            arrList[i * size + j] = 0;

                        if (i > j)
                            arrList[i * size + j] = (int) Math.Pow((int) arrList[i * size + j], 2);
                    }

                    if (min1 > min2) {
                        if (i == size - j - 1)
                            arrList[i * size + j] = 0;

                        if (i > size - j - 1)
                            arrList[i * size + j] = (int) Math.Pow((int) arrList[i * size + j], 2);
                    }
                }
            }

            arrList.Add(Math.Min(min1, min2));

            return arrList;
        }
    }
}
