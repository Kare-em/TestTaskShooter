using _MainAssets.Scripts.Player.Entity;
using UnityEngine;

namespace _MainAssets.Scripts.Player
{
    public class PlayerDeadAnimation : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<PlayerModel>().Dead += OnDead;
        }
        private void OnDisable()
        {
            GetComponent<PlayerModel>().Dead -= OnDead;
        }
        private void OnDead()
        {
            Destroy(gameObject);
        }
    }
}