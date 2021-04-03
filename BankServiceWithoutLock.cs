using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DistributedLockDemoWithThreads
{
    public class BankServiceWithoutLock : IBankService
    {
        private int balance = 0;

        public BankServiceWithoutLock(int balance)
        {
            this.balance = balance;
        }
        public void Deposit(int amount)
        {
            //One assignment operation is converted to multiple operations to simulate a long operation
            int current = balance;
            current += amount;
            Thread.Sleep(10);
            balance = current;

            Console.WriteLine($"[{Thread.CurrentThread.Name}] Balance increased by {amount} to {balance}");

        }

        public bool Withdraw(int amount)
        {
            //One assignment operation is converted to multiple operations to simulate a long operation
            int current = balance;
            current -= amount;
            Thread.Sleep(10);
            balance = current;

            if(balance < 0)
            {
                Console.WriteLine($"[{Thread.CurrentThread.Name}] Attempting withdraw {amount} from balance of {balance + amount} FAILED!");
                balance += amount;
                return false;
            }
            Console.WriteLine($"[{Thread.CurrentThread.Name}] Balance decreased by {amount} to {balance}");
            return true;
        }

        public int BalanceQuery()
        {
            return balance;
        }


    }
}
