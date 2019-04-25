using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUI : MonoBehaviour {
	// Health bar
	// Stamina bar
	GameObject c;
	GameObject dialogPanel;
	GameObject dialogPanelPortrait;
	GameObject dialogPanelText;
	Map m;
	int phraseN;
	int letterN;
	string text;
	string[] textSplit;
	string textShown;
	bool done;
	void Awake()
	{
		done = false;
		text = "sdkjlsahdkahdjksahdjksaldhajkl jsdklñajdklsañdjasklñdjasklñdjsa kdlñsajkldñsajdklsañjdklñas sdjakldñjsakldjasklñdjsaklñ djskalñdslñkdsajkldjkldsj";
		textSplit = text.Split (' ');
		phraseN = 0;
		letterN = 0;
		m = gameObject.GetComponentInParent<Map> ();
		if(m.mapData.bossLevel)
		{
		c = Resources.Load ("UI/Canvas") as GameObject;
		c = GameObject.Instantiate (c);
		dialogPanel = c.transform.GetChild (0).gameObject;
		dialogPanelPortrait = dialogPanel.transform.GetChild (0).gameObject;
		dialogPanelText = dialogPanel.transform.GetChild (4).gameObject;
//
//		dialogPanel.GetComponent<RectTransform> ().sizeDelta = new Vector2 (Screen.width, Screen.height / 4);
//
//		dialogPanelPortrait.GetComponent<RectTransform> ().sizeDelta = new Vector2 (40, 40);
//		dialogPanelPortrait.GetComponent<RectTransform> ().position = new Vector3 (Screen.width / 10, Screen.height / 4 - Screen.height / 5, 0);
			StartCoroutine("ShowText");
		}
	}

	void Update(){
		if (done && dialogPanel.activeSelf) {
			dialogPanel.SetActive (false);
		}
	}

	/// <summary>
	/// Shows the text.
	/// </summary>
	/// <returns>The text.</returns>
	IEnumerator ShowText()
	{
		for (int i = 0; i < textSplit.Length; i++){
			yield return new WaitForSeconds (1);
			for (int j = 0; j < textSplit [i].Length; j++) {
				dialogPanelText.GetComponent<Text> ().text = textSplit [i].Substring (0, j);
				yield return new WaitForSeconds (.05f);
			}
		}
		done = true;
	}
}
