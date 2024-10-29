
using System.Collections.Generic;
using System.Linq;
using Exerussus._1Gears.Custom;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1Gears.Core
{
    public class GearObject : MonoBehaviour
    {
        [SerializeField, HideInInspector] private List<GearComponent> gearComponents;
        private GearsPooler _gearsPooler;
        private EcsPackedEntity _entityPack;
        private bool _isActivated;

        public List<GearComponent> GearComponents => gearComponents;
        public GearsPooler GearsPooler => _gearsPooler;
        public EcsPackedEntity EntityPack => _entityPack;
        public bool Activated => _isActivated;
        public bool IsQuitting { get; private set; }
        
        public void Start()
        {
            OnEnable();
        }

        public void OnEnable()
        {
            if (_isActivated) return;
            if (!Application.isPlaying) return;
            Application.quitting += SetQuitting;
            _gearsPooler = GearsCore.Pooler;
            if (gearComponents is not {Count: > 0})
            {
                gearComponents = new List<GearComponent>(GetComponents<GearComponent>());
                if (gearComponents is not {Count: > 0}) return;
            }
            _entityPack = _gearsPooler.World.PackEntity(_gearsPooler.World.NewEntity());
            
            ref var gearObjectData = ref _gearsPooler.GearObject.AddOrGet(_entityPack.Id);
            gearObjectData.Value = this;
            
            foreach (var gearComponent in gearComponents) gearComponent.InvokeOnActivate(_entityPack.Id, _gearsPooler);
            
            _isActivated = true;
        }

        public void OnDisable()
        {
            if (IsQuitting) return;
            if (_isActivated)
            {
                foreach (var gearComponent in gearComponents)
                {
                    gearComponent.InvokeOnDeactivate(_entityPack.Id, _gearsPooler);
                }
                _gearsPooler.World.DelEntity(_entityPack.Id);
            
                _isActivated = false;
            }
        }

        public void OnDestroy()
        {
            if (_isActivated && _entityPack.Unpack(_gearsPooler.World, out var entity)) OnDisable();
        }

        private void SetQuitting()
        {
            IsQuitting = true;
        }
        
        public void OnValidate()
        {
            gearComponents = GetComponents<GearComponent>().ToList();
        }
    }
}