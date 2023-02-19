namespace WpfApp1.Helpers
{
    public interface ICache<T>
    where T : class
    {
        ICacheEntry<T> GetEntry();
    }
}
