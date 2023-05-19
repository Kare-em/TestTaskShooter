using System;
using System.Collections.Generic;
using _MainAssets.Scripts.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _MainAssets.Scripts
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField] private bool _spawnOnStart;
        [SerializeField] private bool _randomRotation = true;
        [SerializeField] private EnemiesData _enemiesData;
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private int _count;
        [SerializeField] private List<Transform> _points;
        private Pool _pool;
        public Action spawned;

        private void OnEnable()
        {
            _pool = GetComponent<Pool>();
            if (_spawnOnStart)
                Spawn();
        }

        private void Start()
        {
            _pool = GetComponent<Pool>();
        }

        private void OnDrawGizmos()
        {
            foreach (var point in _points)
            {
                Gizmos.DrawSphere(point.position, 1f);
            }
        }

        public void Spawn()
        {
            for (int i = 0; i < _count; i++)
            {
                var enemy = _pool
                    ? _pool.GetActivatedObject()
                    : Instantiate(_enemiesData.Prefabs[(int)_enemyType], _points[i].position, Quaternion.identity);
                enemy.GetComponentInChildren<EnemyMoveController>().Main.rotation =
                    _randomRotation ? Quaternion.Euler(0f, Random.Range(0, 359), 0f) : _points[i].rotation;
                
                EnemyStorage.Instance.EnemyCount++;
            }

            spawned?.Invoke();
        }

        private void ItemConfigure(GameObject enemy, int i)
        {
        }
    }
}