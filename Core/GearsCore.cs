using System;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus._1Extensions.SignalSystem;
using Exerussus._1Extensions.SmallFeatures;
using Exerussus._1Gears.Custom;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1Gears.Core
{
    public class GearsCore : EcsStarter
    {
        [SerializeField] private GameContext gameContext = new();
        protected override Func<float> FixedUpdateDelta { get; } = () => Time.fixedDeltaTime;
        protected override Func<float> UpdateDelta { get; } = () => Time.deltaTime; 
        protected override Signal Signal => _signal;
        private Signal _signal = new();
        private static GearsCore _instance;
        private GearsPooler _pooler;
        public static GearsPooler Pooler => Instance._pooler;
        
        public static GearsCore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject
                    {
                        name = "1Gears"
                    }.AddComponent<GearsCore>();
                    
                    DontDestroyOnLoad(_instance.gameObject);
                    _instance.Initialize();
                }

                return _instance;
            }
        }

        protected override GameContext GetGameContext(GameShare gameShare) => gameContext;

        protected override EcsGroup[] GetGroups()
        {
            return new EcsGroup[]
            {
                new GearsGroup()
            };
        }

        protected override void SetSharingDataOnStart(EcsWorld world, GameShare gameShare)
        {
            
        }

        protected override void SetSharingDataBeforePreInitialized(EcsWorld world, GameShare gameShare)
        {
            _pooler = gameShare.GetSharedObject<GearsPooler>();
        }
    }
}