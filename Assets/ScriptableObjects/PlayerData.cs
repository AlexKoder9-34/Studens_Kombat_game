using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerSO", menuName = "game/PlayerData")]
    public class PlayerData : ScriptableObject
    {
       [SerializeField] private string characterName;
       [SerializeField] private float maxHealth;
       [SerializeField] private float damage;
       [SerializeField] private float critDamage;
       [SerializeField] private float moveSpeed;
       [SerializeField] private float jumpHeight;
        
        public string CharacterName => characterName;
        public float MaxHealth => maxHealth;
        public float Damage => damage;
        public float CritDamage => critDamage;
        public float MoveSpeed => moveSpeed;
        public float JumpHeight => jumpHeight;
    }
}