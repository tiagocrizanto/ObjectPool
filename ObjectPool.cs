using System.Collections.Concurrent;

namespace ObjectPool
{
    public class ObjectPool : IObjectPool<PoolAbleItem>
    {
        // used ConcurrentBag because it's thread safe
        private readonly ConcurrentBag<PoolAbleItem> _poolAbleItems;
        public ObjectPool()
        {
            _poolAbleItems = new ConcurrentBag<PoolAbleItem>();
        }

        public PoolAbleItem Get() => 
            _poolAbleItems.TryTake(out PoolAbleItem? item) ? item : new PoolAbleItem(Guid.NewGuid().ToString());

        public void Return(PoolAbleItem obj) => _poolAbleItems.Add(obj);
    }
}
