using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int boardWidth, boardHeight; // board size init is in Unity editor
    private GameObject playerFolder;// folders for object organization

    private List<Player> players; // list of all placed players
	public Player currentplayer;

    // Beat tracking
    private float clock;
    private float startTime;
    private float BEAT = .5f;
    private int numBeats = 0;
    int playerbeaten = 0;
    int playernum = 0;
	private int playertype;
	public Text HealthText;
	private List<Vector3> shadow;
	private int shadowiterator;
	private Boolean startitr;


    // Level number

    private int level = 99;


    //button locations
    float trayx = 0;
    float traywidth = 0;
    float trayspace = 0;

    // Sound stuff
    public AudioSource music;
    public AudioSource sfx;

    // Music clips
    private AudioClip idle;
    private AudioClip gametrack;
    private AudioClip winmusic;

    // Sound effect clips
    private AudioClip playerDead;
    private AudioClip playerHit;
    private AudioClip click;

    // Use this for initialization
    void Start()
	{
		//set up folder for enemies
		playerFolder = new GameObject ();
		playerFolder.name = "Enemies";
		players = new List<Player> ();
		playertype = 2;
		//addPlayer(0, 1, 0, 0);
		//addPlayer(playertype, 1, -4, -4);
		addPlayer(playertype, 1, -4, -4);
		currentplayer = players [0];
		currentplayer.setCD (0.5f);
		//setHealthText ();
		clock = 0f;
		shadow = new List<Vector3> ();
		shadowiterator = 0;
		startitr = false;

		GameObject bossObject = new GameObject();
		Boss boss = bossObject.AddComponent<Boss>();
		boss.init (this);

	}
        
    // Update is called once per frame
    void Update()
    {
		clock += Time.deltaTime;
	//	shadow.Add (currentplayer.model.transform.localPosition);
		if (Input.GetKey (KeyCode.RightArrow) && currentplayer.transform.position.x < 3) {
			currentplayer.direction = 3;
			currentplayer.transform.eulerAngles = new Vector3 (0, 0, currentplayer.direction * 90);
			currentplayer.transform.Translate (Vector3.up * 4 * Time.deltaTime);
		} 
		if (Input.GetKey (KeyCode.UpArrow) && currentplayer.transform.position.y < 3) {
			currentplayer.direction = 0;
			currentplayer.transform.eulerAngles = new Vector3 (0, 0, currentplayer.direction * 90);
			currentplayer.transform.Translate (Vector3.up * 4 * Time.deltaTime);

		}
		if (Input.GetKey (KeyCode.LeftArrow) && currentplayer.transform.position.x > -8){
			currentplayer.direction = 1;
			currentplayer.transform.eulerAngles = new Vector3 (0, 0, currentplayer.direction * 90);
			currentplayer.transform.Translate (Vector3.up * 4 * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.DownArrow) && currentplayer.transform.position.y > -8) {
			currentplayer.direction = 2;
			currentplayer.transform.eulerAngles = new Vector3 (0, 0, currentplayer.direction * 90);
			currentplayer.transform.Translate (Vector3.up * 4 * Time.deltaTime);
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			currentplayer.shoot();
		}
		//setHealthText ();
		if (currentplayer.getHealth () == 0) {
			currentplayer.destroy();
			players.Remove(currentplayer);
			//startitr = true;

			if (playertype < 4) {
				playertype++;
			}
			addPlayer (playertype, 1, -4, -4);
			currentplayer = players [0];
			if (playertype == 3) {
				currentplayer.setCD (.5f);
			}
		}
//		if (startitr){
//			
//
//
//			print ("we got here!" + shadow[0]);
//
//			enemies [1].model.transform.localPosition = shadow [shadowiterator];
//			shadowiterator++;
//			}
    }

	public void addPlayer(int playerType, int initHealth, int x, int y)
	{
		GameObject playerObject = new GameObject();
		Player player = playerObject.AddComponent<Player>();
		player.transform.parent = playerFolder.transform;
		player.transform.position = new Vector3(x, y, 0);
		player.init(playerType, this);
		players.Add(player);
		playernum++;
		player.name = "Player " + players.Count;
	}

	void setHealthText (){
		HealthText.text = "Health: "+currentplayer.getHealth();
	}



}
