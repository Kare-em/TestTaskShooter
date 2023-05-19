using System.Collections.Generic;
using UnityEngine;

namespace _MainAssets.Scripts.Enemy
{
    [CreateAssetMenu(fileName = "EnemyPrefabs", menuName = "ScriptableObjects/EnemyPrefabsScriptableObject", order = 1)]
    public class EnemiesData : ScriptableObject
    {
        #region Singleton

        private static EnemiesData _instance;

        public static EnemiesData Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<EnemiesData>();
                return _instance;
            }
            private set => _instance = value;
        }

        private void CheckInstance()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this);
                Debug.Log("Duplicate " + nameof(EnemiesData));
            }
        }

        #endregion

        [SerializeField] public List<GameObject> Prefabs;
    }
}