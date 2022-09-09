using izolabella.Storage.Objects.DataStores;
using izolabella.Util.RateLimits.Loadable;

namespace izolabella.Util.RateLimits.Limiters;

public class MaxNumRateLimiter : IRateLimiter
{
    public MaxNumRateLimiter(DataStore PersistFrom, string UniqueAlias, int MaxAllowed)
    {
        this.PersistFrom = PersistFrom;
        this.MaxAllowed = MaxAllowed;
        this.PersistFrom.MakeSubStore(UniqueAlias);                 
    }

    public DataStore PersistFrom { get; }

    public int MaxAllowed { get; }

    public DateTime InitializedAt { get; } = DateTime.Now;
    
    public async Task<bool> PassesAsync(ulong Id)
    {
        LimiterValue<ulong, int>? Res = (await this.PersistFrom.ReadAllAsync<LimiterValue<ulong, int>>()).FirstOrDefault(A => A.Key == Id);
        if (Res == null)
        {
            await this.PersistFrom.SaveAsync(new LimiterValue<ulong, int>(Id, 1));
            return true;
        }
        else
        {
            if (this.MaxAllowed >= Res.Value)
            {
                await this.PersistFrom.SaveAsync(new LimiterValue<ulong, int>(Id, Res.Value + 1, Res.Id));
                return true;
            }
            else
            {
                return false;
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
        await this.PersistFrom.DeleteAllAsync();
    }
}
