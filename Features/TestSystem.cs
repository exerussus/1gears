using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Gears.Custom;
using UnityEngine;

namespace Exerussus._1Gears.Features
{
    public class TestSystem : EcsSignalListener<GearsPooler, GearSignals.OnGearStartHighlight, GearSignals.OnGearEndHighlight>
    {
        protected override void OnSignal(GearSignals.OnGearStartHighlight data)
        {
            
        }

        protected override void OnSignal(GearSignals.OnGearEndHighlight data)
        {
            
        }
    }
}