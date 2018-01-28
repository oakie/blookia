using System;
using System.Collections.Generic;
using System.Linq;

namespace Blookia
{
    public class Blookia
    {
        public List<Block> Chain { get; protected set; } = new List<Block>();

        protected Dictionary<int, double> Balance { get; set; } = new Dictionary<int, double>();

        public Blookia()
        {
            var seed = new Transaction { Tx = -1, Rx = 0, Amount = 100 };
            var genesis = new Block(null, seed);

            Chain.Add(genesis);
            SyncBalance();
        }

        public void Transfer(int tx, int rx, double amount)
        {
            var t = new Transaction { Tx = tx, Rx = rx, Amount = amount };

            ValidateTransaction(t);
            ExecuteTransaction(t);

            var block = new Block(Chain.LastOrDefault(), t);
            Chain.Add(block);
        }

        protected void ValidateTransaction(Transaction transaction)
        {
            if (!Balance.ContainsKey(transaction.Tx) && transaction.Tx > -1)
                throw new Exception("Tx account number is invalid!");
            if (Balance[transaction.Tx] < transaction.Amount)
                throw new Exception("Tx account balance is too low!");
        }

        protected void ExecuteTransaction(Transaction transaction)
        {
            if (transaction.Tx > -1)
                Balance[transaction.Tx] -= transaction.Amount;

            if (!Balance.ContainsKey(transaction.Rx))
                Balance[transaction.Rx] = 0;

            Balance[transaction.Rx] += transaction.Amount;
        }

        protected void SyncBalance()
        {
            Balance.Clear();
            foreach (var block in Chain)
            {
                var t = block.Data as Transaction;
                ExecuteTransaction(t);
            }
        }

        public override string ToString()
        {
            var line = "================================================================================";
            var s = line + "\n";
            s += "Account #\tBalance\n";
            foreach (var account in Balance.Keys)
            {
                s += $"{account}\t\t{Balance[account]}\n";
            }
            s += line + "\n";

            s += "Block\tParent\t\tHash\n";
            for (int i = 0; i < Chain.Count; ++i)
            {
                s += $"{i}\t{Chain[i]}\n";
            }
            s += line + "\n";

            return s;
        }
    }
}
