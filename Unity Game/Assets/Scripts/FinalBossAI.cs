using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossAI : AI_Base
{
   public float moveDistance;
    private Vector3 point1, point2;
    bool moveLeft;
   public float spell1Cooldown,spell1CurrentCooldown;
    bool spell1OnCooldown = false;
   public  float spell2Cooldown, spell2CurrentCooldown;
    bool spell2OnCooldown = false;
    public int phase = 1;
    bool phase4Attack;
  public  float phase4TimeBetweenSwitch, phase4TimeLeft;

   public  float phase3MaxTime;


    private void Start()
    {
        rig = this.GetComponent<Rigidbody2D>();
        point1 = new Vector3(gameObject.transform.position.x - moveDistance, gameObject.transform.position.y, gameObject.transform.position.z);
        point2 = new Vector3(gameObject.transform.position.x + moveDistance, gameObject.transform.position.y, gameObject.transform.position.z);
    }
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }




    private void FixedUpdate()
    {
        movementLeft();

        if (playerInDetectionRange)
        {
            if (phase == 1)
            {
                if (!spell1OnCooldown)
                {
                    castSpell1();
                }
            }
            else if (phase == 2)
            {

                if (!spell2OnCooldown)
                {
                    castSpell2();
                }
            }
            else if (phase == 3)
            {
                phase3TimeCheck();
                if (!spell1OnCooldown)
                {
                    castSpell1();
                }
            }
            else
            {
                phase4changeAttacks();
                if (phase4Attack)
                {
                    if (!spell2OnCooldown)
                    {
                        castSpell2();
                    }
                }
                else if (!phase4Attack)
                {
                    if (!spell1OnCooldown)
                    {
                        castSpell1();
                    }
                }

            }
        }


        
    }
    private void Update()
    {
        CheckIfPlayerInRange();
        spell1IsOnCooldown();
        spell2IsOnCooldown();
    }


    public override void takeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
        wasHitRecently = true;
        hitTimeCUrrent = hitTimeRemeber;
        if (currentHealth <= maxHealth * 0.5 && phase == 1)
        {
            phase = 2;
        }
        else if (currentHealth <= maxHealth * 0.25 && phase == 2)
        {
            spell1Cooldown = 0.05f;
            phase = 3;
        }

        if (currentHealth <= 0)
        {
            Player mainCharacter = FindObjectOfType<Player>();
            mainCharacter.addScore(score);
            mainCharacter.deathScreenShow("You Have Escaped the Dungeon");
            Destroy(this.gameObject, 0);
        }
    }



    void phase4changeAttacks()
    {
        phase4TimeLeft -= Time.deltaTime;
        if (phase4TimeLeft <= 0)
        {
            if (phase4Attack == false)
            {
                phase4Attack = true;
            }
            else
            {
                phase4Attack = false;
            }
            phase4TimeLeft = phase4TimeBetweenSwitch;
        }
    }
    void phase3TimeCheck()
    {
        phase3MaxTime -= Time.deltaTime;
        if (phase3MaxTime <= 0)
        {
            spell1Cooldown = 0.15f;
            spell2Cooldown = 0.7f;
            phase = 4;
        }
    }
    void movementLeft()
    {
        
        if (moveLeft)
        {
            transform.position = new Vector3(gameObject.transform.position.x - (moveSpeed * Time.deltaTime), gameObject.transform.position.y, gameObject.transform.position.z);
            if (transform.position.x <= point1.x)
            {
                moveLeft = false;
            }
        }
        else
        {
            transform.position = new Vector3(gameObject.transform.position.x + (moveSpeed * Time.deltaTime), gameObject.transform.position.y, gameObject.transform.position.z);
            if (transform.position.x >= point2.x)
            {
                moveLeft = true;
            }
        }
    }
    void castSpell1()
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
        spell1CurrentCooldown = spell1Cooldown;
        spell1OnCooldown = true;
    }
    void castSpell2()
    {
        Vector3 difference = player.transform.position - FireLocation.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        FireLocation.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        float distance = difference.magnitude;
        Vector2 direction = difference / distance;
        direction.Normalize();
        spell2.setController(this.gameObject);
        spell2.SetDamage();
        spell2.castSpell(direction, rotationZ, FireLocation, false);
        spell2CurrentCooldown = spell2Cooldown;
        spell2OnCooldown = true;
    }


    void spell1IsOnCooldown()
    {
        if (spell1OnCooldown)
        {
            spell1CurrentCooldown -= Time.deltaTime;
            if (spell1CurrentCooldown <= 0)
            {
                spell1OnCooldown = false;
            }
        }
    }

    void spell2IsOnCooldown()
    {
        if (spell2OnCooldown)
        {
            spell2CurrentCooldown -= Time.deltaTime;
            if (spell2CurrentCooldown <= 0)
            {
                spell2OnCooldown = false;
            }
        }
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


        private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(gameObject.transform.position.x - moveDistance, gameObject.transform.position.y, gameObject.transform.position.z), new Vector3(gameObject.transform.position.x + moveDistance, gameObject.transform.position.y, gameObject.transform.position.z));
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }


}
