using UnityEngine;
using System.Collections;

public class Player{
	public InGameStatistics loaded_data = new InGameStatistics();

	public void SavePlayer()
	{
		GlobalControl.I.gameData = loaded_data;
	}
	void Start()
	{
		loaded_data = GlobalControl.I.gameData;
	}
	}
