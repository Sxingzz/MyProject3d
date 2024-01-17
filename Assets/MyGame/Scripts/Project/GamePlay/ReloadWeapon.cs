using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadWeapon : MonoBehaviour
{
    public Animator rigController;
    public WeaponAnimationEvent animationEvent;
    public ActiveWeapon activeWeapon;
    public Transform leftHand;
    public bool isReloading;

    private GameObject magazineHand;


    // Start is called before the first frame update
    void Start()
    {
        animationEvent.WeaponAnimEvent.AddListener(OnAnimationEvent);
        //if (ListenerManager.HasInstance)
        //{
        //    ListenerManager.Instance.Register(ListenType.RELOAD_ANIMATION_EVENT, OnReloadAnimationEvent);
        //}
    }

    //private void OnDestroy()
    //{
    //    if (ListenerManager.HasInstance)
    //    {
    //        ListenerManager.Instance.Unregister(ListenType.RELOAD_ANIMATION_EVENT, OnReloadAnimationEvent);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        RaycastWeapon weapon = activeWeapon.GetAciveWeapon();
        if (weapon != null)
        {
            if (weapon.EmptyAmmo()) return;

            if (Input.GetKeyDown(KeyCode.R) || weapon.CanReload())
            {
                if (weapon.CanReload())
                {
                    isReloading = true;
                    rigController.SetTrigger("reload_weapon");
                }
            }
        }
    }
    void OnAnimationEvent(string eventName)
    {
        Debug.Log($"Weapon Reload Event: {eventName}");
        switch (eventName)
        {
            case "detach_magazine":
                DetachMagazine();
                break;
            case "drop_magazine":
                DropMagazine();
                break;
            case "refill_magazine":
                RefillMagazine();
                break;
            case "attach_magazine":
                AttachMagazine();
                break;
        }
    }
    private void DetachMagazine()
    {
        RaycastWeapon weapon = activeWeapon.GetAciveWeapon();
        magazineHand = Instantiate(weapon.magazine, leftHand, true);
        weapon.magazine.SetActive(false);
    }
    
    private void DropMagazine()
    {
        GameObject droppedMagazine = Instantiate(magazineHand, magazineHand.transform.position, magazineHand.transform.rotation);
        droppedMagazine.transform.localScale = Vector3.one;
        droppedMagazine.AddComponent<Rigidbody>();
        droppedMagazine.AddComponent<BoxCollider>();
        magazineHand.SetActive(false);
    }
    private void RefillMagazine()
    {
        magazineHand.SetActive(true);

    }
    private void AttachMagazine()
    {
        RaycastWeapon weapon = activeWeapon.GetAciveWeapon();
        weapon.magazine.SetActive(true);
        Destroy(magazineHand);
        weapon.RefillAmmo();
        rigController.ResetTrigger("reload_weapon");
        isReloading = false;
    }

    //void OnReloadAnimationEvent(object value)
    //{
    //    if(value != null)
    //    {
    //        if(value is string eventName)
    //        {
    //            Debug.Log($"Weapon Reload Event By Observer: {eventName}");
    //        }
    //    }
    //}
}
