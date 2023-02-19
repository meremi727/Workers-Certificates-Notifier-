using System;
using System.Threading;

namespace WpfApp1.Helpers
{
    public class WeakReferenceCache<T> : ICache<T>
    where T : class
    {
        // Сохранение делегата, для создание кэшируемого
        // объекта тогда, когда это потребуется.
        public WeakReferenceCache(Func<T> func)
        {
            _func = func;
        }

        // Синхронизация потоков начинается в данном методе
        // и завершается в методе CacheEntry.Dispose.
        public ICacheEntry<T> GetEntry()
        {
            Monitor.Enter(_lock);
            // При необходимости создается новый инстанс кэшируемого объекта,
            // иначе получаем сильную ссылку из слабой.
            if (_cache is not null && _cache.TryGetTarget(out var entry))
            {
                return new CacheEntry<T>(entry, _lock);
            }
            else
            {
                var instance = _func();
                _cache = new WeakReference<T>(instance);
                return new CacheEntry<T>(instance, _lock);
            }
        }

        private WeakReference<T>? _cache = null;
        private readonly Func<T> _func;
        private readonly object _lock = new();
    }
}




