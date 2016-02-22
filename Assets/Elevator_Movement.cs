using UnityEngine;
using System.Collections;

public class Elevator_Movement : MonoBehaviour {

    public bool up = true;
    public float distance = 2f;
    public float speed = 0.1f;

    Vector3 start;
    Vector3 target;
    float startTime;
    float journeyLength;
    bool transitioning;


	// Use this for initialization
	void Start () {
        transitioning = false;
        start = transform.position;
        if (up) {
            target = start + distance * Vector3.up;
        } else {
            target = start + distance * Vector3.down;
        }
        
    }
	
	// Update is called once per frame
	void Update () {

        if (transitioning) {
            Vector3 pos = transform.position;
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            pos.y = Mathf.Lerp(start.y, target.y, fracJourney);
            transform.position = pos;
            if (pos == target) {
                transitioning = false;
                pos = start;
                start = target;
                target = pos;
            }
        }

    }

    void OnMouseDown() {
        if (Time.timeScale != 0) {
            transitioning = true;
            startTime = Time.time;
            journeyLength = Mathf.Abs(start.y - target.y);
        }
    }
}
