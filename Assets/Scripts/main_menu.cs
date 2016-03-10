using UnityEngine;
using System.Collections;

public class main_menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
	}

    static bool AudioBegin = false;
    void Awake()
    {
        if (!AudioBegin)
        {
            this.GetComponent<AudioSource>().Play();
            DontDestroyOnLoad(gameObject);
            AudioBegin = true;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
