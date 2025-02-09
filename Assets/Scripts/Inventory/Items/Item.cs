using System;
using UnityEngine;

namespace Inventory.Items
{
    [Serializable]
    public class Item
    {
        [HideInInspector] public int Index;
        
        public ItemType Type;
        public Sprite Icon;
        public string Name;
        public int MaxStack, Stack;
        public float Defense, Weight;

        public void PrintInfo()
        {
            Debug.Log(
                $"Index: {Index}; Type: {Type}; Name: {Name}\n" +
                $"Stack: {Stack}; MaxStack: {MaxStack}\n;" +
                $"Defense: {Defense}; Weight: {Weight}\n;"
            );
        }
    }
}