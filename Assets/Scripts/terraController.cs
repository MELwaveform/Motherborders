using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terraController : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSpeed = 400f;
    public float accelRate = 100f;
    public float maxRotSpeed = 30f;
    public float rotAccelRate = 10f;
    public float xSpeed = 0f;
    public float ySpeed = 0f;
    public float zSpeed = 0f;
    public float yRotSpeed = 0f;
    public float moveSpeed = 100f;
    public float rSpeed=10;
    public float vSpeed=100;

    public Vector3 movement; 
    public Vector3 rotation; 

    public bool isRotating= false;
    public bool isMoving= false;

    public Rigidbody rb;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.maxAngularVelocity=2*Mathf.PI;
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        
        rotation = new Vector3(Input.GetAxis("Rotato"),0,0);
        
        if(Input.GetAxis("Rotato")==0) isRotating=false;
        else isRotating=true;
        if(Input.GetAxis("Vertical")==0&&Input.GetAxis("Vertical")==0) isMoving=false;
        else isMoving=true;


        /*
                ////////////////////////////////////////////////////////////////////////X
        {
            
        //acceleration controller for map camera movement, bound to WASD+QE
        //if neither OR both are pressed, decelerate to velocity = 0;
            if((!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)))
            {
                //determine needed deceleration.
                if(xSpeed>0) xSpeed=Mathf.Clamp(xSpeed-accelRate*Time.deltaTime,0,maxSpeed);
                if(xSpeed<0) xSpeed=Mathf.Clamp(xSpeed+accelRate*Time.deltaTime,-maxSpeed,0);
            }
            //if either is pressed, accelerate in a specific direction.
            else if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                xSpeed=Mathf.Clamp(xSpeed+accelRate*Time.deltaTime,-maxSpeed,maxSpeed);
            }
            else if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                xSpeed=Mathf.Clamp(xSpeed-accelRate*Time.deltaTime,-maxSpeed,maxSpeed);
            }
        }
        ////////////////////////////////////////////////////////////////////////Y
        {
        //acceleration controller for map camera movement, bound to WASD+QE
        //if neither OR both are pressed, decelerate to velocity = 0;
            if((!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)))
            {
                //determine needed deceleration.
                if(zSpeed>0) zSpeed=Mathf.Clamp(zSpeed-accelRate*Time.deltaTime,0,maxSpeed);
                if(zSpeed<0) zSpeed=Mathf.Clamp(zSpeed+accelRate*Time.deltaTime,-maxSpeed,0);
            }
            //if either is pressed, accelerate in a specific direction.
            else if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                zSpeed=Mathf.Clamp(zSpeed+accelRate*Time.deltaTime,-maxSpeed,maxSpeed);
            }
            else if(Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            {
                zSpeed=Mathf.Clamp(zSpeed-accelRate*Time.deltaTime,-maxSpeed,maxSpeed);
            }
        }
        ////////////////////////////////////////////////////////////////////////Y ROTATION
        
        */
        //acceleration controller for map camera movement, bound to WASD+QE
        //if neither OR both are pressed, decelerate to velocity = 0;
        
            if((!Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q)) || (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Q)))
            {
                
                //determine needed deceleration.
                if(yRotSpeed>0) yRotSpeed=Mathf.Clamp(yRotSpeed-rotAccelRate*Time.deltaTime,0,maxRotSpeed);
                if(yRotSpeed<0) yRotSpeed=Mathf.Clamp(yRotSpeed+rotAccelRate*Time.deltaTime,-maxRotSpeed,0);
                
            }
            //if either is pressed, accelerate in a specific direction.
            else if(Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
            {
                yRotSpeed=Mathf.Clamp(yRotSpeed+rotAccelRate*Time.deltaTime,-maxRotSpeed,maxSpeed);
            }
            else if(Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
            {
                yRotSpeed=Mathf.Clamp(yRotSpeed-rotAccelRate*Time.deltaTime,-maxRotSpeed,maxRotSpeed);
            }
        
        yRotSpeed=0;
        if(Input.GetKey(KeyCode.E)) yRotSpeed=45;
        if(Input.GetKey(KeyCode.Q)) yRotSpeed=-45;
        
        transform.position += Time.deltaTime * zSpeed * transform.forward;       
        transform.position += Time.deltaTime * xSpeed * transform.right;   
        transform.position += Time.deltaTime * zSpeed * transform.forward;  
        transform.position += Time.deltaTime * xSpeed * transform.right;     
        
        //transform.Rotate(new Vector3(0,yRotSpeed,0)*Time.deltaTime);
        
    }
    
    void FixedUpdate()
    {
        move(movement, rotation);
    }

    void move(Vector3 v,Vector3 r)
    {
        rb.AddRelativeForce(v*vSpeed);
        Quaternion q = Quaternion.Euler(r*Time.deltaTime*100);
        Debug.Log(q);
        rb.AddForceAtPosition(r*20,new Vector3(0,0,50));
        //rb.MoveRotation(rb.rotation*q);
        //rb.AddRelativeTorque(r*rSpeed*10000);
        //if(!isMoving) rb.velocity=new Vector3(rb.velocity.x*.95f,rb.velocity.y,rb.velocity.z*.95f);
        if(!isRotating) rb.angularVelocity=new Vector3(0,0,0);
        
    }
    
    }

    
