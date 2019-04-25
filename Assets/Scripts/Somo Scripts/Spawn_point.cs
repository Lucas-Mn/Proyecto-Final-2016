using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_point
{
	Vector3 pos;
	Map map;
	public Spawn_point(Vector3 p, Map m)
	{
		pos = p;
		map = m;
	}
	public void Spawn()
	{
		GameObject mob = new GameObject ();
		mob.name = "Altoke Perro";
		mob.transform.position = pos;
		if (map.mapData.bossLevel) {
			mob.AddComponent<Mob_Pingu> ();
		} else {
			mob.AddComponent<Mob_Firebeast> ();
		}
		map.mobs.Add (mob);
//		mob.AddComponent<SpriteRenderer> ();
//		mob.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Imp");
//		mob.GetComponent<SpriteRenderer> ().sortingOrder = 1000;
	}
	Mob Select_mob()
	{
		return null;
	}
}


