﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zRegulator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=new Vector3(transform.position.x,transform.position.y,GetComponent<Collider2D>().bounds.min.y-10f);
    }
}
