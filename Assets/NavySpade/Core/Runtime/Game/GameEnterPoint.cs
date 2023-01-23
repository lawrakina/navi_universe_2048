using NavySpade.Core.Runtime.Levels;
using Pj_61_Weapon_adv.Common;
using UnityEngine;


namespace NavySpade.Core.Runtime.Game{
    public class GameEnterPoint : MonoBehaviour
    {
        [SerializeField]
        private GameContext _gameContext;
        [SerializeField]
        private LevelsManager _levelsManager;
        public void Init()
        {
            _gameContext.Init();
            _levelsManager.LoadLevel();
            //Todo Insert your Game Logic
            
            
        }
    }
}