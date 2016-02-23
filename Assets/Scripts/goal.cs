using UnityEngine;
using System.Collections;

public class goal : MonoBehaviour {

  // ==[members]================================================================
  // ===========================================================================

  // sprites
  public int frames_per_sprite;
  public Sprite[] sprites;
  SpriteRenderer sprite_renderer;

  // counters
  int i;
  int frame_count;
  bool up;

  // ==[start]==================================================================
  // ===========================================================================

	void Start(){

    // declare variables
    up = true;
    i = 0;
    frame_count = 0;
    frames_per_sprite = 5;

    // grab components
    sprite_renderer = gameObject.GetComponent<SpriteRenderer>();

    // get sprite collection
    if(this.name == "red_goal"){
      sprites = Resources.LoadAll<Sprite>("red_goal");
    }
    else if(this.name == "blue_goal"){
      sprites = Resources.LoadAll<Sprite>("blue_goal");
    }
    else{
      sprites = Resources.LoadAll<Sprite>("normal_goal");
    }

	}

  // ==[update]=================================================================
  // ===========================================================================
	
	// Update is called once per frame
	void Update(){

    // increment frame count
    if(frame_count < frames_per_sprite){
      frame_count += 1;
    }

    // change sprite
    else{

      // go up the array
      if(up){
        if(i < 5){
          i += 1;
        }
        else{
          i = 5;
          up = false;
        }
      }

      // go down the array
      else{
        if(i > 0){
          i -= 1;
        }
        else{
          i = 0;
          up = true;
        }
      }

      // apply the sprite and reset the frame count
      sprite_renderer.sprite = sprites[i];
      frame_count = 0;

    }
	}

  // ==[collisions]=============================================================
  // ===========================================================================

  void OnCollisionEnter(Collision coll){
    GameObject other = coll.gameObject;

    if(other.tag == "player"){
      other.GetComponent<player>().Goal();
    }

  }



}
