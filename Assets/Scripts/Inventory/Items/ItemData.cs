using System;
using UnityEngine;

namespace Inventory.Items
{
    [Serializable]
    [CreateAssetMenu(fileName = "Item", menuName = "Items/Item", order = 100)]
    public class ItemData : ScriptableObject
    {
        public Item item;
    }
}