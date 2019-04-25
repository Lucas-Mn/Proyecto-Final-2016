using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Play_Button : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");
	}
}
