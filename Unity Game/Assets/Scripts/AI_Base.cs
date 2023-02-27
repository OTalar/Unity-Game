using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Base : Character
{
    public Transform player;
    public float moveSpeed = 5f;
    public float canMove = 0f;
    public Rigidbody2D rig;
    public Vector2 movement;
    public float jumpForce = 5f;
    public float damage;

    public float time = 0f;
    public float timeDelay = 1f;
    public float timeDelayMin = 1f;
    public float timeDelayMax = 1f;


    public float detectionRange;
    public bool playerInDetectionRange;
    public bool playerMovementStopRange;
    public LayerMask whatIsPlayer;
    public Vector3 direction;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }
    void Start()
    {
        rig = this.GetComponent<Rigidbody2D>();
    }

    public override void takeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
        wasHitRecently = true;
        hitTimeCUrrent = hitTimeRemeber;
        if (currentHealth <= 0)
        {



            Player mainCharacter = FindObjectOfType<Player>();
            mainCharacter.addScore(score);
            Destroy(this.gameObject, 0);
            

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Character>().takeDamage(damage);
        }
    }
    public void changeTimeDelay()
    {
        timeDelay = Random.Range(timeDelayMin, timeDelayMax);
    }
}
