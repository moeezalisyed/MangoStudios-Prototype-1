
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour {

	private bulletModel model;		// The model object.
	public float movex;
	private float movey;
	public playerModel m;		// A pointer to the manager (not needed here, but potentially useful in general).

	public void init(playerModel m, float x, float y, int angle) {
		this.m = m;
		this.movex = x;
		this.movey = y;
		this.name = "BulletModel";
		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		BoxCollider2D playerbody = gameObject.AddComponent<BoxCollider2D> ();
		Rigidbody2D playerRbody = gameObject.AddComponent<Rigidbody2D> ();
		playerRbody.gravityScale = 0;
		playerbody.name = "BulletModel";
		playerbody.isTrigger = true;
		model = modelObject.AddComponent<bulletModel>();						// Add an enemyModel script to control visuals of the gem.

		model.init(this, x, y, angle);						
	}

	void OnTriggerEnter2D(Collider2D other){
		
		int damage = 0;
		if (other.name == "Boss") {
			print ("entered collider in character bullet");
			switch(m.owner.playerType){

			case 0:
				damage = 11;
				break;

			case 1:
				damage = 8;
				break;

			case 2:
				damage = 6;
				break;

			}

			m.owner.m.THEBOSS.dealDamage (damage);
			Destroy (this.gameObject);
		}
	}
		
}

