using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
    public class EnemyData : ScriptableObject
    {
        public UnitType type;
        public Sprite icon;
        public int health;
        public float dashCooldown = 2f;
        public float dashSpeed = 5f;
    }
}
