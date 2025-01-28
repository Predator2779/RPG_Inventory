using UnityEngine;

namespace Inventory
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Item", menuName = "Items/Item", order = 100)]
    public class Item : ScriptableObject
    {
        public ItemType Type;
        public Sprite Icon;
        public string Name, Description;
        public int MaxStack, Stack;
        public float Defense, Weight;
    }
}
