﻿using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Gears.Custom;
using Leopotam.EcsLite;

namespace Exerussus._1Gears.Features.Rotater
{
    public class RotationSystem : EasySystem<GearsPooler>
    {
        private EcsFilter _rotateFilter;
        
        protected override void Initialize()
        {
            _rotateFilter = Componenter.Filter<GearsData.Rotator>().End();
        }

        protected override void Update()
        {
            _rotateFilter.Foreach(OnRotateUpdate);
        }

        private void OnRotateUpdate(int entity)
        {
            ref var rotateData = ref Pooler.Rotator.Get(entity);
            rotateData.Value.transform.Rotate(rotateData.Value.speed * DeltaTime);
        }
    }
}