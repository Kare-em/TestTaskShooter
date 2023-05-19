using _MainAssets.Scripts.Player.Entity.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace _MainAssets.Scripts.Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(EnemyModel))]
    public class EnemyMoveController : EntityMoveController
    {
        [SerializeField] private Transform _body;
        [SerializeField] private Transform _main;
        [SerializeField] private CapsuleCollider _capsuleCollider;
        private Rigidbody _rigidbody;
        private Transform targetTransform;
        private EnemyModel _enemyModel;

        public Transform Body => _body;
        public Transform Main => _main;
        private NavMeshAgent _navMeshAgent;

        protected override void OnEnable()
        {
            ComponentsInit();
            _enemyModel = GetComponent<EnemyModel>();
            _rigidbody = GetComponent<Rigidbody>();
            GameController.gameEnded += OnEnd;
            _enemyModel.Dead += OnEnd;
            targetTransform = GameController.Instance.GetPlayer().transform;
            _isMobile = true;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = _enemyModel.Speed;
        }

        private void OnDisable()
        {
            _enemyModel.Dead -= OnEnd;
            GameController.gameEnded -= OnEnd;
        }

        private void OnEnd()
        {
            _capsuleCollider.enabled = false;
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
            enabled = false;
            _navMeshAgent.speed = 0f;
            _navMeshAgent.enabled = false;
        }

        private void FixedUpdate()
        {
            if (_enemyModel.IsWithinReach && !_enemyModel.CanAttack)
            {
                if (targetTransform)
                {
                    _navMeshAgent.SetDestination(targetTransform.position);
                }
            }
        }

        protected override void Move(float speed)
        {
            if (_isMobile)
            {
                _rb.velocity = speed * _deltaPosition;
            }
        }
    }
}