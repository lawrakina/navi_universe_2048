using NavySpade.Core.Runtime.Game;
using UnityEngine;

namespace NavySpade.PJ70.Analytics
{
    public class HoopslySDKIntegration : AnalyticsProvider
    {
        protected override void OnResetLevel()
        {
            int levelIndex = GameContext.Instance.LevelsManager.LevelIndex;
            // if(levelIndex == 0)
                // return;
            
            Debug.Log($"<color={Color.white}> Hoopsly startLevel {levelIndex} </color>");
            
            // HoopslyIntegration.RaiseLevelStartEvent(levelIndex.ToString());
            IsLevelStarted = true;
        }

        protected override void OnLevelFailed()
        {
            if(IsLevelStarted == false)
                return;
            
            Debug.Log($"<color={Color.red}> Hoopsly endLevel lose {GameContext.Instance.LevelsManager.LevelIndex} </color>");
            RaiseLevelEndEvent(LevelFinishedResult.lose);
        }

        protected override void OnLevelWin()
        {
            if(IsLevelStarted == false)
                return;
            
            Debug.Log($"<color={Color.green}> Hoopsly endLevel win {GameContext.Instance.LevelsManager.LevelIndex} </color>");
            RaiseLevelEndEvent(LevelFinishedResult.win);
        }

        private void RaiseLevelEndEvent(LevelFinishedResult result)
        {
            int levelIndex = GameContext.Instance.LevelsManager.LevelIndex;
            // HoopslyIntegration.RaiseLevelFinishedEvent(levelIndex.ToString(), result);
            
            IsLevelStarted = false;
        }
    }
}