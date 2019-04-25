using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mob_Animator : MonoBehaviour {

	public string mob_type = null;
	public SpriteRenderer ren;
	protected Dictionary<string, Animation> anims = new Dictionary<string, Animation> ();
	[SerializeField] protected string current;
	public Animation[] animations = new Animation[6]; //just for debug
	bool set_them = false; protected bool set = false;
	string set_path;
	//debug
	public string d_set_anim; public bool d_set;
	// Use this for initialization
	void Start () {
		ren = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update ()
	{
		if (set_them && mob_type != null) 
		{
			_Set_Sprites (set_path);
			set_them = false;
		}	
		if (d_set) 
		{
			Set_Anim (d_set_anim);
			d_set = false;
		}
	}

	void FixedUpdate()
	{
		if(set)
		if(anims[current].Animate ())
		current = "idle";
	}

	/// <summary>
	/// Sets the animation. ayyyy x^D
	/// </summary>
	/// <param name="s">animation name.</param>
	public void Set_Anim(string s)
	{
		if (!anims.ContainsKey (s))
			Debug.LogError ("Asked for animation " + s + "; not found.");
		if(s!=current&&set)anims[current].Reset();
		current = s;
	}

	/// <summary>
	/// Plays an animation. xd
	/// </summary>
	/// <param name="a">The alpha component. lmao</param>
	public void Set_Anim(ANIMATIONS a)
	{
		Set_Anim((int)a);
	}

	public void Set_Anim(int index)
	{
		switch (index) 
		{
		case 0:
			Set_Anim ("idle");
			return;
		case 1:
			Set_Anim ("walk");
			return;
		case 2:
			Set_Anim ("input_1");
			return;
		case 3:
			Set_Anim ("input_2");
			return;
		case 4:
			Set_Anim ("input_3");
			return;
		case 5:
			Set_Anim ("die");
			return;
		}
	}

	public enum ANIMATIONS{idle = 0, walk = 1, input_1 = 2, input_2 = 3, input_3 = 4, die = 5}

	public void Set_Sprites(string path)
	{
		set_them = true; set_path = path;
	}


	private void _Set_Sprites(string path)
	{
		anims.Add ("idle", new Animation (ren, "Sprites/Mobs/"+mob_type+"/"+path+"/idle"));
		anims.Add("walk", new Animation (ren, "Sprites/Mobs/"+mob_type+"/"+path+"/walk"));
		anims.Add("input_1", new Animation (ren, "Sprites/Mobs/"+mob_type+"/"+path+"/input_1"));
		anims.Add("input_2", new Animation (ren, "Sprites/Mobs/"+mob_type+"/"+path+"/input_2"));
		anims.Add("input_3", new Animation (ren, "Sprites/Mobs/"+mob_type+"/"+path+"/input_3"));
		anims.Add("death", new Animation (ren, "Sprites/Mobs/"+mob_type+"/"+path+"/death"));
		anims.Values.CopyTo(animations, 0); //just for debug
		current = "idle";
		set=true;
	}

	//External control
	public void Freeze_Last_Frame()
	{
		anims[current].freeze_last_frame = true;
	}

	public void Unfreeze_Last_Frame()
	{
		anims [current].Reset ();
		current = "idle";
	}

	public void Loop_First_Frames(int amount_of_frames)
	{
		if(amount_of_frames != 0) anims[current].Loop_First_Frames(amount_of_frames);
	}
	
	public void Unloop_First_Frames()
	{
		anims[current].Unloop_First_Frames();
	}
		
}
	
///Animation :P
[System.Serializable]
public class Animation
{
	string path_id;
	public Sprite[] sprite;
	int speed; //number of ticks between animation frames
	int wait; //countdown between frames
	int frame; //current frame
	bool reset; 
	public bool freeze_last_frame;
	[SerializeField] bool loop_first_frames;
	[SerializeField] int amount_loop_frames;
	
	SpriteRenderer ren;

	//get sets
	/// Number of frames.
	public int Length{get{return sprite.Length;}}
	/// True if animation isn't started.
	public bool Is_Reset{get{return reset;}}
	/// Current frame index
	public int Frame{ get { return frame; } }

	/// <summary>
	/// Initializes a new instance of the <see cref="Animation"/> class.
	/// </summary>
	/// <param name="s">Number of ticks between frames</param>
	/// <param name="r">Object's SpriteRenderer</param>
	public Animation(SpriteRenderer r, string path)
	{
		path_id = path;
		ren = r;
		this.speed = 1;
		reset = true;
		sprite = Resources.LoadAll<Sprite> (path);
		if(sprite.Length==0) Debug.LogError("Could not find animation "+path+".");
	}
		
	/// Animates the renderer. Returns true if animation has finished.
	public bool Animate()
	{	
		bool r = false;
		try{
			ren.sprite = sprite [frame];
			if (wait > 0)
				wait--;
			else 
			{
				if (frame < Length - 1)
					if(!loop_first_frames)frame++;
					else
						if(frame==amount_loop_frames-1) frame=0;
						else frame++;

				else if(!freeze_last_frame)
				{frame = 0; r = true;}
				wait = speed;
			}
		}
		catch{
			Debug.LogError ("Incorrect format in animation " + path_id);
		}
		return r;		
	}

	/// Resets frame and cooldown.
	public void Reset() { frame = wait = 0; freeze_last_frame = loop_first_frames = false; }

	public void Loop_First_Frames(int amount_of_frames)
	{
		loop_first_frames = true;
		amount_loop_frames = amount_of_frames;
	}

	public void Unloop_First_Frames()
	{
		loop_first_frames = false;
	}

}