using _MainAssets.Scripts.Enemy;
using UnityEngine;

namespace _MainAssets.Scripts.Player.Entity.Enemy
{
    public class EnemyModel : EntityModel
    {
        public float HandDamage = 1f;
        public float HandReloading = 1f;
        public bool IsWithinReach;
        public bool IsDead;
        public bool CanAttack;
        public PlayerModel Target;

        private void OnEnable()
        {
            Dead += OnDead;
            if(EnemyStorage.Instance)
                Dead += EnemyStorage.Instance.EnemyDead;
            StartHP = HP;
        }
        [ContextMenu("Die")]
        public void DebugDie()
        {
            Dead?.Invoke();
        }
        private void OnDead()
        {
            Dead -= EnemyStorage.Instance.EnemyDead;
            IsDead = true;
            Destroy(gameObject,0.7f);
        }

        private void OnDisable()
        {
            Dead -= OnDead;
            if (EnemyStorage.Instance)
                Dead -= EnemyStorage.Instance.EnemyDead;
        }
    }
}