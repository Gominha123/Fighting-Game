using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private InputHandler inputActions;
    [SerializeField] private PlayerCombat playerCombat;
    public Weapon[] weapons = new Weapon[2];
    public float currentWeapon;
    public static int weaponAnimLayer;

    public bool unarmed;

    private void Start()
    {
        if (!unarmed)
        {
            int i = 0;
            foreach (Transform weapon in transform)
            {
                weapons[i] = weapon.GetComponent<Weapon>();
                i++;
            }
            SelectWeapon();
        }
    }

    public void OnPrimaryWeapon(InputValue value)
    {
        currentWeapon = 0;
        SelectWeapon();
    }
    public void OnSecondaryWeapon()
    {
        currentWeapon = 1;
        SelectWeapon();
    }

    public void SelectWeapon()
    {
        if (playerCombat.IsAttacking())
        {
            int i = 0;
            foreach (Weapon weapon in weapons)
            {
                if (i == currentWeapon)
                {
                    weapons[i].gameObject.SetActive(true);
                    playerCombat.SetWeapon(weapons[i]);
                    weaponAnimLayer = (int)weapon.animLayer;
                }
                else
                {
                    weapons[i].gameObject.SetActive(false);
                }
                i++;
            }
        }
    }

    public static int GetCurrentAnimator()
    {
        return weaponAnimLayer;
    }
}
