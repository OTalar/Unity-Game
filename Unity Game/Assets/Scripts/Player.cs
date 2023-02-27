using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    //Spell
    public GameObject crosshair;

    
    public float spellSpeed = 60.0f;
    private Vector3 target;


    //movement
    private bool canMove = true;
    public float disableMoveDelay;
    public float moveSpeed;
    public Rigidbody2D rig;
    public float jumpForce;
    //public SpriteRenderer sr;
    public float knockbackForce = 3f;
    public float dashForce;

    public GameObject jumpcheck1, jumpcheck2;
    public GameObject deathScreen;
    public DeathMenuScript deathScreenUi;
    //animation




    //public Transform interactPos;
    public float interactRange;
   private float timeBtwInteract;
    public float startTimeBtwInteract;

    //public Transform attackPos;
    //public float attackRange;
    //private float timeBtwAttack;
    //public float startTimeBtwAttack;

    //public int damage;

    private void Start()
    {
        //Cursor.visible = false;
       
    }


    private void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");
        if (canMove)
        {
            rig.velocity = new Vector2(xInput * moveSpeed, rig.velocity.y);
        }
    }


    public override void takeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
        if (currentHealth <= 0)
        {
            deathScreenShow("You Haved Failed to Escape");
            
        }
    }


    // Update is called once per frame
    void Update()
    {


            animator.SetFloat("horizontal", Mathf.Abs(Input.GetAxis("Horizontal")));
        

        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshair.transform.position = new Vector2(target.x, target.y);

        Vector3 difference = target - FireLocation.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        FireLocation.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        if (spell1 != null && Input.GetMouseButtonDown(0) && currentMana >= spell1.getManaCost() && !spell1.onCooldown || spell1 != null && Input.GetMouseButton(0) && currentMana >= spell1.getManaCost() && !spell1.onCooldown)
        {

            spell1.setController(this.gameObject);
            spell1.SetDamage();
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            //fireSpell(direction, rotationZ);
            spell1.castSpell(direction, rotationZ, FireLocation, true);
            currentMana -= spell1.getManaCost();


        }
        else if (spell2 != null && Input.GetMouseButtonDown(1) && currentMana >= spell2.getManaCost() && !spell2.onCooldown || spell2 != null && Input.GetMouseButton(1) && currentMana >= spell2.getManaCost() && !spell2.onCooldown)
        {


            spell2.setController(this.gameObject);
            spell2.SetDamage();
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            //fireSpell(direction, rotationZ);
            spell2.castSpell(direction, rotationZ, FireLocation, true);
            currentMana -= spell2.getManaCost();

        }
        else if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            if (currentMana < maxMana)
            {
                currentMana += manaRegen * Time.deltaTime;
                if (currentMana > maxMana)
                {
                    currentMana = maxMana;
                }
            }
        }



        //Attack();
        //Interact();
        Jump();
        FlipCheck();
        interact();



    }






    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(jumpcheck1.transform.position + new Vector3(0, 0, 0), Vector2.down, 0.2f);
        return hit.collider != null;
    }
    bool IsGrounded2()
    {
        RaycastHit2D hit = Physics2D.Raycast(jumpcheck2.transform.position + new Vector3(0, 0, 0), Vector2.down, 0.2f);
        return hit.collider != null;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && IsGrounded() || Input.GetKeyDown(KeyCode.W) && IsGrounded2())
        {
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
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

    void knockBack(Collision2D collision)
    {
        if (collision.gameObject.transform.position.x > transform.position.x)
        {
            //Debug.Log("right");
            rig.AddForce(Vector2.right * knockbackForce*-1 , ForceMode2D.Impulse);
            
        }

        else
        {
            //Debug.Log("left");
            rig.AddForce(Vector2.right * knockbackForce, ForceMode2D.Impulse);
            
        }
        StartCoroutine(DisablePlayerMovement(disableMoveDelay));
    }

    IEnumerator DisablePlayerMovement(float time)
    {
        
        canMove = false;
        yield return new WaitForSeconds(time);
        
        canMove = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {
            knockBack(collision);
        }
    }

    private void interact()
    { if (Input.GetKeyDown(KeyCode.E) && timeBtwInteract <= 0)
        {
            Collider2D[] interactArea = Physics2D.OverlapCircleAll(this.gameObject.transform.position, interactRange);
            for (int i = 0; i < interactArea.Length; i++)
            {
                if (interactArea[i].CompareTag("Interact"))
                {
                    interactArea[i].GetComponent<Interact>().interact();
                }
            }
            timeBtwInteract = startTimeBtwInteract;
        }
            else if (timeBtwInteract > 0)

            {
                timeBtwInteract -= Time.deltaTime;
            }

    }

    public void setSpell1(BaseSpell spell)
    {
        spell1 = spell;
    }
    public void setSpell2(BaseSpell spell)
    {
        spell2 = spell;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(jumpcheck1.transform.position, 0.1f);
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(attackPos.position, attackRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(jumpcheck2.transform.position, 0.1f);
    }

    public void addScore(float amount)
    {
        score += amount;
    }
    public void deathScreenShow(string text)
    {
        deathScreen.SetActive(true);
        deathScreenUi.setMainText(text);

    }
}
