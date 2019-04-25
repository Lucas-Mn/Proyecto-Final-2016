using UnityEngine;
using System.Collections;

public abstract class Attack : MonoBehaviour 
{

	protected Mob main;
	public float cooldown;

	[Header("Damage n stuff")]
	public int damage; public float knockback, speed, duration, size;
	public string projectile_name;

	public abstract bool Input();

	protected void Construct()
	{main = GetComponent<Mob>(); }
}
