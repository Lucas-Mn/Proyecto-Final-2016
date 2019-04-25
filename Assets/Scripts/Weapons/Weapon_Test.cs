using UnityEngine;
using System.Collections;

public class Weapon_Test : Weapon 
{
	// Use this for initialization
	void Start () {
		Construct ("sword");
		a1=new wpn_atk(5,1,5f, .18f, .5f, .5f, "Swing_Test", 1);
	}
	
	//TODO Add second attack
}
