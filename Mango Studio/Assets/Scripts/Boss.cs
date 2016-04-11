using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

	private BossModel model;
	private float speed;
	private GameManager m;
	private float bcooldown;
	private float lcooldown;
	private bool charge;
	private float charging;
	private float chargeSpeed;
	public int bossHealth;

	// Use this for initialization
	public void init (GameManager owner) {
		this.name = "Boss";
		speed = .7f;
		chargeSpeed = 3;
		m = owner;
		this.bossHealth = 100;
		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		model = modelObject.AddComponent<BossModel>();						// Add a marbleModel script to control visuals of the gem.
		model.init(this);

		BoxCollider2D bossbody = gameObject.AddComponent<BoxCollider2D> ();
		bossbody.name = "Boss";
		Rigidbody2D bossRbody = gameObject.AddComponent<Rigidbody2D> ();
		bossRbody.gravityScale = 0;
		bossbody.isTrigger = true;

		transform.localScale = new Vector3 (2, 2, 1);
	}
	
	// Update is called once per frame
	void Update () {
		model.changeTexture (this.bossHealth/10);
		float playerx = m.currentplayer.transform.position.x;
		float playery = m.currentplayer.transform.position.y;
		if ((playery - this.transform.position.y) <= 0 && !charge) {
			float angle = Mathf.Rad2Deg * Mathf.Acos (Mathf.Abs (playery - this.transform.position.y) / Mathf.Sqrt (Mathf.Pow ((playerx - this.transform.position.x), 2) + Mathf.Pow ((playery - this.transform.position.y), 2)));
			float sign = (playerx - this.transform.position.x) / Mathf.Abs (playerx - this.transform.position.x);
			transform.eulerAngles = new Vector3 (0, 0, 180 + (sign * angle));
			transform.Translate (Vector3.up * speed * Time.deltaTime);
		} else if ((playery - this.transform.position.y) > 0 && !charge) {
			float angle = Mathf.Rad2Deg * Mathf.Acos (Mathf.Abs (playery - this.transform.position.y) / Mathf.Sqrt (Mathf.Pow ((playerx - this.transform.position.x), 2) + Mathf.Pow ((playery - this.transform.position.y), 2)));
			float sign = (playerx - this.transform.position.x) / Mathf.Abs (playerx - this.transform.position.x);
			transform.eulerAngles = new Vector3 (0, 0, 0 + (sign * angle * -1));
			transform.Translate (Vector3.up * speed * Time.deltaTime);
		} else {
			transform.Translate (Vector3.up * chargeSpeed * Time.deltaTime);
			charging = charging - Time.deltaTime;

			if (charging <= 0) {
				charge = false;
			}
		}
			

		if ((Mathf.Sqrt (Mathf.Pow ((playerx - this.transform.position.x), 2) + Mathf.Pow ((playery - this.transform.position.y), 2))) > 3) {
			if ((Mathf.Sqrt (Mathf.Pow ((playerx - this.transform.position.x), 2) + Mathf.Pow ((playery - this.transform.position.y), 2))) > 5) {
				if (lcooldown <= 0) {
					FireBeam ();
					lcooldown = 1;
				}
				lcooldown = lcooldown - Time.deltaTime;
			} else if (bcooldown <= 0) {
				FireBullet ();
				bcooldown = .6f;
			} else {
				bcooldown = bcooldown - Time.deltaTime;
			}

		} else if ((Mathf.Sqrt (Mathf.Pow ((playerx - this.transform.position.x), 2) + Mathf.Pow ((playery - this.transform.position.y), 2))) < 3) {
			if (!charge) {
				charge = true;
				charging = .3f;
			}
		}


	}

	void FireBullet(){ 						//I made this take x and y because I was thinking about it and different enemies will need to fire from different parts of their models
		GameObject bulletObject = new GameObject();		
		BossBullet bullet = bulletObject.AddComponent<BossBullet>();
		bullet.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,0);
		bullet.transform.rotation = new Quaternion(this.transform.rotation .x,this.transform.rotation.y,this.transform.rotation.z,this.transform.rotation.w);
	}

	void FireBeam(){ 						//I made this take x and y because I was thinking about it and different enemies will need to fire from different parts of their models
		GameObject beamObject = new GameObject();		
		BossBeam beam = beamObject.AddComponent<BossBeam>();
		beam.init (this);
		beam.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,0);

	}



	public void dealDamage(int damage){
		this.bossHealth -= damage;
		print ("bosshealth: " + bossHealth);
	}

	void OnGUI(){
		if (this.bossHealth > 30) {			
			GUI.color = Color.green;
		} else {
			GUI.color = Color.red;
		}
		GUI.skin.box.alignment = TextAnchor.MiddleLeft;
		GUI.skin.box.fontSize = 25;
		string s = "";

		for (int i = 0; i < this.bossHealth / 10; i++) {
		
			s += "I";

		}

		GUI.Box(new Rect (10, 10, 200, 100), s);

		GUI.color = Color.white;
		GUI.skin.box.fontSize = 12;
		GUI.skin.box.alignment = TextAnchor.MiddleCenter;
	}

	void OnTriggerEnter2D(Collider2D other){
//		//print ("entered inside the boss");
//		int damage = 0;
//		if (other.name == "BulletModel") {
//			print ("entered collider in character bullet");
//			switch(0){
//
//			case 0:
//				damage = 11;
//				break;
//
//			case 1:
//				damage = 8;
//				break;
//
//			case 2:
//				damage = 6;
//				break;
//
//			}
//
//			m.THEBOSS.dealDamage (damage);
//		}
	
	}
}