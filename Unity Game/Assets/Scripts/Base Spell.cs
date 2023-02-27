using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseSpell: MonoBehaviour
{
    public GameObject controller;
    public float manaCost;
    public float baseDamage;
    public float damage;
    public int damageType;
    public float baseHeal;
    public float heal;
    public float spellSpeed = 60;
    public GameObject spellProjectile;
    public float spellCooldown;
    public float currentCooldown;
    public bool onCooldown = false;
    public string spellName;

    private void Update()
    {
        cooldown();
    }
    public virtual void castSpell(Vector2 direction, float rotationZ, GameObject FireLocation,bool who)
    { 
       
    }
     
    public void SetDamage()
    {
        switch (damageType)
        {
            case 3:
                damage = baseDamage * (1 + (controller.GetComponent<Character>().getVoidDamage() / 100));
                break;
            case 2:
                damage = baseDamage * (1 + (controller.GetComponent<Character>().getHolyDamage() / 100));
                heal = baseHeal * (1 + (controller.GetComponent<Character>().getHolyDamage() / 100));
                break;
            case 1:
                damage = baseDamage * (1 + (controller.GetComponent<Character>().getElementalDamage() / 100));
                break;
            default:
                break;
        }
    }
    public void setController(GameObject controllerObject)
    {
        controller = controllerObject;
    }

    public float getManaCost()
    {
        return manaCost;
    }
    void cooldown()
    {
        if (onCooldown)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0)
            {
                onCooldown = false;
            }
        }
    }
    public void putOnCoolDown()
    {
        currentCooldown = spellCooldown;
        onCooldown = true;
      
    }

    public string getSpellName()
    {
        return spellName;
    }
    public float getCurrentCooldown()
    {
        return currentCooldown;
    }


}
