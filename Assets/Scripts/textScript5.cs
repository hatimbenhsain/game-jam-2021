using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class messageList2
{
    public string[] subMessages;
}

public class textScript5 : MonoBehaviour
{
    public Canvas canvas;
    public Canvas canvas1, canvas2, canvas3;
    public bool inZone;
    public string[] messages;
    public int[] changeSpeaker;
    public int[] changeSpeakerWhich;
    public int currentMessage;

    //portraits
    public GameObject portrait1;
    public GameObject portrait2;
    public GameObject portrait3;

    public GameObject itemUsed = null;
    public GameObject thoughtBubble = null;

    private TwoPlayerActionControl playerActionControl;
    private float movementInput;


    public AudioClip speechSound = null;
    AudioClip typingSound;

    public bool isNpc = true;
    string spokenMessage = "";
    float lettersSpoken = 0f;
    public float textSpeed = 0.6f;
    public bool resolved = false;

    void Awake()
    {
        playerActionControl = new TwoPlayerActionControl();
        canvas = canvas1;
        canvas.enabled = false;
        canvas2.enabled = false;
        canvas3.enabled = false;

        currentMessage = 1;
        int i = 0;

        //typingSound=canvas.GetComponent<AudioSource>().clip;

    }

    void OnEnable()
    {
        playerActionControl.Enable();
    }

    void OnDisable()
    {
        playerActionControl.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        var mouse = Mouse.current;
        for (int i = 0; i < changeSpeaker.Length; i++)
        {
            if (changeSpeaker[i] == currentMessage)
            {
                if (changeSpeakerWhich[i] == 1) //sockolov
                {
                    canvas = canvas2;
                    canvas1.enabled = false;
                    canvas3.enabled = false;
                    canvas.enabled = true;

                    portrait1.transform.SetSiblingIndex(2);
                    portrait2.transform.SetSiblingIndex(4);
                    portrait3.transform.SetSiblingIndex(1);
                }
                else if (changeSpeakerWhich[i] == 0) //sockoko
                {
                    canvas = canvas1;
                    canvas2.enabled = false;
                    canvas3.enabled = false;
                    canvas.enabled = true;

                    portrait1.transform.SetSiblingIndex(4);
                    portrait2.transform.SetSiblingIndex(2);
                    portrait3.transform.SetSiblingIndex(1);
                }
                else //washing machine
                {
                    canvas = canvas3;
                    canvas2.enabled = false;
                    canvas1.enabled = false;
                    canvas.enabled = true;

                    portrait1.transform.SetSiblingIndex(2);
                    portrait2.transform.SetSiblingIndex(1);
                    portrait3.transform.SetSiblingIndex(4);
                }
                changeSpeaker[i] = 0;
                break;
            }
            else if (currentMessage == 1) //washing machine
            {
                canvas = canvas3;
                canvas2.enabled = false;
                canvas1.enabled = false;
                canvas.enabled = true;

                portrait1.transform.SetSiblingIndex(2);
                portrait2.transform.SetSiblingIndex(1);
                portrait3.transform.SetSiblingIndex(4);
            }
        }

        if (mouse.leftButton.wasPressedThisFrame)
        {
            print("pressed");
            if (lettersSpoken >= messages[currentMessage - 1].Length && currentMessage < messages.Length)
            {
                if (canvas.enabled)
                {
                    currentMessage++;
                }

                lettersSpoken = 0;

                if (currentMessage <= messages.Length)
                {
                    canvas.enabled = true;
                }
                else
                {
                    canvas.enabled = false;
                }

            }
            else if (currentMessage < messages.Length)
            {
                lettersSpoken = messages[currentMessage - 1].Length;
            }
            else
            {
                canvas.enabled = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            }
        }

        if (messages.Length > 0 && currentMessage > 0)
        {
            lettersSpoken += textSpeed;
            // if(lettersSpoken<messages[currentMessage-1].Length && !canvas.GetComponent<AudioSource>().isPlaying){
            //     if(speechSound!=null){
            //         canvas.GetComponent<AudioSource>().clip=speechSound;
            //     }else{
            //         canvas.GetComponent<AudioSource>().clip=typingSound;
            //     }
            //     canvas.GetComponent<AudioSource>().Play();
            //     Camera.main.gameObject.GetComponent<AudioSource>().volume=0.5f;
            // }else if(lettersSpoken>messages[currentMessage-1].Length && canvas.GetComponent<AudioSource>().isPlaying){
            //     canvas.GetComponent<AudioSource>().Stop();
            //     Camera.main.gameObject.GetComponent<AudioSource>().volume=1f;
            // }
            spokenMessage = messages[currentMessage - 1].Substring(0, Mathf.Min(messages[currentMessage - 1].Length, (int)Mathf.Ceil(lettersSpoken)));
            (canvas.GetComponentInChildren(typeof(Text)) as Text).text = spokenMessage;
        }

        // if(!canvas.enabled && canvas.GetComponent<AudioSource>().isPlaying){
        //     canvas.GetComponent<AudioSource>().Stop();
        // }


    }

}
