using Asce.Game.Scores;
using Asce.Game.VFXs;
using Asce.Managers;
using Asce.Managers.Pools;
using Asce.Managers.Utils;
using Asce.Shared.Audios;
using System.Collections.Generic;
using UnityEngine;

namespace Asce.Game.Orbs
{
    public class OrbManager : MonoBehaviourSingleton<OrbManager>
    {
        [SerializeField] private SO_Orbs _orbsData;
        [SerializeField] private int _maxLevel = 8;
        private readonly Dictionary<int, Pool<Orb>> _pools = new();

        [Space]
        [SerializeField] private int _mergedCount = 0;

        [Space]
        [SerializeField] private string _mergeSoundName = "Merge";

        public SO_Orbs OrbsData => _orbsData;
        public int MaxLevel => _maxLevel;

        public int MergedCount
        {
            get => _mergedCount;
            set => _mergedCount = value;
        }

        public Orb Merge(Orb orbA, Orb orbB, Vector2 position)
        {
            if (orbA.IsNull() || orbB.IsNull()) return null;
            if (!orbA.IsValid || !orbB.IsValid) 
            {
                GameManager.Instance.EndGame();
                return null;
            }
            if (!OrbExtension.CanMerge(orbA, orbB)) return null;

            orbA.IsMerged = true;
            orbB.IsMerged = true;

            // Play merge SFX
            AudioManager.Instance.PlaySFX(_mergeSoundName);

            // Add score and pop up text
            int score = ScoreManager.Instance.AddMergeOrbScore(orbA.Information.Level);
            PopupTextVFXObject popup = VFXManager.Instance.SpawnAndPlay("Popup Text", position) as PopupTextVFXObject;
            if (popup != null) popup.SetText(score.ToString());

            // Spawn VFX
            OrbMergingVFXObject vfx = VFXManager.Instance.SpawnAndPlay("Orb Merging", position) as OrbMergingVFXObject;
            if (vfx != null)
            {
                if (vfx.SparkParticles != null)
                {
                    var main = vfx.SparkParticles.main;
                    main.startColor = orbA.Information.Color;
                }

                if (vfx.FlashParticles != null)
                {
                    var main = vfx.FlashParticles.main;
                    main.startColor = orbA.Information.Color;
                }
            }

            this.Despawn(orbB);
            this.Despawn(orbA);

            int newLevel = orbA.Information.Level + 1;
            Orb mergedOrb = this.Spawn(newLevel, position);
            if (mergedOrb.IsNull()) return null;

            MergedCount++;
            mergedOrb.IsValid = true;
            return mergedOrb;
        }


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
            MergedCount = 0;
            var pools = _pools.Values;
            foreach (Pool<Orb> pool in pools)
            {
                if (pool == null) continue;
                pool.Clear(isDeactive: true);
            }
        }

        public List<Orb> GetAllActiveOrbs()
        {
            List<Orb> activeOrbs = new();
            var pools = _pools.Values;
            foreach (Pool<Orb> pool in pools)
            {
                if (pool == null) continue;
                activeOrbs.AddRange(pool.Activities);
            }
            return activeOrbs;
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
