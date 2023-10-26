using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MakeRGB : MonoBehaviour
{
    public GameObject[] leds; // Array of LED GameObjects

    private IEnumerator ProcessLEDLines(Dictionary<string, List<string[]>> ledLineDictionary)
    {
        int maxCount = 0;

        foreach (var kvp in ledLineDictionary)
        {
            if (kvp.Value.Count > maxCount)
            {
                maxCount = kvp.Value.Count;
            }
        }

        for (int i = 0; i < maxCount; i++)
        {
            foreach (var kvp in ledLineDictionary)
            {
                string ledName = kvp.Key;
                List<string[]> ledPartLists = kvp.Value;

                if (i < ledPartLists.Count)
                {
                    string[] parts = ledPartLists[i];
                    Debug.Log("Working on LED " + ledName);

                    // Process the lines for the specific LED here

                    GameObject led = GameObject.Find(ledName); // Find the GameObject by LED name
                    Light redLightComponent = led.GetComponent<Light>();

                    if (redLightComponent != null)
                    {
                        // Calculate the intensity based on the value in parts[1]
                        float intensityValue = (float.Parse(parts[1]) / 255) * 50;
                        redLightComponent.intensity = intensityValue;
                    }
                }
            }

            yield return new WaitForSeconds(0.3f); // Wait for 0.3 seconds
        }
    }

    private IEnumerator Start()
    {
        string[] lines = File.ReadAllLines("Assets/LEDCommands.txt");
        Dictionary<string, List<string[]>> ledLineDictionary = new Dictionary<string, List<string[]>>();

        // Group lines by LED name (similar to your previous code)
        foreach (string line in lines)
        {
            if (line.StartsWith("['") && line.EndsWith("]"))
            {
                string[] parts = line.Substring(2, line.Length - 3).Split(',');

                if (parts.Length == 4)
                {
                    string ledName = parts[0].Trim();
                    ledName = ledName.Trim('\'');

                    if (!ledLineDictionary.ContainsKey(ledName))
                    {
                        ledLineDictionary.Add(ledName, new List<string[]>());
                    }

                    ledLineDictionary[ledName].Add(parts);
                }
            }
        }

        // Start the coroutine for processing LEDs
        StartCoroutine(ProcessLEDLines(ledLineDictionary));

        yield return null; // Wait for the processing to finish
        Debug.Log("All LED lines processed.");
    }
}



// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.IO;

// public class MakeRGB : MonoBehaviour
// {
//     public GameObject[] leds; // Array of LED GameObjects

//     private IEnumerator ProcessLEDLines(Dictionary<string, List<string[]>> ledLineDictionary)
//     {
//         foreach (var kvp in ledLineDictionary)
//         {
//             string ledName = kvp.Key;
//             List<string[]> ledPartLists = kvp.Value;

//             Debug.Log("Working on LED " + ledName);

//             foreach (string[] parts in ledPartLists)
//             {
//                 // Process the lines for the specific LED here

//                 GameObject led = GameObject.Find(ledName); // Find the GameObject by LED name
//                 Light redLightComponent = led.GetComponent<Light>();

//                 if (redLightComponent != null)
//                 {
//                     // Calculate the intensity based on the value in parts[1]
//                     float intensityValue = (float.Parse(parts[1]) / 255) * 50;
//                     redLightComponent.intensity = intensityValue;
//                 }

//                 yield return new WaitForSeconds(0.3f); // Wait for 0.3 seconds
//             }
//         }
//     }

//     private IEnumerator Start()
//     {
//         string[] lines = File.ReadAllLines("Assets/LEDCommands.txt");
//         Dictionary<string, List<string[]>> ledLineDictionary = new Dictionary<string, List<string[]>>();

//         // Group lines by LED name (similar to your previous code)
//         foreach (string line in lines)
//         {
//             if (line.StartsWith("['") && line.EndsWith("]"))
//             {
//                 string[] parts = line.Substring(2, line.Length - 3).Split(',');

//                 if (parts.Length == 4)
//                 {
//                     string ledName = parts[0].Trim();
//                     ledName = ledName.Trim('\'');

//                     if (!ledLineDictionary.ContainsKey(ledName))
//                     {
//                         ledLineDictionary.Add(ledName, new List<string[]>());
//                     }

//                     ledLineDictionary[ledName].Add(parts);
//                 }
//             }
//         }

//         StartCoroutine(ProcessLEDLines(ledLineDictionary));
//         yield return null; 

//     }
// }


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.IO;

// public class MakeRGB : MonoBehaviour
// {
//     public GameObject[] leds; // Array of LED GameObjects

//     private IEnumerator wait()
//     {   
//         string[] lines = File.ReadAllLines("Assets/LEDCommands.txt");
//         Dictionary<string, List<string[]>> ledLineDictionary = new Dictionary<string, List<string[]>>();

//         // Group lines by LED name
//         foreach (string line in lines)
//         {
//             if (line.StartsWith("['") && line.EndsWith("]"))
//             {
//                 string[] parts = line.Substring(2, line.Length - 3).Split(',');

//                 if (parts.Length == 4)
//                 {
//                     // Debug.Log(parts[1]);
//                     string ledName = parts[0].Trim();
//                     ledName = ledName.Trim('\'');
                    
//                     // Check if the LED name exists in the dictionary
//                     if (!ledLineDictionary.ContainsKey(ledName))
//                     {
//                         ledLineDictionary.Add(ledName, new List<string[]>());
//                     }
                    
//                     // Add the line to the corresponding LED's list
//                     ledLineDictionary[ledName].Add(parts);
//                 }
//             }
//         }

//         // Process the grouped lines
//         foreach (var kvp in ledLineDictionary)
//         {
//             string ledName = kvp.Key;
//             List<string[]> ledLines = kvp.Value;

//             Debug.Log("Working on LED " + ledName);

//             foreach (string[] parts in ledLines)
//             {
//                 ledName = ledName.Trim('\'');   
//                 GameObject led = GameObject.Find(ledName);
//                 // Debug.Log();

//                 // float newIntensity = Mathf.PingPong(Time.time, 1.0f);
//                 float newIntensity = float.Parse(parts[1]);
//                 Light redLightComponent = led.GetComponent<Light>();

//                 if (redLightComponent != null)
//                 {
//                     redLightComponent.intensity = (newIntensity/255)*50;
//                     yield return new WaitForSeconds(0.3f); // Wait for 0.3 seconds
//                 }
//             }
//         }

//     }

//     private void Start()
//     {
//         StartCoroutine(wait());
//     }
// }



