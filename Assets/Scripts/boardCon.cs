using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using System;
using System.Threading.Tasks;
using Cinemachine;
public class boardCon : MonoBehaviour
{

    public space triKai;

    public float xTriSize=200;
    public float yTriSize=200;
    public GameObject tri;
    public float triSpacingFactor=1.1f;
    private float nextSpawn=0;
    public CinemachineVirtualCamera mapCam;
    public CinemachineVirtualCamera terraCam;
    public GameObject spaceCon;


    public bool initComplete = false;
    public Camera mCam;
    public LayerMask spaceLayer;

    public GameObject terra;
    //if false, the game is in map mode.
    public bool isStageMode;
    public GameObject selectedSpace = null;

    public List<invEntity> fList;

    public List<invEntity> fListDebug;

    public List<invEntity> fListToSend = new List<invEntity> { };

    public GameObject mapCL;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        

        
        var x = 0;
        for(var i = 0; i < 5; i++)
        {
            for(var j = 0; j < 5; j++)
            {
                //instantiate the spaces.
                //Instantiate the models of the triangular bases themselves. These prefabs contain the 
                x++;
                //Debug.Log("Instantiating: " + x);
                //var g = Instantiate(triKai.baseModel,new Vector3(i*xTriSize*.5f*triSpacingFactor,0,j*yTriSize*triSpacingFactor), Quaternion.identity);   
                var g = Instantiate(triKai.baseModel);
                g.transform.localPosition = new Vector3(i*xTriSize*.5f*triSpacingFactor,0,j*yTriSize*triSpacingFactor);
                SpaceObject so =  g.GetComponent<SpaceObject>();
                //pass in variables from controller GO
                so.SID = x;
                so.space=triKai;
                so.bc=this;
                g.transform.parent=spaceCon.transform;

                
                
                //g.GetComponent<MeshRenderer>().material=triKai.mat;
                g.GetComponent<MeshRenderer>().material.color=Color.red;
                //g.transform.rotation=Quaternion.Euler(90,0,0);
                if(x%2==0)
                {
                    //g.transform.rotation=Quaternion.Euler(0,180,0);
                }
                yield return StartCoroutine(waiter(.03f));
            }
        }

        //reset the inventity list for debug.
        //peform fList debug ops.
        //fList = new List<invEntity> { };
        foreach(invEntity ie in fListDebug)
        {
            var iek = ScriptableObject.Instantiate(ie);
            Debug.Log("IE name is: " + iek.name);
            fList.Add(iek);
        }
        

        initComplete=true;
    }


    IEnumerator waiter(float f)
    {
        yield return new WaitForSeconds(f);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Time.time>nextSpawn)
        {
            nextSpawn+=3;
            Debug.Log(nextSpawn);
            if(nextSpawn%2==0) 
            {
                terraCam.Priority=100;
                mapCam.Priority=0;
            } else{
                
                terraCam.Priority=0;
                mapCam.Priority=100;
            }
            
        }
        */

        //only do this if init is done.
        if(initComplete)
        {
            //ray controller for space selection
            if(Input.GetMouseButtonDown(0) && !isStageMode)
            {
                Vector3 mPos = Input.mousePosition;
                Ray ray = mCam.ScreenPointToRay(mPos);
                RaycastHit hit;
                int LM = LayerMask.GetMask("spaces");
                if(Physics.Raycast(ray,out hit,Mathf.Infinity,spaceLayer))
                {
                    GameObject g = hit.transform.gameObject;
                    Debug.Log("Clicked on " + g.GetComponent<SpaceObject>().SID);
                    setSelectedSpace(g);
                }
            }
            //debug swap mode
            if(selectedSpace!=null && Input.GetKeyDown(KeyCode.Space) && !isStageMode)
            {
                enableStageMode(selectedSpace);
            }else if(selectedSpace!=null && Input.GetKeyDown(KeyCode.Space) && isStageMode)
            {
                disableStageMode(selectedSpace);
            }
        }
        
    }

    public void setSelectedSpace(GameObject s)
    {
        //only unassign the active space if it exists.
        if(selectedSpace!=null)
        {
            selectedSpace.GetComponent<SpaceObject>().updateInactive();
            selectedSpace=null;
        }
        selectedSpace = s;
        selectedSpace.GetComponent<SpaceObject>().updateActive();
    }





    //perform all functions necessary to set up and sync board controller and spaces states to enter stage mode, which is the mode in which the FPS minigame is played.
    public void enableStageMode(GameObject space)
    {
        
        Debug.Log("Enabling Stage Mode");

        //Debug ONLY
        foreach(invEntity ie in fList)
        {
            ie.team=1;
            fListToSend.Add(ie);
        }
        ///////////
        isStageMode=true;
        SpaceObject so = space.GetComponent<SpaceObject>();
        //remove InvEntities from BC and send to spaceobject
        foreach(invEntity ie in fListToSend.ToList())
        {
            so.fList.Add(ie);
            fList.Remove(ie);
            fListToSend.Remove(ie);
        }
        //move and enabled Terra.
        terra.transform.position = new Vector3(selectedSpace.transform.position.x,selectedSpace.transform.position.y+100,selectedSpace.transform.position.z);
        terra.SetActive(true);
        setCamera(2);
        mapCL.SetActive(false);
        //hand off stage enable to the space itself.
        space.GetComponent<SpaceObject>().enableStageMode();
        Debug.Log("Done with BC ESM");
    }

    
    //undo all things from enable stage mode.
    public void disableStageMode(GameObject space)
    {
        mapCL.SetActive(true);
        space.GetComponent<SpaceObject>().disableStageMode();

        Debug.Log("Disabling Stage Mode");
        isStageMode=false;

        
        //disable terra.
        terra.SetActive(false);
        
        setCamera(1);
        
        
    }
    //set the current active vCam.

    private void setCamera(int x)
    {
        switch(x)
        {
            case 1:
            mapCam.Priority=100;
            terraCam.Priority=0;
            break;
            case 2:
            mapCam.Priority=0;
            terraCam.Priority=100;
            break;
        }
    }
}