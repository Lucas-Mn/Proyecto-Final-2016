using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile{

	public GameObject Prefab;
	Dictionary<string, Sprite> dictionary;
	private string _index;
	public Tile(Dictionary<string, Sprite> dic)
	{
		dictionary = dic;
	}

	public string Index
	{
		get{return _index;}
		set{
			_index=value;
			Prefab.GetComponent<SpriteRenderer>().sprite = dictionary[_index];
			}

	}
	public string Type()
	{
		if (Index.StartsWith("1")) {
				return "Floor";
			} else {
				return "Wall";
			}

	}
}
