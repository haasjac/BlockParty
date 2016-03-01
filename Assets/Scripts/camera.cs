using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {

  public float speed = 1e-10f;
	
	void Update(){

    if(Input.GetKey(KeyCode.RightArrow)){
      transform.localPosition += transform.right * speed * Time.deltaTime;
    }
    if(Input.GetKey(KeyCode.LeftArrow)){
      transform.localPosition += transform.right * speed * Time.deltaTime * -1;
    }
    if(Input.GetKey(KeyCode.UpArrow)){
      transform.localPosition += transform.up * speed * Time.deltaTime;
    }
    if(Input.GetKey(KeyCode.DownArrow)){
      transform.localPosition += transform.up * speed * Time.deltaTime * -1;
    }

	}

}
