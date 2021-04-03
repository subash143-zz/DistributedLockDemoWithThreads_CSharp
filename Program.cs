using System;
using System.Threading;

namespace DistributedLockDemoWithThreads
{
    class Program
    {
        Random r = new Random(2);
        static void Main(string[] args)
        {
            Program program = new Program();

            //Random generator with seed for reproduction of situation
            Random r = new Random(200);

            IBankService bankService;
            //Uncomment this for checking without lock
            bankService = new BankServiceWithoutLock(500);
            program.RunBankService(bankService, false);

            //Uncomment this for checking with lock
            bankService = new BankServiceWithLock(500);
            program.RunBankService(bankService, true);

        }

        public void RunBankService(IBankService bankService, bool hasLock)
        {
            int expectedBalance = 500;
            if (hasLock)
            {
                Console.WriteLine("-------System with a LOCK mechanism----------");
            }
            else
            {
                Console.WriteLine("-------System without LOCK mechanism----------");
            }
            Console.WriteLine("Account initialized with $500.");

            //Create multiple threads in an array
            Thread[] threads = new Thread[11];

            //Set different tasks to thread
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() =>
                {
                    int value = r.Next(-500, 500);
                    if (value > 0)
                    {
                        bankService.Deposit(value);
                        expectedBalance += value;
                    }
                    else
                    {
                        if (bankService.Withdraw(0 - value))
                        {
                            expectedBalance += value;
                        }
                    }
                });
                threads[i].Name = i.ToString();
            }

            //Start all the threads one by one
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }

            //Join threads to allow continue the parent thread
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            //Display expected and actual values
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine($"Expected Balance = {expectedBalance}");
            Console.WriteLine($"Actual Balance = {bankService.BalanceQuery()}");
            Console.WriteLine("");
        }


    }
}
