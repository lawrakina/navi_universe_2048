using Core.Game;
using Core.UI.Popups;
using EventSystem.Runtime.Core.Dispose;
using EventSystem.Runtime.Core.Managers;
using NavySpade.Core.Runtime.Levels;
using NavySpade.Modules.Extensions.UnityTypes;
using NavySpade.Modules.Sound.Runtime.Core;
using Pj_61_Weapon_adv.Common;
using UnityEngine;


namespace NavySpade.Core.Runtime.Game{
    public class GameStatesManager : ExtendedMonoBehavior{
        private LevelsManager _levelsManager;
        private InGameEarnedCurrency _earned;

        private EventDisposal _disposal = new EventDisposal();

        public void Init(LevelsManager levelsManager, InGameEarnedCurrency currency){
            _levelsManager = levelsManager;
            _earned = currency;

            EventManager.Add(MainEnumEvent.NextLevel, NextLevel).AddTo(_disposal);
            EventManager.Add(MainEnumEvent.Restart, Restart).AddTo(_disposal);
            EventManager.Add(MainEnumEvent.Win, LevelWin).AddTo(_disposal);
            EventManager.Add(MainEnumEvent.Fail, LevelFail).AddTo(_disposal);

            //EventManager.Invoke(PopupsEnum.OpenStartGame);
        }

        private void OnDestroy(){
            _disposal.Dispose();
        }

        private void NextLevel(){
            _levelsManager.UnlockNextLevel();
            EventManager.Invoke(MainEnumEvent.Restart);
        }

        private void Restart(){
            _levelsManager.LoadLevel();
        }

        public void LevelWin(){
            EventManager.Invoke(GameStatesEM.OnWin);
            InvokeAtTime(PopupsConfig.Instance.AfterWin, LevelWinPopup);
        }

        private void LevelWinPopup(){
            Debug.Log("LevelWinPopup");
            SoundPlayer.PlaySoundFx("Win");
            EventManager.Invoke(PopupsEnum.OpenWinPopup, _earned.GenerateReward());
        }

        public void LevelFail(){
            EventManager.Invoke(GameStatesEM.OnFail);
            InvokeAtTime(PopupsConfig.Instance.AfterLose, LevelFailPopup);
        }

        private void LevelFailPopup(){
            SoundPlayer.PlaySoundFx("Lose");
            EventManager.Invoke(PopupsEnum.OpenLosePopup);
        }
    }
}