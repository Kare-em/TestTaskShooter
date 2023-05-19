using System.Collections.Generic;
using UnityEngine;

namespace _MainAssets.Scripts.Player
{
    [CreateAssetMenu(fileName = "PlayersPrefabs", menuName = "ScriptableObjects/PlayersPrefabsScriptableObject", order = 1)]
    public class PlayersData : ScriptableObject
    {
        #region Singleton

        private static PlayersData _instance;

        public static PlayersData Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<PlayersData>();
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
                Debug.Log("Duplicate " + nameof(PlayersData));
            }
        }

        #endregion

        [SerializeField] public List<GameObject> Prefabs;
    }
}