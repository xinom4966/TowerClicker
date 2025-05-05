using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class Pool<T> where T : class, IpoolInterface<T>
{
    private readonly Stack<T> _pooledObjects;
    private readonly Func<T> _createFunc;
    private readonly Action<T> _onGetFunc;
    private readonly Action<T> _onReleaseFunc;

    public Pool(Func<T> createFunc, int capacity = 50, int preloadCount = 0)
        : this(createFunc, null, null, capacity, preloadCount) { }

    public Pool(Func<T> createFunc, Action<T> OnGetFunc, Action<T> onReleaseFunc, int capacity = 50, int preloadCount = 0)
    {
        Assert.IsNotNull(createFunc, "The object creation function can't be null.");
        Assert.IsTrue(capacity >= 1, "The capacity of the pool must be greater than or equal to 1.");
        Assert.IsTrue(preloadCount >= 0, "The pre-allocation count of the pool must be greater than or equal to 0.");

        _pooledObjects = new Stack<T>(capacity);
        _createFunc = createFunc;
        _onGetFunc = OnGetFunc;
        _onReleaseFunc = onReleaseFunc;
        PreAllocatePooledObjects(preloadCount);
    }

    public T Get()
    {
        T pooledObject;
        if (_pooledObjects.Count > 0)
        {
            pooledObject = _pooledObjects.Pop();
        }
        else
        {
            return InstantiatePoolObject();
        }

        if (_onGetFunc != null)
        {
            _onGetFunc.Invoke(pooledObject);

        }
        return pooledObject;
    }

    public void Release(T p_object)
    {
        Assert.IsNotNull(p_object, "The object to release can't be null.");

        _pooledObjects.Push(p_object);
        if (_onReleaseFunc != null)
        {
            _onReleaseFunc.Invoke(p_object);
        }
    }

    private void PreAllocatePooledObjects(int preloadCount)
    {
        for (int i = 0; i < preloadCount; i++)
        {
            T pooledObject = InstantiatePoolObject();
            Release(pooledObject);
        }
    }

    private T InstantiatePoolObject()
    {
        T pooledObject = _createFunc.Invoke();
        Assert.IsNotNull(pooledObject, "The object to create can't be null.");
        pooledObject.SetPool(this);
        return pooledObject;
    }
}