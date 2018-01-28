namespace Blookia
{
    public class Block
    {
        /// <summary>
        /// Pointer back to the previous block in the chain.
        /// </summary>
        public Block Parent { get; protected set; }

        /// <summary>
        /// The hash of the parent block, or null if this is the first block.
        /// </summary>
        public string ParentHash => Parent?.Hash;

        /// <summary>
        /// A compound hash of the entire blockchain, including this block.
        /// </summary>
        public string Hash { get; protected set; }

        /// <summary>
        /// The actual data stored in this block.
        /// </summary>
        public object Data { get; protected set; }

        /// <summary>
        /// Creates a new block, provided the previous block and the data to be
        /// stored in the block.
        /// </summary>
        /// <param name="parent">Parent block, or null if first block</param>
        /// <param name="data">The data to be stored</param>
        public Block(Block parent, object data)
        {
            Parent = parent;
            Data = data;
            Hash = Util.ComputeHash(new object[] { Util.ComputeHash(Data), ParentHash });
        }

        public override string ToString()
        {
            var p = ParentHash ?? "        ";
            var h = Hash ?? "        ";
            return p.Substring(0, 8) + "\t" + h.Substring(0, 8) + "\t[" + Data + "]";
        }
    }
}
