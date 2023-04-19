using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 1)]
public class Weapon : Item
{
    public int InventoryIndex;
    public float Damage;
}