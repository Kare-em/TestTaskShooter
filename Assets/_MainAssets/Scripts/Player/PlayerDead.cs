using System;
using _MainAssets.Scripts.Player.Entity;
using UnityEngine;

namespace _MainAssets.Scripts.Player
{
    public class PlayerDead : MonoBehaviour
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
            GameController.Instance.LevelFail();
        }
    }
}