
using Exerussus._1Gears.Custom;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1Gears.Core
{
    [RequireComponent(typeof(GearObject))]
    public abstract class GearComponent : MonoBehaviour
    {
        [SerializeField, HideInInspector] protected GearObject gearObject;
        public EcsPackedEntity EntityPack => gearObject.EntityPack;
        public bool isActivated { get; private set; }
        public bool IsQuitting => gearObject.IsQuitting;
        
        private void Awake()
        {
            if (gearObject != null) return;
            gearObject = GetComponent<GearObject>();
            if (gearObject == null)
            {
                gearObject = gameObject.AddComponent<GearObject>();
                gearObject.GearComponents.Add(this);
                gearObject.OnEnable();
            }
            else
            {
                if (!gearObject.Activated) return;
                if (!gearObject.EntityPack.Unpack(gearObject.GearsPooler.World, out var entity)) return;
                gearObject.GearComponents.Add(this);
                InvokeOnActivate(entity, gearObject.GearsPooler);
            }
        }

        public void InvokeOnActivate(int entity, GearsPooler pooler)
        {
            if (isActivated) return;

            OnActivate(entity, pooler);
            isActivated = true;
        }
        public void InvokeOnDeactivate(int entity, GearsPooler pooler) 
        {
            if (!isActivated) return;
            
            OnDeactivate(entity, pooler);
            isActivated = false;
        }
        protected abstract void OnActivate(int entity, GearsPooler pooler);
        protected abstract void OnDeactivate(int entity, GearsPooler pooler);
        
        private void OnDestroy()
        {
            if (!isActivated) return;
            if (gearObject == null) return;
            if (!gearObject.EntityPack.Unpack(gearObject.GearsPooler.World, out var entity)) return;
            gearObject.GearComponents.Remove(this);
            InvokeOnDeactivate(entity, gearObject.GearsPooler);
        }

        private void OnValidate()
        {
            gearObject = GetComponent<GearObject>();
        }
    }
}