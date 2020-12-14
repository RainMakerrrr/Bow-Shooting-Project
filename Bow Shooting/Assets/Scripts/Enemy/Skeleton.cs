using BowController.Arrows;
using ObjectPool;
using UnityEngine;

namespace Enemy
{
    public class Skeleton : MonoBehaviour
    {
        private SkeletonSpawner _skeletonSpawner;
        private Animator _animator;
        private int _hashValue;
        
        private void Start()
        {
            _skeletonSpawner = FindObjectOfType<SkeletonSpawner>();
            _animator = GetComponent<Animator>();
            _hashValue = Animator.StringToHash("isDead");
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.GetComponent<Arrow>())
                _animator.SetBool(_hashValue,true);
        }
        
        /// <summary>
        /// Death animation event
        /// </summary>
        private void DeathEvent()
        {
            _skeletonSpawner.ReturnSpawnPoint(transform);
            _skeletonSpawner.RemoveSkeleton(this);
            EnemyPool.Instance.ReturnToPool(this);
        }
    }
}