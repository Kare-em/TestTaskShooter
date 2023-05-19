using System;
using _MainAssets.Scripts.Player.Entity;
using UnityEngine;

namespace _MainAssets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerModel Player { get; private set; }

        private void OnEnable()
        {
            Player ??= new PlayerModel();
        }
    }
}