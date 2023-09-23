namespace izolabella.Util.RateLimits.Limiters
{
    public interface IRateLimiter
    {
        /// <summary>
        /// Checks if an element of an id passes this ratelimiter.
        /// </summary>
        /// <param name="Id">The unique identifier of the object to check.</param>
        /// <returns></returns>
        public Task<bool> PassesAsync(ulong Id);

        /// <summary>
        /// Checks if an element of an id passes this ratelimiter synchronously.
        /// </summary>
        /// <param name="Id">The unique identifier of the object to check.</param>
        /// <returns></returns>
        public bool Passes(ulong Id);
    }
}