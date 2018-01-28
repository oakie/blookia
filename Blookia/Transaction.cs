namespace Blookia
{
    public class Transaction
    {
        public int Tx { get; set; }
        public int Rx { get; set; }
        public double Amount { get; set; }

        public override string ToString()
        {
            return $"{Tx} -> {Rx} ({Amount})";
        }
    }
}
