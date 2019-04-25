using UnityEngine;
using System.Collections;

public class Weapon_Dagger : Weapon {

	[Header("Attack 1")]
	[SerializeField] int attack_1_damage = 2;
	[SerializeField] float attack_1_knockback = .3f;
	[SerializeField] float attack_1_speed = 3;
	[SerializeField] float attack_1_range = .3f;
	[SerializeField] float attack_1_recovery = .25f;
	[SerializeField] float attack_1_size = .2f;
	[SerializeField] string attack_1_sprite_path = "Swing_Test";

	[Header("Attack 2")]
	[SerializeField] int attack_2_damage = 2;
	[SerializeField] float attack_2_knockback = .3f;
	[SerializeField] float attack_2_speed = 7.6f;
	[SerializeField] float attack_2_range = .12f;
	[SerializeField] float attack_2_recovery = .25f;
	[SerializeField] float attack_2_size = .2f;
	[SerializeField] string attack_2_sprite_path = "Swing_Test";

	// Use this for initialization
	void Start () {
		Construct ("dagger");

		a1=new wpn_atk
			(attack_1_damage, attack_1_knockback, attack_1_speed, 
				attack_1_range, attack_1_recovery, attack_1_size, attack_1_sprite_path, 1);
		a2=new wpn_atk
			(attack_2_damage, attack_2_knockback, attack_2_speed, 
				attack_2_range, attack_2_recovery, attack_2_size, attack_2_sprite_path, 2);
	}

	public override void Input2 ()
	{
		return;
	}
}
