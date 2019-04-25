using UnityEngine;
using System.Collections;

public class Tile_Prefab_Script : MonoBehaviour {

	Map m;
	// Use this for initialization
	void Awake () {
		m = GameObject.Find("Code").GetComponent<Map> ();
	}
	public void Check (){
		if (m.Check_Collider (this.name)) {
			this.gameObject.AddComponent<BoxCollider2D>();
			if(!m.Check_Collider_Floor(this.name))
			{
				this.GetComponent<BoxCollider2D>().size = new Vector2(.32f,.32f);
			}
			else
			{
				this.GetComponent<BoxCollider2D>().size = new Vector2(.08f,.32f);
				this.GetComponent<BoxCollider2D>().offset = new Vector2(-.12f,0);
			}
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseUp()
	{
		m.ShowIndex (this.name);
	}
}
