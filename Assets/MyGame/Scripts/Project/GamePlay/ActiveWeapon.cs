using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        Primary = 0,
        Secondary = 1,
    }
    public Transform[] weaponSlots;
    public Transform crossHairTarget;
    public Animator rigController;
    
    private RaycastWeapon[] equippedWeapons = new RaycastWeapon[2];
    private int activeWeaponIndex;

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
        var weapon = GetWeapon(activeWeaponIndex);

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

            if(Input.GetKeyDown(KeyCode.X))
            {
                bool isHostered = rigController.GetBool("hoister_weapon"); // set bien ishostered = true để play animation rút súng
                rigController.SetBool("hoister_weapon", !isHostered); // !isHostered set = false để đưa súng về túi
            }
        }
       
    }

    private RaycastWeapon GetWeapon(int index)
    {
        if(index<0 || index > equippedWeapons.Length)
        {
            return null;
        }
        return equippedWeapons[index];
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.weaponSlot;
        var weapon = GetWeapon(weaponSlotIndex);
        if (weapon)
        {
            Destroy(weapon.gameObject); // phải chấm Gameobject nếu k chỉ destroy cái scripts
        }

        weapon = newWeapon;
        weapon.raycastDestination = crossHairTarget;
        weapon.transform.SetParent(weaponSlots[weaponSlotIndex], false);
        rigController.Play("equip_" + weapon.weaponName);

        equippedWeapons[weaponSlotIndex] = weapon;
        activeWeaponIndex = weaponSlotIndex;
    }
}
