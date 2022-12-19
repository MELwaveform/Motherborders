using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempFriendlyObject : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public GameObject terra;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.AddForce(terra.transform.position-this.transform.position);
        rb.interpolation=RigidbodyInterpolation.Interpolate;
    }
}
