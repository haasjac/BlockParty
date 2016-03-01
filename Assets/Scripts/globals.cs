using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class globals {
    
    private static globals _s = null;

    public int NUM_LEVELS;
    public List<bool> levelLocked;
    //public List<int> levelStars;

    private globals() {
        NUM_LEVELS = SceneManager.sceneCountInBuildSettings - 2;
        levelLocked = new List<bool>(NUM_LEVELS + 1);
        for (int i = 0; i <= NUM_LEVELS; i++)
            levelLocked.Add(false);
        load();
    }

    public static globals S
    {
        get
        {
            if (_s == null)
                _s = new globals();
            return _s;
        }
    }

    public void save() {
        //levelLocked
        for (int i = 1; i <= NUM_LEVELS; i++) {
            string key = "levelLocked" + i.ToString();
            int value = (levelLocked[i] ? 1 : 0);
            PlayerPrefs.SetInt(key, value);
        }

        //save
        PlayerPrefs.Save();
    }

    public void load() {
        //levelLocked
        PlayerPrefs.SetInt("levelLocked1", 0);
        levelLocked[1] = false;
        for (int i = 2; i <= NUM_LEVELS; i++) {
            string key = "levelLocked" + i.ToString();
            if (!PlayerPrefs.HasKey(key)) {
                PlayerPrefs.SetInt(key, 1);
            }
            levelLocked[i] = (PlayerPrefs.GetInt(key) == 1 ? true : false);
        }
    }
}
