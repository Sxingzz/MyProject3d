using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActiveWeapon : MonoBehaviour
{
    public Transform weaponParent;
    public Transform crossHairTarget;
    public Animator rigController;
    
    private RaycastWeapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existingWeapon)
        {
            Equip(existingWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                weapon.StartFiring();
            }

            if (weapon.isFiring) // hàm này khi giữ chuột thì nó bắn liên tục
            {
                weapon.UpdateFiring(Time.deltaTime);
            }
            weapon.UpdateBullets(Time.deltaTime);

            if (Input.GetButtonUp("Fire1"))
            {
                weapon.StopFiring();
            }
        }
       
    }
    public void Equip(RaycastWeapon newWeapon)
    {
        if (weapon)
        {
            Destroy(weapon.gameObject); // phải chấm Gameobject nếu k chỉ destroy cái scripts
        }

        weapon = newWeapon;
        weapon.raycastDestination = crossHairTarget;
        weapon.transform.SetParent(weaponParent, false);
        rigController.Play("equip_" + weapon.weaponName);
        
    }
}
