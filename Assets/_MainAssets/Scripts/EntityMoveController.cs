using _MainAssets.Scripts.Player.Entity;
using UnityEngine;

namespace _MainAssets.Scripts
{
    public abstract class EntityMoveController : MonoBehaviour
    {
        protected Vector3 _deltaPosition;
        protected bool _isMobile;
        protected Rigidbody _rb;

        protected virtual void OnEnable()
        {
        }

        protected void ComponentsInit()
        {
            _rb = GetComponent<Rigidbody>();
        }

        protected virtual void Move(float speed)
        {
            if (_isMobile)
            {
                _rb.velocity =  speed * _deltaPosition;
            }
        }
    }
}