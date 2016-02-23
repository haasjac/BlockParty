using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Switch_Behaviour : MonoBehaviour {

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
        if (coll.gameObject.tag == "Player") {
            ue.Invoke();
        }
    }
}
