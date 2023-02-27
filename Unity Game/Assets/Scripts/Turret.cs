using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : AI_Base
{
    public Transform Target;
    public bool targetInDetectionRange;
    public bool targetIsPlayer;

    GameObject target;

    void Start()
    {
        currentHealth = maxHealth;
        Physics2D.IgnoreLayerCollision(11,11);
        Physics2D.IgnoreLayerCollision(11, 9);
        Physics2D.IgnoreLayerCollision(11, 10);
    }


    // Update is called once per frame
    void Update()
    {

        time = time + 1f * Time.deltaTime;



    }
    private void FixedUpdate()
    {
        CheckIfTargetInRange();

        if (targetInDetectionRange && time >= timeDelay)
        {
            //if (!spell1.onCooldown)
           // {
                
                fire();
                time = 0f;
           // }
        }
    }







    void CheckIfTargetInRange()
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, detectionRange, whatIsPlayer);
        if (collision != null)
        {
            if (collision.gameObject.tag == "Player" && targetIsPlayer)
            {
                
                target = collision.gameObject;
                targetInDetectionRange = true;
            }
            else if (collision.gameObject.tag == "Enemy" && !targetIsPlayer)
            {
                target = collision.gameObject;
                targetInDetectionRange = true;
            }
            else
            {
                //Debug.Log("fail");
                targetInDetectionRange = false;
                
            }

        }
        else
        {

            targetInDetectionRange = false;
        }




    }

    private void fire()
    {
        Vector3 difference = target.transform.position - FireLocation.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        FireLocation.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);



        float distance = difference.magnitude;
        Vector2 direction = difference / distance;
        direction.Normalize();
        spell1.setController(this.gameObject);
        spell1.SetDamage();
        if (targetIsPlayer)
        {
            spell1.castSpell(direction, rotationZ, FireLocation, false);
        }
        else 
        {
            spell1.castSpell(direction, rotationZ, FireLocation, true);
        }
        

    }
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange / 4);

    }
}
