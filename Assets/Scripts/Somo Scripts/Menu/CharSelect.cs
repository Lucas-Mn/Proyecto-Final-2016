using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour {

	bool done;
	public float speed = 0.06f;
	Image background;
	Sprite[] charSelectFrames;
	Sprite activeFrame;
	//GameObject panel;
	RectTransform panel;
	GameObject c;
	Dictionary<string, Vector3> cameraPos = new Dictionary<string, Vector3>();
	Dictionary<string, Vector3> panelPos = new Dictionary<string, Vector3>();
	// Use this for initialization
	void Start () {
		charSelectFrames = Resources.LoadAll<Sprite> ("Menu/Char Select/Backgrounds/");
		StartCoroutine ("CharSelectAnimation");
		FillCameraPos ();
		FillPanelPos ();
		c = GameObject.FindGameObjectWithTag ("MainCamera");
		panel = GameObject.FindGameObjectWithTag ("Panel").GetComponent<RectTransform>();
		panel.gameObject.SetActive (false);
	}

	void FillCameraPos()
	{
		cameraPos.Add ("Jon", new Vector3(-4.09f, 1.36f, -10));
		cameraPos.Add ("Fiora", new Vector3(3.51f, 1.15f, -10));
		cameraPos.Add ("Bormosh", new Vector3(-0.89f, 0.91f, -10));
		cameraPos.Add ("Randal", new Vector3(1.21f, -2.25f, -10));
		cameraPos.Add ("Gurd", new Vector3(-3.89f, -2.25f, -10));
	}

	void FillPanelPos()
	{
		panelPos.Add ("Jon", new Vector3 (4.61f, -3.67f, 0));
		panelPos.Add ("Fiora", new Vector3 (12.27f, -3.77f, 0));
		panelPos.Add ("Bormosh", new Vector3 (7.87f, -4.01f, 0));
		panelPos.Add ("Randal", new Vector3 (9.91f, -7.17f, 0));
		panelPos.Add ("Gurd", new Vector3 (4.91f, -7.22f, 0));
	}
	// Update is called once per frame
	void Update () {
		if (done) {
			done = false;
			StartCoroutine ("CharSelectAnimation");
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			c.transform.position = new Vector3 (0, 0, -10);
			c.GetComponent<Camera> ().orthographicSize = 5f;
			panel.gameObject.SetActive (false);
		}
	}

	IEnumerator CharSelectAnimation()
	{
		for (int i = 0; i < charSelectFrames.Length; i++) {
			this.GetComponent<Image>().sprite = charSelectFrames [i];
			yield return new WaitForSeconds (speed);
		}
		done = true;
	}

	public void ZoomIn(string character)
	{
		c.transform.position = cameraPos [character];
		c.GetComponent<Camera> ().orthographicSize = 2.620265f;
		panel.gameObject.SetActive (true);
		panel.position = panelPos[character] ;

	}
}
