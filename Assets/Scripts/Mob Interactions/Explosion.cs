using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	Rigidbody2D rigid; CircleCollider2D collider;

	// Use this for initialization
	void Awake () {
	rigid = GetComponent<Rigidbody2D>();
	collider = GetComponent<CircleCollider2D>();
	}

	void Start()
	{
		Simple_Animator sa = gameObject.AddComponent<Simple_Animator> ();
		sa.Init ("swing_test");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}