using UnityEngine;
using System.Collections;

public class Drop : MonoBehaviour{
	public GameObject obj;
	Weapon wep;
	Map m;
	void Awake()
	{
		m = GameObject.Find ("Code").GetComponent<Map> ();
		m.drops.Add (this.gameObject);
	}
	public void Initialize(Vector3 pos)
	{
		obj = GameObject.Instantiate (Resources.Load ("Prefabs/Drop")) as GameObject;
		obj.transform.position = pos;
	}
	public void Equip(Mob_Human player)
	{
//		player.Equip(wep);
//		if (player.weapon != null) {
//			Weapon w = player.weapon;
//			player.weapon = weapon;
//			weapon = w;
//		} else {
//			player.weapon = weapon;
//		}
	}
//	public Weapon weapon
//	{
//		get{
//			return wep;
//		}
//		set
//		{ 
//			this.GetComponent<SpriteRenderer> ().sprite = value.sprite;
//			wep = value;
//		}
//	}
}
