using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class controller : MonoBehaviour {

	public Button pause_button;
  public Button last_button;
  public Color green;
  public Color red;
  public Color default_color = Color.white;

	// ==[start & update]=========================================================
  // ===========================================================================

	void Start(){
		Time.timeScale = 0;
    pause_button.image.color = red;
    last_button = pause_button;
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

	public void setTimescale(float t) {
    Time.timeScale = t;
  }

  public void setColor(Button b){
    if (b == last_button){
      return;
    }
    if (b == pause_button){
      b.image.color = red;
    }
  	else{
        b.image.color = green;
    }
    last_button.image.color = default_color;
    last_button = b;
  }

}
