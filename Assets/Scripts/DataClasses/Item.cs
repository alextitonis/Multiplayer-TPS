using UnityEngine;

public abstract class Item : ScriptableObject
{
    public ItemType ItemType;
    public bool IsRightHand;
}

public enum ItemType
{
    Weapon = 0,
}