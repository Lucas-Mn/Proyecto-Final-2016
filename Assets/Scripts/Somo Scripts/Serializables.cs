using UnityEngine;
using System.Collections;

[System.Serializable]
public class InGameStatistics{
	public int level;
	public int levelsPassedN;
	public int stageN;
	public string stage;
	public Mob character;
}

[System.Serializable]
public class PlayerData{
	public int score;
	public PlayerData (){
		score = 0;
	}
}

public class MapData{
	public string Stage;
	public int Count;
	public int TwoBlockChance;
	public int ThreeBlockChance;
	public int NewSpawnChance;
	public float Difficulty;
	public int spawn_count;
	public bool bossLevel;

	public MapData(string s, int c, int two, int three, int n, float d)
	{		
		Stage = s;
		Count = c;
		TwoBlockChance = two;
		ThreeBlockChance = three;
		NewSpawnChance = n;
		Difficulty = d;
		spawn_count = Mathf.RoundToInt(20 * Difficulty);
	}
	public MapData()
	{		
		Stage = GlobalControl.I.gameData.stage;
		if (GlobalControl.I.gameData.level == 4)
		{
			bossLevel = true;
			Count = 1;
			NewSpawnChance = 0;
			TwoBlockChance = 0;
			ThreeBlockChance = 0;
			spawn_count = 1;
		} 
		else 
		{
			bossLevel = false;
			Difficulty = 1 + (GlobalControl.I.gameData.levelsPassedN * 0.15f);
			switch (Stage) {
			case "Sewers":
				Count = 1150;
				NewSpawnChance = 250;
				TwoBlockChance = 0;
				ThreeBlockChance = 0;
				break;
			case "Bosque": 
				Count = 1150;
				NewSpawnChance = 250;
				TwoBlockChance = 30;
				ThreeBlockChance = 20;
				break;
			case "Volcan":
				Count = 1500;
				NewSpawnChance = 250;
				TwoBlockChance = 35;
				ThreeBlockChance = 15;
				break;
			case "Nieve":
				Count = 1500;
				NewSpawnChance = 250;
				TwoBlockChance = 25;
				ThreeBlockChance = 25;
				break;
			case "Castle":
				Count = 200;
				NewSpawnChance = 250;
				TwoBlockChance = 100;
				ThreeBlockChance = 0;
				break;
			}
			spawn_count = Mathf.RoundToInt(20 * Difficulty * Difficulty);
		}

	}
}
