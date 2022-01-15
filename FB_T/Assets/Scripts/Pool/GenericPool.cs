using System;
using System.Collections.Generic;

namespace ML.Pool
{
    public class GenericPool<T>
    {
        Func<T> onCreate;
        Action<T> onGet;
        Action<T> onRelease;
        Action<T> onDestory;
        int defaultCapacity;
        int maxSize;
        Queue<T> pool = new Queue<T>();
        Queue<T> inUse = new Queue<T>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="funcOnCreate"></param>
        /// <param name="actionOnGet"></param>
        /// <param name="actionOnRelease"></param>
        /// <param name="actionOnDestroy"></param>
        /// <param name="defaultCapacity"></param>
        /// <param name="maxSize"></param>
        public GenericPool(Func<T> funcOnCreate, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null, int defaultCapacity = 0, int maxSize = 0)
        {
            onCreate = funcOnCreate;
            onGet = actionOnGet;
            onRelease = actionOnRelease;
            onDestory = actionOnDestroy;
            this.defaultCapacity = defaultCapacity;
            this.maxSize = maxSize;

            if (defaultCapacity > 0)
            {
                PreWarm();
            }
        }

        private void PreWarm()
        {
            for (int i = 0; i < defaultCapacity; i++)
            {
                pool.Enqueue(onCreate());
            }
        }
        public T Get()
        {
            T newInstance;
            if (pool.Count == 0)
            {
                if (inUse.Count >= maxSize && maxSize != 0)
                {
                    newInstance = inUse.Dequeue();
                    onRelease(newInstance);
                }
                else
                {
                    newInstance = onCreate();
                }
            }
            else
            {
                newInstance = pool.Dequeue();
                if (newInstance == null) //if the instance of the object was destroyed
                    newInstance = onCreate();
            }
            inUse.Enqueue(newInstance);
            onGet(newInstance);
            return newInstance;
        }
        public void Release(T element)
        {
            inUse.Dequeue();
            onRelease(element);
            if (pool.Count < defaultCapacity)
            {
                pool.Enqueue(element);
            }
            else
            {
                onDestory(element);
            }
        }
        public void Dispose()
        {
            T[] inUseArray = inUse.ToArray(); //my tests shows that converting queue to array and doing operation on for loop is faster than foreach on queue
            for (int i = inUseArray.Length - 1; i >= 0; i--)
            {
                Release(inUseArray[i]);
            }
        }
        public void Clear()
        {
            int backupDefaultCapacity = defaultCapacity;
            defaultCapacity = 0;
            Dispose();
            defaultCapacity = backupDefaultCapacity;
        }
    }
}
