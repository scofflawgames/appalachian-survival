using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReciever : MonoBehaviour
{
    //put in enums for type to match up with sound from damage(e.g, fleshy enemy, wood block, concrete, metal, etc..)
    public GameObject enemyAIHolder;

    public Animator enemyAnim = null;

    public AudioClip[] damageAudio;
    private AudioSource damageAudioSource;

    public static bool isHit = false;

    public int enemyHealth;
    public int enemyMeatAMT;

    private ItemDatabase itemDatabase;
    private Inventory inventory;

    private void Start()
    {
        itemDatabase = FindObjectOfType<ItemDatabase>();
        inventory = FindObjectOfType<Inventory>();
        damageAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        WeaponDamage damageCollider = other.gameObject.GetComponent<WeaponDamage>();

        if (damageCollider != null && !isHit)
        {
            enemyHealth -= damageCollider.weaponDamageAmt;
            if (enemyHealth <= 0)
            {
                inventory.AddItem(itemDatabase.GetItemById(10), enemyMeatAMT);
                Destroy(enemyAIHolder);
            }
            else
            {
                isHit = true;
                enemyAnim.Play("rat_hit");
                StartCoroutine(damageCoolDown(0.5f));
                damageAudioSource.clip = damageAudio[0];
                damageAudioSource.Play();             
            }
        }
        
    }

    IEnumerator damageCoolDown(float time)
    {
        yield return new WaitForSeconds(time);
        isHit = false;
    }

}
