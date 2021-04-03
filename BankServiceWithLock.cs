using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DistributedLockDemoWithThreads
{
    public class BankServiceWithLock : IBankService
    {
        private int balance = 0;
        private static object _lockObject = new object();

        public BankServiceWithLock(int balance)
        {
            this.balance = balance;
        }
        public void Deposit(int amount)
        {
            lock (_lockObject)
            {
                //One assignment operation is converted to multiple operations to simulate a long operation
                int current = balance;
                current += amount;
                Thread.Sleep(10);
                balance = current;

                Console.WriteLine($"[{Thread.CurrentThread.Name}] Balance increased by {amount} to {balance}");
            }

        }

        public bool Withdraw(int amount)
        {
            lock (_lockObject)
            {
                //One assignment operation is converted to multiple operations to simulate a long operation
                int current = balance;
                current -= amount;
                Thread.Sleep(10);
                balance = current;

                if (balance < 0)
                {
                    Console.WriteLine($"[{Thread.CurrentThread.Name}] Attempting withdraw {amount} from balance of {balance + amount} FAILED!");
                    balance += amount;
                    return false;
                }
                Console.WriteLine($"[{Thread.CurrentThread.Name}] Balance decreased by {amount} to {balance}");
                return true;
            }
        }

        public int BalanceQuery()
        {
            return balance;
        }


    }
}
