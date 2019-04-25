using UnityEngine;
using System.Collections;

public class Simple_Animator : MonoBehaviour {

	bool active; 
	public SpriteRenderer ren;
	Animation anim;

	public bool Active
	{ get { return active; } 
		set { if(active!=value){anim.Reset(); active = value;} } }
	// Use this for initialization
	void Start () {
		ren = gameObject.GetComponent<SpriteRenderer> ();
		Init ("swing_test");
	}
	
	// Update is called once per frame
	void Update () {
		if (active)
			anim.Animate ();
	}

	public void Init(string path)
	{
		anim = new Animation (ren, "Sprites/" + path);
		active = true;
	}
}
