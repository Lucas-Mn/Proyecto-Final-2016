using UnityEngine;
using System.Collections;

public class Mob_Jon : Mob_Human {

	// Use this for initialization
	void Start () {
		Construct("Jon", new Vector2(0,0.04f), .15f, 5, 100f, typeof(Player_Input));
		speed = 1.5f;
	}

	// Update is called once per frame
	void Update () {
		Human_Update ();
	}
}
