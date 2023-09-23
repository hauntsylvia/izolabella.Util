using izolabella.Storage.Objects.Structures;

namespace izolabella.Util.RateLimits.Loadable
{
    public class LimiterValue<TKey, TValue>(TKey Key, TValue Value, ulong? Id = null) : IDataStoreEntity where TKey : notnull where TValue : notnull
    {
        public ulong Id { get; } = Id ?? IdGenerator.CreateNewId();

        public TKey Key { get; } = Key;

        public TValue Value { get; } = Value;
    }
}