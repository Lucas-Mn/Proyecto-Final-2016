using UnityEngine;
using System.Collections;

public class Weapon_Sword : Weapon {

	[Header("Attack 1")]
	[SerializeField] int attack_1_damage = 5;
	[SerializeField] float attack_1_knockback = 1;
	[SerializeField] float attack_1_speed = 5;
	[SerializeField] float attack_1_range = .5f;
	[SerializeField] float attack_1_recovery = .5f;
	[SerializeField] float attack_1_size = .5f;
	[SerializeField] string attack_1_sprite_path = "Swing_Test";

	[Header("Attack 2")]
	[SerializeField] int attack_2_damage = 3;
	[SerializeField] float attack_2_knockback = .3f;
	[SerializeField] float attack_2_speed = 5.65f;
	[SerializeField] float attack_2_range = .45f;
	[SerializeField] float attack_2_recovery = 1f;
	[SerializeField] float attack_2_size = .3f;
	[SerializeField] string attack_2_sprite_path = "Swing_Test";

	// Use this for initialization
	void Start () {
		Construct ("sword");
		a1=new wpn_atk
			(attack_1_damage, attack_1_knockback, attack_1_speed, 
				attack_1_range, attack_1_recovery, attack_1_size, attack_1_sprite_path, 1);
		a2=new wpn_atk
			(attack_2_damage, attack_2_knockback, attack_2_speed, 
				attack_2_range, attack_2_recovery, attack_2_size, attack_2_sprite_path, 2);
	}

	bool second_attack_ready = false;
	float second_attack_timer;

	[Header("Sword Stuff")]
	[SerializeField] float second_attack_delay = .2f;

	public override void Input2 ()
	{
		if (main.Can_Attack) 
		{
			std_atk (a2);
			second_attack_ready = true;
			second_attack_timer = second_attack_delay;
		}
	}

	void Second_Attack()
	{
		main.Can_Attack = true;
		std_atk (a2);
		second_attack_ready = false;
	}

	void Update()
	{
		second_attack_timer -= Time.deltaTime;
		if (second_attack_ready && second_attack_timer <= 0)
			Second_Attack ();
	}
}
