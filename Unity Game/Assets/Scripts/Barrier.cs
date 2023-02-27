using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public float despawnTime;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(0, 9);
        Physics2D.IgnoreLayerCollision(0, 10);
        Destroy(this.gameObject, despawnTime);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("PROJECTTIL");
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject);
        }
    }
}
