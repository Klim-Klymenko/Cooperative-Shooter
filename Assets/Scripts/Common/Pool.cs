using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Common
{
    [UsedImplicitly]
    public sealed class Pool<T> where T : Object
    {
        private readonly List<T> _objects;

        private readonly DiContainer _diContainer;
        
        private readonly int _poolSize;
        private readonly T _prefab;
        private readonly Vector3 _spawnPosition;
        private readonly Quaternion _spawnRotation;
        private readonly Transform _parent;
        
        public readonly string ObjectType;
        
        public Pool(DiContainer diContainer, int poolSize, T prefab, Transform parent, string objectType = null)
        {
            _objects = new List<T>(poolSize);

            _diContainer = diContainer;
            
            _poolSize = poolSize;
            _prefab = prefab;
            _spawnPosition = Vector3.zero;
            _spawnRotation = Quaternion.identity;
            _parent = parent;
            ObjectType = objectType;
            
            Reserve();
        }
        
        private void Reserve()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                T obj = _diContainer.InstantiatePrefabForComponent<T>(_prefab, _spawnPosition, _spawnRotation, _parent);
            
                _objects.Add(obj);
                SetActive(obj, false);
            }
        }

        public T Get()
        {
            T obj = _objects.Count > 0 ? _objects[^1] : _diContainer.InstantiatePrefabForComponent<T>(_prefab, _spawnPosition, _spawnRotation, _parent);
            
            SetActive(obj, true);
            _objects.Remove(obj);
            
            return obj;
        }

        public void Put(T obj)
        {
            SetActive(obj, false);
            _objects.Add(obj);
            
            if (_parent != null)
                SetParent(obj);
        }
        
        private void SetActive(T obj, bool value)
        {
            if (obj is MonoBehaviour monoBehaviour)
                monoBehaviour.gameObject.SetActive(value);
                
            else if (obj is GameObject gameObject)
                gameObject.SetActive(value);
        }

        private void SetParent(T obj)
        {
            if (obj is MonoBehaviour monoBehaviour)
            {
                Transform transform = monoBehaviour.transform;
                
                InstallParent(transform);
            }
                
                
            else if (obj is GameObject gameObject)
            {
                Transform transform = gameObject.transform;
                
                InstallParent(transform);
            }
            
            else if (obj is Transform transform)
            {
                InstallParent(transform);
            }

            return;

            void InstallParent(Transform transform)
            {
                if (transform.parent != _parent)
                    transform.SetParent(_parent);
            }
        }
    }
}