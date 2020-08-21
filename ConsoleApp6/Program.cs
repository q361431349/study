using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Program
    {
        static async Task Main(string[] args)
        {
            CalculateResult(true);


            await CalculateResult(false);




            Console.ReadKey();
        }

        public static async Task CalculateResult(bool b)
        {


            var expensiveResultTask = Task.Run(async () =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine(i);
                    if (b)
                    {
                        await Task.Delay(1000);
                    }

                }
                Console.WriteLine();
            });

            await expensiveResultTask.ConfigureAwait(false);
        }
    }
}
