using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : Bullet
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.rotation *= Quaternion.Euler(0, 0, -150);

        if (collision.gameObject.tag == "Player")
        {
            PlayerController pHit = collision.gameObject.GetComponent<PlayerController>();
            Hit(pHit);
        }
    }
}
