// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.Threading;
// using System;
// using System.Net;
// using System.Net.Sockets;
// using AdditionalClass;


// public class MakeRGB : MonoBehaviour
// {
//     public GameObject[] leds; // Array of LED GameObjects
//     public int sPort = 12345;
//     Thread thread;

//     // audio import from ControlMusic.cs
//     public AudioSource musicPlayer;

//     Boolean audio_played=false;
//     Boolean audio_import=false;

//     static Mutex mutex = new Mutex();

//     LightBlock toPlay=null;
//     //create a light of lightblock
//     public List<LightBlock> lightBlocks = new List<LightBlock>();

//     private IEnumerator ProcessLEDLines()
//     {
//         // //print the light block to play
//         // Debug.Log("pin: "+toPlay.getPinValue()+" intensity: "+toPlay.getPinIntensity());
//         GameObject led = GameObject.Find("red"+toPlay.getPinValue()); // Find the GameObject by LED name
//         Light redLightComponent = led.GetComponent<Light>();

//         if (redLightComponent != null)
//         {
//             float intensityValue = (float.Parse(toPlay.getPinIntensity()) / 255) * 50;
//             redLightComponent.intensity = intensityValue;
//         }

//         toPlay.setProcessed(true);
//         yield return new WaitForSeconds(0.3f); // Wait for 0.3 seconds
//     }

//     void Connection(){
//         TcpListener listener = new TcpListener(IPAddress.Any, sPort);
//         listener.Start();

//         Debug.Log("Waiting for a connection...");
//         TcpClient client = listener.AcceptTcpClient();
//         Debug.Log("Client joined");
//         try
//         {
//             NetworkStream stream = client.GetStream();
//             // using (Stream fileStream = File.Create("Assets/Resources/mp3_file.mp3"))
//             // {
//             //     client.GetStream().CopyTo(fileStream);
//             // }
//             // audio_import=true;
//             while (true)
//             {
//                 byte[] buffer = new byte[2];
//                 int bytes = stream.Read(buffer, 0, buffer.Length);
//                 if (bytes > 0)
//                 {
//                     byte[] receivedData = new byte[2];
//                     Array.Copy(buffer, receivedData, bytes);
//                     //the data has 2 bytes, the first byte is the pin, the second byte is the intensity, extract them

//                     // Extract the pin (first byte)
//                     byte pinB = receivedData[0];

//                     // Extract the intensity (second byte)
//                     byte intensityB = receivedData[1];

//                     // Convert the bytes to unsigned integers
//                     ushort pinValue = BitConverter.ToUInt16(new byte[] { pinB, 0 }, 0);
//                     ushort intensityValue = BitConverter.ToUInt16(new byte[] { intensityB, 0 }, 0);
//                     pinValue+=1;

//                     //print on one single line
//                     //Debug.Log("Pin: " + pinValue + " Intensity: " + intensityValue);

//                     // When receive audio flag play music
//                     if (intensityValue >7 && intensityValue <255 && !audio_played) {
//                         Debug.Log("Music flag received from LSP");
//                         audio_played=true;
//                     }

//                     //if the pin is larger than 11, ignore it
//                     if (pinValue > 11){
//                         continue;
//                     }
//                     //add the light block to the list
//                     LightBlock lightBlock = new LightBlock(pinValue.ToString(), intensityValue.ToString());
//                     lightBlocks.Add(lightBlock);
//                 }
//                 else{
//                     Debug.Log("Client disconnected");
//                     Debug.Log("Waiting for a connection...");
//                     client = listener.AcceptTcpClient();
//                     Thread.Sleep(1000);
//                 }
//             }
//         }
//         catch (Exception e)
//         {
//             Debug.Log("Client disconnected: " + e.Message);
//         }
//         finally
//         {
//             client.Close();
//             listener.Stop();
//         }
//     }

//     private IEnumerator Start()
//     {
//         ThreadStart ts = new ThreadStart(Connection);
//         thread = new Thread(ts);
//         thread.Start();

//         //join the thread
//         thread.Join();
        
//         yield return new WaitForSeconds(0.3f); // Wait for 0.3 seconds
//     }
//     //update function
//     void Update(){
//         if(audio_import){
//             musicPlayer.GetComponent<Music_controoler>().music_import=true;

//         }
//         if (audio_played){
//             //musicPlayer.GetComponent<Music_controoler>().flag = true;
//         }

//         //loop through the light block list
//         foreach (LightBlock lightBlock in lightBlocks)
//         {
//             //if the light block is not processed, process it
//             if (!lightBlock.isProcessed())
//             {
//                 toPlay = lightBlock;
//                 //run the processLEDLines function
//                 StartCoroutine(ProcessLEDLines());
//                 break;
//             }
//         }
//     }
// }

// namespace AdditionalClass{
//     public class LightBlock
//     {
//         protected string pin_value;
//         protected string pin_intensity;
//         protected bool processed;

//         public LightBlock(string pin_value, string pin_intensity)
//         {
//             this.pin_value = pin_value;
//             this.pin_intensity = pin_intensity;
//             this.processed = false;
//         }

//         public string getPinValue()
//         {
//             return this.pin_value;
//         }

//         public string getPinIntensity()
//         {
//             return this.pin_intensity;
//         }

//         public bool isProcessed()
//         {
//             return this.processed;
//         }

//         public void setProcessed(bool processed)
//         {
//             this.processed = processed;
//         }
//     }
// }