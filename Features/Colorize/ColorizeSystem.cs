
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Gears.Custom;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1Gears.Systems
{
    public class ColorizeSystem : EasySystem<GearsPooler>
    {
        private EcsFilter _colorizeImageFilter;
        private EcsFilter _colorizeSpriteFilter;
        
        protected override void Initialize()
        {
            _colorizeImageFilter = Componenter.Filter<GearsData.ColorizeImage>().End();
            _colorizeSpriteFilter = Componenter.Filter<GearsData.ColorizeSprite>().End();
        }

        protected override void Update()
        {
            _colorizeImageFilter.Foreach(OnUpdateImageColorize);
            _colorizeSpriteFilter.Foreach(OnUpdateSpriteColorize);
        }

        private void OnUpdateImageColorize(int entity)
        {
            ref var colorizeData = ref Pooler.ColorizeImage.Get(entity);
            
            colorizeData.ElapsedTime += DeltaTime;

            var t = Mathf.PingPong(colorizeData.ElapsedTime / colorizeData.Value.duration, 1f);
            if (!colorizeData.AnimatingToTarget)
            {
                t = 1f - t;
            }

            colorizeData.Value.image.color = Color.Lerp(colorizeData.OriginalColor, colorizeData.Value.targetColor, t);

            if (colorizeData.ElapsedTime >= colorizeData.Value.duration)
            {
                colorizeData.ElapsedTime = 0f;
                colorizeData.AnimatingToTarget = !colorizeData.AnimatingToTarget;
            }
        }

        private void OnUpdateSpriteColorize(int entity)
        {
            ref var colorizeData = ref Pooler.ColorizeSprite.Get(entity);
            
            colorizeData.ElapsedTime += DeltaTime;

            var t = Mathf.PingPong(colorizeData.ElapsedTime / colorizeData.Value.duration, 1f);
            if (!colorizeData.AnimatingToTarget)
            {
                t = 1f - t;
            }

            colorizeData.Value.spriteRenderer.color = Color.Lerp(colorizeData.OriginalColor, colorizeData.Value.targetColor, t);

            if (colorizeData.ElapsedTime >= colorizeData.Value.duration)
            {
                colorizeData.ElapsedTime = 0f;
                colorizeData.AnimatingToTarget = !colorizeData.AnimatingToTarget;
            }
        }
    }
}