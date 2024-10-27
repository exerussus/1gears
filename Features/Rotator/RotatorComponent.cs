
using Exerussus._1Gears.Core;
using Exerussus._1Gears.Custom;
using UnityEngine;

namespace Exerussus._1Gears.Features.Rotater
{
    [AddComponentMenu("1Gears/Rotator"), DisallowMultipleComponent]
    public class RotatorComponent : GearComponent
    {
        public Vector3 speed = new Vector3(0, 0, 10);
        
        protected override void OnActivate(int entity, GearsPooler pooler)
        {
            ref var rotateData = ref pooler.Rotator.AddOrGet(entity);
            rotateData.Value = this;
            rotateData.OriginalRotation = transform.rotation;
        }

        protected override void OnDeactivate(int entity, GearsPooler pooler)
        {
            if (!pooler.Rotator.Has(entity)) return;
            ref var rotateData = ref pooler.Rotator.Get(entity);
            transform.rotation = rotateData.OriginalRotation;
            pooler.Rotator.Del(entity);
        }
    }
}