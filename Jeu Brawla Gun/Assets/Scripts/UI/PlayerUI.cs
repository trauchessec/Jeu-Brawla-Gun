using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public PlayerController player;

    public Image healthBar, reloadBar, currentWeapon, heartBar;
    public Text bullet;

    private void Start()
    {
        InvokeRepeating("UpdateUI", 0, 0.1f);
    }

    void UpdateUI()
    {
        UpdateHealth();
        UpdateBullet();
        UpdateReload();
        UpdateWeapon();
        UpdateHeart();
    }

    private void UpdateHeart() {
        heartBar.fillAmount = (float)player.resuLeft / 3f;
    }

    private void UpdateWeapon()
    {
        if (player.Weapon != null)
          currentWeapon.sprite = player.Weapon.image;

        currentWeapon.gameObject.SetActive(player.Weapon != null);
    }

    void UpdateHealth()
    {
        healthBar.fillAmount = player.LifePercentage;
    }

    void UpdateBullet()
    {
        if (player.Weapon != null)
        {
            bullet.text = player.Weapon.Bullet.ToString() + " / " + player.Weapon.MaxBullet.ToString();
        }
        else
        {
            bullet.text = "0 / 0";
        }
    }

    void UpdateReload()
    {
        reloadBar.transform.parent.gameObject.SetActive(player.Weapon != null);
        if (player.Weapon != null)
        {
            reloadBar.fillAmount = player.Weapon.ReloadTime;
        }
    }
}
