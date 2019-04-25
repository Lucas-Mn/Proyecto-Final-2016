using UnityEngine;
using System.Collections;

public class Mob_Pingu : Mob_Boss {

	// Use this for initialization
	void Start () {
		Construct ("Boss/Pingu_the_king", new Vector2(0f, 0f), .4f, 100, .4f, typeof(AI_Pingu));
		attacks = new Attack[3];
		attacks[0] = gameObject.AddComponent<Attack_Charged_Shot>();
		attacks[1] = gameObject.AddComponent<Attack_Charge>();
		Attack_Charged_Shot shot = (Attack_Charged_Shot) attacks[0];
		Attack_Charge charge = (Attack_Charge) attacks[1];
		charge.speed = 3.85f;
		shot.cooldown = 3.5f; shot.Charge_Time = .4f; shot.looped_frames = 2; shot.Delay = .1f;
		shot.damage = 1; shot.knockback = 0; shot.speed = 2.5f; shot.duration = 10f; shot.projectile_name = "Swing_Test";
		shot.size = .5f;
	}
	
	// Update is called once per frame
	void Update () {
		Update_ ();
		Look_At_Target();
	}

	public override void Movement (float x, float y){}
	public override void Input1(){ attack(1); }
	public override void Input2(){ attack(2); }
	public override void Input3(){}
}
