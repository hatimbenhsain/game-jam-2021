using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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


	private bool CRrunning=false;

	private float alpha=0.5f;
	private Color c;

    // Start is called before the first frame update
    void Start()
    {
        washImage=washingMachine.GetComponent<Image>();
        
        washImage.enabled=false;

        rect=washingMachine.GetComponent<RectTransform>();
        rect.localScale=new Vector3(0f,0f,0f);

        keySprites=new Sprite[8];
        for(int i=0;i<8;i++){
        	keySprites[i]=playerKeys[i].sprite;
        }

        c=new Color(1f,1f,1f,alpha);

    }

    // Update is called once per frame
    void Update()
    {

    	for(int i=0;i<8;i++){
    		playerKeys[i].color=c;
    	}
        var keyboard=Keyboard.current;
        if(keyboard.spaceKey.wasPressedThisFrame && !CRrunning) doingTheWash();
        if(keyboard.tKey.wasPressedThisFrame) print("key pressed");

        for(int i=0;i<8;i++){
        	print(keys[i]);
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
    }

    void doingTheWash(){
    	StartCoroutine(machineAnimation(animationTime));
    	//change level layout
    	RandomizeKeys();
    }
    
    IEnumerator machineAnimation(float waitTime){
    	CRrunning=true;
    	washImage.enabled=true;
    	rect.localScale=Vector3.zero;
    	var scale=rect.localScale;
    	for(float i=0f;i<=waitTime*2/3;i+=Time.deltaTime){
    		scale=Vector3.Lerp(rect.localScale,new Vector3(maxSize,maxSize,maxSize),growthRate);
    		rect.localScale=scale;
    		yield return new WaitForSeconds(Time.deltaTime);
    	}
    	for(float i=0f;i<=waitTime/3;i+=Time.deltaTime){
    		scale=Vector3.Lerp(rect.localScale,Vector3.zero,growthRate*10);
    		rect.localScale=scale;
    		yield return new WaitForSeconds(Time.deltaTime);
    	}
    	washImage.enabled=false;
    	CRrunning=false;
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