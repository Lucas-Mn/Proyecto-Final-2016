using UnityEngine;
using System.Collections;

public class CharClick : MonoBehaviour {
	CharSelect cs;
	void Awake()
	{
		try{cs = this.GetComponentInParent<CharSelect> ();}

		catch{Debug.Log("Error en la inicializacion del objeto");}
	}
	void OnMouseUp()
	{
		Debug.Log (this.name);
		cs.ZoomIn (this.name);
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds (1);
	}
}
