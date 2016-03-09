using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class level_select_button : MonoBehaviour {

    public int level_num;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public void level_select() {
        if (level_num < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene("_Level_" + (level_num).ToString());
        } else {
            SceneManager.LoadScene("_MainM_-1");
        }
    }
}
