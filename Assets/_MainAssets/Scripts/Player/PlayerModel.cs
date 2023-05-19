using System;
using System.Collections;
using _MainAssets.Scripts.Player.Entity.Enemy;
using UnityEngine;

namespace _MainAssets.Scripts.Player.Entity
{
    [Serializable]
    public class PlayerModel : EntityModel
    {
        public int Id;
        public bool IsAttack;
        public Transform Target;
        public EnemyModel EnemyModel;
        public Action StateUpdate;
        public Action TargetUpdate;
        public bool IsMoveForward { get; private set; }
        public bool IsRun { get; private set; }
        private float _shield = 1f;

        private void Start()
        {
            GameController.enemiesDestroyed += OnEnemiesDead;
            Init();
            SetRun(false);
            StartFindEnemy();
            StateUpdate();
        }

        
        private void Init()
        {
            _shield = 1f;
            HP = 20f;
            StartHP = HP;
            Damage?.Invoke(0f);
        }
        private void OnDisable()
        {
            GameController.enemiesDestroyed -= OnEnemiesDead;
        }

        private void OnEnemiesDead()
        {
            IsRun = false;
            IsAttack = false;
            StateUpdate();
        }

        public override void ChangeHP(float damage)
        {
            base.ChangeHP(damage / _shield);
        }

        public void SetRun(bool value)
        {
            IsRun = value;
            StateUpdate?.Invoke();
        }


        public void SetDirectionForward(bool value)
        {
            IsMoveForward = value;
            StateUpdate?.Invoke();
        }


        public void SetAttack(bool value)
        {
            IsAttack = value;
            StateUpdate?.Invoke();
        }

        private void StartFindEnemy()
        {
            StartCoroutine(FindEnemy());
        }

        private void StartUpdateEnemy()
        {
            StartCoroutine(UpdateEnemy());
        }

        private IEnumerator FindEnemy()
        {
            while (!IsAttack)
            {
                if (EnemyQueue.Instance.GetCount() > 0)
                    Target = EnemyQueue.Instance.GetEnemy();
                if (Target)
                {
                    EnemyModel = Target.gameObject.GetComponentInChildren<EnemyModel>();
                    if (EnemyModel.IsDead)
                    {
                        OnEnemyDead();
                    }
                    else
                    {
                        EnemyModel.Dead += OnEnemyDead;
                        IsAttack = true;
                        SetAttack(true);
                        TargetUpdate?.Invoke();
                        StateUpdate?.Invoke();
                        StartUpdateEnemy();
                    }
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

        private IEnumerator UpdateEnemy()
        {
            yield return new WaitForSeconds(0.1f);
            if(EnemyModel)
                EnemyModel.Dead -= OnEnemyDead;
            Target = null;
            EnemyModel = null;
            IsAttack = false;
            StateUpdate?.Invoke();
            StartFindEnemy();
        }

        private void OnEnemyDead()
        {
            EnemyModel.Dead -= OnEnemyDead;
            EnemyQueue.Instance.DeleteEnemy(Target);
            Target = null;
            EnemyModel = null;
            IsAttack = false;
            StateUpdate();
            StartFindEnemy();
        }
    }
}