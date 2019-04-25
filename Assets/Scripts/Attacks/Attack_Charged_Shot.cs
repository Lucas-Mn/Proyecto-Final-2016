using UnityEngine;
using System.Collections;

public class Attack_Charged_Shot : Attack {

	void Start(){Construct();}

	[SerializeField] bool charging = false;
	public float Charge_Time = 1f; private float charge_time;
	public int looped_frames = 0;

	public override bool Input()
	{
		charging = true; main.cooldown = this.cooldown; charge_time = Charge_Time;
		return true;
	}

	void Update()
	{
		if(charging)
		{
			charge_time -= Time.deltaTime;
			main.Loop_First_Frames(looped_frames);
			if(charge_time <= 0) Shoot();	
		}
		else if (shooting)
		{
			timer += Time.deltaTime;
			if (timer >= Delay) Sequential_Shoot();
		}
	}

	void Shoot()
	{
		charging = false;
		if (!Sequential_Shooting_Enabled) Single_Shoot(); //make it shoot.
		else 
		{
			shooting = true; s_index = 0;
			timer = 0;
		}
	}

	void Single_Shoot()
	{
		charging = false;
		main.Unloop_Frames();
	}

	[Header("Sequential Shooting Mode")]
	public bool Sequential_Shooting_Enabled = true;
	[SerializeField] private bool shooting = false;
	public float Delay = .2f; private int s_index = 0;
	private float timer;
	void Sequential_Shoot()
	{
		#region Target
		Vector2 target = Vector2.zero;
		if(s_index == 7 || s_index == 0 || s_index == 1) target.y = 50;
		else if (s_index == 2 || s_index == 6) target.y = 0;
		else target.y = -50;

		if(s_index == 1 || s_index == 2 || s_index == 3) target.x = 50;
		else if (s_index == 0 || s_index == 4) target.x = 0;
		else target.x = -50;
		
		Vector2 temp = transform.position;
		target += temp;
		#endregion

		Projectile.Shoot_Projectile(damage, knockback, speed, duration, projectile_name, size, target, main);
		Debug.Log("Shot at target " + target.ToString() + " on index " + s_index.ToString());

		#region Values
		s_index++;
		timer = 0;
		if(s_index == 8) Reset();
		#endregion
	}

	void Reset()
	{
		shooting = false;
		main.Unloop_Frames();
	}
}
