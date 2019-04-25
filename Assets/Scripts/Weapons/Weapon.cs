using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	///Identification
	string name; public string Name{get{return name;}}

	Sprite sprite;
	protected Mob_Human main;

	public wpn_atk a1, a2;

	public virtual void Input1()
	{std_atk(a1);}
	public virtual void Input2()
	{std_atk(a2);}

	protected void std_atk(wpn_atk a)
	{
		if (main.Can_Attack) 
		{
			Projectile.Shoot_Projectile(a, main.target, main);
			main.cooldown=a.recovery;
			main.Arm_Play_Animation(a.anim_index);
		}
	}

	[System.Serializable]
	///Contains stats for attack methods to use.
	public class wpn_atk
	{
		/// <summary>
		/// Sets values.
		/// </summary>
		/// <param name="damage">Damage.</param>
		/// <param name="knockback">Knockback.</param>
		/// <param name="speed">Speed of the swing projectile</param>
		/// <param name="duration">Time before the swing projectile disappears.</param>
		/// <param name="recovery">Cooldown.</param>
		/// <param name="size">Size of the swing projectile.</param>
		/// <param name="path">Path of the....</param>
		/// <param name="anim_index">Animation index. (1 or 2)</param>
		public wpn_atk(int damage, float knockback, float speed, float duration, float recovery, float size, string path, int anim_index)
		{
		this.damage=damage; this.knockback=knockback; this.speed=speed; this.duration=duration; 
		this.recovery=recovery; this.size=size; this.path=path; this.anim_index=anim_index;
		}
		public int damage;
		public float knockback;
		public float speed, duration, recovery, size;
		public string path; public int anim_index;
	}

	#region Functional Stuff
	/// <summary>
	/// Must be called on Start()
	/// </summary>
	protected virtual void Construct(string sprite_path)
	{
		main = GetComponent<Mob_Human> ();
		main.Arm_Set_Sprites (sprite_path);
	}

	/// <summary>
	/// Call before adding a new weapon.
	/// </summary>
	public virtual void Destroy()
	{
		GameObject.Destroy (this);
	}
	#endregion

	/*

	put on top of every new weapon for initialization
	  
	[Header("Attack 1")]
	[SerializeField] int attack_1_damage = ;
	[SerializeField] float attack_1_knockback = ;
	[SerializeField] float attack_1_speed = ;
	[SerializeField] float attack_1_range = ;
	[SerializeField] float attack_1_recovery = ;
	[SerializeField] float attack_1_size = ;
	[SerializeField] string attack_1_sprite_path = ;

	[Header("Attack 2")]
	[SerializeField] int attack_2_damage = ;
	[SerializeField] float attack_2_knockback = ;
	[SerializeField] float attack_2_speed = ;
	[SerializeField] float attack_2_range = ;
	[SerializeField] float attack_2_recovery = ;
	[SerializeField] float attack_2_size = ;
	[SerializeField] string attack_2_sprite_path = ;

	then this in start vvv
	
	a1=new wpn_atk
			(attack_1_damage, attack_1_knockback, attack_1_speed, 
				attack_1_range, attack_1_recovery, attack_1_size, attack_1_sprite_path, 1);
		a2=new wpn_atk
			(attack_2_damage, attack_2_knockback, attack_2_speed, 
				attack_2_range, attack_2_recovery, attack_2_size, attack_2_sprite_path, 2);
	*/
}
