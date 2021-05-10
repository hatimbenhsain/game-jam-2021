using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hover : MonoBehaviour
{
    Vector3 increment;
    public float height = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _vec = transform.localPosition;
        _vec.y = height * Mathf.Cos(2 * Time.time);
        transform.localPosition = _vec;
        /*increment = new Vector3(0, height*Mathf.Cos(2*Time.time), 0);
        transform.localPosition = increment;*/
    }
}
