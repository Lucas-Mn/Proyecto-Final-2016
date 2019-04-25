using UnityEngine;
using System.Collections;

public class Trigger_Tiles : MonoBehaviour {
	Map m;
	// Use this for initialization
	void Awake () {
		m = GameObject.Find("Code").GetComponent<Map> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseUp()
	{
		m.ShowIndex	 (this.name);
	}
}
