using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VectorField : MonoBehaviour
{
    private float[,,] vectorFieldValues = new float[19,11,2];
    private float f1, f2, scalingFactor;
    private int x, y, xPos, yPos;
    private bool hasPlaced = false;
    private Rigidbody2D newPlayerRB;
    private GameObject newPlayer;
    public GameObject arrow, player, shark, raft;
    public float speed;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 19; i++) {
            for(int j = 0; j < 11; j++) {
                x = i - 9;
                y = j - 5;
                
                //INSERT EQUATION HERE
                f1 = x - y; //x value in vector
                f2 = x + y; //y value in vector

                vectorFieldValues[i,j,0] = f1;
                vectorFieldValues[i,j,1] = f2;
            }
        }

        for(int i = 0; i < 18; i++) {
            for(int j = 0; j < 10; j++) {
                x = i - 9;
                y = j - 5;
                f1 = vectorFieldValues[i,j,0];
                f2 = vectorFieldValues[i,j,1];

                //to find the rotation of the vector
                Quaternion rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(f2,f1) * Mathf.Rad2Deg);
                Instantiate(arrow, new Vector3(x + 0.5f, y + 0.5f, 0f), rotation);

                //Look disgusting but it scales the arrow to a correct size.
                /*
                GameObject newArrow = Instantiate(arrow, new Vector3(x + 0.5f, y + 0.5f, 0f), rotation) as GameObject;
                scalingFactor = Mathf.Sqrt(f1 * f1 + f2 * f2);
                newArrow.transform.localScale = new Vector3(.5f, Mathf.Min(Mathf.Max(scalingFactor - 0.5f, 0.1f), 1f), .5f);
                */
                
            }
        }

        //generate random position and rotation for the sharks and raft
        Instantiate(shark, new Vector3(Random.Range(-15f, 6f), Random.Range(-3f, 3f), 0f), Quaternion.Euler(0f, 0f, Random.Range(-15f, 15f)));
        Instantiate(shark, new Vector3(Random.Range(-15f, 6f), Random.Range(-3f, 3f), 0f), Quaternion.Euler(0f, 0f, Random.Range(-15f, 15f)));
        Instantiate(shark, new Vector3(Random.Range(-15f, 6f), Random.Range(-3f, 3f), 0f), Quaternion.Euler(0f, 0f, Random.Range(-15f, 15f)));
        Instantiate(raft, new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), 0f), Quaternion.Euler(0f, 0f, Random.Range(0,360)));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !(hasPlaced)) {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            
            xPos = (int) Mathf.Round(worldPos.x) + 9;
            yPos = (int) Mathf.Round(worldPos.y) + 5;
            f1 = vectorFieldValues[xPos, yPos, 0];
            f2 = vectorFieldValues[xPos, yPos, 1];
            Quaternion rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(f2,f1) * Mathf.Rad2Deg - 90);

            newPlayer = Instantiate(player, worldPos, rotation) as GameObject;
            newPlayerRB = newPlayer.GetComponent<Rigidbody2D>();
            hasPlaced = true;
        }
    }

    void FixedUpdate() {
        if(hasPlaced){
            Vector2 movement = new Vector2(0f, speed);
            newPlayerRB.velocity = newPlayer.transform.TransformDirection(movement);

            xPos = (int) Mathf.Round(newPlayer.transform.position.x) + 9;
            yPos = (int) Mathf.Round(newPlayer.transform.position.y) + 5;
            f1 = vectorFieldValues[xPos, yPos, 0];
            f2 = vectorFieldValues[xPos, yPos, 1];
            newPlayer.transform.rotation = Quaternion.Slerp(newPlayer.transform.rotation, Quaternion.Euler(0f, 0f, Mathf.Atan2(f2,f1) * Mathf.Rad2Deg - 90), 0.1f);
            //newPlayer.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(f2,f1) * Mathf.Rad2Deg - 90);
            score++;
            //need to put a check to see if player has collided with enemy or if player is out of bounds
            
            if (newPlayer.GetComponent<PlayerCollision>().howEnded != 0) {
                if(newPlayer.GetComponent<PlayerCollision>().howEnded == 1) { //eaten
                    SceneManager.LoadScene("eaten");
                }
                if(newPlayer.GetComponent<PlayerCollision>().howEnded == 2) { //win
                    if(score < 100) {
                        SceneManager.LoadScene("WinBad");
                    }
                    else if(score < 200) {
                        SceneManager.LoadScene("WinOkay");
                    }
                    else if(score < 300) {
                        SceneManager.LoadScene("WinGood");
                    }
                    else {
                        SceneManager.LoadScene("WinGod");
                    }
                }
            }
            if(newPlayer.transform.position.x > 9f || newPlayer.transform.position.x < -9f 
            || newPlayer.transform.position.y > 5f || newPlayer.transform.position.y < -5f) { //lost at sea
                SceneManager.LoadScene("DriftedAway");
            }
        }
    }
}
