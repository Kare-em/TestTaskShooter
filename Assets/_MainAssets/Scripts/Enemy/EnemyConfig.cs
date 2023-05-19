using UnityEngine;

namespace _MainAssets.Scripts.Enemy
{
    public class EnemyConfig: MonoBehaviour
    {
        [SerializeField] public EnemyType EnemyType;
        [SerializeField] public GameObject Prefab;
    }
}