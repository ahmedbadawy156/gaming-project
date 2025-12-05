using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D other){
if (other.tag == "Player")
//if the collider of the object whose name is Sonic GameObjects touches the checkpoint's circle collider

FindObjectOfType<LevelManager>().CurrentCheckpoint = this.gameObject;
//go to the Level Manager script, and update the value of CurrentCheckpoint to become the new Checkpoint the player has
//just passed through. This is necessary when you have several checkpoints in a level
}
}
