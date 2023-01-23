using System;
using GameAnalyticsSDK;
using NavySpade.Core.Runtime.Game;
using UnityEngine;

namespace NavySpade.PJ70.Analytics
{
    public class GASDKIntegration : AnalyticsProvider
    {
        private void Start()
        {
            GameAnalytics.Initialize();
        }

        protected override void OnResetLevel()
        {
            int levelIndex = GameContext.Instance.LevelsManager.LevelIndex;
            // if(levelIndex == 0)
                // return;
            
            Debug.Log($"<color=#{Color.white}> GA startLevel {levelIndex} </color>");
            
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, levelIndex.ToString());
            IsLevelStarted = true;
        }

        protected override void OnLevelFailed()
        {
            if(IsLevelStarted == false)
                return;
            
            Debug.Log($"<color=#{Color.red}> GA endLevel lose {GameContext.Instance.LevelsManager.LevelIndex} </color>");
            RaiseLevelEndEvent(GAProgressionStatus.Fail);
        }

        protected override void OnLevelWin()
        {
            if(IsLevelStarted == false)
                return;
            
            Debug.Log($"<color=#{Color.green}> GA endLevel win {GameContext.Instance.LevelsManager.LevelIndex} </color>");
            RaiseLevelEndEvent(GAProgressionStatus.Complete);
        }

        private void RaiseLevelEndEvent(GAProgressionStatus status)
        {
            int levelIndex = GameContext.Instance.LevelsManager.LevelIndex;
            GameAnalytics.NewProgressionEvent(status, levelIndex.ToString());
            IsLevelStarted = false;
        }
    }
}