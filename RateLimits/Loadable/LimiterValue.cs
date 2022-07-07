using izolabella.Storage.Objects.Structures;

namespace izolabella.Util.RateLimits.Loadable
{
    public class LimiterValue<TKey, TValue> : IDataStoreEntity where TKey : notnull where TValue : notnull
    {
        public LimiterValue(TKey Key, TValue Value, ulong? Id = null) : base()
        {
            this.Key = Key;
            this.Value = Value;
            this.Id = Id ?? IdGenerator.CreateNewId();
        }

        public ulong Id { get; }

        public TKey Key { get; }

        public TValue Value { get; }
    }
}
