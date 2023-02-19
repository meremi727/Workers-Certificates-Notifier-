using Newtonsoft.Json.Linq;
using System;
using System.Threading;

namespace WpfApp1.Helpers
{
    public class CacheEntry<T> : ICacheEntry<T>
    where T : class
    {
        public T Value
        {
            get => !_isDisposed ? _value
                : throw new ObjectDisposedException(nameof(CacheEntry<T>));
        }

        internal CacheEntry(T value, object _lock)
        {
            this._lock = _lock;
            _value = value;
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                Monitor.Exit(_lock);
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                _value = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                _isDisposed = true;
                GC.SuppressFinalize(this);
            }
        }

        private bool _isDisposed = false;
        private T _value;
        private readonly object _lock;
    }
}
