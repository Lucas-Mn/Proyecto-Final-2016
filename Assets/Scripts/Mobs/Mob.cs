using UnityEngine;
using System.Collections;

public abstract class Mob : MonoBehaviour {

	///visual; gameobject with sprites and animations
	protected GameObject vis;
	protected Mob_Animator anim;
	SpriteRenderer ren; Rigidbody2D rigid;
	protected AI ai;

	#region Stats
	[SerializeField] protected int health; public int Health{ get { return health; } }
	[SerializeField] protected float speed; public float Speed { get { return speed; } }
	public float cooldown;
	#endregion

	#region Vectors
	public Vector2 velocity;
	[HideInInspector] public Vector3 target;
	Vector2 last_position;
	#endregion

	#region Attacks
	public Attack[] attacks;
	#endregion

	void Update () {Update_();}

	public abstract void Movement(float x, float y);
	public abstract void Input1();
	public abstract void Input2();
	public abstract void Input3();

	#region Functional Stuff
	/// <summary>
	/// Standard update function, call on Update() if needed.
	/// </summary>
	protected virtual void Update_()
	{ 
		velocity = (last_position - new Vector2(transform.position.x, transform.position.y)) / Time.deltaTime;
		last_position =  new Vector2(transform.position.x, transform.position.y);
		rigid.velocity = Vector2.zero;
		//position z equals y for rendering purposes
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);
		cooldown-=Time.deltaTime;
		//Debug
		if (Global.Debug_Mobs) Print_Debug ();
	}
	/// <summary>
	/// Must be called by mob on Start()
	/// </summary>
	/// <param name="path">The path for the sprite folder.</param>
	protected void Construct(string sprite_path, Vector2 collider_offset, float collider_size, int health, float speed, System.Type default_ai)//sets obj and vis
	{
		//GameObject that contains sprite and animator
		vis = new GameObject ("visual"); vis.AddComponent<SpriteRenderer> ();
		anim = vis.AddComponent<Mob_Animator> ();
		//Set vis as child and set position
		vis.transform.parent = this.transform; vis.transform.position=this.transform.position;
		anim.Set_Sprites (sprite_path);
		ren = vis.GetComponent<SpriteRenderer> ();
		//RigidBody and collisions
		rigid = gameObject.AddComponent<Rigidbody2D> ();
		rigid.angularDrag = rigid.gravityScale = 0;
		rigid.fixedAngle = true;
		BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D> ();
		if (!init_debug) 
		{
			collider.offset = collider_offset;
			collider.size = new Vector2 (collider_size, collider_size);
		} 
		else 
		{
			collider.offset = init_collider_offset;
			collider.size = new Vector2 (init_collider_size, init_collider_size);
		}
		//AI
		gameObject.AddComponent(default_ai);
		//Global Mods
		transform.localScale *= Global.Mob_Size;
		gameObject.tag = "Mob";
		//Set Stats
		if (!init_debug) 
		{
			this.health = health;
			this.speed = speed;
		} 
		else 
		{
			this.health = this.init_health;
			this.speed = this.init_speed; 
		}
		cooldown = 1f;
		if(anim.mob_type == null)
			anim.mob_type = "Enemy";
	}

	#region Initialization
	[Header("Debug Initialization")]
	[SerializeField] private bool init_debug;
	[SerializeField] private Vector2 init_collider_offset;
	[SerializeField] private float init_collider_size;
	[SerializeField] private int init_health;
	[SerializeField] private float init_speed;
	#endregion
	#endregion

	#region Externals
	/// <summary>
	/// Applies damage and knockback, kills instance if necessary.
	/// </summary>
	/// <param name="damage">Damage.</param>
	/// <param name="knockback">Knockback.</param>
	public virtual void Damage(int damage, float knockback)
	{
		health-=damage;
		if (health < 1)
			Die ();
	}

	public bool Can_Attack { get { return cooldown <= 0; } set { cooldown = value ? 0 : Mathf.Infinity; } }

	#region Animation Stuff
	/// <summary>
	/// Plays the animation.
	/// </summary>
	/// <param name="animation"> The animation XD. </param>
	public void Play_Anim(Mob_Animator.ANIMATIONS animation)
	{
		anim.Set_Anim (animation);
	}
	
	public void Play_Anim(int index){anim.Set_Anim(index);}

	public void Freeze_Last_Frame()
	{
		anim.Freeze_Last_Frame ();
	}

	public void Unfreeze_Last_Frame()
	{
		anim.Unfreeze_Last_Frame ();
	}

	public void Loop_First_Frames(int amount_of_frames)
	{
		anim.Loop_First_Frames(amount_of_frames);
	}
	
	public void Unloop_Frames()
	{
		anim.Unloop_First_Frames();
	}

	public bool Freeze_Look_Direction = false;

	public void Set_Animation_Speed(Mob_Animator.ANIMATIONS animation, int speed)
	{
		//TODO let mobs set individual animation speed.
	}
	#endregion
	#endregion

	#region Data
    public int Direction
    { get { return vis.transform.rotation.eulerAngles.y == 0 ? 1 : -1; } }
	#endregion

	#region Generic Methods
	/// <summary>
	/// Flips sprite towards mouse X position
	/// </summary>
	public void Look_At_Target()
	{
		if (!Freeze_Look_Direction) 
		{
			if (target.x > transform.position.x)
				vis.transform.eulerAngles =
				new Vector3 (vis.transform.rotation.eulerAngles.x, 0, vis.transform.rotation.eulerAngles.z);
			else
				vis.transform.eulerAngles =
			new Vector3 (vis.transform.rotation.eulerAngles.x, 180, vis.transform.rotation.eulerAngles.z);
		}
	}

	/// <summary>
	/// Standard 2 axis movement.
	/// </summary>
	/// <param name="x">The x axis.</param>
	/// <param name="y">The y axis.</param>
	/// <param name="speed">Speed.</param>
	public void std_Movement(float x, float y, float speed)
	{	
		transform.position += new Vector3(x*speed*Time.deltaTime, y*speed*Time.deltaTime, 0);

		if (x != 0 || y != 0)
			anim.Set_Anim ("walk");
		else
			anim.Set_Anim ("idle");
	}

	public void std_Movement (Vector2 v, float speed)
	{
		std_Movement (v.x, v.y, speed);
	}

	/// <summary>
	/// Creates a swing. Whoosh.
	/// </summary>
	/// <param name="name">Path under Sprites/Swings/</param>
	/// <param name="recovery_time">How much time to add to cooldown.</param>
	protected void std_atk(int damage, int knockback, float speed, float duration, string name, float size, float recovery_time)
	{
		if (Can_Attack) 
		{   
			Projectile.Shoot_Projectile(damage, knockback, speed, duration, name, size, target, this);
			cooldown = recovery_time;
		}
	}

	/// <summary>
	/// Attacks using the indicated script from attacks array.
	/// </summary>
	/// <param name="attack_index">Attack index in array (1 - 3).</param>
	protected void attack(int attack_index)
	{
		if(attack_index < 1 || attack_index > 3) Debug.LogError("Asked for attack " + attack_index + ". Must be 1 through 3.");
		if (Can_Attack && attacks[attack_index - 1].Input()) Play_Anim(attack_index + 1);
	}
	#endregion

	//Debug
	void Print_Debug()
	{
		string nl = "\n"; //end line
		string debug_out = this.name+":";
		if(health<1)debug_out += nl + "Health is "+health+"! Not dead though!";
		if(debug_out!=this.name+":") 
			Debug.Log (debug_out);
	}

	/// <summary>
	/// Ded xd
	/// </summary>
	void Die()
	{
		GlobalControl.I.CheckDrop (this.transform.position.x, this.transform.position.y, 50);
		Destroy (this.gameObject);
	}


}
