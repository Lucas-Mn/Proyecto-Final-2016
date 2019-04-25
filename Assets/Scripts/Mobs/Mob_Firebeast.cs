using UnityEngine;
using System.Collections;

public class Mob_Firebeast : Mob {

	Attack_Charge charge;

	void Start () 
	{
		Construct ("firebeast", new Vector2(0, 0.2f), .3f, 10, 1, typeof(AI_Firebeast));
		charge = gameObject.AddComponent<Attack_Charge> ();
		charge.speed = 3;
	}

	void Update () {
		Look_At_Target ();
		Update_();
	}

	public override void Input1()
	{
		if (charge.Input ())
			Play_Anim (Mob_Animator.ANIMATIONS.input_1);
	} 
	public override void Input2(){}
	public override void Input3(){}

	public override void Movement(float x, float y)
	{std_Movement (x, y, speed);}

}