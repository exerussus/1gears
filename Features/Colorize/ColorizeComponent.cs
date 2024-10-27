using Exerussus._1Gears.Core;
using Exerussus._1Gears.Custom;
using UnityEngine;
using UnityEngine.UI;

namespace Exerussus._1Gears.Features.Colorize
{
    [AddComponentMenu("1Gears/Colorize"), DisallowMultipleComponent]
    public class ColorizeComponent : GearComponent
    {
        public float duration = 2;
        public Color targetColor = Color.white;
        public Image image;
        public SpriteRenderer spriteRenderer;

        protected override void OnActivate(int entity, GearsPooler pooler)
        {
            if (duration <= 0) return;
            if (image == null) image = GetComponent<Image>();
            if (image != null)
            {
                ref var colorizeData = ref pooler.ColorizeImage.AddOrGet(entity);

                colorizeData.Value = this;
                colorizeData.AnimatingToTarget = true;
                colorizeData.OriginalColor = image.color;
            }

            if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                ref var colorizeData = ref pooler.ColorizeSprite.AddOrGet(entity);

                colorizeData.Value = this;
                colorizeData.AnimatingToTarget = true;
                colorizeData.OriginalColor = spriteRenderer.color;
            }
        }

        protected override void OnDeactivate(int entity, GearsPooler pooler)
        {
            if (pooler.ColorizeImage.Has(entity))
            {
                ref var colorizeData = ref pooler.ColorizeImage.Get(entity);
                image.color = colorizeData.OriginalColor;
                pooler.ColorizeImage.Del(entity);
            }
            
            if (pooler.ColorizeSprite.Has(entity))
            {
                ref var colorizeData = ref pooler.ColorizeSprite.Get(entity);
                spriteRenderer.color = colorizeData.OriginalColor;
                pooler.ColorizeSprite.Del(entity);
            }
        }
    }
}