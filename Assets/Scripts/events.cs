using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class events : MonoBehaviour
{
    bool sqTouch = false;
    bool sockTouch = false;
    public Transform hehe;
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "sock")
        {
            sockTouch = true;
        }
        Debug.Log("touched " + collision.gameObject.tag);
        if (collision.gameObject.tag == "obj")
        {
            sqTouch = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("touched "+collision.gameObject.tag);
        if (collision.gameObject.tag == "obj")
        {
            sqTouch = true;
        }
    }
    private void OnCollisionExit2D(Collider2D collision)
    {
        //Debug.Log("stopped touching "+collision.gameObject.name);
        sqTouch = false;
    }

    IEnumerator flip_screen()
    {
        hehe = gameObject.transform.GetChild(0);
        var rotationVector = hehe.transform.rotation.eulerAngles;
        rotationVector.z = 180;
        hehe.transform.rotation = Quaternion.Euler(rotationVector);
        yield return new WaitForSeconds(5f);
        rotationVector.z = 0;
        hehe.transform.rotation = Quaternion.Euler(rotationVector);

    }

    void Update()
    {
        if (sockTouch == true)
        {
            Debug.Log("WIN");
            sockTouch = false;
        }
        if (sqTouch==true)
        {
            //StopCoroutine("flip_screen");
            //StartCoroutine("flip_screen");
            sqTouch = false;
        }
    }

}
