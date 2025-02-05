using System;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    [CreateAssetMenu(fileName = "Item", menuName = "Items/Item", order = 100)]
    public class Item : ScriptableObject
    {
        [NonSerialized] public int Index;
        
        public ItemType Type;
        public Sprite Icon;
        public string Name;
        public int MaxStack, Stack;
        public float Defense, Weight;
    }
}
