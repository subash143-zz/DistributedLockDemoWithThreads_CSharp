using System;
using System.Collections.Generic;
using System.Text;

namespace DistributedLockDemoWithThreads
{
    public interface IBankService
    {

        public void Deposit(int amount);

        public bool Withdraw(int amount);

        public int BalanceQuery();
    }
}
