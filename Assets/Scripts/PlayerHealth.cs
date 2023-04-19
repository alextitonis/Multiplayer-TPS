using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] Animator anim;
    [SerializeField] Player player;

    float currentHealth = 100;

    public System.Action OnDoDie;
    public bool isDead { get { return currentHealth <= 0; } }

    private void Start()
    {
        if (isServer)
        {
            SetHealth(maxHealth);
        }

        if (player.isLocal)
        {
            PlayerUIHandller.getInstance.healthSlider.maxValue = maxHealth;

        }
    }

    public void TakeDamage(float value)
    {
        Debug.Log("TakeDamage: " + value);

        if (NetworkServer.active)
        {
            SetHealth(currentHealth - value);
        }
        else
        {
            Debug.Log("CmdTakeDamage");
            CmdTakeDamage(value);
        }
    }

    [Command]
    private void CmdTakeDamage(float value)
    {
        var newValue = currentHealth - value;
        Debug.Log(name + " Take Damage: " + newValue + ": " + value);
        SetHealth(newValue);
    }

    [Server]
    private void SetHealth(float value)
    {
        currentHealth = value;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        RpcSetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            DoDie();
        }
    }

    [ClientRpc]
    private void RpcSetHealth(float value)
    {
        Debug.Log(name + "got new health: " + value);
        currentHealth = value;
        anim.SetFloat("Health", currentHealth);

        if (player.isLocal)
        {
            PlayerUIHandller.getInstance.healthSlider.value = currentHealth;
        }
    }

    private void DoDie()
    {
        OnDoDie?.Invoke();

        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5f);
        SetHealth(maxHealth);
    }
}