using UnityEngine;

namespace Main.Levels.Data
{
    [CreateAssetMenu(fileName = "new Level", menuName = "Game/Level/Prefab", order = 51)]
    public class PrefabLevelData : LevelDataBase
    {
        [field:SerializeField] public GameObject Prefab { get; private set; }
    }
}