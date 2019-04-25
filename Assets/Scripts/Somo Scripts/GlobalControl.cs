using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GlobalControl : MonoBehaviour {

	public static GlobalControl I = new GlobalControl();
	public InGameStatistics gameData = new InGameStatistics();
	public PlayerData playerData;
	public Dictionary<string,Mob> mobs = new Dictionary<string, Mob>();
	public List<string> stages = new List<string> ();
	GameObject Code;
	Map m;
	void Awake ()   
	{		
		if (I == null) {
			DontDestroyOnLoad (gameObject);
			I = this;
			I.AddStages ();
			I.gameData.stage = I.stages [Random.Range (0, I.stages.Count)];
			I.stages.Remove (I.gameData.stage);
			I.gameData.level = 1;
			I.gameData.stageN = 1;
//			try{
//				LoadData ();
//			}
//			catch{
// 				playerData = new PlayerData ();
//			}
			CreateMap ();
		}
		else if (I != this)
		{
			CreateMap ();
			Destroy (gameObject);
		}				
	}
	void AddStages()
	{
		stages.Add ("Sewers");
		stages.Add ("Volcan");
		stages.Add ("Castle");
		stages.Add ("Nieve");
		stages.Add ("Bosque");
	}
	void CreateMap()
	{
		m = GameObject.FindGameObjectWithTag ("Code").GetComponent<Map> ();
//		Code = new GameObject ();
//		Code.name = "Code";
//		Code.AddComponent<Map> ();
//		Code.GetComponent<Map> ().version = 3;
//		Code.tag = "Code";

	}
	public void CheckDrop(float x, float y, int chance)
	{
		if (Random.Range (1, 101) <= chance) {
			GameObject d = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/Drop"));
			d.transform.position = new Vector3 (x, y, 0);
		}
	}
	public void EndLevel()
	{
		if (I.gameData.level == 4) {
			if (I.gameData.stageN == 3) {
				EndGame ();
			} else {
				I.gameData.level = 1;
				I.gameData.stageN++;
				int n = Random.Range(0,stages.Count);
				I.gameData.stage = I.stages[n];
				I.stages.RemoveAt(n);
				NewLevel ();
			}

		} else {

			I.gameData.level++;
			NewLevel ();
		}
	}
	void NewLevel()
	{
		I.gameData.levelsPassedN++;
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");
	}

	void EndGame()
	{
		Destroy (this);
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Menu");
	}

	#region Save/load functions
	public void SaveData()
	{
		if (!Directory.Exists("Saves"))
			Directory.CreateDirectory("Saves");

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream saveFile = File.Create("Saves/save.binary");

		formatter.Serialize (saveFile, GlobalControl.I.playerData);

		saveFile.Close();
	}

	public void LoadData()
	{
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);

		GlobalControl.I.playerData = (PlayerData)formatter.Deserialize(saveFile);

		saveFile.Close();
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.F5))
			GlobalControl.I.SaveData();
		
		if (Input.GetKey(KeyCode.F9))
			GlobalControl.I.LoadData();
//		if (Input.GetKeyDown (KeyCode.K))
//			Debug.Log ("Player score: " + I.playerData.score.ToString ());
	}
	#endregion

	/// <summary>
	/// Adds the mobs to the dictionary.
	/// </summary>
	void AddMobs()
	{
		///
	}

	public void Score()
	{
		GlobalControl.I.playerData.score += 40;
		Debug.Log ("Player score: " + I.playerData.score.ToString ());

	}
}
