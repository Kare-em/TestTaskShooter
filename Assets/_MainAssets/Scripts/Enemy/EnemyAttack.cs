using System.Collections;
using _MainAssets.Scripts.Player.Entity;
using _MainAssets.Scripts.Player.Entity.Enemy;
using UnityEngine;

namespace _MainAssets.Scripts.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        private EnemyModel _enemyModel;
        private bool _attacks;

        private void OnEnable()
        {
            _enemyModel = GetComponentInParent<EnemyModel>();
            _enemyModel.Dead += OnDead;
        }
        private void OnDisable()
        {
            _enemyModel.Dead -= OnDead;
        }
        private void OnDead()
        {
            StopAllCoroutines();
            enabled = false;
        }

        

        private void OnTriggerEnter(Collider other)
        {
            if (_enemyModel.Target!=null)
                return;
            if (other.CompareTag("Player"))
            {
                if (_enemyModel.Target!=null)
                    return;
                _enemyModel.Target = GameController.Instance.GetPlayer().GetComponent<PlayerModel>();
                _enemyModel.CanAttack = true;
                _enemyModel.Attack?.Invoke();
            }
            if (other.CompareTag("RadiusTrigger"))
            {
                if(_enemyModel.Target)
                    return;
                _enemyModel.IsWithinReach = true;
                _enemyModel.Run?.Invoke();

            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _enemyModel.Target = null;
                _enemyModel.CanAttack = false;
                _enemyModel.Run?.Invoke();
                StopCoroutine(Hit());
            }
        }

        public void Attack()
        {
            if (_attacks)
                return;
            StartCoroutine(Hit());
        }

        private IEnumerator Hit()
        {
            _attacks = true;
            _enemyModel.Target?.ChangeHP(_enemyModel.HandDamage);
            yield return new WaitForSeconds(_enemyModel.HandReloading);
            _attacks = false;
        }
    }
}