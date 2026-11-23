using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerSO", menuName = "game/PlayerData")]
    public class PlayerSO : ScriptableObject
    {
        public string characterName;
        public int maxHealth;
        public int damage;
    }
}