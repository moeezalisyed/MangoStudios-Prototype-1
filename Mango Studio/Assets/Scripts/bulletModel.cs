using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class bulletModel : MonoBehaviour
{
	private float clock;	// to keep track of the time(not used for now)
	private Bullet owner;	// object that created it
	private Material mat;	// material (for texture)
	private float movex;
	private float movey;
	private float speed;
	private float movebuf;
	private float currentx;
	private float currenty;


	public void init(Bullet owner, float x, float y, int angle) {
		this.owner = owner;
		movex = x;
		movey = y;
		currentx = 0;
		currenty = 0;
		movebuf = 0;
		transform.position = new Vector3 (owner.m.transform.position.x, owner.m.transform.position.y, 0);
		name = "BulletModel";
		mat = GetComponent<Renderer> ().material;
		mat.shader = Shader.Find ("Sprites/Default");
		/*if (owner.m.getType () == 1) {
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/rdot");
			mat.color = new Color (3, 3, 3, 1);
		} else if (owner.m.getType () == 2) {
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/bullet2");
			transform.eulerAngles = new Vector3 (0, 0, angle+90);
			mat.color = new Color (1, 1, 1, 1);
		} else if (owner.m.getType () == 3) {
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/bullet3");
			transform.eulerAngles = new Vector3 (0, 0, 90);
			mat.color = new Color (1, 1, 1, 1);
		}*/

		mat.mainTexture = Resources.Load<Texture2D> ("Textures/bullet2");
		mat.color = new Color (1, 1, 1, 1);

		transform.eulerAngles = new Vector3 (0, 0, angle);


	

		//transform.eulerAngles = new Vector3 (0, 0, angle+90);

		mat.SetTextureScale("_MainTex", new Vector2(0.3f, 0.3f));

	}

	void Start(){
		clock = 0f;
		speed = 8;
	}

	void Update(){
		clock += Time.deltaTime;
		transform.Translate (Vector3.up * speed * Time.deltaTime);
		if (owner.m.getType () == 0) {
			//transform.position = new Vector3 (transform.position.x + speed * movex, transform.position.y + speed * movey);
			if (clock > 1.3) {
				destroy ();
			}


		} else if (owner.m.getType () == 2) {
			//transform.position = new Vector3 (transform.position.x + speed * movex, transform.position.y + speed * movey);
			if (clock > 0.9) {
				destroy ();
			}
		} else if (owner.m.getType () == 1) {
			//transform.position = new Vector3 (transform.position.x + speed, transform.position.y + speed *Mathf.Sin(clock*20));
			if (clock > 0.8) {
				destroy ();
			}
		} else if (owner.m.getType () == 3) {
			//speed = 0.1f;
			//transform.position = new Vector3 (transform.position.x + speed * movex, transform.position.y + speed * movey);
			if (clock > 0.6) {
				destroy ();
			}
		} 

	}

	public void destroy(){
		DestroyImmediate (gameObject);
	}

}
