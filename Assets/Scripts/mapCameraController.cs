using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapCameraController : MonoBehaviour
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
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
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
        
        transform.Rotate(new Vector3(0,yRotSpeed,0)*Time.deltaTime);
        /*
        float xNewPos = transform.position.x + xSpeed*Time.deltaTime; 
        float yNewPos = transform.position.y + ySpeed*Time.deltaTime; 
        float zNewPos = transform.position.z + zSpeed*Time.deltaTime;
        float yNewRot = transform.rotation.y + yRotSpeed*Time.deltaTime; 
        transform.position = new Vector3(xNewPos,yNewPos,zNewPos);
        transform.Rotate(new Vector3(0,yRotSpeed,0)*Time.deltaTime);
        */
        /*
        transform.
        if(Input.GetKey(KeyCode.W)) transform.position += Time.deltaTime * moveSpeed * transform.forward;       
        if(Input.GetKey(KeyCode.A)) transform.position += Time.deltaTime * moveSpeed * transform.right*-1;   
        if(Input.GetKey(KeyCode.S)) transform.position += Time.deltaTime * moveSpeed * transform.forward*-1;  
        if(Input.GetKey(KeyCode.D)) transform.position += Time.deltaTime * moveSpeed * transform.right;  
        if(Input.GetKey(KeyCode.Q)) transform.rotate
        if(Input.GetKey(KeyCode.E)) transform.position += Time.deltaTime * moveSpeed * transform.forward; 
        */
    }
}
