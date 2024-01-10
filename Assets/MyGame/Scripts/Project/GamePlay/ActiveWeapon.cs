﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class ActiveWeapon : MonoBehaviour
{
    public CinemachineFreeLook playerCamera;
    public Transform[] weaponSlots;
    public Transform crossHairTarget;
    public Animator rigController;
    
    private RaycastWeapon[] equippedWeapons = new RaycastWeapon[2];
    private int activeWeaponIndex;
    private bool isHolsterd = false;

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

        if (weapon != null && !isHolsterd)
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
        if (Input.GetKeyDown(KeyCode.X))
        {
            //bool isHostered = rigController.GetBool("hoister_weapon"); // set bien ishostered = true để play animation rút súng
            //rigController.SetBool("hoister_weapon", !isHostered); // !isHostered set = false để đưa súng về túi
            ToggleActiveWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveWeapon(WeaponSlot.Primary);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveWeapon(WeaponSlot.Secondary);
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
       

        weapon = newWeapon;
        weapon.raycastDestination = crossHairTarget;
        weapon.weaponRecoil.playerCamera = playerCamera;
        weapon.transform.SetParent(weaponSlots[weaponSlotIndex], false);
        rigController.Play("equip_" + weapon.weaponName);

        equippedWeapons[weaponSlotIndex] = weapon;
        activeWeaponIndex = weaponSlotIndex;

        SetActiveWeapon(newWeapon.weaponSlot);

    }

    private void ToggleActiveWeapon()
    {
        bool isHoslsterd = rigController.GetBool("holster_weapon");
        if(isHoslsterd)
        {
            StartCoroutine(ActivateWeapon(activeWeaponIndex));
        }
        else
        {
            StartCoroutine(HolsterWeapon(activeWeaponIndex));
        }
    }

    private void SetActiveWeapon(WeaponSlot weaponSlot)
    {
        int holsterIndex = activeWeaponIndex;
        int activateIndex = (int) weaponSlot;

        if(holsterIndex == activateIndex)
        {
            holsterIndex = -1;
        }

        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    }

    private IEnumerator SwitchWeapon(int holsterIndex, int activateIndex)
    {
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));
        activeWeaponIndex = activateIndex;
    }
    private IEnumerator HolsterWeapon(int index)
    {
        isHolsterd = true;
        var weapon = GetWeapon(index);
        if(weapon != null)
        {
            rigController.SetBool("holster_weapon", true);
            yield return new WaitForSeconds(0.1f);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
    }

    private IEnumerator ActivateWeapon(int index)
    {
        var weapon = GetWeapon(index);
        if(weapon != null)
        {
            rigController.SetBool("holster_weapon", false);
            rigController.Play("equip_" + weapon.weaponName);
            yield return new WaitForSeconds(0.1f);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
            isHolsterd = false;
        }
    }
}
