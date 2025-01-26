using UnityEngine;

[System.Serializable]
public class Item
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public ItemType Type;
    public int MaxStack;
    public int Stack;
    public float Defense;
    public float Weight;
}
