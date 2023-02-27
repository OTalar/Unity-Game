using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Text spell1Cooldown;
    public Text spell2Cooldown;
    public Text manaText;
    public Text hpText;
    public Character healthObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = "HP " + healthObject.getHealth();
        manaText.text = "MP " + Mathf.Round(healthObject.getCurrentMana());
        if (healthObject.spell1 != null)
        {
            if (healthObject.spell1.getCurrentCooldown() < 0)
            {
                spell1Cooldown.text = healthObject.spell1.getSpellName() + " Cooldown: 0";
            }
            else
            {
                spell1Cooldown.text = healthObject.spell1.getSpellName() + " Cooldown: " + Mathf.Round(healthObject.spell1.getCurrentCooldown());
            }
        }
        else
        {
            spell1Cooldown.text = "";
        }
        if (healthObject.spell2 != null)
        {
            if (healthObject.spell2.getCurrentCooldown() < 0)
            {
                spell2Cooldown.text = healthObject.spell2.getSpellName() + " Cooldown: 0";
            }
            else
            {
                spell2Cooldown.text = healthObject.spell2.getSpellName() + " Cooldown: " + Mathf.Round(healthObject.spell2.getCurrentCooldown());
            }
        }
        else 
        {
            spell2Cooldown.text = "";
        }

    }
}
