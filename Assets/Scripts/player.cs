using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

  // ==[members]================================================================
  // ===========================================================================

  // movement
  Rigidbody body;
  public int direction;
  public float speed;
  
  // jump
  public float thrust;
  public int jump_triggers;
  public int jump_frame;

  // states
  public bool initial_fall;
  public bool grounded;

  // sprites
  public Sprite[] sprites;
  SpriteRenderer sprite_renderer;

  // ==[start]==================================================================
  // ===========================================================================

	void Start(){

    // set initial variables for both players
    thrust = 300f;
    jump_triggers = 0;
    jump_frame = 0;
    initial_fall = true;

    // grab components
    sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
    body = gameObject.GetComponent<Rigidbody>();

    // set variables specific to red (currently the speed, but with the option to differentiate)
    if(this.name == "red_player"){
      sprites = Resources.LoadAll<Sprite>("red_monster");
      speed = 2f;
      direction = 1;
      sprite_renderer.flipX = true;
    }

    // set variables specific to blue (currently the speed, but with the option to differentiate)
    else{
      sprites = Resources.LoadAll<Sprite>("blue_monster");
      speed = 2f;
      direction = -1;
      sprite_renderer.flipX = false;
    }

    sprite_renderer.sprite = sprites[1];

	}

  // ==[update]=================================================================
  // ===========================================================================

  void FixedUpdate(){

    // don't apply horizontal movement to the initial fall
    if(!initial_fall){
      Movement();
    }

  }

  // ==[actions]================================================================
  // ===========================================================================

  void Movement(){

    // delcare vairables
    float jump_speed = 1.25f;
    float modifications = 1f;
    
    // adjust speed if in the air, and apply jump animation
    if(!grounded){
      modifications = jump_speed / speed;
      JumpAnimation(17, 25);
    }
    
    // apply movement
    transform.localPosition += transform.right * direction * speed * modifications * Time.fixedDeltaTime;
  }

  void Toggle(){

    // switch direction of movement and sprite
    direction *= -1;
    sprite_renderer.flipX = !sprite_renderer.flipX;

  }

  void Jump(int count_delta){

    // shift jump trigger count by delta
    jump_triggers += count_delta;

    // if currently touching both left and right jump triggers, apply thrust
    if(jump_triggers == 2){
      jump_triggers = 0;
      body.AddForce(transform.up * thrust);
    }

    // maintain >= 0 count
    if(jump_triggers <= 0){
      jump_triggers = 0;
    }

  }

  void Landing(){

    jump_frame = 0; // reset frames for animation
    grounded = true; // set position as grounded
    sprite_renderer.sprite = sprites[0]; // set sprite back to normal
    initial_fall = false; // sets initial_fall false forever

  }

  void JumpAnimation(int shift_1, int shift_2){
    
    // before peak
    if(jump_frame >= 0 && jump_frame < shift_1){
      sprite_renderer.sprite = sprites[2];
    }
    
    // peak
    else if(jump_frame >= shift_1 && jump_frame < shift_2){
      sprite_renderer.sprite = sprites[0];
    }
    
    // after peak
    else{
      sprite_renderer.sprite = sprites[1];
    }
    
    // increment jump_frame
    jump_frame += 1;

  }

  // ==[collisions & triggers]==================================================
  // ===========================================================================

  void OnCollisionEnter(Collision coll){
    GameObject other = coll.gameObject;

    // landing from a jump
    if(other.tag == "ground"){
      Landing();
    }

    // switch direction except if hitting jump block
    else if(other.tag != "jump_ground"){
      Toggle();
    }
  }

  void OnCollisionExit(Collision coll){
    GameObject other = coll.gameObject;

    // leaving ground from jump
    if(other.tag == "ground"){
      grounded = false;
    }
  }

  void OnTriggerEnter(Collider coll){
    GameObject other = coll.gameObject;

    // hitting a jump trigger pane, increment jump_trigger count
    if(other.tag == "jump"){
      Jump(1);
    }

  }

  void OnTriggerExit(Collider coll){
    GameObject other = coll.gameObject;

    // leaving a jump trigger pane, decrement jump_trigger count
    if(other.tag == "jump"){
      Jump(-1);
    }

  }

}
