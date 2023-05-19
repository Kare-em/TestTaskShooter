using System.Collections.Generic;
using UnityEngine;

namespace _MainAssets.Scripts
{
    public class EnemyPool : MonoBehaviour
    {
        #region Singleton

        private static EnemyPool _instance;

        public static EnemyPool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<EnemyPool>();
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
                Debug.Log("Duplicate " + nameof(EnemyPool));
            }
        }

        #endregion
        
        public List<GameObject> _enemies;

        private void OnEnable()
        {
            CheckInstance();
            _enemies = new List<GameObject>();
        }

        private void OnDisable()
        {
            _enemies.Clear();
        }

        public void AddEnemy(GameObject enemy)
        {
            _enemies.Add(enemy);
        }

        public void EnableEnemy(GameObject enemy)
        {
            enemy.SetActive(true);
        }

        public void DisableEnemy(GameObject enemy)
        {
            enemy.SetActive(false);
        }

        public void DeleteEnemy(GameObject enemy)
        {
            _enemies.Remove(enemy);
        }
    }
}