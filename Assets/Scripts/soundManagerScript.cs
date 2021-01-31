using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManagerScript : MonoBehaviour
{
	public AudioSource washSound;
	public AudioSource bgMusic;

	public float lerpValue1=0.01f;
	public float lerpValue2=0.5f;

	private bool washing=false;

	public bool win=false;
	public AudioSource winSound;
    // Start is called before the first frame update
    void Start()
    {
        bgMusic=GetComponent<AudioSource>();
        washSound.volume=0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(washing){
        	washSound.volume=Mathf.Lerp(washSound.volume,1f,lerpValue1);
        	bgMusic.volume=Mathf.Lerp(bgMusic.volume,0.05f,lerpValue1);
        }else if(!washing && washSound.isPlaying){
        	washSound.volume=Mathf.Lerp(washSound.volume,0f,lerpValue2);
        	bgMusic.volume=Mathf.Lerp(bgMusic.volume,1f,lerpValue2);
        	if(washSound.volume<=0.01f) washSound.Stop();
        }
        if(win && !winSound.isPlaying){
        	winSound.Play();
        	bgMusic.Stop();
        	washSound.Stop();
        }
    }

    public void Wash(){
    	washing=true;
    	washSound.Play();
    }

    public void StopWash(){
    	washing=false;
    }
}
