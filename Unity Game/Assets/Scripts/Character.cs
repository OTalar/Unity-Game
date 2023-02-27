using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{

    public float maxHealth;
    public float currentHealth;

    public float maxMana;
    public float currentMana;
    public bool usesMana;
    public float manaRegen;

    public bool wasHitRecently;
    public float hitTimeRemeber = 5f;
    public float hitTimeCUrrent;
    public float elementalDamage;
    public float holyDamage;
    public float voidDamage;


    public GameObject FireLocation;
    public BaseSpell spell1;
    public BaseSpell spell2;


    public Animator animator;
    public Sprite character;
    public SpriteRenderer sr;

    public float score;







    void Start()
    {
        currentHealth = maxHealth;
    }

    public float getHealth()
    {
        return currentHealth;
    }
    public virtual void takeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
        wasHitRecently = true;
        hitTimeCUrrent = hitTimeRemeber;
        if (currentHealth <= 0)
        {
            if (this.gameObject.tag == "Player")
            {
                SceneManager.LoadScene("MainLevel");
            }
            else
            {
                
                Destroy(this.gameObject, 0);
            }

        }
    }
    public void heal(float healAmout)
    {
        currentHealth = currentHealth + healAmout;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public float getElementalDamage()
    {
        return elementalDamage;
    }
    public float getHolyDamage()
    {
        return holyDamage;
    }
    public float getVoidDamage()
    {
        return voidDamage;
    }

    public void increaseElementalDamage(float increaseBy)
    {
        elementalDamage = elementalDamage + increaseBy;
    }
    public void increaseHolyDamage(float increaseBy)
    {
        holyDamage = holyDamage + increaseBy;
    }

    public void increaseVoidDamage(float increaseBy)
    {
        voidDamage = voidDamage + increaseBy;
    }

    public void checkIfHit()
    {
        if (hitTimeCUrrent! < 0)
        {
            hitTimeCUrrent -= Time.deltaTime;
        }
        else 
        {
            wasHitRecently = false;
        }
    }
    public float getCurrentMana()
    {
        return currentMana;
    }







}
