using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public RaycastWeapon weaponPrefabs;

    private void OnTriggerEnter(Collider other)
    {
        //player
       ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if(activeWeapon != null)
        {
            RaycastWeapon newWeapon = Instantiate(weaponPrefabs);
            activeWeapon.Equip(newWeapon);
            Destroy(gameObject);
        }

        //AI
        AIWeapon aiWeapon = other.gameObject.GetComponent<AIWeapon>();
        if (aiWeapon != null)
        {
            RaycastWeapon newWeapon = Instantiate(weaponPrefabs);
            aiWeapon.EquipWeapon(newWeapon);
            Destroy(gameObject);
        }
    }
}
