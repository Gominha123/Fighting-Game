using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private InputHandler inputActions;
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private Weapon[] weapons = new Weapon[2];
    float previousWeapon;

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

    private void Update()
    {
        if (previousWeapon != inputActions.SelectedWeapon && playerCombat.IsAttacking())
        {
            SelectWeapon();
        }
    }

    public void SelectWeapon()
    {
        previousWeapon = inputActions.SelectedWeapon;
        int i = 0;
        foreach (Weapon weapon in weapons)
        {
            if (i == inputActions.SelectedWeapon)
            {
                weapons[i].gameObject.SetActive(true);
                playerCombat.SetWeapon(weapons[i]);
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
            }
            i++;
        }
    }
}
