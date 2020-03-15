using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pump : Weapon
{
    public override void Interact()
    {
        Bullet go = Instantiate(bulletPrefab, anchor.position, Quaternion.identity);

        go.Init(p);

        Destroy(gameObject);
    }
}
