using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Gears.Custom;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1Gears.Features.Scaler
{
    public class ScaleSystem : EcsSignalListener<GearsPooler, GearSignals.OnGearStartHighlight, GearSignals.OnGearEndHighlight>
    {
        private EcsFilter _scaleFilter;

        protected override void Initialize()
        {
            _scaleFilter = Componenter.Filter<GearsData.Scale>().End();
        }

        protected override void Update()
        {
            _scaleFilter.Foreach(OnScaleUpdate);
        }

        private void OnScaleUpdate(int entity)
        {
            ref var scaleData = ref Pooler.Scale.Get(entity);
            if (scaleData.Value.onHighlightOnly && !Pooler.HighlightedMark.Has(entity))
            {
                if (Pooler.InScaleProcessMark.Has(entity))
                {
                    scaleData.Value.transform.localScale = scaleData.OriginalScale;
                    scaleData.Timer = 0;
                    scaleData.IsExpanding = true;
                    Pooler.InScaleProcessMark.Del(entity);
                }
                return;
            }

            Pooler.InScaleProcessMark.AddOrGet(entity);
            scaleData.Timer = Mathf.Max(0, scaleData.Timer + DeltaTime);
    
            var scaleDiff = scaleData.Value.scaleDifference;
            var duration = scaleData.Value.duration;
    
            if (scaleData.Timer >= duration) 
            {
                scaleData.Timer -= duration;
            }
            
            var normalizedTime = scaleData.Timer / duration;
            var oscillation = Mathf.Sin(normalizedTime * Mathf.PI * 2) * 0.5f + 0.5f;
            var targetScale = scaleData.OriginalScale + new Vector3(scaleDiff.x, scaleDiff.y, 0);
    
            scaleData.Value.transform.localScale = Vector3.Lerp(scaleData.OriginalScale, targetScale, oscillation);
        }

        protected override void OnSignal(GearSignals.OnGearStartHighlight data)
        {
            
        }

        protected override void OnSignal(GearSignals.OnGearEndHighlight data)
        {
            
        }
    }
}