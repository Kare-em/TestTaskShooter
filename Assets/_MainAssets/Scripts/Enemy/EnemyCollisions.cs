using _MainAssets.Scripts.Player.Entity;
using _MainAssets.Scripts.Player.Entity.Enemy;
using UnityEngine;

namespace _MainAssets.Scripts.Enemy
{
    public class EnemyCollisions : MonoBehaviour
    {
        private EnemyModel _enemyModel;
        private void OnEnable()
        {
            _enemyModel = GetComponent<EnemyModel>();
            _enemyModel.Dead += OnDead;
        }

        private void OnDisable()
        {
            _enemyModel.Dead += OnDead;
        }

        private void OnDead()
        {
            VFX.vfx.Invoke(VFXType.BloodDeath,transform.position);
            enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("RadiusTrigger"))
            {
                EnemyQueue.Instance.AddEnemy(transform);
            }
            
            if (other.CompareTag("Bomb"))
            {
                EnemyQueue.Instance.AddEnemy(transform);
                _enemyModel.ChangeHP(other.gameObject.GetComponent<BulletController>().Damage);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("RadiusTrigger"))
            {
                EnemyQueue.Instance.DeleteEnemy(transform);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                _enemyModel.ChangeHP(collision.gameObject.GetComponent<BulletController>().Damage);
                VFX.vfx.Invoke(VFXType.BloodHit,collision.contacts[0].point);
                
            }
            
        }

    }
}