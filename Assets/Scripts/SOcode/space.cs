using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Space", menuName = "MB/New Space")]
public class space : ScriptableObject
{
    

    public GameObject baseModel;
    public Material mat;

    public GameObject cube;


    public GameObject mapTopModel;
    public GameObject gameTopModel;

    public List<invEntity> ePool;
}
