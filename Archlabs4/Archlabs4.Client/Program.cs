using System;
using System.Collections;
using Nwc.XmlRpc;

namespace Archlabs4.Client
{
    class Program
    {
        private static void Main(string[] args) {
            var address = "http://127.0.0.1:8005";
            var req = new XmlRpcRequest();
            var size = int.Parse(Console.ReadLine()!);
            req.MethodName = "servese.DoStuff";

            var arrList = new ArrayList();
            for (var i = 0; i < size; i++) {
                for (var j = 0; j < size; j++) {
                    Console.Write($"[{i}, {j}]: ");
                    arrList.Add(int.Parse(Console.ReadLine()!));
                }
            }

            req.Params.Add(arrList);
            req.Params.Add(size);
            var response = req.Invoke(address);
            var newArrList = (ArrayList) response;

            Console.WriteLine("original matrix:");
            for (var i = 0; i < size; i++) {
                for (var j = 0; j < size; j++) {
                    Console.Write($"{arrList[i * size + j]}\t");
                }

                Console.WriteLine(" ");
            }

            Console.WriteLine("new matrix:");
            for (var i = 0; i < size; i++) {
                for (var j = 0; j < size; j++) {
                    Console.Write($"{newArrList[i * size + j]}\t");
                }

                Console.WriteLine(" ");
            }

            Console.WriteLine($"min: {newArrList[newArrList.Count-1]}");
        }
    }
}
