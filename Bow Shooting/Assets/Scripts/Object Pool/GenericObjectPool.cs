using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class GenericObjectPool<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private T _prefab;
        
        private Queue<T>  _objects = new Queue<T>();
        public static GenericObjectPool<T> Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public T Get()
        {
            if (_objects.Count == 0)
                AddObjects();

            return _objects.Dequeue();
        }

        public void ReturnToPool(T objectToReturn)
        {
            objectToReturn.gameObject.SetActive(false);
            _objects.Enqueue(objectToReturn);
        }

        private void AddObjects()
        {
            var newObject = Instantiate(_prefab);
            newObject.gameObject.SetActive(true);
            _objects.Enqueue(newObject);
        }
    }
}