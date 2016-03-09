using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class time_controller : MonoBehaviour {

    public Button pause_button;
    public Button half_button;
    public Button normal_button;
    public Button double_button;
    public Button last_button;
    public Color green;
    public Color red;
    public Color default_color = Color.white;
    public float desired_timescale;
    public bool paused;

// ==[start & update]=========================================================
// ===========================================================================

    void Start(){
        desired_timescale = 1;
        Time.timeScale = 0;
        paused = true;
        pause_button.image.color = red;
        normal_button.image.color = green;
        last_button = normal_button;
    }
	
    void Update(){

        if (!paused) {
            Time.timeScale = desired_timescale;
        } else {
            Time.timeScale = 0;
        }

        // restart
        if(Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Escape)) {
            reset();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            toggle();
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            if (last_button == half_button) {
                setTimescale(1);
                setColor(normal_button);
            } else if (last_button == normal_button) {
                setTimescale(2);
                setColor(double_button);
            }
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            if (last_button == double_button) {
                setTimescale(1);
                setColor(normal_button);
            } else if (last_button == normal_button) {
                setTimescale(0.5f);
                setColor(half_button);
            }
        }
    }

    public void setTimescale(float t) {
        desired_timescale = t;
    }

    public void setColor(Button b){

        if (b == last_button){
            return;
        }
        b.image.color = green;
        last_button.image.color = default_color;
        last_button = b;
    }

    public void toggle() {
        if (paused) {
            paused = false;
            pause_button.image.color = green;
            pause_button.GetComponentInChildren<Text>().text = "||";
        } else {
            paused = true;
            pause_button.image.color = red;
            pause_button.GetComponentInChildren<Text>().text = ">";
        }
    }

    public void reset() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void menu() {
        SceneManager.LoadScene("_MainM_-1");
    }

}
