using UnityEngine;
using System.Collections;

public class Drop_Interaction : MonoBehaviour {

	void OnTriggerStay2D(Collider2D c)
    { 		
		Map m = GameObject.Find ("Code").GetComponent<Map> ();
		
		for (int i = 0; i < m.drops.Count; i++) {
			if (c == m.drops[i].GetComponent<BoxCollider2D> () && Input.GetKeyDown(KeyCode.E)) {
				Debug.Log ("agarro arma");
		}
			//c.gameObject.GetComponent<Drop> ().Equip (this.GetComponentInParent<Mob_Human> ());
		}
    }
}
