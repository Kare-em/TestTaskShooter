using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _MainAssets.Scripts.Player.Entity
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] public float BulletSpeed;
        [SerializeField] public float Damage;

        [FormerlySerializedAs("DPS")] [SerializeField]
        public float ShotsPerSec;

        [SerializeField] private Transform[] _muzzle;
        public Transform[] Muzzle => _muzzle;
        [SerializeField] private GameObject[] _effects;

        public Action ready;
        public bool IsReady;

        private Pool _bullets;
        private GameObject _currentBullet;
        private float _currentReloadingTime;
        private AudioSource _audioSource;

        private void OnEnable()
        {
            _bullets = GetComponent<Pool>();
            _audioSource ??= GetComponent<AudioSource>();
        }

        private void Update()
        {
            CheckReloading();
        }

        private void CheckReloading()
        {
            if (IsReady)
                return;

            _currentReloadingTime -= Time.deltaTime;
            if (_currentReloadingTime < 0f)
            {
                ready?.Invoke();
                IsReady = true;
            }
        }

        private void CheckBullets()
        {
            _currentReloadingTime = 1 / ShotsPerSec;
        }


        [ContextMenu("Shot")]
        public void Shot()
        {
            if (!IsReady)
                return;
            IsReady = false;

            InitBullets();
            CheckBullets();
        }

        public void ShotLeft()
        {
            _currentBullet = _bullets.GetObject();
            _currentBullet.transform.position = _muzzle[0].position;
            _currentBullet.transform.rotation = _muzzle[0].rotation;
            _currentBullet.SetActive(true);
            _currentBullet.GetComponent<BulletController>().Release(BulletSpeed, Damage);
            CheckBullets();
        }

        public void ShotRight()
        {
            _currentBullet = _bullets.GetObject();
            _currentBullet.transform.position = _muzzle[1].position;
            _currentBullet.transform.rotation = _muzzle[1].rotation;
            _currentBullet.SetActive(true);
            _currentBullet.GetComponent<BulletController>().Release(BulletSpeed, Damage);
            CheckBullets();
        }

        public void InitBullets()
        {
            foreach (var muzzle in _muzzle)
            {
                _currentBullet = _bullets.GetObject();
                _currentBullet.transform.position = muzzle.position;
                _currentBullet.transform.rotation = muzzle.rotation;
                _currentBullet.SetActive(true);
                _currentBullet.GetComponent<BulletController>().Release(BulletSpeed, Damage);
            }

            if (_effects.Length > 0)
                foreach (var effect in _effects)
                {
                    effect?.SetActive(true);
                }
        }
    }
}