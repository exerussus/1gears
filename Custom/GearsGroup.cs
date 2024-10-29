using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus._1Gears.Features.Rotater;
using Exerussus._1Gears.Features.Scaler;
using Exerussus._1Gears.Systems;
using Leopotam.EcsLite;
using Exerussus._1Gears.Features;

namespace Exerussus._1Gears.Custom
{
    public class GearsGroup : EcsGroup<GearsPooler>
    {
        protected override void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems)
        {
            fixedUpdateSystems.Add(new ColorizeSystem());
            fixedUpdateSystems.Add(new RotationSystem());
            fixedUpdateSystems.Add(new ScaleSystem());
            fixedUpdateSystems.Add(new ScaleSystem());
            fixedUpdateSystems.Add(new TestSystem());
            
#if UNITY_EDITOR
            fixedUpdateSystems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif
        }
    }
}