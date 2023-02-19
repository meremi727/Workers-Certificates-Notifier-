using System;

namespace WpfApp1.Helpers
{
    public interface ICacheEntry<T> : IDisposable
    where T : class
    {
        T Value { get; }
    }
}
