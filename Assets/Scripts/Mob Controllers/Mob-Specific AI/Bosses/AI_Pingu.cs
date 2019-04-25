using UnityEngine;
using System.Collections;

public class AI_Pingu : AI_Basic {

	// Use this for initialization
	void Start () {
		Construct ();
	}
	
	bool lmao = true;
	void Update () {
		Set_Target ();

		if(Random.Range(0,2) == 1) main.Input1();
		else main.Input2();
	}
}
