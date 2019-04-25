using UnityEngine;
using System.Collections;

public class Player_Input : AI {
	
	Mob main;
	public float x, y; //movement axes
	public bool d_auto_attack = false;

	// Use this for initialization
	void Start () {
		main = gameObject.GetComponent<Mob>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Up")) y=1;
		if(Input.GetButtonDown("Down")) y=-1;
		if(Input.GetButtonDown("Right")) x=1;
		if(Input.GetButtonDown("Left")) x=-1;

		if(Input.GetButtonUp("Up")) y=Input.GetButton("Down")? -1:0;
		if(Input.GetButtonUp("Down")) y=Input.GetButton("Up")? 1:0;
		if(Input.GetButtonUp("Right")) x=Input.GetButton("Left")? -1:0;
		if(Input.GetButtonUp("Left")) x=Input.GetButton("Right")? 1:0;

		main.Movement(x, y); 
		main.target
			= Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, 1));

		if (Input.GetButton ("Input1") || d_auto_attack)main.Input1 ();
		if (Input.GetButton ("Input2"))main.Input2 ();
		if (Input.GetButton ("Input3"))main.Input3 ();
	}
}
