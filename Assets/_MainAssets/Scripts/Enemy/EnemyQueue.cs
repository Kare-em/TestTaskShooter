using System.Collections.Generic;
using UnityEngine;

namespace _MainAssets.Scripts
{
    public class EnemyQueue : MonoBehaviour
    {
        #region Singleton

        private static EnemyQueue _instance;

        public static EnemyQueue Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<EnemyQueue>();
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
                Debug.Log("Duplicate " + nameof(EnemyQueue));
            }
        }

        #endregion

        private List<Transform> _enemies;
        private Transform closest;

        private void OnEnable()
        {
            CheckInstance();
            _enemies = new List<Transform>();
        }

        public int GetCount() => _enemies.Count;

        public Transform GetEnemy()
        {
            closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = GameController.Instance.GetPlayer().transform.position;
            foreach (Transform go in _enemies)
            {
                if(!go)
                    continue;
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }

            return closest;
        }

        public void AddEnemy(Transform enemyController)
        {
            _enemies.Add(enemyController);
        }

        public void DeleteEnemy(Transform enemy)
        {
            _enemies.Remove(enemy);
        }
    }
}