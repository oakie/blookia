using System;
using System.Collections.Generic;
using System.Linq;

namespace Blookia
{
    public class Blookia
    {
        /// <summary>
        /// Stores the blockchain history for easy access.
        /// </summary>
        protected List<Block> Chain { get; set; }

        /// <summary>
        /// Caches the current balance for each account number.
        /// </summary>
        protected Dictionary<int, double> Balance { get; set; }

        protected Blookia()
        {
            Chain = new List<Block>();
            Balance = new Dictionary<int, double>();
        }

        /// <summary>
        /// Creates a new Blookia blockchain with the specified genesis funds.
        /// </summary>
        /// <param name="amount">Seed funds to deposit to account 0</param>
        /// <returns>Initialized Blookia instance</returns>
        public static Blookia Create(double amount = 0)
        {
            var seed = new Transaction { Tx = -1, Rx = 0, Amount = amount };
            var genesis = new Block(null, seed);

            var blookia = new Blookia();
            blookia.Chain.Add(genesis);
            blookia.SyncBalance();

            return blookia;
        }

        /// <summary>
        /// Transfer funds from account tx to account rx.
        /// </summary>
        /// <param name="tx">Account number from which to withdraw</param>
        /// <param name="rx">Account number to which to deposit</param>
        /// <param name="amount">Amount of funds to be transferred</param>
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

        /// <summary>
        /// Initializes the account balance table from the transaction history.
        /// </summary>
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

            s += "Block\tParent\t\tHash\t\tTransaction\n";
            for (int i = 0; i < Chain.Count; ++i)
            {
                s += $"{i}\t{Chain[i]}\n";
            }
            s += line + "\n";

            return s;
        }
    }
}
