using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//autokills the unit if it is spawned 
[ExecuteInEditMode]
public class autokill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying)
        {
            DestroyImmediate(this.gameObject);
        }
    }
}
