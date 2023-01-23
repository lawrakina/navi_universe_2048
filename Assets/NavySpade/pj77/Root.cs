using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem.Runtime.Core.Managers;
using GameAnalyticsSDK;
using Guirao.UltimateTextDamage;
using Main.Levels.Data;
using NavySpade.Core.Runtime.Game;
using NavySpade.Core.Runtime.Levels;
using NavySpade.Meta.Runtime.Economic.Currencies;
using NavySpade.Modules.Saving.Runtime;
using NavySpade.pj77.Buildings.House.UselessUnits;
using NavySpade.pj77.Cubes;
using NavySpade.pj77.Effects;
using NavySpade.pj77.Extension;
using NavySpade.pj77.Input;
using NavySpade.pj77.Plane;
using NavySpade.pj77.Player;
using NavySpade.pj77.Tutorial;
using NavySpade.UI.Popups.DifferentPopups;
using NS.Core.Levels;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;


namespace NavySpade.pj77{
    public class Root : LevelBase{
        [SerializeField]
        private LevelPlane _plane;
        [SerializeField]
        private EcsEngine _ecsEngine;
        [SerializeField]
        private Currency _money;
        [SerializeField]
        private LimitedCurrency _progressLimit;
        [SerializeField]
        private GameObject[] _objectsForActivAfterStart;

        private ObjectPool<CubeView> _pool;
        private PlayerSettings _playerSettings;
        private int _timeGA;
        private float _startTime;

        public override void Init(LevelDataBase.AdditionData[] data){
            EventManager.Add(GameStatesEM.StartGame, StartGame);
            // EventManager.Add(GameStatesEM.ForceStartGame, StartGame);
            CubesSettings cubesSettings = ResourceLoader.LoadConfig<CubesSettings>();
            _playerSettings = ResourceLoader.LoadConfig<PlayerSettings>();

            // PoolCubes.Instance = new ObjectPool<CubeView>(createFunc: () => {
            //     return Object.Instantiate(cubesSettings.CubeView);
            // }, actionOnGet: (obj) => {
            //     obj.gameObject.SetActive(true);
            //     obj.transform.rotation = Quaternion.identity;
            // }, actionOnRelease: (obj) => {
            //     obj.gameObject.SetActive(false);
            // }, actionOnDestroy: (obj) => {
            //     Destroy(obj);
            //     // obj.gameObject.SetActive(false);
            // }, collectionCheck: false, defaultCapacity: 50, maxSize: 60);

            _ecsEngine.Inject(GameContext.Instance.LevelsManager);
            _ecsEngine.Inject(Object.FindObjectOfType<LevelsManager>());
            _ecsEngine.Inject(Object.FindObjectOfType<PlayerView>());
            _ecsEngine.Inject(Object.FindObjectOfType<UltimateTextDamageManager>());
            _ecsEngine.Inject(cubesSettings);
            _ecsEngine.Inject(ResourceLoader.LoadConfig<InputSettings>());
            _ecsEngine.Inject(ResourceLoader.LoadConfig<EffectsSettings>());
            _ecsEngine.Inject(_playerSettings);
            _ecsEngine.Inject(ResourceLoader.LoadConfig<UselessSettings>());
            _ecsEngine.Inject(_money);
            _ecsEngine.Inject(_progressLimit);
            _ecsEngine.Inject(_plane);

            _ecsEngine.Init();
            TutorialController.Instance.HideTutorClick();

            foreach (var obj in _objectsForActivAfterStart){
                obj.SetActive(true);
            }

            // _levelsManager.UnlockNextLevel();
            // _levelsManager.LoadLevel();

            var saver = SaveManager.Load<string>($"version");
            if (saver == Application.version){
                FindObjectOfType<StartGamePopup>().gameObject.SetActive(false);
                // EventManager.Invoke(GameStatesEM.ForceStartGame);
            }
        }

        private void StartGame(){
            var version = SaveManager.Load<string>($"version");
            if (version != Application.version){
                SaveManager.DeleteAll();
                TutorialController.Instance.gameObject.SetActive(true);
                TutorialController.Instance.ShowTutorClick();
                _money.Count = _playerSettings.MoneyAfterFirstStart;
                SaveManager.Save<string>($"version", Application.version);
            } else{
                TutorialController.ForcedAction(TutorAction.End);
            }

            _progressLimit.Count = 0;

            //it`s for remove StartGamePopup
            // EventManager.Invoke(GameStatesEM.StartGame);
            EventManager.Invoke(GameStatesEM.LevelLoaded);

            _startTime = Time.time;
            var timeHoopsly = SaveManager.Load<int>($"TimeBeforeStartHoopsly");
            _timeGA = SaveManager.Load<int>($"TimeBeforeStartGA");
            StartCoroutine(SendMessageHoopsly());
            StartCoroutine(SendMessageGA(_timeGA));
        }

        private IEnumerator SendMessageGA(int _timeGA){
            // GameAnalytics
            while (_timeGA % 10 != 0){
                yield return new WaitForSeconds(1);
                _timeGA++;
            }

            while (_timeGA < 120){
                yield return new WaitForSeconds(10);
                _timeGA += 10;
                //Send GA

                GameAnalytics.NewDesignEvent($"FTUE:TimeInGame:{_timeGA}");

                SaveManager.Save($"TimeBeforeStartGA", _timeGA);
            }

            while ((_timeGA > 119) && (_timeGA < 1200)){
                yield return new WaitForSeconds(30);
                _timeGA += 30;
                //Send GA
                GameAnalytics.NewDesignEvent($"FTUE:TimeInGame:{_timeGA}");

                SaveManager.Save($"TimeBeforeStartGA", _timeGA);
            }
        }

        private IEnumerator SendMessageHoopsly(){
            var i = 1;
            while (true){
                HoopslyIntegration.RaiseLevelStartEvent(i.ToString());
                yield return new WaitForSeconds(40f);
                HoopslyIntegration.RaiseLevelFinishedEvent(i.ToString(), LevelFinishedResult.win);
                yield return new WaitForSeconds(3f);
                i++;
            }

        }

        private void OnDestroy(){
            _timeGA = (int) (Time.time - _startTime);
            SaveManager.Save($"TimeBeforeStartGA", _timeGA);
        }
    }

    // public class PoolCubes{
    //     public static ObjectPool<CubeView> Instance;
    // }
}