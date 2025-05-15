using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Utiles.Pool
{
    public class AbstractPool<T> : IDisposable where T : Component, IPoolable
    {
        private IObjectPool<T> _objectPool;
        
        private T _pooledObject;
        
        private Transform _parent;
        
        public AbstractPool(T pooledObject, Transform parent, int minPoolSize = 1, int maxPoolSize = 100, bool collectionChecks = true)
        {
            _pooledObject = pooledObject;
            
            _parent = parent;
            
            _objectPool = new ObjectPool<T>(OnCreate, OnGet, OnRelease, OnDestroy, collectionChecks, minPoolSize, maxPoolSize);
        }

        private T OnCreate()
        {
            var obj = Object.Instantiate(_pooledObject, _parent);
            obj.gameObject.SetActive(false);
            
            return obj;
        }

        private void OnGet(T objectToActive)
        {
            objectToActive.gameObject.SetActive(true);
        }

        private void OnRelease(T objectToRelease)
        {
            objectToRelease.gameObject.SetActive(false);
        }

        private void OnDestroy(T overflowedPoolObject)
        {
            Object.Destroy(overflowedPoolObject.gameObject);
        }
        
        public T Get() => _objectPool.Get();

        public void Release(T objectToRelease)
        {
            objectToRelease.Release();
            
            _objectPool.Release(objectToRelease);
            
            objectToRelease.transform.SetParent(_parent);
            objectToRelease.transform.localPosition = Vector3.zero;
        }
        public void Dispose() => _objectPool.Clear();
    }
}