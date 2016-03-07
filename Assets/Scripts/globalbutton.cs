using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class globalbutton : MonoBehaviour {

    // ==[members]================================================================
    // ===========================================================================

    // target objects and functions
    public GameObject[] targets;
    public UnityEvent ue;

    public bool check_red = false;
    public bool check_blue = false;

    GameObject blue_player;
    GameObject red_player;
    Vector2 x_range;

    // ==[start]==================================================================
    // ===========================================================================

    void Start()
    {
        // null handler
        if (ue == null)
        {
            ue = new UnityEvent();
        }


        // get players
        blue_player = GameObject.Find("blue_player");
        red_player = GameObject.Find("red_player");

        // get color of toggle
        char color = gameObject.GetComponent<Renderer>().material.name[0];
        if (color == 'r' || color == 'n')
        {
            check_red = true;
        }
        if (color == 'b' || color == 'n')
        {
            check_blue = true;
        }

        // get overlap range
        x_range = new Vector2(gameObject.transform.position.x - 0.3f, gameObject.transform.position.x + 0.3f);
    }



    void OnMouseDown()
    {

        //Checks to see if players are not blocking walls
        for (int i = 0; i < targets.Length; ++i)
        {
            char color = targets[i].GetComponent<Renderer>().material.name[0];

            check_red = false;
            check_blue = false;

            if (color == 'r' || color == 'n')
            {
                check_red = true;
            }
            if (color == 'b' || color == 'n')
            {
                check_blue = true;
            }

            // get overlap range
            x_range = new Vector2(targets[i].transform.position.x - 0.3f, targets[i].transform.position.x + 0.3f);

            // checks if red_player is above toggle
            if (red_player != null)
            {
                if (check_red && DoesOverlap(red_player))
                {
                    return;
                }
            }

            // checks if blue_player is above toggle
            if (blue_player != null)
            {
                if (check_blue && DoesOverlap(blue_player))
                {
                    return;
                }
            }
        }

        // toggle set of targets (walls and bridges)
        for (int i = 0; i < targets.Length; ++i)
        {
            GameObject target = targets[i];
            target.SetActive(!target.activeInHierarchy);
        }
    }

    bool DoesOverlap(GameObject player)
    {

        // compares position of toggle to current position of player
        Vector2 comparison = new Vector2(player.transform.position.x - 0.3f, player.transform.position.x + 0.3f);
        return (x_range.x <= comparison.y && comparison.x <= x_range.y);

    }

}
