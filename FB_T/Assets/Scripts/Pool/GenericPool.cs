using System;
using System.Collections.Generic;

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

    GenericPool(Func<T> funcOnCreate, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null, int defaultCapacity = 0, int maxSize = 0)
    {
        onCreate = funcOnCreate;
        onGet = actionOnGet;
        onRelease = actionOnRelease;
        onDestory = actionOnDestroy;
        this.defaultCapacity = defaultCapacity;
        this.maxSize = maxSize;

        if(defaultCapacity > 0)
        {
            PreWarm();
        }
    }

    private void PreWarm() 
    {
        for(int i = 0; i < maxSize; i++)
        {
            pool.Enqueue(onCreate());
        }
    }
    public T Get()
    {
        T newInstance;
        if (pool.Count == 0)
        {
            if (inUse.Count >= maxSize)
            {
                newInstance = inUse.Dequeue();
                inUse.Enqueue(newInstance);
                onRelease(newInstance);
                onGet(newInstance);
            }
            else
            {
                newInstance = onCreate();
            }
        }
        else
        {
            newInstance = pool.Dequeue();
            if(newInstance == null) //if the instance of the object was destroyed
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
        if(pool.Count <= defaultCapacity)
        {
            pool.Enqueue(element);
        }
        else
        {
            onDestory(element);
        }
    }
}
