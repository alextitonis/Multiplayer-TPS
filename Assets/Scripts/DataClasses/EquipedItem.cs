using UnityEngine;

public abstract class EquipedItem : MonoBehaviour
{
    public Item item;

    public abstract bool OnUse();
}