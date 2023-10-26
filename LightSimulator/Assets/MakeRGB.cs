using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
using System.Net;
using System.Net.Sockets;
using AdditionalClass;
using System.IO;
using System.Text;
using TMPro;


public class MakeRGB : MonoBehaviour
{
    public GameObject[] leds; // Array of LED GameObjects
    public int sPort;
    Thread thread;

    private float CameraZDistance;

    //Main camera object
    private Camera mainCamera;

    public GameObject stripLEDContainer; // hold strip and rectangle

    // audio import from ControlMusic.cs
    public AudioSource musicPlayer;

    //Connection text that changes when connection is lost
    public TMP_Text connectionText;

    Boolean audio_played=false;
    Boolean audio_played_check=false;
    Boolean audio_import=false;
    Boolean audio_import_check=false;

    static Mutex mutex = new Mutex();

    LightBlock toPlay=null;
    //create a light of lightblock
    public List<LightBlock> lightBlocks = new List<LightBlock>();

    private void OnMouseDrag()
    {

        //finding the position of mouse from camera
        Vector3 ScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraZDistance);

        //setting object position to mouse position
        transform.position = mainCamera.ScreenToWorldPoint(ScreenPosition);

    }

    public void changeLightShowFlag() 
    {

        GameObject.Find("MusicPlayer").GetComponent<Music_controoler>().flag = !GameObject.Find("MusicPlayer").GetComponent<Music_controoler>().flag;


        // musicPlayer.GetComponent<Music_controoler>().flag = !musicPlayer.GetComponent<Music_controoler>().flag;

    }

    private IEnumerator ProcessLEDLines()
    {
        // //print the light block to play
        // Debug.Log("pin: "+toPlay.getPinValue()+" intensity: "+toPlay.getPinIntensity());
        
        foreach (Transform child in stripLEDContainer.GetComponentsInChildren<Transform>()) {

            // print(child.name);

            if (child.name == "red"+toPlay.getPinValue()){

                // print(child);
                
                // Find the GameObject by LED name
                Light redLightComponent = child.GetComponent<Light>();

                if (redLightComponent != null)
                {
                    // Calculate the intensity based on the value in parts[1]
                    float intensityValue = (float.Parse(toPlay.getPinIntensity()) / 255) * 50;
                    redLightComponent.intensity = intensityValue;

                }
            }
        }
        
        // GameObject led = GameObject.Find("red"+toPlay.getPinValue()); // Find the GameObject by LED name
        // Light redLightComponent = led.GetComponent<Light>();

        // if (redLightComponent != null)
        // {
        //     float intensityValue = (float.Parse(toPlay.getPinIntensity()) / 255) * 50;
        //     redLightComponent.intensity = intensityValue;
        // }
        
        toPlay.setProcessed(true);
        yield return new WaitForSeconds(0.00000001f); 
    }

    void Connection(){
        while (true)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, sPort);
            listener.Start();

            // Debug.Log("Waiting for a connection...");
            TcpClient client = listener.AcceptTcpClient();

            // Debug.Log("Client joined");
            try
            {
                NetworkStream stream = client.GetStream();
                // using (Stream fileStream = File.Create("Assets/Resources/mp3_file.mp3"))
                // {
                //     client.GetStream().CopyTo(fileStream);
                // }
                // receive an mp3 file from LSP
                using (FileStream fileStream = File.Create("Assets/Resources/mp3_file.mp3"))
                {
                    byte[] buffer0 = new byte[1024]; // You can adjust the buffer size as needed
                    int bytesRead;

                    while (true)
                    {
                        bytesRead = stream.Read(buffer0, 0, buffer0.Length);

                        if (bytesRead == 0)
                        {
                            break; // End of stream (sender has closed the connection)
                        }

                        // Check for the end-of-MP3-file signal
                        if (bytesRead >= 14 && Encoding.UTF8.GetString(buffer0, 0, bytesRead) == "SIGNAL_END_MP3")
                        {
                            // Debug.Log("End of MP3 file reached.");
                            break;
                        }

                        fileStream.Write(buffer0, 0, bytesRead);
                    }
                }
                audio_import=true;
                while (true)
                {
                    byte[] buffer = new byte[2];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    if (bytes > 0)
                    {
                        byte[] receivedData = new byte[2];
                        Array.Copy(buffer, receivedData, bytes);
                        //the data has 2 bytes, the first byte is the pin, the second byte is the intensity, extract them

                        // Extract the pin (first byte)
                        byte pinB = receivedData[0];

                        // Extract the intensity (second byte)
                        byte intensityB = receivedData[1];

                        // Convert the bytes to unsigned integers
                        ushort pinValue = BitConverter.ToUInt16(new byte[] { pinB, 0 }, 0);
                        ushort intensityValue = BitConverter.ToUInt16(new byte[] { intensityB, 0 }, 0);
                        pinValue+=1;

                        //print on one single line
                        //Debug.Log("Pin: " + pinValue + " Intensity: " + intensityValue);

                        // When receive audio flag play music
                        if (!audio_played) {
                            if (intensityValue >8 && intensityValue <255 ){
                            // Debug.Log("Music flag received from LSP");
                            audio_played=true;
                            }
                            else{
                                continue;
                            }
                        }
                        //add the light block to the list
                        LightBlock lightBlock = new LightBlock(pinValue.ToString(), intensityValue.ToString());
                        //lock the mutex
                        mutex.WaitOne();
                        lightBlocks.Add(lightBlock);
                        //unlock the mutex
                        mutex.ReleaseMutex();
                    }
                    else{
                        // Debug.Log("Client disconnected");
                        // Debug.Log("Waiting for a connection...");

                        //changing text to tell user client has disconnected
                        connectionText.text = "Client disconnected. Waiting for a connection...";

                        client = listener.AcceptTcpClient();
                        continue;
                    }
                }
            }
            catch (Exception e)
            { 
                //do nothing
                // Debug.Log("Client disconnected: " + e.Message);
            }
            finally
            {
                client.Close();
                listener.Stop();
            }
        }
    }

    private IEnumerator Start()
    {
        if (sPort==12345){
            ThreadStart ts = new ThreadStart(Connection);
            thread = new Thread(ts);
            thread.Start();
        }
        
        yield return new WaitForSeconds(0.3f); // Wait for 0.3 seconds
    }
    //update function
    void Update(){
        if (sPort==12345){
            if(audio_import && !audio_import_check){
                musicPlayer.GetComponent<Music_controoler>().music_import=true;
                audio_import_check=true;
                // Debug.Log("audio imported");
            }

            if (audio_played && !audio_played_check){
                musicPlayer.GetComponent<Music_controoler>().flag = true;
                audio_played_check=true;
                // Debug.Log("audio played");

                //changing text to tell user client has joined on screen
                // connectionText.text = "Client has connected. Starting Lightshow...";
            
            }

            //if the flag is true
            if (musicPlayer.GetComponent<Music_controoler>().flag)
            {

                //lock the mutex
                mutex.WaitOne();
                //loop through the light block list
                foreach (LightBlock lightBlock in lightBlocks)
                {
                    //if the light block is not processed, process it
                    if (!lightBlock.isProcessed())
                    {
                        toPlay = lightBlock;
                        //run the processLEDLines function
                        StartCoroutine(ProcessLEDLines());
                        break;
                    }
                }
                //unlock the mutex
                mutex.ReleaseMutex();
            }
        }
    }
}