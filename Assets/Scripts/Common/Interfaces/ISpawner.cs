using UnityEngine;

namespace Common
{
    public interface ISpawner<T> where T : Object
    {
        T Spawn(string objectType);
        void Despawn(T obj);
    }
    
    public interface ISpawner<T, in T1> 
        where T : Object
        where T1 : Transform
    {
        T Spawn(T1 spawnPoint);
        void Despawn(T obj);
    }
}