using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class game_controller : MonoBehaviour {

    public List<GameObject> players;
    public GameObject win_screen;
    //string scene;
    int scene_num;

    bool win;
    bool saved;


	// Use this for initialization
	void Start () {
        win_screen.SetActive(false);
        scene_num =  int.Parse(SceneManager.GetActiveScene().name.Substring(7, SceneManager.GetActiveScene().name.Length - 7));
        saved = false;
    }
	
	// Update is called once per frame
	void Update () {
        win = true;
	    foreach (GameObject go in players) {
            if (!go.GetComponent<player>().pause)
                win = false;
        }
        if (win) {
            if (!saved) {
                if (scene_num + 1 <= globals.S.NUM_LEVELS)
                    globals.S.levelLocked[scene_num + 1] = false;
                globals.S.save();
                saved = true;
            }
            win_screen.SetActive(true);
        }
	}

    public void playAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void nextLevel() {
        if (scene_num + 1 <= globals.S.NUM_LEVELS) {
            SceneManager.LoadScene("_Level_" + (scene_num + 1).ToString());
        } else {
            SceneManager.LoadScene("_MainM_-1");
        }
    }
}
