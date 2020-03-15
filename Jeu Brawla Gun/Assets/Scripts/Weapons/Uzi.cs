using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uzi : Weapon
{
    public float nbBullet;

    public override void Interact()
    {
        Destroy(GetComponent<Rigidbody2D>());

        StartCoroutine(ShootInDir());
    }

    IEnumerator ShootInDir()
    {
        for (int i = 0; i < nbBullet; i++)
        {
            Vector3 dir = new Vector3(0, 0, Random.value * 360);

            SpawnBullet(Quaternion.Euler(dir), bulletPrefab);

            yield return new WaitForSeconds(shotDelay);
        }

        Destroy(gameObject);
    }
}
