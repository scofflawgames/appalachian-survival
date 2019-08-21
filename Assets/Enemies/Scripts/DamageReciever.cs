using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReciever : MonoBehaviour
{
    //put in enums for type to match up with sound from damage(e.g, fleshy enemy, wood block, concrete, metal, etc..)

    public AudioClip[] damageAudio;
    private AudioSource damageAudioSource;
    public static bool isHit = false;

    private void Start()
    {
        damageAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        WeaponDamage damageCollider = other.gameObject.GetComponent<WeaponDamage>();

        if (damageCollider != null && !isHit)
        {
            isHit = true;
            StartCoroutine(damageCoolDown(0.5f));
            damageAudioSource.clip = damageAudio[0];
            damageAudioSource.Play();
            print("WeaponDamage has made contact with DamageReciever");
        }
        
    }

    IEnumerator damageCoolDown(float time)
    {
        yield return new WaitForSeconds(time);
        isHit = false;
    }

}
