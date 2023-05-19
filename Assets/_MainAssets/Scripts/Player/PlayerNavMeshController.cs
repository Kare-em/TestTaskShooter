using System;
using _MainAssets.Scripts.Player.Entity;
using _MainAssets.Scripts.Player.Entity.Enemy;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace _MainAssets.Scripts.Enemy
{
    public class PlayerNavMeshController : EntityMoveController
    {
        public Transform TargetTransform;
        private PlayerModel _playerModel;

        private NavMeshAgent _navMeshAgent;

        protected override void OnEnable()
        {
            ComponentsInit();
            _playerModel = GameController.Instance.GetPlayer().GetComponent<PlayerModel>();
            GameController.gameEnded += OnEnd;
            _playerModel.Dead += OnEnd;
            _isMobile = true;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = 1.3f * _playerModel.Speed;
        }

        private void OnDisable()
        {
            _playerModel.Dead -= OnEnd;
            GameController.gameEnded -= OnEnd;
        }

        private void OnEnd()
        {
            enabled = false;
            _navMeshAgent.enabled = false;
            _navMeshAgent.speed = 0f;
        }

        private void Update()
        {
            if (TargetTransform)
            {
                if (_playerModel.IsRun)
                    _navMeshAgent.destination = TargetTransform.position;
                else
                    _navMeshAgent.destination = transform.position;
            }
        }

        protected override void Move(float speed)
        {
            if (_isMobile)
            {
                _rb.velocity = speed * _deltaPosition;
            }
        }
    }
}