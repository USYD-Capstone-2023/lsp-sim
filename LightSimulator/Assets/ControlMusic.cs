using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMusic : MonoBehaviour
{

    public AudioSource musicPlayer;
    public GameObject test;

    // Start is called before the first frame update
    void Start()
    {
    
        print("START");

    }

    // Update is called once per frame
    void Update()
    {
        
        // Check if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {

            print("SPACE PRESSED");

            // musicPlayer.GetComponent<Music_controoler>().flag = true;

            // Check if the music is not already playing
            if (!musicPlayer.isPlaying)
            {
                // Play the loaded music
                musicPlayer.Play();
            }

            else {
                // Pause the music
                musicPlayer.Pause();
            }
        }
    }
}
