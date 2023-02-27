using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    private float damage;
    private bool whoFired;
    public bool destoryOnHit = true;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 2);
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void setWhoFired(bool who)
    {
        whoFired = who;
    }

    public void setDamage(float number)
    {
        damage = number;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && whoFired)
        {
            collision.gameObject.GetComponent<Character>().takeDamage(damage);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Player" && !whoFired)
        {
            collision.gameObject.GetComponent<Character>().takeDamage(damage);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            canDestory();
        }
    }

    private void canDestory()
    {
        if (destoryOnHit)
        {
            Destroy(this.gameObject);
        }

    }
}
