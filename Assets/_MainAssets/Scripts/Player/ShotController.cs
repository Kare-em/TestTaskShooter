using _MainAssets.Scripts.Player.Entity;
using _MainAssets.Scripts.Player.Entity.Enemy;
using UnityEngine;

namespace _MainAssets.Scripts.Player
{
    public class ShotController : MonoBehaviour
    {
        [SerializeField] private bool _autoShot = true;
        private Vector3 _targetVector3;
        private Vector3 _deltaVector3;
        private Quaternion _targetRotation;
        private EnemyModel _enemyModel;
        private Weapon _weapon;
        private float _angle;
        private float _angularSpeed = 15f;
        private float _angularSpeedIdle = 4f;
        private PlayerModel _playerModel;

        private void OnEnable()
        {
            _playerModel = GameController.Instance.GetPlayer().GetComponent<PlayerModel>();
            _playerModel.TargetUpdate += OnTargetUpdate;
            _weapon = GetComponentInChildren<Weapon>();
            GameController.gameEnded += OnEnd;
        }


        private void OnDisable()
        {
            GameController.gameEnded -= OnEnd;
            _playerModel.TargetUpdate -= OnTargetUpdate;
        }

        private void OnTargetUpdate()
        {
        }

        private void OnEnd()
        {
            Destroy(this);
        }


        private void Update()
        {
            if (_playerModel.Target)
            {
                _angle = Vector3.Angle(_weapon.Muzzle[0].forward, _playerModel.Target.position - transform.position);
                if (Mathf.Abs(_angle) < 8f && _autoShot)
                    _weapon.Shot();
                _targetVector3 = _playerModel.Target.position;
                LerpRotate(_angularSpeed);
            }
            else
            {
                if (!TouchHandler.Instance.Pressed)
                    return;
                _targetVector3 = transform.position + new Vector3(TouchHandler.Instance.TempDelta.x, 0f,
                    TouchHandler.Instance.TempDelta.y);
                LerpRotate(_angularSpeedIdle);
            }
            //if (TouchHandler.Instance.TempDelta != Vector2.zero)
            //    if (Mathf.Abs(Vector3.Angle(transform.forward,  TouchHandler.Instance.TempDelta)) < 180f != _playerModel.IsMoveForward)
            //        _playerModel.SetDirectionForward(!_playerModel.IsMoveForward);
        }

        private void LerpRotate(float speed)
        {
            _deltaVector3 = _targetVector3 - transform.position;
            if (_deltaVector3 == Vector3.zero)
                return;
            _targetRotation = Quaternion.LookRotation(_deltaVector3);

            transform.rotation =
                Quaternion.LerpUnclamped(transform.rotation, _targetRotation, speed * Time.deltaTime);
        }
    }
}