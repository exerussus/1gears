using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Gears.Custom;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1Gears.Features.Scaler
{
    public class ScaleSystem : EasySystem<GearsPooler>
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

            // Понизить до нуля и увеличить таймер на текущий шаг
            scaleData.Timer = Mathf.Max(0, scaleData.Timer + DeltaTime);
    
            // Кэшировать часто используемые значения
            var scaleDiff = scaleData.Value.scaleDifference;
            var duration = scaleData.Value.duration;
    
            // Если достигли конца цикла, уменьшаем таймер, чтобы сбросить цикл
            if (scaleData.Timer >= duration) 
            {
                scaleData.Timer -= duration;
            }

            // Рассчитать текущий масштаб с минимальными операциями
            float normalizedTime = scaleData.Timer / duration;
            float oscillation = Mathf.Sin(normalizedTime * Mathf.PI * 2) * 0.5f + 0.5f;  // Normalize to [0, 1]
            var targetScale = scaleData.OriginalScale + new Vector3(scaleDiff.x, scaleDiff.y, 0);
    
            // Сразу установить новый масштаб
            scaleData.Value.transform.localScale = Vector3.Lerp(scaleData.OriginalScale, targetScale, oscillation);
        }
    }
}