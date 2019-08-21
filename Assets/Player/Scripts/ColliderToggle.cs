using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderToggle : MonoBehaviour
{
    public SphereCollider weaponCollider;

    public void ColliderToggleON()
    {
        weaponCollider.enabled = true;
    }

    public void ColliderToggleOFF()
    {
        weaponCollider.enabled = false;
    }

}
