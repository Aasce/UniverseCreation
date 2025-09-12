using Asce.Managers;
using Asce.Managers.Pools;
using System.Collections.Generic;
using UnityEngine;

namespace Asce.Game.VFXs
{
    public class VFXManager : MonoBehaviourSingleton<VFXManager>
    {
        [SerializeField] private SO_VFXs _vfxsData;
        private readonly Dictionary<string, Pool<VFXObject>> _pools = new();

        public SO_VFXs VFXsData => _vfxsData;

        public VFXObject SpawnAndPlay(string name, Vector2 position = default)
        {
            VFXObject vfxObject = this.Spawn(name, position);
            if (vfxObject != null) vfxObject.Play();
            
            return vfxObject;
        }

        public VFXObject Spawn(string name, Vector2 position = default)
        {
            if (_vfxsData == null)
            {
                Debug.LogWarning("Spawn failure! VFXs Data is not assigned in VFXManager.");
                return null;
            }

            if (string.IsNullOrEmpty(name)) return null;

            VFXInformation vfxInfo = _vfxsData.GetVFX(name);
            if (vfxInfo == null)
            {
                Debug.LogWarning($"Spawn failure! VFX Information for {name} not found in VFXs Data.");
                return null;
            }

            Pool<VFXObject> pool = this.GetPool(name);
            if (pool == null)
            {
                Debug.LogWarning($"Spawn failure! No pool found for VFX {name}.");
                return null;
            }

            VFXObject vfxObject = pool.Activate();
            if (vfxObject != null)
            {
                vfxObject.name = vfxInfo.Name;
                vfxObject.DespawnCooldown.BaseTime = vfxInfo.Duration;
                vfxObject.transform.position = position;
                vfxObject.OnCompleted += (sender) =>
                {
                    pool.Deactivate(vfxObject);
                };
            }
            return vfxObject;
        }

        public void Despawn(VFXObject vfxObject)
        {
            if (vfxObject == null) return;

            string name = vfxObject.name;
            Pool<VFXObject> pool = this.GetPool(name);
            if (pool == null) return;

            vfxObject.Stop();
            pool.Deactivate(vfxObject);
        }

        public Pool<VFXObject> GetPool(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            if (!_pools.ContainsKey(name)) this.CreatePool(name);

            _pools.TryGetValue(name, out Pool<VFXObject> pool);
            return pool;
        }

        private void CreatePool(string name)
        {
            if (_vfxsData == null) return;
            VFXInformation vfxInfo = _vfxsData.GetVFX(name); 
            if (vfxInfo == null) return;
            if (vfxInfo.VFXPrefab == null)
            {
                Debug.LogWarning($"Create pool failure! VFX Prefab for {name} is not assigned in VFXs Data.");
                return;
            }

            if (_pools.ContainsKey(name))
            {
                Debug.LogWarning($"Create pool failure! Pool for VFX {name} already exists.");
                return;
            }
            
            GameObject poolObject = new ($"Pool_{name}");
            poolObject.transform.SetParent(this.transform);

            Pool<VFXObject> pool = new (vfxInfo.VFXPrefab, poolObject.transform);
            _pools.Add(name, pool);
        }
    }
}
