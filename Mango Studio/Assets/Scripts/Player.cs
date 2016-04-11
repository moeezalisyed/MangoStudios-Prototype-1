
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public playerModel model;		// The model object.
	private int playerType;
	//private int initHealth;
	public GameManager m;		// A pointer to the manager (not needed here, but potentially useful in general).
	public int direction = 0;

	public void init(int playerType, GameManager m) {
		this.playerType = playerType;
		//this.initHealth = initHealth;
		this.m = m;

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		BoxCollider2D playerbody = gameObject.AddComponent<BoxCollider2D> ();
		Rigidbody2D playerRbody = gameObject.AddComponent<Rigidbody2D> ();
		playerRbody.gravityScale = 0;
		playerbody.isTrigger = true;
		model = modelObject.AddComponent<playerModel>();						// Add an playerModel script to control visuals of the gem.
		model.init(playerType, this);						
	}

	public void move(int x, int y){
		this.model.transform.eulerAngles = new Vector3 (0, 0, 90 * this.direction);
		model.move (x, y);
	}

	public int getHealth(){
		return model.getHealth();
	}

	public void damage(){
		model.damage ();
	}

	public void
	 destroy(){
		model.damage();
	}

	public void shoot(){
		model.shoot ();
	}

	public void setCD(float a){
		model.setCD (a);
	}

	public int getType(){
		return model.getType();
	}

	void OnTriggerEnter2D(Collider2D other){
		print ("entered collider on player");
		if (other.name == "Boss") {
			this.destroy ();
		}
		if (other.name == "BossBullet") {
			this.destroy ();
		}
		if (other.name == "BossBeam") {
			this.destroy ();
		}
	}
}

