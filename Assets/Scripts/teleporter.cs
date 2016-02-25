using UnityEngine;
using System.Collections;

public class teleporter : MonoBehaviour {

    public GameObject other_portal;
    [HideInInspector]
    public bool can_switch = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision coll) {
        if (can_switch && coll.gameObject.tag == "player") {
            other_portal.GetComponent<teleporter>().can_switch = false;
            coll.gameObject.transform.position = other_portal.transform.position;
        }
    }

    void OnCollisionExit(Collision coll) {
        if (coll.gameObject.tag == "player") {
            can_switch = true;
        }
    }
}
