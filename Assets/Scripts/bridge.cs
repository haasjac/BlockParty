using UnityEngine;
using System.Collections;

public class bridge : MonoBehaviour{

	// ==[members]================================================================
	// ===========================================================================

	// states
	public bool clickable = true;

	// game objects and settings
	GameObject target_bridge;

	// ==[start]==================================================================
	// ===========================================================================

	void Start(){

		// get target_wall
		target_bridge = gameObject.transform.parent.gameObject.transform.GetChild(0).gameObject;

		// if not clickable, don't show switch
		if (!clickable){
			gameObject.SetActive(false);
		}

	}

	// ==[actions]================================================================
	// ===========================================================================

	// TODO: appear/disappear animation to make it ~juicy~
	void Toggle(){

		// currently a simple active true/false toggle
		target_bridge.SetActive(!target_bridge.activeInHierarchy);

	}

	// ==[events]=================================================================
	// ===========================================================================

	void OnMouseDown(){

		// toggle target bridge based on click
		Toggle();

	}

}