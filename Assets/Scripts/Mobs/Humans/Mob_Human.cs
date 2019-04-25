using UnityEngine;
using System.Collections;

public abstract class Mob_Human : Mob {

	protected Weapon wpn; SpriteRenderer weapon_sprite;
	protected Arms arm;
    public Vector3 arm_offset;

	//Info
	public Weapon weapon{get{return wpn;}}

	//DEBUG
	[Header("Debug")]
	public bool D_Give_Weapon = false;
	public string D_Weapon_Name = "sword";

	// Update is called once per frame
	void Update () {Human_Update ();}

	public override void Movement(float x, float y)
	{std_Movement(x, y, speed);}
	
	public override void Input1()
	{
		if(Has_Weapon)wpn.Input1 ();

		else if(Global.Debug_Mobs)Debug.Log(name+ " attacked without weapon. (Input1)");
	}
	public override void Input2()
	{
		if(Has_Weapon)wpn.Input2 ();

		else if(Global.Debug_Mobs)Debug.Log(name+ " attacked without weapon. (Input2)");
	}
	public override void Input3()
	{
		
	}

	#region Functional Stuff
	/// <summary>
	/// Must be called by mob on Start()
	/// </summary>
	/// <param name="path">The path for the sprite folder.</param>
	protected void Construct(string path, Vector2 collider_offset, float collider_size, int health, float speed, System.Type default_ai)
	{
		base.Construct (path, collider_offset, collider_size, health, speed, default_ai);
		arm = new GameObject ("arm").AddComponent<Arms>(); arm.transform.position = this.transform.position;
		arm.transform.parent = this.transform; arm.transform.localScale *= Global.Mob_Size;
        arm_offset = new Vector3(0.05f, 0.1f, -1f);
		arm.Initialize (path);
		weapon_sprite = new GameObject("weapon").AddComponent<SpriteRenderer>();
		weapon_sprite.transform.parent=arm.transform.parent;
		Give_Weapon (typeof(Weapon_Dagger));
		base.anim.mob_type = "Human";
	}

	protected void Human_Update()
	{
		Look_At_Target ();
		Update_ ();
		#region Position Arm
		Vector3 dir = target - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		arm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		arm.transform.localPosition 
		= new Vector3(-arm_offset.x*Direction, arm_offset.y, arm_offset.z);
		arm.ren.flipY = target.x < this.transform.position.x;
		#endregion

		//DEBUG!
		if (D_Give_Weapon) 
		{
			Give_Weapon (D_Weapon_Name);
			D_Give_Weapon = false;
		}
	}
	#endregion

	#region Externals
	/// <summary>
	/// Removes current weapon and adds weapon of type C.
	/// </summary>
	/// <typeparam name="T">The weapon to give.</typeparam>
	public void Give_Weapon(System.Type c)
	{
		if (wpn != null)
			wpn.Destroy ();
		wpn=(Weapon)gameObject.AddComponent(c);

		if(Global.Debug_Mobs)Debug.Log (name + " was given " + c.ToString() + " weapon.");
	}

	public void Give_Weapon(string weapon_name)
	{
		switch (weapon_name) 
		{
		case "sword":
			Give_Weapon(typeof(Weapon_Sword));
			break;
		case "dagger":
			Give_Weapon(typeof(Weapon_Dagger));
			break;
		default:
			Debug.LogError ("Weapon doesn't exist fuck u xd");
			break;
		}
	}

	public void Arm_Set_Sprites(string weapon_path)
	{
		arm.Set_Animations (weapon_path);
	}

	public void Arm_Play_Animation(int a)
	{
		arm.Play_Animation(a);
	}

	public bool Has_Weapon{get{return wpn!=null;}}
	#endregion
}
