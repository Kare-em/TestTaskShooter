using System;
using _MainAssets.Scripts.Player.Entity;
using _MainAssets.Scripts.Touch;
using UnityEngine;

namespace _MainAssets.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMoveController : EntityMoveController, IDraggable
    {
        public static Action OnBase;
        private bool drag;
        private Vector3 xzDelta;
        private PlayerModel _playerModel;
        private float _speed;

        protected override void OnEnable()
        {
            ComponentsInit();
            _isMobile = true;
            _playerModel = GetComponent<PlayerModel>();
            TouchHandler.OnBeginDrag += OnBeginDrag;
            TouchHandler.OnDrag += OnDrag;
            TouchHandler.OnEndDrag += OnEndDrag;
            GameController.gameEnded += OnEnd;
        }

        private void Start()
        {
            _speed = _playerModel.Speed;
        }

        private void OnDisable()
        {
            TouchHandler.OnBeginDrag -= OnBeginDrag;
            TouchHandler.OnDrag -= OnDrag;
            TouchHandler.OnEndDrag -= OnEndDrag;
            GameController.gameEnded -= OnEnd;
        }

        private void OnEnd()
        {
            this.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Base"))
                OnBase?.Invoke();
        }

        private void Update()
        {
            if (drag) PlayerMove();
            
        }

        private void PlayerMove()
        {
            xzDelta.x = TouchHandler.Instance.TempDelta.x;
            xzDelta.z = TouchHandler.Instance.TempDelta.y;
            _deltaPosition = xzDelta;
            Move(_speed);
        }

        public void OnBeginDrag(Vector2 position)
        {
            _playerModel.SetRun(true);
            _playerModel.SetDirectionForward(true);
            drag = true;
        }

        public void OnDrag(Vector2 position)
        {
        }

        public void OnEndDrag()
        {
            _playerModel.SetRun(false);
            drag = false;
        }
    }
}