using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class player : MonoBehaviour {

  // ==[members]================================================================
  // ===========================================================================

  // movement
  Rigidbody body;
  public int start_direction = 1;
  int direction;
  public float speed;
  Vector3 start_pos;
  
  // jump
  public float thrust;
  public int jump_triggers;

  // states
  public bool initial_fall;
  public bool pause;
  public int grounded;
  public bool enemy;

  // sprites
  public Sprite[] sprites;
  SpriteRenderer sprite_renderer;

  // items
  public enum Item {none, key, sword};
  public Item item_hold_type;
  public GameObject item_hold;
  public SpriteRenderer item_display;
  public List<GameObject> stars = new List<GameObject>();

  // ==[start]==================================================================
  // ===========================================================================

	void Start(){

    // set initial variables for both players
    initial_fall = true;
    pause = false;
    grounded = 0;
    jump_triggers = 0;
    speed = 2f;
    thrust = 300f;
    start_pos = transform.position;
    enemy = false;

    // grab components
    sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
    body = gameObject.GetComponent<Rigidbody>();

    // 

    // set variables specific to red (currently the speed, but with the option to differentiate)
    if(this.name == "red_player"){
      sprites = Resources.LoadAll<Sprite>("red_monster");
      item_display = gameObject.gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
    }

    // set variables specific to blue (currently the speed, but with the option to differentiate)
    else if(this.name == "blue_player"){
      sprites = Resources.LoadAll<Sprite>("blue_monster");
      item_display = gameObject.gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
    }

    else{
      sprites = Resources.LoadAll<Sprite>("enemy_monster");
      enemy = true;
      item_display = null;
    }

    direction = start_direction;
    if(direction == 1){
        sprite_renderer.flipX = true;
    }
    else{
      sprite_renderer.flipX = false;
    }

    sprite_renderer.sprite = sprites[0];
    item_hold = null;
	}


  // ==[update]=================================================================
  // ===========================================================================

  void FixedUpdate(){

    if(!pause){
      // don't apply horizontal movement to the initial fall
      if(!initial_fall){
        Movement();
      }
      else{
        JumpAnimation();
      }
    }

    if(!enemy){
      if(item_hold_type != Item.none){
        item_display.sprite = item_hold.GetComponent<SpriteRenderer>().sprite;
      }
      else{
        item_display.sprite = null;
      }
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

    initial_fall = true;
    transform.position = start_pos;
    direction = start_direction;
    if(direction == 1){
      sprite_renderer.flipX = true;
    }
    else{
      sprite_renderer.flipX = false;
    }
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

  public void Goal(){
    
    // stop movement
    pause = true;

    // make sprite slightly transparent
    Color sprite_color = sprite_renderer.color;
    sprite_color.a = 0.8f;
    sprite_renderer.color = sprite_color;
  }

  void PickUpItem(GameObject item){

    if(item.tag == "star"){
      item.SetActive(false);
      stars.Add(item);
    }

    else if(item_hold == null){

      item.SetActive(false);
      item_hold = item;

      if(item.tag == "key"){
        item_hold_type = Item.key;
      }
      else{
        item_hold_type = Item.sword;
      }

    }
  }

  void Unlock(GameObject lock_wall){
    
    lock_wall.SetActive(false);
    item_hold_type = Item.none;

  }

  // ==[collisions & triggers]==================================================
  // ===========================================================================

  void OnCollisionEnter(Collision coll){
    GameObject other = coll.gameObject;

    // landing from a jump
    if(other.tag == "ground"){
      Landing();
    }

    // switch direction
    else{

      // if spike, kill player
      if(other.tag == "spike"){
        Death();
      }

      // run into enemy
      else if(other.tag == "enemy"){

        // kill enemy if player has sword
        if(item_hold_type == Item.sword){
          other.SetActive(false);
          item_hold_type = Item.none;
        }

        // die
        else{
          Death();
        }
        
      }

      // unlock
      else if(other.tag == "lock"){
        if(item_hold_type == Item.key){
          Unlock(other);
        }
        else{
          Toggle();
        }
      }

      // swap direction
      else if(other.tag != "button"){
        if(!enemy){
          Toggle();
        }
        else if(other.tag != "player"){
          Toggle();
        }
      }
    }
  }

  void OnCollisionExit(Collision coll){
    // leaving ground from jump
    if(coll.gameObject.tag == "ground"){
      grounded -= 1;
    }
  }

  void OnTriggerEnter(Collider coll){
    GameObject other = coll.gameObject;

    if(!enemy){
      // hitting a jump trigger pane, increment jump_trigger count
      if(other.tag == "jump"){
        Jump(1);
      }
      else if(other.tag != "teleporter"){
        PickUpItem(other);
      }
    }

  }

  void OnTriggerExit(Collider coll){

    if(!enemy){
      // leaving a jump trigger pane, decrement jump_trigger count
      if(coll.gameObject.tag == "jump"){
        Jump(-1);
      }
    }
      
  }

}
