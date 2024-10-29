using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus._1Extensions.SignalSystem;
using Leopotam.EcsLite;

namespace Exerussus._1Gears.Custom
{
    public class GearsPooler : IGroupPooler
    {
        public void Initialize(EcsWorld world)
        {
            ColorizeSprite = new(world);
            ColorizeImage = new(world);
            GearObject = new(world);
            Rotator = new(world);
            Scale = new(world);
            Highlighter = new(world);
            HighlightedMark = new(world);
            InScaleProcessMark = new(world);
            InColorizeProcess = new(world);
        }

        [InjectSharedObject] public EcsWorld World { get; private set; }
        [InjectSharedObject] public Signal Signal { get; private set; }
        public PoolerModule<GearsData.ColorizeImage> ColorizeImage { get; private set; }
        public PoolerModule<GearsData.ColorizeSprite> ColorizeSprite { get; private set; }
        public PoolerModule<GearsData.GearObject> GearObject { get; private set; }
        public PoolerModule<GearsData.Rotator> Rotator { get; private set; }
        public PoolerModule<GearsData.Scale> Scale { get; private set; }
        public PoolerModule<GearsData.Highlighter> Highlighter { get; private set; }
        public PoolerModule<GearsData.HighlightedMark> HighlightedMark { get; private set; }
        public PoolerModule<GearsData.InScaleProcessMark> InScaleProcessMark { get; private set; }
        public PoolerModule<GearsData.InColorizeProcessMark> InColorizeProcess { get; private set; }

        public void InvokeStartHighlight(int entity)
        {
            HighlightedMark.AddOrGet(entity);
            Signal.RegistryRaise(new GearSignals.OnGearStartHighlight { PackedEntity = World.PackEntity(entity) });
        }
        public void InvokeStopHighlight(int entity)
        {
            if (!HighlightedMark.Has(entity)) return;
            HighlightedMark.Del(entity);
            Signal.RegistryRaise(new GearSignals.OnGearEndHighlight { PackedEntity = World.PackEntity(entity) });
        }
    }
}