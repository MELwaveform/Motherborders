using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    // Start is called before the first frame update
    public int SID;
    public space space;

    public bool isActive;
    public bool stageMode;
    public GameObject maptop;

    public GameObject gametop;

    public GameObject mt;
    public GameObject gt;
    void Start()
    {
        
        //add the maptop.
        mt = Instantiate(space.mapTopModel);
        mt.transform.parent=transform;
        mt.transform.localPosition=new Vector3(0,25,0);
        //if(SID%2==0)mt.transform.Rotate(new Vector3(0,0,180));
        mt.GetComponent<MeshRenderer>().material.color=Color.red;
        maptop = mt;
        maptop.SetActive(false);

        gametop = Instantiate(space.gameTopModel);
        gametop.transform.parent=transform;
        gametop.transform.localPosition=new Vector3(0,25,0);
        //if(SID%2==0)mt.transform.Rotate(new Vector3(0,0,180));
        gametop.GetComponent<MeshRenderer>().material.color=Color.red;
        








        //rotate the entire object after spawning.
        if(SID%2==0)transform.localRotation *= Quaternion.Euler(0,180,0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //perform stuff for active and inactive piece states
    public void updateActive()
    {
            GetComponent<MeshRenderer>().material.color=Color.blue;
            GetComponent<SpaceObject>().maptop.GetComponent<MeshRenderer>().material.color=Color.blue;    
    }
    public void updateInactive()
    {
        GetComponent<MeshRenderer>().material.color=Color.red;
        GetComponent<SpaceObject>().maptop.GetComponent<MeshRenderer>().material.color=Color.red;
    }

    public void enableStageMode()
    {

    }
}
