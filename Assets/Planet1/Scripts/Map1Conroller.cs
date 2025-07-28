using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1Conroller : MonoBehaviour
{
    
    public MeshRenderer[] borders;
    // Start is called before the first frame update
    void Start()
    {
       foreach(MeshRenderer borer in borders)
        {
            borer.enabled = false;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
