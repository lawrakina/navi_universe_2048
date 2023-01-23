using Core.Game;
using NavySpade.Core.Runtime.Levels;
using Pj_61_Weapon_adv.Common;
using UnityEngine;


namespace NavySpade.Core.Runtime.Game{
    public class GameContext : MonoBehaviour
    {
        //[field: SerializeField] public ParticleManager ParticleManager { get; private set; }
        
        [field: SerializeField] public InGameEarnedCurrency EarnedCurrency { get; private set; }
        
        [field: SerializeField] public GameStatesManager GameStatesManager { get; private set; }
        
        [field: SerializeField] public LevelsManager LevelsManager { get; private set; }
        
        public static GameContext Instance { get; private set; }

        public void Init()
        {
            Instance = this;
            
            //ParticleManager.Init();
            LevelsManager.Init();
            GameStatesManager.Init(LevelsManager, EarnedCurrency);
        }
    }
}