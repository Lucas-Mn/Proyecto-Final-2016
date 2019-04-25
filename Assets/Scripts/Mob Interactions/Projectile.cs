using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	
	public int damage;
	public float knockback;
	public float speed, range;
	public string sprite_path;

	public BoxCollider2D col;
	Vector3 start_pos;
	Mob attacker;
	
	void Update () 
	{
		transform.position += transform.up * Time.deltaTime * speed;
		//speed -= speed / 10;
		if (Vector3.Distance(start_pos, this.transform.position) > range)
			GameObject.Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag=="Mob" && collision.gameObject.name!=this.gameObject.name)
		{
			collision.gameObject.GetComponent<Mob>().Damage(damage,knockback);
		}
	}

	/// <summary>
	/// Sets the values.
	/// </summary>
	/// <param name="damage">Damage</param>
	/// <param name="knockback">Knockback</param>
	/// <param name="speed">Movement Speed</param>
	/// <param name="duration">Duration before disappearing</param>
	/// <param name="name">Path of the sprite under Sprites/Swings</param>
	/// <param name="size">Size</param>
	/// <param name="target">Target position.</param>
	public void Set_Values(int damage, float knockback, float speed, float range, string name, float size, Vector3 target, Mob attacker)
	{
		this.damage=damage; this.knockback=knockback; this.speed = speed; this.range=range; sprite_path=name;
		SpriteRenderer ren = gameObject.AddComponent<SpriteRenderer> ();
		ren.sprite = Resources.Load<Sprite> ("Sprites/Swings/" + sprite_path);
		Vector3 dir = target - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.localScale *= size;
		col = gameObject.AddComponent<BoxCollider2D> (); col.isTrigger = true;
		col.size = ren.sprite.bounds.size;
		start_pos=this.transform.position;
		this.attacker = attacker;
	}

	/// <summary>
	/// Sets the values.
	/// </summary>
	/// <param name="attack">Attack object to take stats from.</param>
	/// <param name="target">Target position.</param>
	public void Set_Values(Weapon.wpn_atk attack, Vector3 target, Mob attacker)
	{
		Set_Values(attack.damage, attack.knockback, attack.speed, attack.duration, attack.path, attack.size, target, attacker);
	}

	public static void Shoot_Projectile(int damage, float knockback, float speed, float duration, string name, float size, Vector2 target, Mob attacker)
	{
			GameObject furry = new GameObject(attacker.name);
			furry.transform.position = attacker.transform.position;
			Projectile swing = furry.AddComponent<Projectile> ();
			swing.Set_Values (damage, knockback, speed, duration, name, size, target, attacker);
	}

	public static void Shoot_Projectile(Weapon.wpn_atk a, Vector2 target, Mob attacker)
	{
			GameObject furry = new GameObject(attacker.name);
			furry.transform.position = attacker.transform.position;
			Projectile swing = furry.AddComponent<Projectile> ();
			swing.Set_Values (a, target, attacker);
	}
}
