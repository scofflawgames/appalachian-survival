using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    //REFACTORING POSSIBLY CHANGE MY ITEMID SYSTEM TO ENUMS

    [Header("Public Stuff")]
    public GameObject[] equippable;
    public static int equippableItemID;

    public Animator fpsArmsAnim;

    private void Start()
    {
        fpsArmsAnim = GetComponent<Animator>();
    }

    public void playerAnimation()
    {
        //fpsArmsAnim = GetComponent<Animator>();
        fpsArmsAnim.Play("WeaponSwitch");
    }

    public void equipItem()
    {

        if (equippableItemID == 1)
        {
            equippable[0].SetActive(true);
        }
        else
        {
            equippable[0].SetActive(false);
        }
    }

    public void GetAnimatorComponent()
    {
        fpsArmsAnim = GetComponent<Animator>();
    }

}
