using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon[] weapons;

    public float spawnDelay = 3f;
    public int maxWeapon = 4;

    public Transform startSpawnWeaponPos, endSpawnWeaponPos;

    void Start()
    {
        InvokeRepeating("SpawnWeapon", 0, spawnDelay);
    }

    private void SpawnWeapon()
    {
        var weaponsOnMap = FindObjectsOfType<Weapon>();
        var weaponsActive = weaponsOnMap.ToList().FindAll(weapon => weapon.GetComponent<Collider2D>().enabled);

        if (weaponsActive != null && weaponsActive.Count < maxWeapon)
        {
            Vector3 pos = Vector3.Lerp(startSpawnWeaponPos.position, endSpawnWeaponPos.position, Random.value);

            pos.z = 0;

            var index = Random.Range(0, weapons.Length);

            GameObject newWeapon = Instantiate(weapons[index].gameObject, pos, Quaternion.identity);
            newWeapon.AddComponent(typeof(FallingWeapon));
        }
    }
}
