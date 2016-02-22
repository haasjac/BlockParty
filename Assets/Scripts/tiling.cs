using UnityEngine;
using System.Collections;

public class tiling : MonoBehaviour {

	void Start(){

    // get number of tiles required, based on size (size must be multiple of 0.6)
    float x = gameObject.GetComponent<Transform>().lossyScale.x / 0.6f;
    float y = gameObject.GetComponent<Transform>().lossyScale.y / 0.6f;

    // set texture tiling for specific instance
    gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(x, y);
    
	}

}
