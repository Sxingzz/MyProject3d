using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public RaycastWeapon weaponPrefabs;

    private void OnTriggerEnter(Collider other)
    {
       ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if(activeWeapon != null)
        {
            RaycastWeapon newWeapon = Instantiate(weaponPrefabs);
            activeWeapon.Equip(newWeapon);
        }
    }
}
