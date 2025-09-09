using Asce.Managers;
using Asce.Managers.Pools;
using Asce.Managers.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Asce.Game.Orbs
{
    public class OrbManager : MonoBehaviourSingleton<OrbManager>
    {
        [SerializeField] private SO_Orbs _orbsData;
        [SerializeField] private int _maxLevel = 8;
        private readonly Dictionary<int, Pool<Orb>> _pools = new();

        public SO_Orbs OrbsData => _orbsData;
        public int MaxLevel => _maxLevel;

        public Orb Spawn(int level, Vector2 position = default)
        {
            if (level <= 0 || level > MaxLevel) return null;

            Pool<Orb> pool = this.GetPool(level);
            if (pool == null)
            {
                Debug.LogError($"[{nameof(OrbManager).ColorWrap(Color.red)}] No pool found for level {level}.");
                return null;
            }

            Orb orb = pool.Activate();
            if (orb.IsNull())
            {
                Debug.LogError($"[{nameof(OrbManager).ColorWrap(Color.red)}] Failed to activate orb from pool for level {level}.");
                return null;
            }

            orb.IsValid = false;
            orb.IsMerged = false;
            orb.transform.position = position;
            return orb;
        }

        public void Despawn(Orb orb)
        {
            if (orb.IsNull()) return;
            int level = orb.Information.Level;
            Pool<Orb> pool = this.GetPool(level);
            if (pool == null)
            {
                Debug.LogError($"[{nameof(OrbManager).ColorWrap(Color.red)}] No pool found for level {level}.");
                Destroy(orb.gameObject);
                return;
            }

            pool.Deactivate(orb);
        }

        public void DespawnAll()
        {
            var pools = _pools.Values;
            foreach (Pool<Orb> pool in pools)
            {
                if (pool == null) continue;
                pool.Clear(isDeactive: true);
            }
        }

        private Pool<Orb> GetPool(int level)
        {
            if (_pools.TryGetValue(level, out Pool<Orb> pool))
                return pool;
            return this.CreatePool(level);
        }

        private Pool<Orb> CreatePool(int level)
        {
            if (_orbsData == null)
            {
                Debug.LogError($"[{nameof(OrbManager).ColorWrap(Color.red)}] No Orbs Data assigned in the inspector.");
                return null;
            }

            Orb orbPrefab = _orbsData.GetOrbPrefab(level);
            if (orbPrefab.IsNull())
            {
                Debug.LogError($"[{nameof(OrbManager).ColorWrap(Color.red)}] No Orb prefab found for level {level} in Orbs Data.");
                return null;
            }

            GameObject gameObject = new($"Orb Pool (Level {level})");
            gameObject.transform.SetParent(this.transform);

            Pool<Orb> newPool = new(prefab: orbPrefab, parent: gameObject.transform);
            _pools.Add(level, newPool);
            return newPool;
        }
    }
}
