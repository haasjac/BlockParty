using UnityEngine;
using System.Collections;

public class teleporter : MonoBehaviour {

	[HideInInspector]
	public GameObject other_portal;
	public bool can_switch = true;

	void OnTriggerEnter(Collider coll){
		if(can_switch && coll.gameObject.tag == "player") {
			other_portal.GetComponent<teleporter>().can_switch = false;
			coll.gameObject.transform.position = other_portal.transform.position;
		}
	}

	void OnTriggerExit(Collider coll) {
		if(coll.gameObject.tag == "player") {
			can_switch = true;
		}
	}
}
