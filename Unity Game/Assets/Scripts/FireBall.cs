using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireBall : BaseSpell
{
   
    public override void castSpell(Vector2 direction, float rotationZ, GameObject FireLocation,bool who)
    {

        // fire the spell
        if (!onCooldown)
        {
            GameObject b = GameObject.Instantiate(spellProjectile) as GameObject;
            b.GetComponent<BasicProjectile>().setWhoFired(who);
            b.GetComponent<BasicProjectile>().setDamage(damage);
            b.transform.position = FireLocation.transform.position;
            b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
            b.GetComponent<Rigidbody2D>().velocity = direction * spellSpeed;
            putOnCoolDown();
        }
    }
   

}
