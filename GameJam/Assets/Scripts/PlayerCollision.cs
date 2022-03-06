using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public int howEnded = 0;

    void OnCollisionEnter2D(Collision2D collision) {
        //find a way to read in that value to the UIScript so that it can enable and disable
        //the thingys
        if(collision.gameObject.tag == "Shark") {
            howEnded = 1; //eaten by shark value
        }
        else {
            howEnded = 2; //got to raft value
        }
    }
}
