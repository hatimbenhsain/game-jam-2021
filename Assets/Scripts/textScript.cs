using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

 public class messageList
 {
     public string[] subMessages;
 }

public class textScript : MonoBehaviour
{
	public Canvas canvas;
    public Canvas canvas1, canvas2;
	public bool inZone; 
	public string[] messages;
	public int[] changeSpeaker;
	public int currentMessage;
	public GameObject portrait1;
    public GameObject portrait2;
    public GameObject itemUsed=null;
    public GameObject thoughtBubble=null;

     private TwoPlayerActionControl playerActionControl;
     private float movementInput;


    public AudioClip speechSound=null;
    AudioClip typingSound;

    public bool isNpc=true;
    string spokenMessage="";
    float lettersSpoken=0f;
    public float textSpeed=0.2f;
    public bool resolved=false;

    void Awake()
    {
        playerActionControl = new TwoPlayerActionControl();
        canvas=canvas1;
        canvas.enabled=true;
        canvas2.enabled=false;
        currentMessage=1;
        int i=0;

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

    public void UpdateText(){
        var mouse=Mouse.current;
        if(mouse.leftButton.wasPressedThisFrame) print("pressed");
        for(int i=0;i<changeSpeaker.Length;i++){
            if(changeSpeaker[i]==currentMessage){
                if(canvas==canvas1){
                    canvas=canvas2;
                    canvas1.enabled=false;
                    canvas.enabled=true;
                    portrait1.transform.SetSiblingIndex(1);
                    portrait2.transform.SetSiblingIndex(3);
                }else{
                    canvas=canvas1;
                    canvas2.enabled=false;
                    canvas.enabled=true;
                    portrait1.transform.SetSiblingIndex(3);
                    portrait2.transform.SetSiblingIndex(1);
                }
                changeSpeaker[i]=0;
                break;
            }
        }

        if(mouse.leftButton.wasPressedThisFrame){
        	if(lettersSpoken>=messages[currentMessage-1].Length && currentMessage<messages.Length){
            	currentMessage++;
                lettersSpoken=0;

            	if(currentMessage<=messages.Length){
            		canvas.enabled=true;
            	}
            	else{
            		canvas.enabled=false;
            	}
            	
            }else if(currentMessage<messages.Length){
                lettersSpoken=messages[currentMessage-1].Length;
            }else{
                canvas.enabled=false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1, LoadSceneMode.Single);
            }
        }

        if(messages.Length>0 && currentMessage>0){
            lettersSpoken+=textSpeed;
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
            spokenMessage=messages[currentMessage-1].Substring(0,Mathf.Min(messages[currentMessage-1].Length,(int)Mathf.Ceil(lettersSpoken)));
        	(canvas.GetComponentInChildren(typeof(Text)) as Text).text=spokenMessage;
        }

        // if(!canvas.enabled && canvas.GetComponent<AudioSource>().isPlaying){
        //     canvas.GetComponent<AudioSource>().Stop();
        // }


    }

}
