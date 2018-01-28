namespace Blookia
{
    public class Transaction
    {
        /// <summary>
        /// Withdrawal account number.
        /// </summary>
        public int Tx { get; set; }

        /// <summary>
        /// Deposit account number.
        /// </summary>
        public int Rx { get; set; }

        /// <summary>
        /// Amount of funds to transfer from Tx to Rx.
        /// </summary>
        public double Amount { get; set; }

        public override string ToString()
        {
            return $"{Tx} -> {Rx} ({Amount})";
        }
    }
}
