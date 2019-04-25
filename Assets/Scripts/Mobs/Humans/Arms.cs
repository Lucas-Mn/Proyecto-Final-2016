using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Arms : MonoBehaviour{

	public SpriteRenderer ren;
	[SerializeField] private int current;
	public Animation[] animations= new Animation[3];
	public bool set=false;

	[SerializeField] string weapon_name;

	[SerializeField] string mob_name;

	private int Current{ get { return current; } }

	void FixedUpdate()
	{
		if(set && animations[Current].Animate()) //plays animation
			Play_Animation(0);
	}

	/// <summary>
	/// Sets animation to either idle, input_1, or input_2.
	/// </summary>
	/// <param name="anim_name">Animation name.</param>
	public void Play_Animation(int index)
	{
		if (index != current && set) animations [Current].Reset ();
			current = index;
	}

	/// <summary>
	/// Calls reference to all external data interactors and stores in datebase.
	/// Also corrects all improper values in the game global fluctuator.
	/// Must wait one tick before calling again, otherwise it might corrupt flowing data.
	/// </summary>
	private void Reset_Animation()
	{
		animations [Current].Reset ();
	}
		
	/// Loads each animation by name (weapon_animation)
	public void Set_Animations(string weapon_name)
	{	this.weapon_name = weapon_name;
		animations[0] = new Animation (ren, "Sprites/Mobs/Human/" + mob_name + "/weapons/" + weapon_name + "/idle");
		animations[1] = new Animation (ren, "Sprites/Mobs/Human/" + mob_name + "/weapons/" + weapon_name + "/input_1");
		animations[2] = new Animation (ren, "Sprites/Mobs/Human/" + mob_name + "/weapons/" + weapon_name + "/input_2");
	}

	public Arms Initialize(string mob_name)
	{
		ren = gameObject.AddComponent<SpriteRenderer> ();
		this.mob_name = mob_name;
		set=true;
		return this;
	}

	private static Vector2 v(float x, float y){return new Vector2 (x, y);}
}