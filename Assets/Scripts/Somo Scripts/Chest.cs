using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour{
	bool open = false;
	Sprite open_sprite;
	public GameObject prefab;
	Map map;
	public void Init(GameObject pref, Vector3 pos, Map m, Transform parent)
	{
		prefab = pref;
		map = m;
		open_sprite = map.open_chest;
		prefab.transform.SetParent (parent);
		prefab.transform.position = pos;
	}
	void OnMouseUp()
	{
		OnCollisionEnter2D ();
		Debug.Log (map.Pythagoras(new Vector3((Mathf.Sqrt(map.m_x * map.m_x) / 2)*0.32f, (-1*(Mathf.Sqrt(map.m_y * map.m_y)/2))*0.32f,-10),prefab.gameObject).ToString());
	}
	void OnCollisionEnter2D()
	{
		if (open == false) {
 			prefab.GetComponent<SpriteRenderer> ().sprite = open_sprite;
			open = true;
			GlobalControl.I.CheckDrop (prefab.transform.position.x, prefab.transform.position.y, 100);
		}
	}
	void Drops_chest()
	{

	}
}
