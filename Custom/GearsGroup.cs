using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus._1Gears.Features.Rotater;
using Exerussus._1Gears.Features.Scaler;
using Exerussus._1Gears.Systems;
using Leopotam.EcsLite;

namespace Exerussus._1Gears.Custom
{
    public class GearsGroup : EcsGroup<GearsPooler>
    {
        protected override void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems)
        {
            fixedUpdateSystems.Add(new ColorizeSystem());
            fixedUpdateSystems.Add(new RotationSystem());
            fixedUpdateSystems.Add(new ScaleSystem());
        }
    }
}