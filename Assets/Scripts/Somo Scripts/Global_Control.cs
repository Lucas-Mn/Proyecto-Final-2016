//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.IO;
//
//public class Global_Control : MonoBehaviour {
//
//	public static Global_Control I = new Global_Control();
//	public Player_Statistics data = new Player_Statistics();
//	public bool IsSceneBeingLoaded = false;
//	public Dictionary<string,Mob> mobs = new Dictionary<string, Mob>();
//	public List<string> stages = new List<string> ();
//	GameObject Code;
//	void Awake ()   
//	{		
//		if (I == null) {
//			DontDestroyOnLoad (gameObject);
//			I = this;
//			I.AddStages ();
//			I.data.stage = I.stages [Random.Range (0, I.stages.Count)];
//			I.stages.Remove (I.data.stage);
//			I.data.level = 1;
//			I.data.stageN = 1;
//			CreateMap ();
//		}
//		else if (I != this)
//		{
//			CreateMap ();
//			Destroy (gameObject);
//		}				
//	}
//	void AddStages()
//	{
//		stages.Add ("Sewers");
//		stages.Add ("Volcan");
//		stages.Add ("Castle");
//		stages.Add ("Nieve");
//		stages.Add ("Bosque");
//	}
//	void CreateMap()
//	{
////		Code = new GameObject ();
////		Code.name = "Code";
////		Code.AddComponent<Map> ();
////		Code.GetComponent<Map> ().version = 3;
////		Code.tag = "Code";
//
//	}
//	public void CheckDrop(float x, float y, int chance)
//	{
//		if (Random.Range (1, 101) <= chance) {
//			GameObject d = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/Drop"));
//			d.transform.position = new Vector3 (x, y, 0);
//		}
//	}
//	public void EndLevel()
//	{
//		if (I.data.level == 4) {
//			if (I.data.stageN == 3) {
//				EndGame ();
//			} else {
//				I.data.level = 1;
//				I.data.stageN++;
//				int n = Random.Range(0,stages.Count);
//				I.data.stage = I.stages[n];
//				I.stages.RemoveAt(n);
//				NewLevel ();
//			}
//
//		} else {
//
//			I.data.level++;
//			NewLevel ();
//		}
//	}
//	void NewLevel()
//	{
//		I.data.levelsPassedN++;
//		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");
//	}
//
//	void EndGame()
//	{
//		Destroy (this);
//		UnityEngine.SceneManagement.SceneManager.LoadScene ("Menu");
//	}
////	public void SaveData()
////	{
////		if (!Directory.Exists("Saves"))
////			Directory.CreateDirectory("Saves");
////
////		BinaryFormatter formatter = new BinaryFormatter();
////		FileStream saveFile = File.Create("Saves/save.binary");
////
////		data = Global_Control.I.data;
////
////		formatter.Serialize(saveFile, data);
////
////		saveFile.Close();
////	}
////
////	public void LoadData()
////	{
////		BinaryFormatter formatter = new BinaryFormatter();
////		FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
////
////		data = (Player_Statistics)formatter.Deserialize(saveFile);
////
////		saveFile.Close();
////	}
//	/// <summary>
//	/// Adds the mobs to the dictionary.
//	/// </summary>
//	void AddMobs()
//	{
//		///
//	}
//}
