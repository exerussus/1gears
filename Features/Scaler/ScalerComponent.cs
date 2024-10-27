using Exerussus._1Gears.Core;
using Exerussus._1Gears.Custom;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1Gears.Features.Scaler
{
    [AddComponentMenu("1Gears/Scaler"), DisallowMultipleComponent]
    public class ScalerComponent : GearComponent
    {
        public Vector2 scaleDifference = new Vector2(0.2f, 0.2f);
        public float duration = 2f;
        
        protected override void OnActivate(int entity, GearsPooler pooler)
        {
            ref var scaleData = ref pooler.Scale.AddOrGet(entity);
            scaleData.Value = this;
            scaleData.OriginalScale = transform.localScale;
            scaleData.IsExpanding = true;
        }

        protected override void OnDeactivate(int entity, GearsPooler pooler)
        {
            if (!pooler.Scale.Has(entity)) return;
            
            ref var scaleData = ref pooler.Scale.Get(entity);
            transform.localScale = scaleData.OriginalScale;
            pooler.Scale.Del(entity);
        }
    }
}