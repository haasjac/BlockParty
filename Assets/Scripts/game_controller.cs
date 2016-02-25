using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class game_controller : MonoBehaviour {

    public List<GameObject> players;
    public GameObject win_screen;

    bool win;


	// Use this for initialization
	void Start () {
        win_screen.SetActive(false);
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
        print("next level");
    }
}
