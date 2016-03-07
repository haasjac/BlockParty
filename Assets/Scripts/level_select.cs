using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class level_select : MonoBehaviour {

    public Scrollbar sb;
    public GameObject page_prefab;
    public GameObject Levels;
    public List<GameObject> pages;
    public List<Button> levels;

	// Use this for initialization
	void Start () {
        sb.numberOfSteps = Mathf.CeilToInt(globals.S.NUM_LEVELS / 4.0f);
        if (sb.numberOfSteps < 2) {
            sb.gameObject.SetActive(false);
        }
        for (int i = 0; i < sb.numberOfSteps; i++) {
            GameObject go = Instantiate<GameObject>(page_prefab);
            go.transform.SetParent(Levels.transform, false);
            Vector3 pos = Vector3.zero;
            pos.x = i * 1600;
            go.GetComponent<RectTransform>().anchoredPosition = pos;
            pages.Add(go);
            int j = 1;
            foreach (Button b in go.GetComponentsInChildren<Button>()) {
                int n = ((i * 4) + j);
                b.GetComponent<level_select_button>().level_num = n;
                b.GetComponentInChildren<Text>().text = n.ToString() + "\n\nStars: " + globals.S.levelStars[n];
                if (n > globals.S.NUM_LEVELS) {
                    b.gameObject.SetActive(false);
                } else {
                    j++;
                    levels.Add(b);
                }
            }
        }
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
        int delta = Mathf.RoundToInt((sb.value * (sb.numberOfSteps - 1))) * -1600;
        foreach (GameObject go in pages) {
            Vector3 pos = go.GetComponent<RectTransform>().anchoredPosition;
            pos.x = baseX + delta;
            baseX += 1600;
            go.GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }
}
