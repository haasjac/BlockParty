using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class button : MonoBehaviour {

	// ==[members]================================================================
  // ===========================================================================

	// target objects and functions
	public GameObject[] targets;
	public UnityEvent ue;

	// ==[start]==================================================================
  // ===========================================================================

	void Start(){
		// null handler
		if(ue == null) {
			ue = new UnityEvent();
		}
	}

	// ==[triggers]===============================================================
  // ===========================================================================

	void OnTriggerEnter(Collider coll){

		// emulate button being pressed
		gameObject.SetActive(false);

		// run list of target functions (elevator)
		ue.Invoke();

		// toggle set of targets (walls, bridges, spikes, buttons)
		for(int i = 0; i < targets.Length; ++i){
			GameObject target = targets[i];
			target.SetActive(!target.activeInHierarchy);
		}
	}

	void OnTriggerExit(Collider coll){
		// emulate button being unpressed
		gameObject.GetComponent<Renderer>().enabled = true;
	}
	
}
