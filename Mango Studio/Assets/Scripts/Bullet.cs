
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

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		model = modelObject.AddComponent<bulletModel>();						// Add an enemyModel script to control visuals of the gem.
		model.init(this, x, y, angle);						
	}
		
}

