using izolabella.Storage.Objects.DataStores;
using izolabella.Util.RateLimits.Loadable;

namespace izolabella.Util.RateLimits.Limiters;

public class DateRateLimiter : IRateLimiter
{
    public DateRateLimiter(DataStore LoadFrom, string UniqueAlias, TimeSpan MinimumTimePerRequest, int MaxExtraGraceAmounts = 0, TimeSpan TimeToResetGrace = default)
    {
        this.LoadFrom = LoadFrom;
        this.MinimumTimePerRequest = MinimumTimePerRequest;
        this.MaxExtraGraceAmounts = MaxExtraGraceAmounts;
        this.TimeToResetGrace = TimeToResetGrace;
        this.LoadFrom.MakeSubStore(UniqueAlias);
    }

    public DataStore LoadFrom { get; }

    public TimeSpan MinimumTimePerRequest { get; }

    public int MaxExtraGraceAmounts { get; }

    public TimeSpan TimeToResetGrace { get; }

    public DateTime InitializedAt { get; } = DateTime.Now;

    public List<KeyValuePair<ulong, DateTime>> GraceTable { get; } = new();

    public async Task<bool> PassesAsync(ulong Id)
    {
        LimiterValue<ulong, DateTime>? Res = (await this.LoadFrom.ReadAllAsync<LimiterValue<ulong, DateTime>>()).FirstOrDefault(A => A.Key == Id);
        if (Res == null)
        {
            await this.LoadFrom.SaveAsync(new LimiterValue<ulong, DateTime>(Id, DateTime.UtcNow.Add(this.MinimumTimePerRequest)));
            return true;
        }
        else
        {
            if (DateTime.UtcNow >= Res.Value)
            {
                await this.LoadFrom.SaveAsync(new LimiterValue<ulong, DateTime>(Id, DateTime.UtcNow.Add(this.MinimumTimePerRequest), Res.Id));
                return true;
            }
            else
            {
                foreach(KeyValuePair<ulong, DateTime> T in this.GraceTable.Where(UD => UD.Key == Id).ToList())
                {
                    if(DateTime.UtcNow >= T.Value)
                    {
                        this.GraceTable.Remove(T);
                    }
                }
                if(this.GraceTable.Where(UD => UD.Key == Id).Count() >= this.MaxExtraGraceAmounts)
                {
                    return false;
                }
                else
                {
                    this.GraceTable.Add(new(Id, DateTime.UtcNow.Add(this.TimeToResetGrace)));
                    return true;
                }
            }
        }
    }

    public bool Passes(ulong Id)
    {
        return this.PassesAsync(Id).Result;
    }

    /// <summary>
    /// Deletes all existing records for ratelimits.
    /// </summary>
    public async Task ClearAsync()
    {
        this.GraceTable.Clear();
        await this.LoadFrom.DeleteAllAsync();
    }
}
