using UnityEngine;
using System.Collections;

public class tiling : MonoBehaviour {

	// ==[start]================================================================
	// =========================================================================

	void Start(){

		// get number of tiles required, based on size
		float x = gameObject.GetComponent<Transform>().lossyScale.x / 0.6f;
		float y = gameObject.GetComponent<Transform>().lossyScale.y / 0.6f;

		// set texture tiling for specific instance
		gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(x, y);

		// set offset if not exact multiples of 0.6
		float mod = x % 1;
		if(mod < 0.99 && mod != 0){
		  gameObject.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0.5f, 1f);
		}
	
	}

}
