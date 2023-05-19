using System;
using UnityEngine;

namespace _MainAssets.Scripts.Enemy
{
    public class EnemyStorage : MonoBehaviour
    {
        #region Singleton

        private static EnemyStorage _instance;

        public static EnemyStorage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<EnemyStorage>();
                return _instance;
            }
        }

        private void CheckInstance()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(this);
            }
        }

        #endregion

        private void OnEnable()
        {
            CheckInstance();
        }

        private void OnDisable()
        {
            _instance = null;
        }

        public int EnemyCount { get; set; }
        public static Action enemyDead;

        public void EnemyDead()
        {
            enemyDead?.Invoke();
            EnemyCount--;
            if (EnemyCount < 1)
                GameController.Instance.EnemiesDestroyComplete();
        }
    }
}