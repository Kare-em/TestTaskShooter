using System;
using _MainAssets.Scripts.Player.Entity;
using _MainAssets.Scripts.Player.Entity.Enemy;
using UnityEngine;
using UnityEngine.Serialization;

namespace _MainAssets.Scripts
{
    public class EnemyAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private EnemyModel _model;
        private bool ended;

        private void OnEnable()
        {
            _model = GetComponent<EnemyModel>();

            _model.Idle += OnIdle;
            _model.Run += OnRun;
            _model.Attack += OnAttack;
            _model.Dead += OnDead;
            GameController.levelFailed += OnGameEnded;
        }

        private void OnDisable()
        {
            _model.Idle -= OnIdle;
            _model.Run -= OnRun;
            _model.Attack -= OnAttack;
            _model.Dead -= OnDead;
            GameController.levelFailed -= OnGameEnded;
        }


        private void OnIdle()
        {
            SetTrigger("Idle");
        }

        private void OnRun()
        {
            SetTrigger("Run");
        }

        private void OnAttack()
        {
            SetTrigger("Attack");
        }

        private void OnDead()
        {
            SetTrigger("Dead");
        }


        private void OnGameEnded()
        {
            SetTrigger("PlayerDead");
        }

        private void SetTrigger(string index)
        {
            _animator?.SetTrigger(index);
        }
    }
}