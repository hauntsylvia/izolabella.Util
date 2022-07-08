namespace izolabella.Util
{
    public class IdGenerator
    {
        public IdGenerator()
        {
        }

        /// <summary>
        /// Generates a <see cref="ulong"/> from a <see cref="Guid"/>.
        /// </summary>
        /// <returns></returns>
        public static ulong CreateNewId()
        {
            return BitConverter.ToUInt64(Guid.NewGuid().ToByteArray(), 0);
        }
    }
}
