﻿using UnityEngine;
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
                if (n > globals.S.NUM_LEVELS) {
                    b.gameObject.SetActive(false);
                } else { 
                    b.GetComponent<level_select_button>().level_num = n;
                    b.GetComponentInChildren<Text>().text = n.ToString();
                    if (globals.S.levelStars[n] == 3) {
                        b.GetComponent<level_select_button>().star1.gameObject.SetActive(true);
                        b.GetComponent<level_select_button>().star2.gameObject.SetActive(true);
                        b.GetComponent<level_select_button>().star3.gameObject.SetActive(true);
                    } else if (globals.S.levelStars[n] == 2) {
                        b.GetComponent<level_select_button>().star1.gameObject.SetActive(true);
                        b.GetComponent<level_select_button>().star2.gameObject.SetActive(true);
                        b.GetComponent<level_select_button>().star3.gameObject.SetActive(false);
                    } else if (globals.S.levelStars[n] == 1) {
                        b.GetComponent<level_select_button>().star1.gameObject.SetActive(true);
                        b.GetComponent<level_select_button>().star2.gameObject.SetActive(false);
                        b.GetComponent<level_select_button>().star3.gameObject.SetActive(false);
                    } else {
                        b.GetComponent<level_select_button>().star1.gameObject.SetActive(false);
                        b.GetComponent<level_select_button>().star2.gameObject.SetActive(false);
                        b.GetComponent<level_select_button>().star3.gameObject.SetActive(false);
                    }
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
            globals.S.save();
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            for (int j = 2; j < globals.S.levelLocked.Count; j++)
                globals.S.levelLocked[j] = true;
            for (int j = 0; j < globals.S.levelStars.Count; j++) {
                globals.S.levelStars[j] = 0;
            }
            globals.S.save();
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
