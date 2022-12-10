using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class boardCon : MonoBehaviour
{

    public GameObject tri;

    public float spawnRate=2;

    private float nextSpawn=0;

    // Start is called before the first frame update
    async void Start()
    {
        var x = 0;
        for(var i = 0; i < 5; i++)
        {
            for(var j = 0; j < 5; j++)
            {
                x++;
                Debug.Log("Before.");
                var g = Instantiate(tri,new Vector3(i*1.2f,0,j*2.1f),Quaternion.identity);   
                if(x%2==0)
                {
                    g.transform.rotation=Quaternion.Euler(0,180,0);
                }
                await waitTime(.03f);
                Debug.Log("After.");
            }
        }
    }

    // Update is called once per frame
    async void Update()
    {
        if(Time.time>nextSpawn)
        {
            nextSpawn+=spawnRate;
            
        }
        
    }

    private async Task<Exception> waitTime(float t)
    {
        if(!Application.isPlaying) 
        {
            throw new Exception("Bang ding ow");
        }else 
        {
            await Task.Delay(TimeSpan.FromSeconds(Convert.ToDouble(t)));
            return null;
        }
    }
}
