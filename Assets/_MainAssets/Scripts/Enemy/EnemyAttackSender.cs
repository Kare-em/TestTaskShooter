using UnityEngine;

namespace _MainAssets.Scripts.Enemy
{
    public class EnemyAttackSender : MonoBehaviour
    {
        [SerializeField] private EnemyAttack _enemyAttack;
        public void Attack()
        {
            _enemyAttack.Attack();
        }
    }
}