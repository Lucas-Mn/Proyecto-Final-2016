using UnityEngine;
using System.Collections;

public class AI_Firebeast : AI_Basic {
	 
	// Use this for initialization
	void Start () {
		Construct ();
	}
	
	// Update is called once per frame
	void Update () {
		Set_Target ();
		main.Input1 ();
	}
}
