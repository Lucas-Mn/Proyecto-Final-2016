using UnityEngine;
using System.Collections;

public class Mob_Pingu_The_King : Mob {

	void Start()
	{
		Construct ("pingu_the_king", new Vector2 (0, .2f), .3f, 10, 100, typeof(Player_Input));
	}

	void Update()
	{
		Look_At_Target ();
		Update_ ();
	}

	public override void Input1 ()	{}
	public override void Input2 ()	{}
	public override void Input3 ()	{}
	public override void Movement (float x, float y)
	{std_Movement (x, y, speed);}
}
