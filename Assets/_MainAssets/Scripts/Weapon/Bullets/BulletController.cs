using System;
using UnityEngine;

namespace _MainAssets.Scripts.Player.Entity
{
    public class BulletController : MonoBehaviour
    {
        public float Damage;
        private Rigidbody _rb;
        public Action hit;
        private void OnEnable()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Player"))
            {
                hit?.Invoke();
                gameObject.SetActive(false);
                //VFX.vfx?.Invoke(VFXType.Bullet, transform.position);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("BulletZone"))
                gameObject.SetActive(false);
        }

        public void Release(float speed, float damage)
        {
            _rb.velocity = transform.forward * speed;
            Damage = damage;
        }
    }
}