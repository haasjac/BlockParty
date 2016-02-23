using UnityEngine;
using System.Collections;

public class blink : MonoBehaviour {

  // ==[members]================================================================
  // ===========================================================================

  public float duration;
  public float alpha;
  Renderer switch_renderer;

  // ==[start & update]=========================================================
  // ===========================================================================

  void Start(){
    switch_renderer = gameObject.GetComponent<Renderer>();
  }
  
  void Update(){

    duration = 0.7f;
    alpha = Mathf.Lerp(0.4f, 1f, Mathf.PingPong(Time.time, duration) / duration);

    Color blinker_color = switch_renderer.material.color;
    blinker_color.a = alpha;
    switch_renderer.material.color = blinker_color;

  }

}
