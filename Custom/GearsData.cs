using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Gears.Features.Colorize;
using Exerussus._1Gears.Features.Rotater;
using Exerussus._1Gears.Features.Scaler;
using Exerussus._1Gears.Systems;
using UnityEngine;

namespace Exerussus._1Gears.Custom
{
    public static class GearsData
    {
        public struct GearObject : IEcsComponent
        {
            public Core.GearObject Value;
        }

        public struct Hider : IEcsComponent
        {
            public float ColorStep;
            public float DelayRemaining;
            public float HideRemaining;
            public bool DestroyOnEnd;
        }
        
        public struct ColorizeImage : IEcsComponent
        {
            public float ElapsedTime;
            public bool AnimatingToTarget;
            public Color OriginalColor;
            public ColorizeComponent Value;
        }
    
        public struct ColorizeSprite : IEcsComponent
        {
            public float ElapsedTime;
            public bool AnimatingToTarget;
            public Color OriginalColor;
            public ColorizeComponent Value;
        }
        
        public struct Rotator : IEcsComponent
        {
            public Quaternion OriginalRotation;
            public RotatorComponent Value;
        }
        
        public struct Scale : IEcsComponent
        {
            public float Timer;
            public ScalerComponent Value;
            public Vector3 OriginalScale;
            public bool IsExpanding;
        }

        public struct InScaleProcessMark : IEcsComponent
        {
            
        }

        public struct InColorizeProcessMark : IEcsComponent
        {
            
        }

        public struct InRotationProcessMark : IEcsComponent
        {
            
        }
        
        public struct Highlighter : IEcsComponent
        {
            
        }
        
        public struct HighlightedMark : IEcsComponent
        {
            
        }
    }
}
