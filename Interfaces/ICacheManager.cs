namespace CovidDataDashboard.Interfaces
{
    public interface ICacheManager
    {
        public TItem GetCacheItem<TItem>(object key);
    }
}
