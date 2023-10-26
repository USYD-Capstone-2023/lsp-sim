using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Music_controoler : MonoBehaviour
{
    // Start is called before the first frame update

    AudioSource musicPlayer;
    // string songName = "ovenrake_deck-the-halls";
    List<string> fileLines;
    public bool flag = false; // This is the flag you set in the Unity Inspector
    private bool hasPlayed = false; // Additional flag to track if music has been played

    public bool music_import=false;

    private void Update()
    {

        if (music_import){
            // Setting string config file path
            string fileContents =  "./Assets" + "/Config" + ".txt";

            //reading data in config file
            fileLines = File.ReadAllLines(fileContents).ToList(); //reading all lines in file
            // Debug.Log(fileLines[0]);

            // Get the AudioSource component attached to this GameObject
            musicPlayer = GetComponent<AudioSource>();

            // Load the audio clip from the Resources folder
            AudioClip songClip = Resources.Load<AudioClip>(fileLines[0]);

            // Assign the loaded audio clip to the AudioSource
            if (songClip != null)
            {
                musicPlayer.clip = songClip;
                music_import= false;
            }
        }

        if (flag && !hasPlayed) // Check if flag is true and music hasn't been played yet
        {
            musicPlayer.Play();
            hasPlayed = true; // Set the flag to indicate that music has been played
        }
        else if (!flag && hasPlayed) // Check if flag is false and music has been played
        {
            musicPlayer.Pause();
            hasPlayed = false; // Reset the flag
        }

    }

}
