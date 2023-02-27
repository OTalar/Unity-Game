using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpell : BaseSpell
{
    private GameObject turret1, turret2;
    public GameObject turret;
    private int cycle;
    // Start is called before the first frame update


    public override void castSpell(Vector2 direction, float rotationZ, GameObject FireLocation, bool who)
    {
        
        if (turret1 == null)
        {
            
            turret1 = GameObject.Instantiate(turret) as GameObject;
            turret1.transform.position = new Vector3(controller.transform.position.x, controller.transform.position.y, 0);
            turret1.GetComponent<Character>().increaseElementalDamage(controller.GetComponentInParent<Character>().getElementalDamage());
            turret1.GetComponent<Character>().increaseHolyDamage(controller.GetComponentInParent<Character>().getHolyDamage());
            turret1.GetComponent<Character>().increaseVoidDamage(controller.GetComponentInParent<Character>().getVoidDamage());
            cycle = 2;
        }
        else if (turret2 == null)
        {
            turret2 = GameObject.Instantiate(turret) as GameObject;
            turret2.transform.position = new Vector3(controller.transform.position.x , controller.transform.position.y , 0);
            turret2.GetComponent<Character>().increaseElementalDamage(controller.GetComponentInParent<Character>().getElementalDamage());
            turret2.GetComponent<Character>().increaseHolyDamage(controller.GetComponentInParent<Character>().getHolyDamage());
            turret2.GetComponent<Character>().increaseVoidDamage(controller.GetComponentInParent<Character>().getVoidDamage());
            cycle = 2;

        }
        else if (cycle == 1)
        {
            Destroy(turret1);
            turret1 = GameObject.Instantiate(turret) as GameObject;
            turret1.transform.position = new Vector3(controller.transform.position.x , controller.transform.position.y , 0);
            turret1.GetComponent<Character>().increaseElementalDamage(controller.GetComponentInParent<Character>().getElementalDamage());
            turret1.GetComponent<Character>().increaseHolyDamage(controller.GetComponentInParent<Character>().getHolyDamage());
            turret1.GetComponent<Character>().increaseVoidDamage(controller.GetComponentInParent<Character>().getVoidDamage());
            cycle = 2;

        }
        else if (cycle == 2)
        {
            Destroy(turret2);
            turret2 = GameObject.Instantiate(turret) as GameObject;
            turret2.transform.position = new Vector3(controller.transform.position.x , controller.transform.position.y , 0);
            turret2.GetComponent<Character>().increaseElementalDamage(controller.GetComponent<Character>().getElementalDamage());
            turret2.GetComponent<Character>().increaseHolyDamage(controller.GetComponent<Character>().getHolyDamage());
            turret2.GetComponent<Character>().increaseVoidDamage(controller.GetComponent<Character>().getVoidDamage());
            cycle = 1;
        }

        putOnCoolDown();
    }
}
