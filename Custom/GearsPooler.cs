using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
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
        }

        [InjectSharedObject] public EcsWorld World { get; private set; }
        public PoolerModule<GearsData.ColorizeImage> ColorizeImage { get; private set; }
        public PoolerModule<GearsData.ColorizeSprite> ColorizeSprite { get; private set; }
        public PoolerModule<GearsData.GearObject> GearObject { get; private set; }
        public PoolerModule<GearsData.Rotator> Rotator { get; private set; }
        public PoolerModule<GearsData.Scale> Scale { get; private set; }
    }
}