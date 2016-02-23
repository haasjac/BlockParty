using UnityEngine;
using System.Collections;

public class elevator : MonoBehaviour {

	// ==[members]================================================================
  // ===========================================================================

	// parameters
	public float distance = 1.8f;
	public float speed = 2f;
	public bool up = true;

	// movement information
	Vector3 start;
	Vector3 target;
	float startTime;
	float journeyLength;
	bool transitioning;

	// ==[start]==================================================================
  // ===========================================================================

	void Start(){

		// initialize variables
		transitioning = false;
		start = transform.position;

		// set target position
		if(up){
			target = start + distance * Vector3.up;
		}
		else{
			target = start + distance * Vector3.down;
		}
		
	}

	// ==[update]=================================================================
  // ===========================================================================
	
	void Update(){
		Move();
	}

	// ==[actions]================================================================
  // ===========================================================================

	public void Transition(){

		// sets movement variables
		if(!transitioning && Time.timeScale != 0){
			transitioning = true;
			startTime = Time.time;
			journeyLength = Mathf.Abs(start.y - target.y);
		}

	}

	void Move(){

		// only move if in transition state
		if(transitioning){

			// initialize movement variables
			Vector3 pos = transform.position;
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;

			// lerp position
			pos.y = Mathf.Lerp(start.y, target.y, fracJourney);
			transform.position = pos;

			// check if target has been reached
			if(pos == target){
				transitioning = false;
				pos = start;
				start = target;
				target = pos;
			}
		}

	}

	// ==[events]=================================================================
  // ===========================================================================
	
	void OnMouseDown(){
		Transition();
	}



}
