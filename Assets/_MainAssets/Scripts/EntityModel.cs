using System;
using UnityEngine;

namespace _MainAssets.Scripts.Player.Entity
{
    public abstract class EntityModel : MonoBehaviour
    {
        public float HP;
        private float _startHP;

        public float StartHP
        {
            get
            {
                if (_startHP == 0)
                    _startHP = HP;
                return _startHP;
            }
            protected set
            {
                _startHP = value;
                startHPUpdated?.Invoke(_startHP);
            }
        }

        public float Speed;
        
        public Action Idle;
        public Action Run;
        public Action Attack;
        public Action Dead;
        
        public Action<float> Damage;
        public Action<float> startHPUpdated;


        public virtual void ChangeHP(float damage)
        {
            if (HP <= 0.1f)
                return;
            HP -= damage;
            if (HP <= 0.1f)
            {
                Dead?.Invoke();
                damage += HP;
            }

            Damage?.Invoke(damage);
        }
    }
}