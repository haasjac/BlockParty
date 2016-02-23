using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class controller : MonoBehaviour {

	// ==[start & update]=========================================================
  // ===========================================================================

	void Start(){
		Time.timeScale = 0;
	}
	
	void Update(){

		// restart
		if(Input.GetKeyDown(KeyCode.Escape)) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		// start
		if(Input.GetKeyDown(KeyCode.Return)) {
			Time.timeScale = 1;
		}

	}
}
