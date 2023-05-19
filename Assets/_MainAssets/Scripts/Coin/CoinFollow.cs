using System;
using _MainAssets.Scripts.UI;
using UnityEngine;

namespace _MainAssets.Scripts.Coin
{
    public class CoinFollow : MonoBehaviour
    {
        [SerializeField] private CurrencyType _currencyType;
        public static Action<CurrencyType> pickedUp;
        private Transform _player;
        private bool _triggerFollow;

        private void OnEnable()
        {
            _triggerFollow = true;
            GameController.enemiesDestroyed += OnDestroyed;
            _player = GameController.Instance.GetPlayer().transform;
        }

        private void OnDisable()
        {
            GameController.enemiesDestroyed -= OnDestroyed;
        }

        private void OnDestroyed()
        {
            GameController.enemiesDestroyed -= OnDestroyed;
            _triggerFollow = false;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_triggerFollow)
                if (other.CompareTag("Player"))
                    Follow();
        }

        private void FixedUpdate()
        {
            if (!_triggerFollow||GameController.Instance.EnemiesDestroyed)
                Follow();
        }

        private void Follow()
        {
            if (Vector3.Distance(transform.position, _player.position) > 2.5f)
                transform.position =
                    Vector3.Lerp(transform.position, _player.position, 0.07f);
            else
            {
                pickedUp?.Invoke(_currencyType);
                Destroy(gameObject);
            }
        }
    }
}