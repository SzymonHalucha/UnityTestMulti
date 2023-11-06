using System.Collections.Generic;
using UnityEngine;
using TestMulti.Game.Pooling;

namespace TestMulti.Game.Managers
{
    public class PoolingManager : BaseManager
    {
        [SerializeField] private List<TransformPool> transformPools = new();
        [SerializeField] private List<ProjectilePool> projectilePools = new();

        public override void OnSpawn()
        {
            GameObject go = new GameObject("---POOLS---");

            foreach (TransformPool pool in transformPools)
                CreatePool<Transform>(pool, go.transform);

            foreach (ProjectilePool pool in projectilePools)
                CreatePool<Projectiles.BaseProjectile>(pool, go.transform);
        }

        public override void OnDespawn()
        {
            foreach (TransformPool pool in transformPools)
                pool.DeInit();

            foreach (ProjectilePool pool in projectilePools)
                pool.DeInit();
        }

        private void CreatePool<T>(BasePool<T> pool, Transform parent) where T : Component
        {
            GameObject go = new GameObject(pool.name);
            go.transform.SetParent(parent);

            pool.Init(go.transform);
        }
    }
}