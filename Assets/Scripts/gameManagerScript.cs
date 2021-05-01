using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class gameManagerScript : MonoBehaviour
{
	public PlayerController sock1, sock2;
	public GameObject washingMachine;
	public float animationTime=1f;
	public float growthRate=0.5f;
	public float maxSize=7f;

	private Image washImage;
	private RectTransform rect;

	private string[] keys={"w","s","a","d","upArrow","downArrow","leftArrow","rightArrow"};
	public Image[] playerKeys;
	public Sprite[] keySprites;
	public Sprite questionSprite;


	private bool CRrunning=false;

	private float alpha=0.5f;
	private Color c;

	public float timeLimit=30f;
	public float preWashTime=5f;
	public soundManagerScript soundManager;
	public float currentTime;
	float startTime;

	public float shakeMagnitude=1f;

	public Animator timer;

	public bool victory=false;
	public GameObject victoryStuff;
    public GameObject path;

    // Start is called before the first frame update
    void Start()
    {
    	soundManager=FindObjectOfType<soundManagerScript>();
    	startTime=Time.time;
        washImage=washingMachine.GetComponent<Image>();
        
        washImage.enabled=false;

        rect=washingMachine.GetComponent<RectTransform>();
        rect.localScale=new Vector3(0f,0f,0f);

        keySprites=new Sprite[8];
        for(int i=0;i<8;i++){
        	keySprites[i]=playerKeys[i].sprite;
        }

        c=new Color(1f,1f,1f,alpha);
        timer.speed=1/timeLimit;

    }

    // Update is called once per frame
    void Update()
    {
    	currentTime=Time.time-startTime;

    	if(!victory){
	    	if(Time.time>startTime+timeLimit-preWashTime && !CRrunning){
	    		doingTheWash();
	    	}

	    	for(int i=0;i<8;i++){
	    		playerKeys[i].color=c;
	    	}
	        var keyboard=Keyboard.current;
	        // if(keyboard.spaceKey.wasPressedThisFrame && !CRrunning) doingTheWash();
	        // if(keyboard.tKey.wasPressedThisFrame) print("key pressed");

	        for(int i=0;i<8;i++){
	        	//print(keys[i]);
	        	if(keys[i].Length>1){
	        		if(keys[i]=="upArrow" && keyboard.upArrowKey.isPressed){
	        			playerKeys[i].color=Color.white;
	        		}else if(keys[i]=="downArrow" && keyboard.downArrowKey.isPressed){
	        			playerKeys[i].color=Color.white;
	        		}else if(keys[i]=="leftArrow" && keyboard.leftArrowKey.isPressed){
	        			playerKeys[i].color=Color.white;
	        		}else if(keys[i]=="rightArrow" && keyboard.rightArrowKey.isPressed){
	        			playerKeys[i].color=Color.white;
	        		}
	        	}else if(keyboard.FindKeyOnCurrentKeyboardLayout(keys[i]).isPressed){
	        		playerKeys[i].color=Color.white;
	        	}
	        }
	   	}else{
	   		victoryStuff.GetComponent<Animator>().SetBool("victory",true);
	   		soundManager.win=true;
            StartCoroutine(nextScene());
	   	}
    }

    void doingTheWash(){
    	StartCoroutine(machineAnimation(animationTime));

    	//change level layout
    	
    }
    
    IEnumerator nextScene(){
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1, LoadSceneMode.Single);

    }

    IEnumerator machineAnimation(float waitTime){
    	CRrunning=true;
    	soundManager.Wash();
    	StartCoroutine(sock1.Shake(preWashTime,shakeMagnitude/10f));
    	StartCoroutine(sock2.Shake(preWashTime,shakeMagnitude/10f));
    	yield return new WaitForSeconds(preWashTime);
    	StartCoroutine(sock1.Shake(animationTime,shakeMagnitude));
    	StartCoroutine(sock2.Shake(animationTime,shakeMagnitude));
    	washImage.enabled=true;
    	rect.localScale=Vector3.zero;
    	var scale=rect.localScale;
    	sock1.loss=true;
    	sock2.loss=true;
    	yield return new WaitForSeconds(waitTime*5/6);

    	for(float i=0f;i<=waitTime*5/6;i+=Time.deltaTime){
    		scale=Vector3.Lerp(rect.localScale,new Vector3(maxSize,maxSize,maxSize),growthRate);
    		rect.localScale=scale;
    		yield return new WaitForSeconds(Time.deltaTime);
    	}
    	RandomizeKeys();

        //remake level
        //delete everything with tag floorTile obj out
        // GameObject[] temp = GameObject.FindGameObjectsWithTag("floorTile");
        // for (int x = 0; x < temp.Length; x++)

        // {

        //     Destroy(temp[x]);

        // }
        // GameObject[] temp1 = GameObject.FindGameObjectsWithTag("obj");
        // for (int x = 0; x < temp1.Length; x++)

        // {

        //     Destroy(temp1[x]);

        // }
        // GameObject[] temp2 = GameObject.FindGameObjectsWithTag("out");
        // for (int x = 0; x < temp2.Length; x++)

        // {

        //     Destroy(temp2[x]);

        // }
        // //pathmaker return box collider
        // GameObject[] temp3 = GameObject.FindGameObjectsWithTag("pathmaker");

        // for (int i = 0; i < temp3.Length; i++)

        // {

        //     temp3[i].GetComponent<BoxCollider2D>().enabled = true;

        // }

        // //re-run pathmaker skript

        // Debug.Log(path.GetComponent<pathManager>().end);

        // Destroy(path.GetComponent<pathManager>()); //toggle this script to re-invoke it

        // path.AddComponent<pathManager>();

        // Debug.Log(path.GetComponent<pathManager>().end);

        // path.GetComponent<pathManager>().end = false;

        // for(float i=0f;i<=waitTime/6;i+=Time.deltaTime){

        // 	scale=Vector3.Lerp(rect.localScale,Vector3.zero,growthRate*10);

        // 	rect.localScale=scale;

        // 	yield return new WaitForSeconds(Time.deltaTime);

        // }

        washImage.enabled=false;
    	CRrunning=false;
    	soundManager.StopWash();
    	startTime=Time.time;
    	timer.Play("restart");
    	timer.speed=1f;
    	StartCoroutine(sock1.Shake(1f,shakeMagnitude/10f));
    	StartCoroutine(sock2.Shake(1f,shakeMagnitude/10f));
    	yield return new WaitForSeconds(1f);
    	sock1.loss=false;
    	sock2.loss=false;
    	timer.speed=1/timeLimit;
    	startTime=Time.time;
    }

    void RandomizeKeys(){
    	Shuffle();
    	sock1.RandomizeKeys(keys);
    	sock2.RandomizeKeys(keys);
    }

   	public void Shuffle() { //https://answers.unity.com/questions/1189736/im-trying-to-shuffle-an-arrays-order.html
         string tempString;
         Sprite tempSprite;
         List<int> indexes=new List<int>();
         for (int k = 0; k < keys.Length; k++){
         	indexes.Add(k);
         }
         for (int k = 0; k < keys.Length; k++) {
             int l = Random.Range(0, indexes.Count);
             int rnd=indexes[l];
             tempString = keys[rnd];
             keys[rnd] = keys[k];
             keys[k] = tempString;
             tempSprite = keySprites[rnd];
             keySprites[rnd] = keySprites[k];
             keySprites[k] = tempSprite;
             playerKeys[k].sprite=keySprites[k];
             indexes.RemoveAt(0);
         }
     }


}
