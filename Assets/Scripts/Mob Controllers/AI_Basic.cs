                                                                                                                                                                                                                                                                                                                                                                                                                                     using UnityEngine;
using System.Collections;

public class AI_Basic : AI {

	public Mob main;
	public Mob player;

	protected Vector3 target{get{return main.target;}}

	protected void Construct()
	{
		main = GetComponent<Mob> ();
		player = GameObject.Find ("Player").GetComponent<Mob>();
	}     

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected void Set_Target()
	{
		main.target = player.transform.position;
	}

	protected void Standard_Movement()
	{main.Movement (target.x > this.transform.position.x ? 1 : -1,
		target.y > this.transform.position.y ? 1 : -1);}
}
