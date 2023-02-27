using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISlime : AI_Base
{
    void Start()
    {
        Physics2D.IgnoreLayerCollision(10, 12);
       
        currentHealth = maxHealth;
        rig = this.GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        
        Vector3 direction = player.position - transform.position;



        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

      
        direction.Normalize();
        movement = direction;
        time = time + 1f * Time.deltaTime;
        

        
    }
    private void FixedUpdate()
    {
        CheckIfPlayerInRange();


        if (time >= timeDelay && IsGrounded() && playerInDetectionRange || time >= timeDelay && IsGrounded() && wasHitRecently)
        {
            stopMovement();
            moveCharacterX();
            changeTimeDelay();
            time = 0f;
        }
        else
        {
            //stopMovement();
        }
    }

    void moveCharacterX()
    {
       
        rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        rig.AddForce(Vector2.right * movement.x * moveSpeed, ForceMode2D.Impulse);


    }

    void stopMovement()
    {
        if (IsGrounded())
        {
            rig.velocity = new Vector2(0,rig.velocity.y);
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.6f, 0), Vector2.down, 0.1f);

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
                playerInDetectionRange = true;
            }
            else
            {
               // Debug.Log("fail");
                playerInDetectionRange = false;
            }

        }
        else
        {
            playerInDetectionRange = false;
        }
       
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
   
    }

}
