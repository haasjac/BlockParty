using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class button : MonoBehaviour {

	public UnityEvent ue;

	// Use this for initialization
	void Start () {
		if (ue == null) {
			ue = new UnityEvent();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider coll) {
		print(coll.gameObject.name);
		if (coll.gameObject.tag == "Player") {
			ue.Invoke();
		}
	}
	
}
