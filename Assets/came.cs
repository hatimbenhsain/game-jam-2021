using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class came : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1f+0.1f*Mathf.Cos(2*Time.time), 1f + 0.1f * Mathf.Cos(2*Time.time),1);
    }
}
