using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject creature1;
    public GameObject creature2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (creature1 == null && creature2 == null)
        {
            Destroy(gameObject);
        }
    }
}
