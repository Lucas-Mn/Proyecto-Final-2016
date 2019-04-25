using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class Map : MonoBehaviour{

	#region Imports
	public Sprite open_chest;
	public GameObject Tile_Prefab, Chest_Prefab;
	#endregion

	#region Data from the map
	public const int Mapa_x = 100;
	public const int Mapa_y = 100;
	Vector3 pos_inicial;
	public int m_x,m_y;
	public MapData mapData;
	#endregion

	#region Map objects
	Dictionary<string,Sprite> Tiles = new Dictionary<string,Sprite>();
	Dictionary<string,Mob> mobDict = new Dictionary<string,Mob>();
	List<Chest> chests = new List<Chest>();
	public List<GameObject> drops = new List<GameObject> ();
	List<Spawn_point> spawns = new List<Spawn_point> ();
	public List<GameObject> mobs = new List<GameObject> ();
	#endregion

	#region Debug
	public string tipo_mapa;
	public bool showMode, mobs_on;
	#endregion

	#region Map creation
	Tile[,] Mapa = new Tile[Mapa_x, Mapa_y];
	float Destroyer_X, Destroyer_Y;
	int Last_Move = 0, first_move = 5;
	int counter = 500;
	List<Vector3> pos_spawns = new List<Vector3>();

	/// <summary>
	/// The n represents the dimension of rooms in castle
	/// </summary>
	int n = Mathf.RoundToInt ((9 * 100) / Mapa_x);
	#endregion

	#region Scene data
	public Camera C;
	float C_Size;
	#endregion

	//TODO

	void Awake(){
		SetSceneVariables ();
		SetMapData ();
		Import_resources ();
		Create_Tiles ();
		Create_destroyer ();
		if (!showMode) {
			Destroy_Walls ();
			Add_Detail ();
			if(!mapData.bossLevel)
				FilterChests ();
			Check_Colliders ();
			Change_tile_z ();
			if (mobs_on) {
				Create_Spawns ();
				Spawn_Enemies ();
				EnlargeBossMobs ();
			}
		}
		CreateGUI();
	}

	/// <summary>
	/// Creates the GUI.
	/// </summary>
	void CreateGUI()
	{
		gameObject.AddComponent<GUI> ();
	}

	/// <summary>
	/// Sets the map data.
	/// </summary>
	void SetMapData()
	{
		pos_inicial = new Vector3 ((Mathf.Sqrt (Mapa.Length) / 2) * 0.32f, (-1 * (Mathf.Sqrt (Mapa.Length) / 2)) * 0.32f, -10);
		m_x = Mapa_x;
		m_y = Mapa_y;
		if (tipo_mapa == "") {
			mapData = new MapData ();
		} else {
			mapData = new MapData (tipo_mapa, 400, 100, 0, 250, 1);
		}


	}

	/// <summary>
	/// Sets the scene variables.
	/// </summary>
	void SetSceneVariables()
	{
		C_Size = 20.14982f;
		C = GameObject.FindWithTag ("MainCamera").GetComponent<Camera> ();
		C.GetComponent<Camera> ().orthographicSize = 1.25f;
		C.transform.position = new Vector3 ((Mathf.Sqrt (Mapa.Length) / 2) * 0.32f, (-1 * (Mathf.Sqrt (Mapa.Length) / 2)) * 0.32f, -499);

	}

	/// <summary>
	/// Imports the resources.
	/// </summary>
	public void Import_resources()
	{
		open_chest = Resources.Load<Sprite>("Objects/Chest_Open");
		Tile_Prefab = (GameObject)Resources.Load ("Prefabs/Tile");
		Chest_Prefab = (GameObject)Resources.Load ("Prefabs/Chest");
		Import_Sprite_Packages ();
	}

	/// <summary>
	/// Enlarges the boss mob.
	/// </summary>
	void EnlargeBossMobs()
	{
		for (int i = 0; i < mobs.Count; i++)
			mobs [i].transform.localScale = new Vector3 (1, 1, 1);		
	}

	/// <summary>
	/// Destroys the walls.
	/// </summary>
	void Destroy_Walls()
	{
		if (mapData.bossLevel) 
		{			
			CreateRoom (ConvertidorAVector(Destroyer_X),ConvertidorAVector(Destroyer_Y));
		}
		else {
			int Temp3 = mapData.Count;
			for (int i = 0; i < Temp3; i++) {
				DestroyerMovement ();
				mapData.Count--;
			}
		}
	}

	/// <summary>
	/// Checks the colliders.
	/// </summary>
	void Check_Colliders()
	{
		for (int i = 0; i < Mapa_x; i++) {
			for(int j = 0; j <Mapa_y;j++)
			{
				Mapa[i,j].Prefab.GetComponent<Tile_Prefab_Script>().Check();
			}
		}
	}

	/// <summary>
	/// Creates the tiles.
	/// </summary>
	void Create_Tiles()
	{
		float x = 0;
		float y = 0;
		for (int i = 0; i < Mapa_x; i++) {
			x = 0;
			
			for(int j = 0; j < Mapa_y; j++){
				Mapa [j,i] = new Tile(Tiles);
				Mapa [j,i].Prefab = Instantiate(Tile_Prefab);
				Mapa [j,i].Prefab.transform.SetParent(this.transform);
				Mapa [j,i].Prefab.transform.position = new Vector2(x,y);	
				Mapa [j, i].Index = "6";
				string n1 = "";
				if(i < 10)
				{
					n1 += "0";
				}
				n1 +=i.ToString();
				if(j < 10)
				{
					n1 += "0";
				}
				n1 += j.ToString();
				Mapa[j,i].Prefab.name = n1;
				x += 0.32f;
			}
			y -= 0.32f;
		}
	}

	/// <summary>
	/// Imports the sprite packages.
	/// </summary>
	void Import_Sprite_Packages()
	{
		tipo_mapa = GlobalControl.I.gameData.stage;
		Sprite[] paquete_usado;
		paquete_usado = Resources.LoadAll<Sprite> ("Tiles/ola/" + tipo_mapa + "/");

		for (int i = 0; i < paquete_usado.Length; i++) {
			Tiles.Add (paquete_usado [i].name, paquete_usado [i]);
		}
	}

	/// <summary>
	/// Adds detail to the tiles.
	/// </summary>
	void Add_Detail()
	{
		for (int y = 2; y < Mapa_y - 2; y++) {
			for (int x = 2; x < Mapa_x - 2; x++) {
				if (Mapa [x, y].Type() == "Wall") {
					if (V3_check_2a (x, y)) {
						Mapa [x, y].Index = "2a";
					} else if (V3_check_2b (x, y)) {
						Mapa [x, y].Index = "2b";
					} else if (V3_check_3b (x, y)) {
						Mapa [x, y].Index = "3b";
					}else if (V3_check_3a (x, y)) {
						Mapa [x, y].Index = "3a";
					}  else if (V3_check_4a (x, y)) {
						Mapa [x, y].Index = "4a";
					} else if (V3_check_4b (x, y)) {
						Mapa [x, y].Index = "4b";
					} else if (V3_check_5 (x, y)) {
						Mapa [x, y].Index = "5";
					}
				} else {
					if(tipo_mapa == "Hidden Level")
					{
						if(y % 2 == 1)
						{
							Mapa[x,y].Index = "1a";
						}
						else
						{
							Mapa[x,y].Index = "1b";
						}
					}else
					{
						int r = UnityEngine.Random.Range (1, 101);
						try
						{
							if (r < 33) {
								Mapa [x, y].Index = "1b";
							} else if (r < 66 && r >= 33) {
								Mapa [x, y].Index = "1c";
							}
						}
						catch{}
					}
					if (V3_check_7a (x, y)) {
						GameObject wall_part = new GameObject();
						wall_part.AddComponent<SpriteRenderer> ();
						wall_part.GetComponent<SpriteRenderer> ().sprite = Tiles ["7a"];
						wall_part.GetComponent<SpriteRenderer> ().sortingOrder = 14;
						wall_part.transform.position = Mapa [x, y].Prefab.transform.position;
						wall_part.transform.parent = Mapa [x, y].Prefab.transform;
					}
					else if (V3_check_7b (x, y)) {
						GameObject wall_part = new GameObject();
						wall_part.AddComponent<SpriteRenderer> ();
						wall_part.GetComponent<SpriteRenderer> ().sprite = Tiles ["7b"];
						wall_part.GetComponent<SpriteRenderer> ().sortingOrder = 14;
						wall_part.transform.position = Mapa [x, y].Prefab.transform.position;
						wall_part.transform.parent = Mapa [x, y].Prefab.transform;
					}

					if (tipo_mapa == "Bosque") {
						CreateBush (x, y);
					}

				}
			}
		}		
	}
		
	/// <summary>
	/// Creates the bush.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	void CreateBush(int x, int y)
	{
		string st = "abcd";
		if (UnityEngine.Random.Range (1, 101) < 8) {
			if (Check_Tiles_Surrounding (x, y, "Floor"))
				Mapa [x, y].Index = "8" + st[UnityEngine.Random.Range(0, st.Length)];
		}

	}

	bool V3_check_2a(int x, int y)
	{
		if (Mapa [x + 1, y].Type() == "Floor" && Mapa [x, y - 1].Type() == "Wall" && Mapa [x, y + 1].Type() == "Wall") {
			return true;
		} else {
			return false;
		}
	}

	bool V3_check_2b(int x, int y)
	{
		if (Mapa [x + 1, y].Type() == "Floor" && Mapa [x, y - 1].Type() == "Floor" && Mapa [x, y + 1].Type() == "Wall") {
			return true;
		} else {
			return false;
		}
	}

	bool V3_check_3a(int x, int y)
	{
		if (Mapa [x, y + 1].Type() == "Floor" && Mapa [x + 1, y].Type() == "Wall") {
			return true;
		} else {
			return false;
		}
	}

	bool V3_check_3b(int x, int y)
	{
		if (Mapa [x, y + 1].Type() == "Floor" && Mapa [x + 1, y].Type() == "Wall" && Mapa [x + 1, y + 1].Type() == "Wall") {
			return true;
		} else {
			return false;
		}
	}

	bool V3_check_4a(int x, int y)
	{
		if (Mapa [x, y + 1].Type() == "Floor" && Mapa [x + 1, y].Type() == "Floor" && Mapa [x, y - 1].Type() == "Wall") {
			return true;
		} else {
			return false;
		}
	}

	bool V3_check_4b(int x, int y)
	{
		if (Mapa [x, y + 1].Type() == "Floor" && Mapa [x + 1, y].Type() == "Floor" && Mapa [x, y - 1].Type() == "Floor") {
			return true;
		} else {
			return false;
		}
	}

	bool V3_check_5(int x, int y)
	{
		if (Mapa [x, y + 1].Type() == "Wall" && Mapa [x + 1, y].Type() == "Wall" && Mapa [x + 1, y + 1].Type() == "Floor") {
			return true;
		} else {
			return false;
		}
	}

	bool V3_check_7a(int x, int y)
	{
		if (Mapa [x + 1, y].Type() == "Wall" && Mapa [x + 1, y + 1].Type() == "Floor") {
			return true;
		} else {
			return false;
		}
	}

	bool V3_check_7b(int x, int y)
	{
		if (Mapa [x + 1, y].Type() == "Wall" && Mapa [x + 1, y + 1].Type() == "Wall") {
			return true;
		} else {
			return false;
		}
	}

	/// <summary>
	/// Checks the tiles surrounding.
	/// </summary>
	/// <returns><c>true</c>, if tiles_ surrounding was check_ed, <c>false</c> otherwise.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="type">Type.</param>
	bool Check_Tiles_Surrounding(int x, int y, string type)
	{
		if (Mapa [x - 1, y - 1].Type() == type && Mapa [x - 1, y].Type() == type && Mapa [x- 1, y + 1].Type() == type && Mapa [x, y - 1].Type() == type && Mapa [x + 1, y - 1].Type() == type && Mapa [x + 1, y].Type() == type && Mapa [x + 1, y + 1].Type() == type && Mapa [x, y + 1].Type() == type) {
			return true;
		
		} else {
			return false;
		}

	}

	/// <summary>
	/// Show index for specified Tile.
	/// </summary>
	/// <param name="Name">Name.</param>
	public void ShowIndex(string Name)
	{
		Debug.Log (Name + " " +Mapa [System.Int32.Parse((Name.Substring (2, 2))), System.Int32.Parse(Name.Substring (0, 2))].Index.ToString ());

	}
	/// <summary>
	/// Returns the distance between the specified pos_inicial and chest. 
	/// </summary>
	/// <param name="pos_inicial">Pos_inicial.</param>
	/// <param name="chest">Chest.</param>
	public float Pythagoras(Vector3 pos_inicial, GameObject chest)
	{
		float temp_x,temp_y;
		temp_x = chest.transform.position.x * 0.32f - pos_inicial.x * 0.32f;
		temp_y = chest.transform.position.y * 0.32f - pos_inicial.y * 0.32f; 
		return Mathf.Sqrt((temp_x * temp_x) + (temp_y * temp_y));
	}

	/// <summary>
	/// Filters the chests.
	/// </summary>
	void FilterChests()
	{
		float[] distances_chests = new float[chests.Count];
		GameObject[] chests_temp = new GameObject[chests.Count];
		for (int i = 0; i < chests.Count; i++) {
			chests_temp[i] = chests[i].prefab;
			distances_chests [i] = Pythagoras(pos_inicial,chests[i].prefab);

		}
		Array.Sort (distances_chests, chests_temp);
		int n = 0;
		while (chests.Count != 1) {
			if(Pythagoras(pos_inicial,chests[n].prefab) == distances_chests[distances_chests.Length-1] && n== 0)
			{
				n++;
			}
			else
			{

				Destroy(chests[n].prefab);
				chests.RemoveAt(n);
			}
		}
	}

	/// <summary>
	/// Creates the destroyer.
	/// </summary>
	void Create_destroyer()
	{
		Destroyer_X = Mapa [Mapa_x / 2,Mapa_y / 2].Prefab.transform.position.x;
		Destroyer_Y = Mapa [Mapa_x / 2,Mapa_y / 2].Prefab.transform.position.y;//GameObject.Find (a + b).transform.position.y;
		Mapa [Mapa_x / 2, Mapa_y / 2].Index = "1a";
		first_move = 5;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.B)) {
			for (int i = 0; i < mobs.Count; i++)
				mobs [i].transform.localScale = new Vector3 (1, 1, 1);		
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			GlobalControl.I.EndLevel ();
		}
		if(Input.GetKeyDown(KeyCode.G))
		{
			C.transform.position = new Vector3(Destroyer_X, Destroyer_Y,-499);
		}
		if (Input.GetKeyDown (KeyCode.H)) {
			C.GetComponent<Camera>().orthographicSize = 20.14982f;
		}
		if (Input.GetKeyDown (KeyCode.T)) {
			C.transform.position = new Vector3(chests[0].prefab.transform.position.x,chests[0].prefab.transform.position.y,-499);
		}

		if (Input.GetKeyDown (KeyCode.Y)) {
			if(C_Size == 1.25f)
			{
			C.GetComponent<Camera>().orthographicSize = 20.14982f;
				C_Size = 20.14982f;
			}else
			{
				C.GetComponent<Camera>().orthographicSize = 1.25f;
				C_Size = 1.25f;
			}
		}
		if (mapData.Count == 0) {
			mapData.Count--;
		}
		if (counter == 0) {
			counter--;
		}
	}

	/// <summary>
	/// Convertidors A vector.
	/// </summary>
	/// <returns>The A vector.</returns>
	/// <param name="Vec">Vec.</param>
	int ConvertidorAVector(float Vec)
	{
		float Temp1 = Vec;
		if (Vec < 0) {
			Temp1 = Vec * -1;
		}
		return (int)Mathf.Round(Temp1 / 0.32f);
	}

	/// <summary>
	/// Checks to see if the tile needs collider.
	/// </summary>
	/// <returns><c>true</c>, if rigidbody was check_ed, <c>false</c> otherwise.</returns>
	/// <param name="Name">Name.</param>
	public bool Check_Collider(string Name){
		int x = System.Int32.Parse(Name.Substring (2, 2));
		int y = System.Int32.Parse(Name.Substring (0, 2));
		try{
			if(Mapa [x, y].Type() == "Wall" || Mapa [x, y].Index.StartsWith("4"))
			{
				if (Mapa [x, y].Type() == "Wall" && Mapa [x - 1, y].Type() == "Wall" && Mapa [x + 1, y].Type() == "Wall" && Mapa [x, y + 1].Type() == "Wall" && Mapa [x, y - 1].Type() == "Wall") {
					return false;
				}
				 else {
					return true;
				}
			}
			else
			{
				return false;
			}
		}
		catch{return false;}		
	}

	/// <summary>
	/// Checks the collider floor.
	/// </summary>
	/// <returns><c>true</c>, if collider floor was checked, <c>false</c> otherwise.</returns>
	/// <param name="name">Name.</param>
	public bool Check_Collider_Floor(string name)
	{
		int x = System.Int32.Parse(name.Substring (2, 2));
		int y = System.Int32.Parse(name.Substring (0, 2));
		if (Mapa [x, y].Type() == "Floor") {
			return true;
		} else {
			return false;	
		}
	}

	/// <summary>
	/// Moves the destroyer
	/// </summary>
	void DestroyerMovement()
	{
		int Dimension = UnityEngine.Random.Range (1, 101);
		int Direction = UnityEngine.Random.Range (0, 4);
		if (tipo_mapa == "Sewers") {
			if (first_move == 5) {
				first_move = Direction;
			} else {
				while (Direction == first_move) {
					Direction = UnityEngine.Random.Range (0, 4);
				}
			}
		}
		if (tipo_mapa == "Castle") {
			int newRoomChance = UnityEngine.Random.Range (1, 151);
			int newDirectionChance = UnityEngine.Random.Range (1, 101);
			if (first_move == 5) {
				CreateRoom (ConvertidorAVector(Destroyer_X), ConvertidorAVector(Destroyer_Y));
				first_move = Direction;
			} 
			else {
				if (newDirectionChance >= 8) {
					while (Direction != first_move) {
						Direction = UnityEngine.Random.Range (0, 4);
					}
				} else {
					first_move = Direction;
				}
				if (newRoomChance == 1 && ConvertidorAVector(Destroyer_X) < Mapa_x - n - 1 && ConvertidorAVector(Destroyer_X) > n + 1 && ConvertidorAVector(Destroyer_Y) < Mapa_y - n - 1 && ConvertidorAVector(Destroyer_Y) > n + 1) {

					CreateRoom (ConvertidorAVector(Destroyer_X), ConvertidorAVector(Destroyer_Y));


				}
			}

		}
		int New_Spawn = UnityEngine.Random.Range (0, mapData.NewSpawnChance + 1);
		if (New_Spawn == mapData.NewSpawnChance || ConvertidorAVector(Destroyer_X) >= Mapa_x - 2 || ConvertidorAVector(Destroyer_X) <= 2 || ConvertidorAVector(Destroyer_Y) >= Mapa_y - 2 || ConvertidorAVector(Destroyer_Y) <= 2) {
			Create_destroyer();
			return;
		}

//		if (Direction - 2 == Last_Move || Direction + 2 == Last_Move) {
//			if(move_backwards > move_backwards_chance)
//			{
//				return;
//			}
//		}
		Destroyer_Dimension(Dimension, Direction);
	}

	/// <summary>
	/// Destroyer dimension.
	/// </summary>
	/// <param name="Dimension">Dimension.</param>
	/// <param name="Direction">Direction.</param>
	void Destroyer_Dimension(int Dimension, int Direction)
	{
			try {
				Mapa [ConvertidorAVector (Destroyer_X), ConvertidorAVector (Destroyer_Y)].Index = "1a";//.Prefab.GetComponent<SpriteRenderer>().sprite = Tiles[1];
			if (Dimension > 100 - mapData.TwoBlockChance && Dimension <= 100) {
					switch (Direction) {
					case 0:
					case 2:
						Mapa [ConvertidorAVector (Destroyer_X) + 1, ConvertidorAVector (Destroyer_Y)].Index = "1a";
						break;
					case 1:
					case 3:
						Mapa [ConvertidorAVector (Destroyer_X), ConvertidorAVector (Destroyer_Y) - 1].Index = "1a";//.Prefab.GetComponent<SpriteRenderer>().sprite = Tiles[1];
						break;
					}
			} else if (Dimension <= 100 - mapData.TwoBlockChance && Dimension >= 100 - mapData.TwoBlockChance - mapData.ThreeBlockChance) {
					switch (Direction) {
					case 0:
					case 2:
						Mapa [ConvertidorAVector (Destroyer_X) + 1, ConvertidorAVector (Destroyer_Y)].Index = "1a";//.Prefab.GetComponent<SpriteRenderer>().sprite = Tiles[1];
						Mapa [ConvertidorAVector (Destroyer_X) - 1, ConvertidorAVector (Destroyer_Y)].Index = "1a";//.Prefab.GetComponent<SpriteRenderer>().sprite = Tiles[1];
						break;
					case 1:
					case 3:
						Mapa [ConvertidorAVector (Destroyer_X), ConvertidorAVector (Destroyer_Y) - 1].Index = "1a";//.Prefab.GetComponent<SpriteRenderer>().sprite = Tiles[1];
						Mapa [ConvertidorAVector (Destroyer_X), ConvertidorAVector (Destroyer_Y) + 1].Index = "1a";//.Prefab.GetComponent<SpriteRenderer>().sprite = Tiles[1];
						break;
					}
							}

				if (Direction - 2 == Last_Move || Direction + 2 == Last_Move) {
					GameObject g = Instantiate (Chest_Prefab);
					chests.Add (g.GetComponent<Chest>());
					chests[chests.Count - 1].Init(g,Mapa [ConvertidorAVector (Destroyer_X), ConvertidorAVector (Destroyer_Y)].Prefab.transform.position,this,this.transform);

				}
				Last_Move = Direction;
			} catch {
				Create_destroyer ();
				return;
			}
			switch (Direction) {
			case 0:
				Destroyer_Y -= 0.32f;	
				break;
			case 1:
				Destroyer_X += 0.32f;	
				break;
			case 2:
				Destroyer_Y += 0.32f;	
				break;
			case 3:
				Destroyer_X -= 0.32f;	
				break;
			}

	}


	/// <summary>
	/// Changes tile's z position.
	/// </summary>
	void Change_tile_z()
	{
		for (int y = 0; y < Mapa_y; y++) {
			for(int x = 0; x < Mapa_x;x++)
			{
				if(Mapa[x,y].Type() == "Wall")
				{
					Mapa[x,y].Prefab.transform.position = new Vector3(Mapa[x,y].Prefab.transform.position.x,Mapa[x,y].Prefab.transform.position.y,Mapa[x,y].Prefab.transform.position.y);
				}
				else
				{
					Mapa[x,y].Prefab.transform.position = new Vector3(Mapa[x,y].Prefab.transform.position.x,Mapa[x,y].Prefab.transform.position.y,500);
				}
			}
		}
	}

	void New_Spawn(Vector3 pos)
	{
		Spawn_point s = new Spawn_point (pos, this);
		spawns.Add (s);
	}

	void Create_Spawns()
	{
		while (mapData.spawn_count > 0 || counter > 0) {
			for (int y = 0; y < Mapa_y; y++) 
				for (int x = 0; x < Mapa_x; x++) 
					if (Mapa [x, y].Type() == "Floor" && mapData.spawn_count > 0 && !pos_spawns.Contains(Mapa [x, y].Prefab.transform.position) /*&& Mapa [x, y].Prefab.transform.position.x > pos_inicial.x + 1 * 0.32f && Mapa [x, y].Prefab.transform.position.x < pos_inicial.x - 1 * 0.32f && Mapa [x, y].Prefab.transform.position.y > pos_inicial.y + 1 * 0.32f && Mapa [x, y].Prefab.transform.position.y < pos_inicial.y - 1 * 0.32f*/)						
						if (UnityEngine.Random.Range (1, 101) > 98) {
							New_Spawn (Mapa [x, y].Prefab.transform.position);
							mapData.spawn_count--;
							pos_spawns.Add (Mapa [x, y].Prefab.transform.position);
						}
			counter--;
		}
 	}

	void Spawn_Enemies()
	{
		while(spawns.Count != 0)
		{
			spawns [0].Spawn ();
			spawns.RemoveAt (0);
		}
	}

	void CreateRoom(int posX, int posY)
	{
		for (int y = posY - n; y < posY + n; y++)
			for (int x = posX - n; x < posX + n; x++)				
				Mapa [x, y].Index = "1a";
	}

}