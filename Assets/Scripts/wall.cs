using UnityEngine;
using System.Collections;

public class wall : MonoBehaviour {

  // ==[members]================================================================
  // ===========================================================================

  // states
  public bool clickable = true;
  public bool check_red = false;
  public bool check_blue = false;
  
  // game objects and settings
  GameObject target_wall;
  GameObject blue_player;
  GameObject red_player;
  Vector2 x_range;

  // ==[start]==================================================================
  // ===========================================================================

  void Start(){

    // get players
    blue_player = GameObject.Find("blue_player");
    red_player = GameObject.Find("red_player");

    // get target_wall
    target_wall = gameObject.transform.parent.gameObject.transform.GetChild(0).gameObject;

    // get color of toggle
    char color = gameObject.GetComponent<Renderer>().material.name[0];
    if(color == 'r' || color == 'n'){
      check_red = true;
    }
    if(color == 'b' || color == 'n'){
      check_blue = true;
    }

    // if not clickable, don't show switch
    if(!clickable){
      gameObject.SetActive(false);
    }

    // get overlap range
    x_range = new Vector2(gameObject.transform.position.x - 0.3f, gameObject.transform.position.x + 0.3f);

  }

  // ==[actions]================================================================
  // ===========================================================================

  bool DoesOverlap(GameObject player){

    // compares position of toggle to current position of player
    Vector2 comparison = new Vector2(player.transform.position.x - 0.3f, player.transform.position.x + 0.3f);
    return (x_range.x <= comparison.y && comparison.x <= x_range.y);

  }

  // TODO: appear/disappear animation to make it ~juicy~
  void Toggle(){

    // currently a simple active true/false toggle
    target_wall.SetActive(!target_wall.activeInHierarchy);

  }

  // ==[events]=================================================================
  // ===========================================================================

  void OnMouseDown(){

    // checks if red_player is above toggle
    if(check_red && DoesOverlap(red_player)){
      return;
    }

    // checks if blue_player is above toggle
    if(check_blue && DoesOverlap(blue_player)){
      return;
    }

    // toggle target wall based on click
    Toggle();

  }

}
