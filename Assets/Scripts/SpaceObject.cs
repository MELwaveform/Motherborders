using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SpaceObject : MonoBehaviour
{
    // Start is called before the first frame update
    public int SID;
    public space space;

    public bool isActive;
    public bool stageMode;

    public float threat;
    public List<GameObject> fReg;
    public List<GameObject> eReg;
    public List<GameObject> nReg;
    public GameObject maptop;
    public GameObject gametop;

    public List<invEntity> ePool;

    public List<invEntity> fList = new List<invEntity> { };

    public boardCon bc;
    void Start()
    {
        //Debug.Log("Instantiating space " + SID);
        threat = Random.Range(0,100);
        //Debug.Log("Threat has been set to " + threat);
        //add the maptop.
        maptop = Instantiate(space.mapTopModel);
        maptop.transform.parent=transform;
        maptop.transform.localPosition=new Vector3(0,25,0);
        //if(SID%2==0)mt.transform.Rotate(new Vector3(0,0,180));
        maptop.GetComponent<MeshRenderer>().material.color=Color.red;
        

        gametop = Instantiate(space.gameTopModel);
        gametop.transform.parent=transform;
        gametop.transform.localPosition=new Vector3(0,25,0);
        //if(SID%2==0)mt.transform.Rotate(new Vector3(0,0,180));
        gametop.GetComponent<MeshRenderer>().material.color=Color.red;
        gametop.SetActive(false);








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


    //perform all space actions needed to accomodate stage mode.
    public void enableStageMode()
    {  
        //enable the gametop and disable the maptop.
        gametop.SetActive(true);
        maptop.SetActive(false);   
        //spawn initial enemy lineup.
        Debug.Log("Spawning enemies...");
        determineAndSpawnEnemies();

        //spawn in all your allies. this involves removing InvEntities from the BC fListToSend and adding them to the spaceobject fList.
        Debug.Log("Spawning allies...");
        spawnAllies();
        
    }

    //based on threat level, determine what enemy should be spawned.
    public void determineAndSpawnEnemies()
    {
        threat = 122;
        Debug.Log("Determining and spawning enemies.");
        //get a list of threat costs from all ePool entities. 

        ePool = space.ePool.OrderByDescending(o=>o.threatCost).ToList();
       
        //Debug.Log(ePool[0].threatCost + " " + ePool[1].threatCost +" " + ePool[2].threatCost);
        float remainingThreat = threat;
        bool isPieceSpawnable = true;
        int count=0;
        while(isPieceSpawnable && count < 100)
        {
            //after every spawn, determine what units are spawnable with the remaining threat 
            List<invEntity> spawnable = new List<invEntity> {};
            for(var i = 0; i < ePool.Count;i++)
            {
                //dont consider a unit spawnable if its too expensive.
                if(remainingThreat-ePool[i].threatCost>0) spawnable.Add(ePool[i]);
                //else Debug.Log("Piece to expensive to spawn: " + ePool[i].threatCost);
            }

            if(!(spawnable.Count>0)) isPieceSpawnable=false;
            else{
                int rand = Random.Range(0,spawnable.Count);
                //Debug.Log("Spawning unit:" + spawnable[rand].name);
                remainingThreat-=spawnable[rand].threatCost;
                //spawn a random unit that's spawnable.
                //at a random location
                float x = Random.Range(-50,50);
                float y = Random.Range(0,50);
                float z = Random.Range(-50,50);
                
                spawnInvEntity(spawnable[rand],new Vector3(x,y,z));

            }
            //Debug.Log("Remaining threat: " + remainingThreat);
            count++;
        }
        Debug.Log("Done spawning enemies.");
    }

    //undo all the actions from enable stage mode and prepare for threshold control mode.
    public void disableStageMode()
    {

        //unspawn all enemies and add them back to the BC fList
        foreach(Transform child in gametop.transform)
        {
            var go = child.gameObject;
            var eo = child.GetComponent<EntityObject>();
            if(eo.team==1)
            {   
                bc.fList.Add(eo.ie);
                //DEBUG ONLY
                
                Destroy(child.gameObject);    
            }
        }

        //unspawn everything else on the GT (debug only)
        foreach(Transform child in gametop.transform)
        {
            Destroy(child.gameObject);
        }

        gametop.SetActive(false);
        maptop.SetActive(true);
    }

    //spawn allies from the fList. this also removes them from the fList, as they become gameobjects. reinstantiate them later.
    public void spawnAllies()
    {
        foreach(invEntity ie in fList.ToList())
        {
            float x = Random.Range(-50,50);
            float y = Random.Range(0,50);
            float z = Random.Range(-50,50);
            spawnInvEntity(ie,new Vector3(x,y,z));
            fList.Remove(ie);
        }
    }
    //
    public void spawnInvEntity(invEntity target, Vector3 v)
    {
        var go =  Instantiate(target.go);
        go.transform.parent=gametop.transform;
        go.transform.localPosition=v;

        //instantiate properties of eo.
        EntityObject eo = go.AddComponent<EntityObject>();
        eo.team = target.team;
        eo.ie = target;

        //if the entity is a friendly unit, give it a temporary controller.
        if(target.team==1)
        {
            go.GetComponent<MeshRenderer>().material.color=new Color(0,200,0);
            var tfo = go.AddComponent<tempFriendlyObject>();
            tfo.terra=bc.terra;
        }

        
    }
}
