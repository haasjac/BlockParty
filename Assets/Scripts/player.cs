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

  // states
  public bool initial_fall;
  public int grounded;

  // sprites
  public Sprite[] sprites;
  SpriteRenderer sprite_renderer;

  // ==[start]==================================================================
  // ===========================================================================

	void Start(){

    // set initial variables for both players
    initial_fall = true;
    grounded = 0;
    jump_triggers = 0;
    speed = 2f;
    thrust = 300f;

    // grab components
    sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
    body = gameObject.GetComponent<Rigidbody>();

    // set variables specific to red (currently the speed, but with the option to differentiate)
    if(this.name == "red_player"){
      sprites = Resources.LoadAll<Sprite>("red_monster");
      sprite_renderer.flipX = true;
      direction = 1;
    }

    // set variables specific to blue (currently the speed, but with the option to differentiate)
    else{
      sprites = Resources.LoadAll<Sprite>("blue_monster");
      sprite_renderer.flipX = false;
      direction = -1;
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
    float jump_speed = 1.5f;
    float modifications = 1f;
    
    // adjust speed if in the air, and apply jump animation
    if(grounded == 0){
      modifications = jump_speed / speed;
      JumpAnimation();
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

  void Death(){

     // TODO: death animations
     Destroy(this.gameObject);

  }

  void Landing(){

    grounded += 1; // set position as grounded
    sprite_renderer.sprite = sprites[0]; // set sprite back to normal
    initial_fall = false; // sets initial_fall false forever

  }

  void JumpAnimation(){

    float peak = 0.75f;
    
    // rising
    if(body.velocity.y > peak){
      sprite_renderer.sprite = sprites[2];
    }
    
    // peak
    else if(body.velocity.y <= peak && body.velocity.y >= -peak){
      sprite_renderer.sprite = sprites[0];
    }
    
    // falling
    else{
      sprite_renderer.sprite = sprites[1];
    }

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

      // if spike, kill player
      if(other.tag == "normal_spike" || other.tag == "blue_spike" || other.tag == "red_spike" ){
        Death();
      }

      // swap direction
      else{
        Toggle();
      }
      
    }

  }

    void OnCollisionExit(Collision coll){
    GameObject other = coll.gameObject;

    // leaving ground from jump
    if(other.tag == "ground"){
      grounded -= 1;
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
