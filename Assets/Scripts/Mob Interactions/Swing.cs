using UnityEngine;
using System.Collections;

public class Swing : MonoBehaviour {

	public int damage, knockback;
	public float speed, duration;
	public string sprite_path;

	public BoxCollider2D col;

	void Update () 
	{
		transform.position += transform.up * Time.deltaTime * speed;
		speed -= speed / 11; duration -= Time.deltaTime;
		if (duration < 0)
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
	/// <param name="duration">Duration before disappearirng</param>
	/// <param name="name">Path of the sprite under Sprites/Swings</param>
	/// <param name="size">Size</param>
	/// <param name="target">Target position.</param>
	public void Set_Values(int damage, int knockback, float speed, float duration, string name, float size, Vector3 target)
	{
		this.damage=damage; this.knockback=knockback; this.speed = speed; this.duration=duration; sprite_path=name;
		SpriteRenderer ren = gameObject.AddComponent<SpriteRenderer> ();
		ren.sprite = Resources.Load<Sprite> ("Sprites/Swings/" + sprite_path);
		Vector3 dir = target - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.localScale *= size;
		col = gameObject.AddComponent<BoxCollider2D> (); col.isTrigger = true;
		col.size = ren.sprite.bounds.size;
	}

	/// <summary>
	/// Sets the values.
	/// </summary>
	/// <param name="attack">Attack object to take stats from.</param>
	/// <param name="target">Target position.</param>
	public void Set_Values(Weapon.wpn_atk attack, Vector3 target)
	{
		Set_Values(attack.damage, attack.knockback, attack.speed, attack.duration, attack.path, attack.size, target);
	}
}
