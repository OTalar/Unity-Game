using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : BaseSpell
{


    public override void castSpell(Vector2 direction, float rotationZ, GameObject FireLocation, bool who)
    {
        if (!onCooldown)
        {
            
            controller.GetComponent<Character>().heal(heal);
            putOnCoolDown();
        }


    }
    
}
