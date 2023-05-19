using System.Collections;
using UnityEngine;

namespace _MainAssets.Scripts.Enemy
{
    public class GameEnemiesSpawner : MonoBehaviour
    {
        [SerializeField] private EnemiesSpawner[] _enemiesSpawners;

        private void Start()
        {
            StartCoroutine(AutoSpawner());
        }

        private IEnumerator AutoSpawner()
        {
            while (!GameController.Instance.GameEnded)
            {
                Spawn();
                yield return new WaitForSeconds(0.5f);
            }
        }

        private void Spawn()
        {
            _enemiesSpawners[Random.Range(0, _enemiesSpawners.Length)].Spawn();
        }
    }
}