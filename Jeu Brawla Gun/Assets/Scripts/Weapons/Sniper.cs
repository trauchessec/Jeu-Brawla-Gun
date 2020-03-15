using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{
    public Bullet bulletInterractPrefab;

    public override void Interact()
    {
        Vector3 dir = new Vector3(0, 0, Random.value * 360);

        Bullet go = SpawnBullet(Quaternion.Euler(dir), bulletInterractPrefab);
        go.transform.position = GameManager.singleton.centerOfMap;

        Destroy(gameObject);    
    }
}
