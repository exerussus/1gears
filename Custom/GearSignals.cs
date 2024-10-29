using Leopotam.EcsLite;

namespace Exerussus._1Gears.Custom
{
    public static class GearSignals
    {
        public struct OnGearStartHighlight
        {
            public EcsPackedEntity PackedEntity;
        }
        
        public struct OnGearEndHighlight
        {
            public EcsPackedEntity PackedEntity;
        }
    }
}