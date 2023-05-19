using _MainAssets.Scripts.Player.Entity.Enemy;
using UnityEngine;

namespace _MainAssets.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public EnemyModel Enemy { get; private set; }

        private void OnEnable()
        {
            Enemy ??= new EnemyModel();
            
        }
    }
}