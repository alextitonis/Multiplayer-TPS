using System.Collections;
using UnityEngine;

public class EquipedWeapon : EquipedItem
{
    [SerializeField] float attackTime = .5f;
    [SerializeField] Player owner;

    Weapon weapon => (Weapon)item;

    bool attacking = false;

    public override bool OnUse()
    {
        if (!owner.isLocal)
        {
            return false;
        }

        if (attacking)
        {
            return false;
        }

        StartCoroutine(Attack());
        return true;
    }

    IEnumerator Attack()
    {
        attacking = true;
        yield return new WaitForSeconds(attackTime);
        attacking = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!owner.isLocal)
        {
            return;
        }

        if (attacking)
        {
            Debug.Log(collision.collider.name);
            if (collision.collider.tag == "Player")
            {
                var player = collision.collider.GetComponent<Player>();
                if (player.isLocal)
                {
                    return;
                }

                var playerHealth = player.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(weapon.Damage);
            }
        }
    }
}