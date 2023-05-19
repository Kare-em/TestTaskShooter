using System;
using System.Collections.Generic;
using _MainAssets.Scripts.Enemy;
using _MainAssets.Scripts.Player.Entity;
using UnityEngine;

namespace _MainAssets.Scripts.Player
{
    public enum PlayerStates
    {
        Idle = 0,
        IdleShoot = 1,
        Run = 2,
        RunShoot = 3,
        RunBack = 4,
        RunBackShoot = 5,
        Dead = 6
    }

    public class PlayerAnimations : MonoBehaviour
    {
        private List<Animator> _playerAnimators;
        private PlayerModel _playerModel;
        private const string state = "State";

        private void OnEnable()
        {
            _playerAnimators = new List<Animator>();
            _playerModel = GetComponent<PlayerModel>();

            _playerModel.StateUpdate += OnStateUpdate;
            _playerModel.Dead += OnDead;
            PlayerSpawner.playerSpawned += OnSpawn;
            GameController.enemiesDestroyed += OnGameEnded;
            GameController.gameEnded += OnGameEnded;
        }

       
        private void OnDisable()
        {
            _playerModel.StateUpdate -= OnStateUpdate;
            _playerModel.Dead -= OnDead;
            PlayerSpawner.playerSpawned -= OnSpawn;
            GameController.enemiesDestroyed -= OnGameEnded;
            GameController.gameEnded -= OnGameEnded;
        } 

        private void OnGameEnded()
        {
            SetState(0);
        }

        private void OnDead()
        {
            SetState(6);
            Destroy(this);
        }


        

        private void OnStateUpdate()
        {
            if (_playerModel.IsRun)
                if (_playerModel.IsMoveForward)
                    if (_playerModel.IsAttack)
                        SetState(3);
                    else
                        SetState(2);
                else if (_playerModel.IsAttack)
                    SetState(5);
                else
                    SetState(4);
            else if (_playerModel.IsAttack)
                SetState(1);
            else
                SetState(0);
        }

        private void OnSpawn(GameObject obj)
        {
            _playerAnimators.Add(obj.GetComponentInChildren<Animator>());
        }

        private void SetState(int index)
        {
            foreach (var playerAnimator in _playerAnimators)
            {
                if(playerAnimator)
                    playerAnimator.SetInteger("State",  index);
            }
        }
    }
}