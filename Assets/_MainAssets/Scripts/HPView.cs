using System;
using _MainAssets.Scripts.Player.Entity;
using _MainAssets.Scripts.Player.Entity.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace _MainAssets.Scripts
{
    public class HPView : MonoBehaviour
    {
        [SerializeField] private Text _hp;
        [SerializeField] private Slider _slider;
        [SerializeField] private EnemyModel _enemyModel;
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private Transform _main;
        private EntityModel _entityModel;
        private Camera _camera;

        private void OnEnable()
        {
            _camera = Camera.main;
            if (_enemyModel != null)
                _entityModel = _enemyModel;

            if (_playerModel != null)
                _entityModel = _playerModel;
            OnHPChanged(_entityModel.HP);
            _entityModel.Damage += OnHPChanged;
            _entityModel.startHPUpdated += OnStartHPChanged;

            _entityModel.Dead += OnDead;
            if(_enemyModel)
                OnStartHPChanged(_enemyModel.HP);
            if(_playerModel)
                OnStartHPChanged(_playerModel.HP);
        }


        private void OnDisable()
        {
            _entityModel.Damage -= OnHPChanged;
            _entityModel.Dead -= OnDead;
            _entityModel.startHPUpdated -= OnStartHPChanged;
        }

        private void OnStartHPChanged(float obj)
        {
            _slider.maxValue = obj;
            _slider.value = obj;
        }

        private void OnDead()
        {
            Destroy(gameObject);
        }

        private void OnHPChanged(float obj)
        {
            float round = (float)Math.Round(_entityModel.HP, 1);
            _slider.value = round;
            _hp.text = round.ToString();
        }

        private void Update()
        {
            if (_main)
                transform.position = _main.position;
        }
    }
}