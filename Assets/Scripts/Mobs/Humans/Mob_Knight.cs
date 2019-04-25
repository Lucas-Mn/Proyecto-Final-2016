using UnityEngine;
using System.Collections;

public class Mob_Knight : Mob_Human 
{

	void Start () {
		Construct("knight", new Vector2(0, 0.04f), .15f, 5, 100, typeof(Player_Input));
	}
		
	void Update()
	{
		Human_Update ();
	}
		
}


