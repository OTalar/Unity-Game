using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiRanged : AI_Base
{


    private void Start()
    {
        currentHealth = maxHealth;
        rig = this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (animator != null)
        {
            animator.SetFloat("Horizontal", Mathf.Abs(rig.velocity.x));
        }

        
        direction = player.position - transform.position;



        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        direction.Normalize();
        movement = direction;
        time = time + 1f * Time.deltaTime;

        if (sr != null)
        {
            FlipCheck();
        }

    }
    private void FixedUpdate()
    {
        CheckIfPlayerInRange();

        if (IsGrounded() && playerInDetectionRange && !playerMovementStopRange || time >= timeDelay && IsGrounded() && wasHitRecently)
        {

            moveCharacterX();

        }
        else 
        {
            stopMovement();
        }

        if (playerInDetectionRange && time >= timeDelay)
        {
            if (!spell1.onCooldown)
            {
                fire();
                time = 0f;
            }
        }
    }

    void moveCharacterX()
    {

        
        //rb.AddForce(Vector2.right * movement.x * moveSpeed, ForceMode2D.Impulse);
        rig.velocity = new Vector2(movement.x * moveSpeed, rig.velocity.y);
        //Debug.Log(rb.velocity);
    }

    void stopMovement()
    {
        rig.velocity = new Vector2(movement.x * moveSpeed * 0, rig.velocity.y);
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -1.1f, 0), Vector2.down, 0.01f);
        return hit.collider != null;

    }


    void CheckIfPlayerInRange()
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, detectionRange, whatIsPlayer);
        if (collision != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                //Debug.Log("works");
                //canMove = 1f;
                playerInDetectionRange = true;
                return;
            }
            else
            {
                //Debug.Log("fail");
                playerInDetectionRange = false;
            }

        }
        else
        {
            //canMove = 0f;
            playerInDetectionRange = false;
        }

        Collider2D collision2 = Physics2D.OverlapCircle(transform.position, detectionRange/3, whatIsPlayer);
        if (collision2 != null)
        {
            if (collision2.gameObject.tag == "Player")
            {
                //Debug.Log("works");
                
                playerMovementStopRange = true;
            }
            else
            {
                //Debug.Log("fail");
                playerMovementStopRange = false;
            }

        }
        else
        {
            
            playerMovementStopRange = false;
        }

    }

    private void fire()
    {
        Vector3 difference = player.transform.position - FireLocation.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        FireLocation.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);


        
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            spell1.setController(this.gameObject);
            spell1.SetDamage();
            spell1.castSpell(direction, rotationZ, FireLocation, false);
        
    }


    private void FlipCheck()
    {
        if (rig.velocity.x > 0)
        {
            sr.flipX = false;
        }
        else if (rig.velocity.x < 0)
        {
            sr.flipX = true;
        }
    }
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange/4);

    }

}
