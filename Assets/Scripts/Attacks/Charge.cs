using UnityEngine;
using System.Collections;

public class Attack_Charge : Attack {

	Mob main;

	void Start()
	{
		main = GetComponent<Mob> ();
	}

	void Update()
	{
		if(starting)
			if (channeling > 0)
			{channeling -= Time.deltaTime; main.Freeze_Last_Frame();}
			 else
				Start_Charge ();

		if (charging) 
		{
			if (charge_time >= duration ) //end of charge
			{
				charging = false;
				main.Unfreeze_Last_Frame();
				main.Freeze_Look_Direction = false;
				Reset ();
			} 
			else //charge
			{				
				base.transform.position += charge_velocity * speed * Time.deltaTime;
				charge_time+=Time.deltaTime;
			}
		}
	}
	[Header("Variables")]
	public float duration = .7f;
	public float cooldown = 2.8f;
	public float channel_time = 0.3f;
	public float speed = 1;
	public bool Debug_Enabled = false;

	[Header("Technical Stuff")]
	[SerializeField] bool starting = false;
	[SerializeField] float channeling; float Channeling { get { return channeling; } }
	[SerializeField] bool charging = false; public bool Charging { get { return charging; } }
	[SerializeField] float charge_time;
	[SerializeField] Vector3 charge_target, charge_velocity;

	Vector3 Charge_Direction()
	{
//		Vector3 v = new Vector3 (0,0,0);
//		if (base.transform.position.x > charge_target.x)
//			v.x = -0.1f;
//		else
//			v.x = 0.1f;
//		if (base.transform.position.y > charge_target.y)
//			v.y = -0.1f;
//		else
//			v.y = 0.1f;	
//		return v;
		return (charge_target - this.transform.position).normalized;
	}

	public override bool Input()
	{
			if (cooldown <= duration + channel_time)
				Debug.LogError ("Charge Cooldown is shorter than total Charge duration on " + gameObject.name);
			starting = true;
			channeling = channel_time;
			main.cooldown = this.cooldown;
			return true;
	}

	void Start_Charge()
	{
		charging = true; starting = false;
		charge_target = main.target;
		charge_velocity = Charge_Direction ();
		main.Freeze_Look_Direction = true;
	}

	void Reset()
	{
		channeling = charge_time = 0;
		charging = false;
	}
}
