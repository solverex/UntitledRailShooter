using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class weaponswitch : MonoBehaviour
{
    [SerializeField] int currentweapon = 1;
    [SerializeField] InputAction Weaponswitcher;
    [SerializeField] TMP_Text weapontext;

    void OnEnable()
    {
        Weaponswitcher.Enable();
    }

    void OnDisable()
    {
        Weaponswitcher.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        print(transform.childCount);
    }

    // Update is called once per frame
    void Update()
    {
        SetWeaponActive();
        PSWI();

        if (currentweapon == 0)
        {
            weapontext.text = "Rapid-Fire Blaster";
        }
        if (currentweapon == 1)
        {
            weapontext.text = "Explosive Rocket";
        }
    }

    private void PSWI()
    {
        float switchinput = Weaponswitcher.ReadValue<float>();
        if (switchinput > 0)
        {
            print(switchinput);
            currentweapon++;
        }
        else if (switchinput < 0)
        {
            print(switchinput);
            currentweapon--;
        }

        if (currentweapon >= transform.childCount)
        {
            currentweapon = 0;
        }
        else if (currentweapon < 0)
        {
            currentweapon = transform.childCount - 1;
        }
    }

    private void SetWeaponActive()
    {
        int Weaponindex = 0;

        foreach (Transform weapon in transform)
        {
            if (Weaponindex == currentweapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else if (Weaponindex != currentweapon)
            {
                weapon.gameObject.SetActive(false);
            }
            Weaponindex++;
        }
    }
}
