using UnityEngine;
using System.Collections;

public class blink : MonoBehaviour {

  public float duration;
  Renderer switch_renderer;

  void Start(){

    duration = 1f;
    switch_renderer = gameObject.GetComponent<Renderer>();

  }
  
  void Update(){

    float lerp = Mathf.PingPong(Time.time, duration) / duration;
    float alpha = Mathf.Lerp(.3f, .5f, lerp);

    Color blinker_color = switch_renderer.material.color;
    blinker_color.a = alpha;
    switch_renderer.material.color = blinker_color;

  }

}
