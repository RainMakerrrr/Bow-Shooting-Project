using System;
using System.Collections.Generic;
using ObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class SkeletonSpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
        [SerializeField]private List<Skeleton> _skeletons = new List<Skeleton>();
        
        private void Update()
        {
            if (_skeletons.Count < 3)
                SpawnSkeleton();
        }

        private Skeleton SpawnSkeleton()
        {
            var skeleton = EnemyPool.Instance.Get();
            skeleton.transform.position = GetRandomSpawnPoint().position;
            
            skeleton.gameObject.SetActive(true);
            _skeletons.Add(skeleton);
            
            return skeleton;
        }
        
        private Transform GetRandomSpawnPoint()
        {
            var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
            _spawnPoints.Remove(spawnPoint);
            return spawnPoint;
        }

        public void ReturnSpawnPoint(Transform spawnPoint) => _spawnPoints.Add(spawnPoint);
        public void RemoveSkeleton(Skeleton skeleton) => _skeletons.Remove(skeleton);
    }
}