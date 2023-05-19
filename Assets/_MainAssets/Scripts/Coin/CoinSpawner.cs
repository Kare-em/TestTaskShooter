using System;
using _MainAssets.Scripts.Enemy;
using _MainAssets.Scripts.Player.Entity.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _MainAssets.Scripts.Coin
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] _coin;
        private EnemyModel _enemyModel;
        private GameObject _spawnedCoin;

        private void OnEnable()
        {
            _enemyModel = GetComponent<EnemyModel>();
            _enemyModel.Dead += OnDead;
            Spawn();
        }

        private void Spawn()
        {
            int randomIndex = Random.Range(0, _coin.Length);
            _spawnedCoin = Instantiate(_coin[randomIndex], new Vector3(transform.position.x, 0f, transform.position.z),
                Quaternion.identity);
            _spawnedCoin.SetActive(false);
        }

        private void OnDisable()
        {
            _enemyModel.Dead -= OnDead;
        }

        private void OnDead()
        {
            _spawnedCoin.transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            _spawnedCoin.SetActive(true);
        }
    }
}