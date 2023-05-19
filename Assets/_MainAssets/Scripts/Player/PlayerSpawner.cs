using System;
using System.Collections.Generic;
using _MainAssets.Scripts.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _MainAssets.Scripts.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayersData _playersData;
        [SerializeField] private bool _randomRotation = true;
        [SerializeField] private List<Transform> _points;
        [SerializeField] private GameObject _glow;
        public static Action<GameObject> playerSpawned;
        private List<GameObject> _players;
        private GameObject _lastSpawned;
        private List<GameObject> _glows;

        private void OnEnable()
        {
            _players = new List<GameObject>();
            _glows = new List<GameObject>();
        }


        private void OnBought(int obj, int idprod)
        {
            foreach (var player in _players)
            {
                Destroy(player);
            }

            Spawn();
            var glow = Instantiate(_glow, _lastSpawned.transform.position, Quaternion.identity);
            _glows.Add(glow);
        }



        private void Start()
        {
            Spawn();
        }

        private void OnDrawGizmos()
        {
            foreach (var point in _points)
            {
                Gizmos.DrawSphere(point.position, 0.5f);
            }
        }

        public void Spawn()
        {
            var player = Instantiate(_playersData.Prefabs[0], _points[0].position,
                _randomRotation ? Quaternion.Euler(0f, Random.Range(0, 359), 0f) : _points[0].rotation);
            _lastSpawned = player;
            _players.Add(player);
            player.GetComponent<PlayerNavMeshController>().TargetTransform = _points[0];

            playerSpawned?.Invoke(player);
        }
    }
}