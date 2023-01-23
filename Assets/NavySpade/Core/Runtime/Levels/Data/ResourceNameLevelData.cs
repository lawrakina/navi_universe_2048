using UnityEngine;

namespace Main.Levels.Data
{
    public class ResourceNameLevelData : LevelDataBase
    {
        [field:SerializeField] public string PrefabName { get; private set; }
    }
}