using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class game_controller : MonoBehaviour {

    public List<GameObject> players;
    public GameObject win_screen;
    string scene;
    int scene_num;

    bool win;


	// Use this for initialization
	void Start () {
        win_screen.SetActive(false);
        scene = SceneManager.GetActiveScene().name;
        scene_num =  int.Parse(scene.Substring(7,scene.Length - 7));
    }
	
	// Update is called once per frame
	void Update () {
        win = true;
	    foreach (GameObject go in players) {
            if (!go.GetComponent<player>().pause)
                win = false;
        }
        if (win) {
            win_screen.SetActive(true);
        }
	}

    public void playAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void nextLevel() {
        if (SceneManager.GetSceneByName("_Level_" + (scene_num + 1).ToString()).IsValid()) {
            SceneManager.LoadScene("_Level_" + (scene_num + 1).ToString());
        } else {
            SceneManager.LoadScene("_Main_Menu");
        }
    }
}
