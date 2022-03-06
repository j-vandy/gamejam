using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIScript : MonoBehaviour
{
    private bool hasPlaced = false;

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !(hasPlaced)) {
            //disable elements of the UI!
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            hasPlaced = true;
        }
    }
}
