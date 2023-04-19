using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] InventoryItem[] leftInventory;
    [SerializeField] InventoryItem[] rightInventory;
    [SerializeField] Transform leftInventoryParent;
    [SerializeField] Transform rightInventoryParent;
    [SerializeField] Animator anim;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] Player player;

    int CurrentIndexLeft = -1;
    int CurrentIndexRight = -1;

    Item CurrentItemLeft;
    Item CurrentItemRight;

    EquipedItem LeftEquiped;
    EquipedItem RightEquiped;

    private void Start()
    {
        for (int i = 0; i < leftInventory.Length; i++)
        {
            leftInventory[i].Object.SetActive(false);
        }

        for (int i = 0; i < rightInventory.Length; i++)
        {
            rightInventory[i].Object.SetActive(false);
        }


        WearItemLeft(0);
        WearItemRight(0);
    }


    private void Update()
    {
        if (playerHealth.isDead || !player.isLocal)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (RightEquiped)
            {
                if (RightEquiped.OnUse())
                {
                    Attack();
                }
            }
        }
    }

    private void Attack()
    {
        if (CurrentItemRight != null)
        {
            int index = Random.Range(1, 2);
            var trigger = "Attack_0" + index;
            anim.Play(trigger);
        }
    }

    public void WearItem(int index, bool isRightHand)
    {
        if (isRightHand)
        {
            WearItemRight(index);
        }
        else
        {
            WearItemLeft(index);
        }
    }

    private void WearItemRight(int index)
    {
        if (CurrentIndexRight == index)
        {
            return;
        }

        for (int i = 0; i < rightInventory.Length; i++)
        {
            rightInventory[i].Object.SetActive(rightInventory[i].Index == index);
            if (rightInventory[i].Index == index)
            {
                CurrentItemRight = rightInventory[i].Item;
                RightEquiped = rightInventory[i].EI;
            }
        }

        CurrentIndexRight = index;
    }
    private void WearItemLeft(int index)
    {
        if (CurrentIndexLeft == index)
        {
            return;
        }

        for (int i = 0; i < leftInventory.Length; i++)
        {
            leftInventory[i].Object.SetActive(leftInventory[i].Index == index);
            if (leftInventory[i].Index == index)
            {
                CurrentItemLeft = leftInventory[i].Item;
                LeftEquiped = leftInventory[i].EI;
            }
        }

        CurrentIndexLeft = index;
    }
}
