using _MainAssets.Scripts.Enemy;
using _MainAssets.Scripts.Player;
using _MainAssets.Scripts.Player.Entity;
using UnityEngine;

namespace _MainAssets.Scripts
{
    public class GameModel : MonoBehaviour
    {
        private GameObject _player;

        public GameObject Player
        {
            get
            {
                if (_player == null)
                {
                    var player = FindObjectOfType<PlayerModel>();
                    _player = player?.gameObject;
                }

                return _player;
            }
        }
    }
}