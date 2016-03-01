using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class level_select : MonoBehaviour {

    public Scrollbar sb;
    public List<GameObject> pages;
    public List<Button> levels;

	// Use this for initialization
	void Start () {
        sb.numberOfSteps = Mathf.CeilToInt(globals.S.NUM_LEVELS / 4);
	}
	
	// Update is called once per frame
	void Update () {
        int i = 1;
        foreach (Button b in levels) {
            b.interactable = !globals.S.levelLocked[i];
            i++;
        }

        if (Input.GetKeyDown(KeyCode.U)) {
            for (int j = 2; j < globals.S.levelLocked.Count; j++)
                globals.S.levelLocked[j] = false;
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            for (int j = 2; j < globals.S.levelLocked.Count; j++)
                globals.S.levelLocked[j] = true;
        }
    }

    public void changeScreen() {
        int baseX = 0;
        int delta = Mathf.RoundToInt(sb.value) * -1600;
        foreach (GameObject go in pages) {
            Vector3 pos = go.GetComponent<RectTransform>().anchoredPosition;
            pos.x = baseX + delta;
            baseX += 1600;
            go.GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }

    public void selectLevel(int n) {
        if (n < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene("_Level_" + (n).ToString());
        } else {
            SceneManager.LoadScene("_MainM_0");
        }
    }
}
