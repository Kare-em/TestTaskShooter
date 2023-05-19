using System;
using System.Collections;
using UnityEngine;

namespace _MainAssets.Scripts
{
    public enum VFXType
    {
        Bullet = 0,
        Grenade = 1,
        BloodHit = 2,
        BloodDeath = 3
    }

    public class VFX : MonoBehaviour
    {
        public static Action<VFXType, Vector3> vfx;
        [SerializeField] private Pool[] _pool;

        private void OnEnable()
        {
            vfx += OnVFX;
        }

        private void OnDisable()
        {
            vfx -= OnVFX;
        }

        private void OnVFX(VFXType arg1, Vector3 position)
        {
            var obj = _pool[(int)arg1].GetObject();
            obj.transform.position = position;
            obj.SetActive(true);
            StartCoroutine(Deactivate(obj));
        }


        private IEnumerator Deactivate(GameObject obj)
        {
            yield return new WaitForSeconds(2f);
            obj.SetActive(false);
        }
    }
}