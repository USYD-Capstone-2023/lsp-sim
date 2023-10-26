using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestButtonScript : MonoBehaviour
{

    // public TextMeshProUGUI numberText;
    public GameObject plane;
    public GameObject otherButton;
    public GameObject camera;
    public GameObject addLEDButton;

    //changing text to BACK when editor mode
    public TMP_Text backText; //editorpage
    public TMP_Text backTextViewing; //viewingpage

    //button to start and stop lightshow
    public GameObject startStopLightShowButton;
    public TMP_Text startStopLightShowButtonTEXT;

    //Text for INTPUT FIELD intial and final LED position
    public TMP_Text startLEDText;
    public TMP_Text finalLEDText;

    //button to rotate
    public GameObject rotateLEDButton;

    //field for analog strip length and stirp pin value
    public TMP_Text analogStripLengthFieldText;
    public TMP_Text analogStripPinFieldText;

    //Text for analog strip length and stirp pin value
    public TMP_Text analogStripLengthText;
    public TMP_Text analogStripPinText;
    public TMP_Text analogStripLEDAmountText;

    //button for Add analog LED
    public GameObject analogAddStripButton;

    //fields for anaolog
    public GameObject analogLengthField;
    public GameObject analogPinField;

    //Text for selected LED and Chnaged LED
    public TMP_Text selectedLEDText;
    public TMP_Text changedLEDText;

    //Connection text that changes when connection is lost
    public TMP_Text connectionText;

    //text of the current selected LED count
    public TMP_Text currentSelectedLEDText;

    //Text for start and final, above Fields
    public TMP_Text startText;
    public TMP_Text finalText;
    
    //Field for selected LED and Chnaged LED
    public GameObject startLEDField;
    public GameObject finalLEDField;

    //Buttons to choose between digital and Analog LEDs
    public GameObject analogLEDbutton;
    public GameObject digitalLEDbutton;

    private bool analogInUseFlag;
    private bool digitalInUseFlag;

    //dropdown for analog LED length and amount
    public GameObject analogDropdownLength;
    public GameObject analogDropdownAmount;

    //button for changing LED reading
    public GameObject buttonSetLED;

    //Selected LED current length
    private static int selectedLEDlength;

    public GameObject stripObject; //strip LED
    public GameObject stripObjectCollision;  // rectangle same size LED strpi
    public GameObject stripLEDContainer; // hold strip and rectangle

    public GameObject addLength;
    public GameObject single_led_cube; //single LED

    //reference to input field LED Count
    public GameObject textFieldCountLED;

    //text of current LED count
    public TMP_Text currentLEDLengthText;

    //text to indicate too big or too small LED length
    public GameObject tooBigText;
    public GameObject tooSmallText;

    //text for LED change too small and big
    public GameObject tooBigTextLEDChange;
    public GameObject tooSmallTextLEDChange;

    //bin text and circle
    public GameObject binText;
    public GameObject binObject;
    public GameObject binUIObject;

    //keeping track of number of LED's, distinguished
    public int created_LEDs;

    //updating current length of LED's created, defualt 1
    private int led_length = 1; //digital length

    //vairables to hold analog length and pin
    private int analog_led_length = 1; //anaolog length
    private int analog_led_pin = 1; //anaolog length

    //position of camera to object (Z position)
    private float CameraZDistance;

    //Main camera object
    private Camera mainCamera;

    //selected game object
    private static GameObject selectedLED;

    //value for intial and final LED
    private int initialLEDValue = 1;
    private int finalLEDValue = 1;

    //extra ui features for main menu
    public TMP_Text mainMenuTitle;
    public GameObject lamp;

    //variabels to give objects the effect of red fade to original colour
    public Color buttonFromoColor = Color.red; //from
    private Color buttonToColor; //to

    //meter for currrent analog LED strip
    private int currentLEDMeter = 1;

    //number of LEDs of analog strip
    private int currentLEDAmount = 30;

    // int counter = 0;

    private void OnMouseDown()
    {

        // print("MOUSE CLICKED CUBE");
        // print(this);
        // print(this.gameObject);

        //getting object that is selected
        selectedLED = this.gameObject;

        int counter = 0;

        foreach (Transform child in selectedLED.GetComponentsInChildren<Transform>()) {

            if (child.name.Contains("LEDCubeUnit") ){

                counter++;
            }
        }

        //divide by 2, since VIEWING and EDITOR cubes have the same parent
        selectedLEDlength = counter/2;

    }

    public void changingLEDMeterOption (int val) {

        if (val == 0) {
            // print("CURRENT METER : 1");
            currentLEDMeter = 1;

        } else if (val == 1) {
            // print("CURRENT METER : 2");
            currentLEDMeter = 2;

        } else if (val == 2) {
            // print("CURRENT METER : 3");
            currentLEDMeter = 3;

        } else if (val == 3) {
            // print("CURRENT METER : 4");
            currentLEDMeter = 4;

        } else if (val == 4) {
            // print("CURRENT METER : 5");
            currentLEDMeter = 5;

        }

        //changing LED length text
        analogStripLengthText.text = "LED Length: " +  currentLEDMeter + "m";

    }

    public void changingLEDAmountOption (int val) {

        if (val == 0) {
            // print("CURRENT LED AMOUNT : 30");
            currentLEDAmount = 30;

        } else if (val == 1) {
            // print(currentLEDAmount);
            currentLEDAmount = 60;

        } else if (val == 2) {
            // print("CURRENT METER : 90");
            currentLEDAmount = 90;

        } 

        //changing LED length text
        analogStripLEDAmountText.text = "LED Amount: " +  currentLEDAmount;

    }

    // public void changeButtonColour(){

    //     if (selectedLED == null) {
           
    //         // print(GameObject.Find("LEDRotator").GetComponent<Image>().material.color);
    //         print(GameObject.Find("LEDRotator").GetComponent<Image>().color);

    //         buttonToColor = GameObject.Find("LEDRotator").GetComponent<Image>().color;

    //         print("button is now red");
    //         GameObject.Find("LEDRotator").GetComponent<Image>().color = Color.red;

    //         print(buttonToColor);
    //         print(Color.red);
    //         //creating new color
    //         double currentRValue = buttonToColor.r;
    //         double currentGValue = buttonToColor.g;
    //         double currentBValue = buttonToColor.b;

    //         print(GameObject.Find("LEDRotator").GetComponent<Image>().color.r < Color.red.r);
    //         print(GameObject.Find("LEDRotator").GetComponent<Image>().color.g > Color.red.g);
    //         print(GameObject.Find("LEDRotator").GetComponent<Image>().color.b > Color.red.b);

    //         while (currentRValue < Color.red.r || 
    //                 currentGValue > Color.red.g ||
    //                 currentBValue> Color.red.b){

    //                     print("LOOPING");

    //             if (currentRValue < GameObject.Find("LEDRotator").GetComponent<Image>().color.r ) {
    //                 currentRValue += 0.0005;
    //                 // GameObject.Find("LEDRotator").GetComponent<Image>().color.r = currentRValue;
    //             }

    //             if (currentGValue > GameObject.Find("LEDRotator").GetComponent<Image>().color.g ) {
    //                 currentGValue -= 0.0005;
    //                 // GameObject.Find("LEDRotator").GetComponent<Image>().color.g = 0.01;
    //             }

    //             if (currentBValue > GameObject.Find("LEDRotator").GetComponent<Image>().color.b ) {
    //                 currentBValue -= 0.0005;
    //                 // GameObject.Find("LEDRotator").GetComponent<Image>().color.b += 0.01;
    //             }


    //             // print(currentRValue);
    //             // print(currentGValue);
    //             // print(currentBValue);

    //             print(new Color((float)currentRValue, (float)currentGValue, (float)currentBValue));

    //             //setting new color
    //             GameObject.Find("LEDRotator").GetComponent<Image>().color = new Color((float)currentRValue, (float)currentGValue, (float)currentBValue);
    //         }



    //     }

    // }

    // private void OnMouseDrag()
    // {



    //     print("TEST SCRIPT MOVEMENT");
    //     //finding the position of mouse from camera
    //     Vector3 ScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraZDistance);

    //     //setting object position to mouse position
    //     transform.position = mainCamera.ScreenToWorldPoint(ScreenPosition);


    // }

    public void rotateSelectedLED() {

        if (selectedLED != null){

            // print(selectedLED);

            //vector of edit center strip
            float xMiddleEdit = 0;
            float yMiddleEdit = 0;
            float zMiddleEdit = 0; 

            //vector of view center strip
            float xMiddleView = 0;
            float yMiddleView = 0;
            float zMiddleView = 0; 

            //getting all LEDs from editing and viewing mode

            List<GameObject> rotatingLEDs = new List<GameObject>();

            //parent because you're only selecting editor LED
            foreach (Transform child in selectedLED.GetComponentsInChildren<Transform>()) {

                if (child.name.Contains("LEDCubeUnit")){

                    rotatingLEDs.Add(child.gameObject);
                }
            }


            // print(rotatingLEDs[rotatingLEDs.Count/4].transform.position.x);
            // print(rotatingLEDs[rotatingLEDs.Count/4 +1].transform.position.x);
            // print(rotatingLEDs[rotatingLEDs.Count/4].transform.position.y);
            // print(rotatingLEDs[rotatingLEDs.Count/4 +1].transform.position.y);
            // print(rotatingLEDs[rotatingLEDs.Count/4].transform.position.z);
            // print(rotatingLEDs[rotatingLEDs.Count/4 +1].transform.position.z);
            // print(rotatingLEDs.Count);
            // print(rotatingLEDs.Count/4 % 2);

            //finding center of Edit strip

            //getting the middle two postions and averaging x,y,z value for ceter point
            xMiddleEdit = ((rotatingLEDs[rotatingLEDs.Count/4].transform.position.x) + (rotatingLEDs[rotatingLEDs.Count/4 -1].transform.position.x)) / 2;
            yMiddleEdit = ((rotatingLEDs[rotatingLEDs.Count/4].transform.position.y) + (rotatingLEDs[rotatingLEDs.Count/4 -1].transform.position.y)) / 2;
            zMiddleEdit = ((rotatingLEDs[rotatingLEDs.Count/4].transform.position.z) + (rotatingLEDs[rotatingLEDs.Count/4 -1].transform.position.z)) / 2;

            //getting the middle two postions and averaging x,y,z value for ceter point
            xMiddleView = ((rotatingLEDs[rotatingLEDs.Count/4 *3].transform.position.x) + (rotatingLEDs[rotatingLEDs.Count/4 *3 -1].transform.position.x)) / 2;
            yMiddleView = ((rotatingLEDs[rotatingLEDs.Count/4 *3].transform.position.y) + (rotatingLEDs[rotatingLEDs.Count/4 *3 -1].transform.position.y)) / 2;
            zMiddleView = ((rotatingLEDs[rotatingLEDs.Count/4 *3].transform.position.z) + (rotatingLEDs[rotatingLEDs.Count/4 *3 -1].transform.position.z)) / 2;


            //separate Editing and viewing LEDs then set center for both groups, rotating center for both groups

            // print(xMiddleEdit);
            // print(yMiddleEdit);
            // print(zMiddleEdit);
            
            //editor leds
            for (int i =0; i<rotatingLEDs.Count/2 ; i++){

                // print("rotating");

                rotatingLEDs[i].transform.RotateAround( new Vector3( xMiddleEdit, yMiddleEdit, zMiddleEdit), Vector3.forward, 90f/3);

            }

            //viewing leds
            for (int i =rotatingLEDs.Count/2; i<rotatingLEDs.Count ; i++){

                // print(rotatingLEDs[i]);

                rotatingLEDs[i].transform.RotateAround( new Vector3( xMiddleView, yMiddleView, zMiddleView), Vector3.forward, 90f/3);

            }

            //having collider match up with child
            selectedLED.transform.RotateAround( new Vector3( xMiddleEdit, yMiddleEdit, zMiddleEdit), Vector3.forward, 90f/3);

             foreach (Transform child in selectedLED.GetComponentsInChildren<Transform>()) {

                if (child.name.Contains("LEDCubeUnit") ){

                    child.transform.RotateAround( new Vector3( xMiddleEdit, yMiddleEdit, zMiddleEdit), Vector3.forward, -90f/3);
                }
            }

            // print(selectedLED.transform.parent.gameObject);

            //parent because you're only selecting editor LED
            // foreach (Transform child in selectedLED.transform.parent.gameObject.GetComponentsInChildren<Transform>()) {
                
            //     if (child.name.Contains("LEDCubeUnit")){
            //         print("ONCE");

            //         // foreach (Transform child in selectedLED.transform.parent.gameObject.GetComponentsInChildren<Transform>()) {
                
            //         //         if (child.name.Contains("LEDCubeUnit")){
                            

            //         //         // child.transform.RotateAround( new Vector3( xMiddleEdit, yMiddleEdit, zMiddleEdit), Vector3.forward, -90f/3);
            //         //     }

            //         // }

            //         child.transform.RotateAround( new Vector3( xMiddleEdit, yMiddleEdit, zMiddleEdit), Vector3.forward, -90f/3);
            //     }

            // }

            // //reversing any changes made by parent
            // for (int i =0; i<rotatingLEDs.Count/2 ; i++){

            //     // print("reversed");

            //     rotatingLEDs[i].transform.RotateAround( new Vector3( xMiddleEdit, yMiddleEdit, zMiddleEdit), Vector3.forward, -90f/3);

            // }

            // //reversing any changes made by parent
            // for (int i =rotatingLEDs.Count/2; i<rotatingLEDs.Count ; i++){

            //     // print(rotatingLEDs[i]);

            //     rotatingLEDs[i].transform.RotateAround( new Vector3( xMiddleEdit, yMiddleEdit, zMiddleEdit), Vector3.forward, -90f/3);

            // }

        }
        
    }

    public void allowDigitalLEDFucntionailty()
    {
        
        //give options to create digital

        //if button is clicked once features appear, clicked twice everything diasppears
        if (digitalInUseFlag == false) {

            digitalInUseFlag = true;

            analogLEDbutton.SetActive(false);

            //add button for LED fixed 12 LEDs
            // addLEDButton.SetActive(true);

            //add input box for count and text count
            textFieldCountLED.SetActive(true);
            currentLEDLengthText.gameObject.SetActive(true);

            //adding button to add LED dynamic
            addLength.SetActive(true);

            // //add bin
            // binText.SetActive(true);
            // binObject.SetActive(true);

            //ALL fields to change LED pin
            //Text for INTPUT FIELD intial and final LED position TRUE
            startLEDText.gameObject.SetActive(true);
            finalLEDText.gameObject.SetActive(true);

            // //Text for start and final, above Fields
            // startText.gameObject.SetActive(true);
            // finalText.gameObject.SetActive(true);

            //Text for selected LED and Chnaged LED
            selectedLEDText.gameObject.SetActive(true);
            changedLEDText.gameObject.SetActive(true);
            
            //Field for selected LED and Chnaged LED
            startLEDField.SetActive(true);
            finalLEDField.SetActive(true);

            //button for changing LED reading
            buttonSetLED.SetActive(true);

            //clearing error messages
            tooSmallTextLEDChange.SetActive(false);
            tooBigTextLEDChange.SetActive(false);
        }

        else {
            // print("entering deiappser");
            digitalInUseFlag = false;

            analogLEDbutton.SetActive(true);

            //remove button for LED fixed
            addLEDButton.SetActive(false);

            //remove input box for count and text count
            textFieldCountLED.SetActive(false);
            currentLEDLengthText.gameObject.SetActive(false);

            //removing button for LED dynamice
            addLength.SetActive(false);

            // //remove bin
            // binText.SetActive(false);
            // binObject.SetActive(false);

            //ALL fields to change LED pin
            //Text for INTPUT FIELD intial and final LED position OFF
            startLEDText.gameObject.SetActive(false);
            finalLEDText.gameObject.SetActive(false);

            //Text for start and final, above Fields
            // startText.gameObject.SetActive(false);
            // finalText.gameObject.SetActive(false);

            //Text for selected LED and Chnaged LED
            selectedLEDText.gameObject.SetActive(false);
            changedLEDText.gameObject.SetActive(false);
            
            //Field for selected LED and Chnaged LED
            startLEDField.SetActive(false);
            finalLEDField.SetActive(false);

            //button for changing LED reading
            buttonSetLED.SetActive(false);

            //clearing error messages
            tooSmallTextLEDChange.SetActive(false);
            tooBigTextLEDChange.SetActive(false);

        }

    }

    public void allowAnalogLEDFucntionailty()
    {
        
        //give options to create analog

        //if button is clicked once features appear, clicked twice everything diasppears
        if (analogInUseFlag == false) {

            analogInUseFlag = true;

            digitalLEDbutton.SetActive(false);

            //add bin
            // binText.SetActive(true);
            // binObject.SetActive(true);

            //ALL fields to change LED pin
            //Text for INTPUT FIELD intial and final LED position TRUE
            analogStripLengthText.gameObject.SetActive(true);
            analogStripPinText.gameObject.SetActive(true);
            analogStripLEDAmountText.gameObject.SetActive(true);
            
            //Field for LED length and LED pin
            // analogLengthField.SetActive(true);
            analogPinField.SetActive(true);

            //button to add led
            analogAddStripButton.SetActive(true);

            //clearing error messages
            tooSmallTextLEDChange.SetActive(false);
            tooBigTextLEDChange.SetActive(false);

            //dropdowns
            analogDropdownLength.SetActive(true);
            analogDropdownAmount.SetActive(true);

            
        }

        else {
            // print("entering deiappser");
            analogInUseFlag = false;

            digitalLEDbutton.SetActive(true);

            //add bin
            // binText.SetActive(false);
            // binObject.SetActive(false);

            //ALL fields to change LED pin
            //Text for INTPUT FIELD intial and final LED position TRUE
            analogStripLengthText.gameObject.SetActive(false);
            analogStripPinText.gameObject.SetActive(false);
            analogStripLEDAmountText.gameObject.SetActive(false);
            
            //Field for LED length and LED pin
            // analogLengthField.SetActive(false);
            analogPinField.SetActive(false);

            //button to add led
            analogAddStripButton.SetActive(false);

            //clearing error messages
            tooSmallTextLEDChange.SetActive(false);
            tooBigTextLEDChange.SetActive(false);

            //dropdowns
            analogDropdownLength.SetActive(false);
            analogDropdownAmount.SetActive(false);
            
        }

    }

    // public void changeLightShowButtonText()
    // {

    //     // print(startStopLightShowButtonTEXT.text);

    //     //changing text from start to stop
    //     if (startStopLightShowButtonTEXT.text == "Start LightShow"){

    //         otherButton.SetActive(false);

    //         startStopLightShowButtonTEXT.text = "Stop LightShow";
    //     }

    //     //changing text from stop to start
    //     else if (startStopLightShowButtonTEXT.text == "Stop LightShow"){

    //         otherButton.SetActive(true);

    //         startStopLightShowButtonTEXT.text = "Start LightShow";
    //     }

    // }

    // public void changeLightShowButtonFlag()
    // {

    //     // //starting to change flag for every script
    //     // //looping through children that only have the movement script and enabling
    //     //     foreach (Transform child in stripLEDContainer.GetComponentsInChildren<Transform>()) {

    //     //         if (child.GetComponent<MakeRGB>() != null ){

    //     //             child.GetComponent<MakeRGB>().changeLightShowFlag();
    //     //         }
    //     //     }

    //     GameObject.Find("MusicPlayer").GetComponent<Music_controoler>().flag = !GameObject.Find("MusicPlayer").GetComponent<Music_controoler>().flag;

    // }

    public void ChangeLEDLength(string Input)
    {
        
        //checking input is greater then 0
        if (int.Parse(Input)<1) {

            Debug.Log("LED length is TOO Small");

            //big error then small error edge case
            tooSmallText.SetActive(true);
            tooBigText.SetActive(false);
            return;

        }

        //checking input is less then 300
        if (int.Parse(Input)>300) {

            Debug.Log("LED length is TOO BIG");

            //small error then big error edge case
            tooSmallText.SetActive(false);
            tooBigText.SetActive(true);
            return;

        }

        //no error messages
        tooSmallText.SetActive(false);    
        tooBigText.SetActive(false);

        //setting new LED length
        // Debug.Log(led_length);
        led_length = int.Parse(Input);
        // Debug.Log(led_length);

        //changing LED length text
        currentLEDLengthText.text = "Current LED Length: " +  led_length;

    }

    public void ChangeAnalogLEDLength(string Input)
    {
        
        //checking input is greater then 0
        if (int.Parse(Input)<1) {

            Debug.Log("LED length is TOO Small");

            //big error then small error edge case
            tooSmallText.SetActive(true);
            tooBigText.SetActive(false);
            return;

        }

        //checking input is less then 300
        if (int.Parse(Input)>300) {

            Debug.Log("LED length is TOO BIG");

            //small error then big error edge case
            tooSmallText.SetActive(false);
            tooBigText.SetActive(true);
            return;

        }

        //no error messages
        tooSmallText.SetActive(false);    
        tooBigText.SetActive(false);

        //setting new LED length
        // Debug.Log(led_length);
        analog_led_length = int.Parse(Input);
        // Debug.Log(led_length);

        //changing LED length text
        analogStripLengthText.text = "Current Length: " +  analog_led_length;

    }

    public void ChangeAnalogLEDPin(string Input)
    {
        
        //checking input is greater then 0
        if (int.Parse(Input)<1) {

            Debug.Log("LED length is TOO Small");

            //big error then small error edge case
            tooSmallText.SetActive(true);
            tooBigText.SetActive(false);
            return;

        }

        //checking input is less then 300
        if (int.Parse(Input)>300) {

            Debug.Log("LED length is TOO BIG");

            //small error then big error edge case
            tooSmallText.SetActive(false);
            tooBigText.SetActive(true);
            return;

        }

        //no error messages
        tooSmallText.SetActive(false);    
        tooBigText.SetActive(false);

        //setting new LED length
        // Debug.Log(led_length);
        analog_led_pin = int.Parse(Input);
        // Debug.Log(led_length);

        //changing LED length text
        analogStripPinText.text = "Using Pin: " +  analog_led_pin;

    }

    public void GrabbingIntialLEDValue(string Input)
    {

        //storing user input
        initialLEDValue = int.Parse(Input);

        //showing users what LED is selected
        selectedLEDText.text = "Selected LED: " + initialLEDValue;

    }

    public void GrabbingFinalLEDValue(string Input)
    {

        //storing user input
        finalLEDValue = int.Parse(Input);

        //showing users what LED is changed
        changedLEDText.text = "| Changed To LED: " + finalLEDValue;

    }

    public void ChangeCurrentLEDPositionToDifferentLED() 
    {

        //grabbing the inital LED and final LED
        // print(initialLEDValue);
        // print(finalLEDValue);

        //NEED TO ERROR CHECK

        if (selectedLED != null) {
        
            if (initialLEDValue < 1 || finalLEDValue < 1) {

                // print("LED Start and final MUST be at least 1!!!!!!!");
                tooSmallTextLEDChange.SetActive(true);
                tooBigTextLEDChange.SetActive(false);

            }

            if (initialLEDValue > selectedLEDlength || finalLEDValue > selectedLEDlength) {

                // print("LED Start and final can't be greater then SELECTED LED length!!!!!");
                tooSmallTextLEDChange.SetActive(false);
                tooBigTextLEDChange.SetActive(true);
            }

            //Setting LED, starts at 0, not 1
            foreach (Transform child in selectedLED.GetComponentsInChildren<Transform>()) {

                //CUBE doesn't really matter
                if (child.name == "Cube " + initialLEDValue) {
                    // print("CUBE MATCH");

                    child.name = "Cube " + finalLEDValue; 

                    //remove error msg
                    tooSmallTextLEDChange.SetActive(false);
                    tooBigTextLEDChange.SetActive(false);
                }

                //red LEDs
                if (child.name == "red" + initialLEDValue){
                    // print("red MATCH");

                    child.name = "red" + finalLEDValue; 

                    //remove error msg
                    tooSmallTextLEDChange.SetActive(false);
                    tooBigTextLEDChange.SetActive(false);
                }

                //blue LEDs
                if (child.name == "blue" + initialLEDValue){
                    // print("blue MATCH");

                    child.name = "blue" + finalLEDValue; 

                    //remove error msg
                    tooSmallTextLEDChange.SetActive(false);
                    tooBigTextLEDChange.SetActive(false);
                }

                //green LEDs
                if (child.name == "green" + initialLEDValue){
                    // print("green MATCH");

                    child.name = "green" + finalLEDValue; 

                    //remove error msg
                    tooSmallTextLEDChange.SetActive(false);
                    tooBigTextLEDChange.SetActive(false);
                }

            }
        }
        // else {

        //     changeButtonColour();

        // }
        
    }

    // private void changeZCamera() {

    //     //setting camera
    //     mainCamera = Camera.main;

    //     //fiindind z position from mouse to camera.
    //     CameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z;

    // }

    //function for Editor mode
    public void ChangeToEditorMode()
    {

        //disabling plane when entering, enabling plane when leaving
        if (plane.activeSelf == true) {

            // print("Entering Editor Mode");

            //changing text to back and position to top left
            backText.text = "BACK";
            this.gameObject.GetComponent<Transform>().transform.position = new Vector3(155f,960f,0f);
            
            //looping through children that only have the movement script and enabling
            foreach (Transform child in stripLEDContainer.GetComponentsInChildren<Transform>()) {

                if (child.name.Contains("Strip")){

                    // print(child.GetComponentInChildren<Movement>());
                    if (child.GetComponentInChildren<Movement>() == null){

                        child.gameObject.AddComponent<Movement>();

                    }
                }
            }

            //remove plane
            plane.SetActive(false);

            //rotate LED button
            rotateLEDButton.SetActive(true);

            //giving users two options of LED's to create
            analogLEDbutton.SetActive(true);
            digitalLEDbutton.SetActive(true);

            //removing mainmenu title and lamp
            lamp.SetActive(false);
            mainMenuTitle.gameObject.SetActive(false);

            //bin always active in editingpage
            binObject.SetActive(true);
            binUIObject.SetActive(true);

            //text of the current selected LED count
            currentSelectedLEDText.gameObject.SetActive(true);

            //move camera

            //ZOOMED OUT
            // camera.GetComponent<Transform>().transform.position = new Vector3(1064.35f,250.6264f,-548.6f);
            
            //ZOOMED IN
            camera.GetComponent<Transform>().transform.position = new Vector3(1625f,260f,-400f);
            
            // changeZCamera();


        } else{

            // print("Exiting Editor Mode");

            //changing text to back and position to middle for main menue
            backText.text = "EDIT LIGHTSHOW";
            this.gameObject.GetComponent<Transform>().transform.position = new Vector3(976f,275.3f,0f);
            
            //looping through children that only have the movement script and disabling FOR VIEWING MODE
            foreach (Transform child in stripLEDContainer.GetComponentsInChildren<Transform>()) {

                if (child.name.Contains("Strip") ){
                    // print(child.name);
                    // print(child.GetComponentInChildren<Movement>());

                    if (child.GetComponentInChildren<Movement>() != null){
                        Destroy(child.GetComponentInChildren<Movement>());

                    }
                }
            }

            //brining back plane
            plane.SetActive(true);

            //returning mainmenu title and lamp
            lamp.SetActive(true);
            mainMenuTitle.gameObject.SetActive(true);

            //rotate LED button
            rotateLEDButton.SetActive(false);

            //removing users two options of LED's to create
            analogLEDbutton.SetActive(false);
            digitalLEDbutton.SetActive(false);

            //remove button for LED fixed
            addLEDButton.SetActive(false);

            //remove input box for count and text count
            textFieldCountLED.SetActive(false);
            currentLEDLengthText.gameObject.SetActive(false);

            //removing button for LED dynamice
            addLength.SetActive(false);

            //remove bin
            binObject.SetActive(false);
            binUIObject.SetActive(false);

            //ALL fields to change LED pin
            //Text for INTPUT FIELD intial and final LED position OFF
            startLEDText.gameObject.SetActive(false);
            finalLEDText.gameObject.SetActive(false);

            //Text for start and final, above Fields
            // startText.gameObject.SetActive(false);
            // finalText.gameObject.SetActive(false);

            //Text for selected LED and Chnaged LED
            selectedLEDText.gameObject.SetActive(false);
            changedLEDText.gameObject.SetActive(false);

            //text of the current selected LED count
            currentSelectedLEDText.gameObject.SetActive(false);
            
            //Field for selected LED and Chnaged LED
            startLEDField.SetActive(false);
            finalLEDField.SetActive(false);

            //button for changing LED reading
            buttonSetLED.SetActive(false);

            //clearing error messages
            tooSmallTextLEDChange.SetActive(false);
            tooBigTextLEDChange.SetActive(false);

            //ALL fields to change LED pin
            //Text for INTPUT FIELD intial and final LED position TRUE
            analogStripLengthText.gameObject.SetActive(false);
            analogStripPinText.gameObject.SetActive(false);
            analogStripLEDAmountText.gameObject.SetActive(false);

            //dropdown
            analogDropdownAmount.SetActive(false);
            analogDropdownLength.SetActive(false);
            
            //Field for LED length and LED pin
            // analogLengthField.SetActive(false);
            analogPinField.SetActive(false);

            //button to add led
            analogAddStripButton.SetActive(false);

            //clearing error messages
            tooSmallTextLEDChange.SetActive(false);
            tooBigTextLEDChange.SetActive(false);

            //move camera
            camera.GetComponent<Transform>().transform.position = new Vector3(120f,260f,-590f);

            // changeZCamera();
        }


        //remvoing other button
        if (otherButton.activeSelf == true) {
            otherButton.SetActive(false);
        } else{
            otherButton.SetActive(true);
        }

    }

    //function for viewing lightshow
    public void ChangeToViewingMode()
    {

        //testing removin plane and brining it back

        //disabling plane when entering, enabling plane when leaving
        if (plane.activeSelf == true) {

            // print("Entering Veiewing Mode");

            //changing text to back and position to top left
            backTextViewing.text = "BACK";
            this.gameObject.GetComponent<Transform>().transform.position = new Vector3(155f,960f,0f);

            //remove plane
            plane.SetActive(false);

            //removing mainmenu title and lamp
            lamp.SetActive(false);
            mainMenuTitle.gameObject.SetActive(false);

            //move camera
            camera.GetComponent<Transform>().transform.position = new Vector3(-1300f,260f,-400f);

        } else{

            // print("Exiting Veiewing Mode");

            //changing text to back and position to middle for main menue
            backTextViewing.text = "VIEW LIGHTSHOW";
            this.gameObject.GetComponent<Transform>().transform.position = new Vector3(976f,385.3f,0f);

            //bring back plane
            plane.SetActive(true);

            //returning mainmenu title and lamp
            lamp.SetActive(true);
            mainMenuTitle.gameObject.SetActive(true);

            //move camera
            camera.GetComponent<Transform>().transform.position = new Vector3(120f,260f,-590f);
        }

        //remvoing other button
        if (otherButton.activeSelf == true) {
            otherButton.SetActive(false);
        } else{
            otherButton.SetActive(true);
        }

        // print("connectionTEXT visible");

        if (connectionText.gameObject.activeSelf == true) {
            connectionText.gameObject.SetActive(false);
        } else{
            connectionText.gameObject.SetActive(true);
        }

    }

    // //function for viewing lightshow
    // public void AddingLEDStrip()
    // {
    //     // print("ADDING a new LED Strip");

    //     //exact coords to palce an object infront of LED strips
    //     GameObject ClickDetection = Instantiate(stripObjectCollision, new Vector3(1064.35f,250.6264f,0f), stripObjectCollision.transform.rotation);
        
    //     //change collisioin based of length
    //     ClickDetection.transform.localScale = new Vector3(ClickDetection.transform.localScale.x *12 ,ClickDetection.transform.localScale.y,ClickDetection.transform.localScale.z);

    //     //LED strip fixed object
    //     GameObject StripClone = Instantiate(stripObject, new Vector3(1057.35f,249.8f,-177.35f), stripObject.transform.rotation);

    //     //LED for viewing mode
    //     GameObject StripClone2 = Instantiate(stripObject, new Vector3(-745f,249.8f,-177.35f), stripObject.transform.rotation);

    //     //removing movement from viewing clones
    //     // StripClone2.GetComponent<Movement>.enabled = false;


    //     //setting container -> LED hitbox -> LED strip
    //     ClickDetection.transform.parent = stripLEDContainer.transform;
    //     StripClone.transform.parent = ClickDetection.transform;
    //     StripClone2.transform.parent = ClickDetection.transform;

    // }
    public void AddingLength(){
        // print("ADDING new LED Digital");
        // print("current LED length is: " + led_length );


        //adding the hitbox
        GameObject stripClone = Instantiate(stripObjectCollision, new Vector3(1600f + 3f/2 * (led_length -1),250.6264f,10f), Quaternion.Euler(new Vector3(0, 0, 0)));

        //change collisioin based of length
        stripClone.transform.localScale = new Vector3(stripClone.transform.localScale.x *led_length ,stripClone.transform.localScale.y,stripClone.transform.localScale.z);

        //create object for Viewing LEDs and EditingLEDs
        // GameObject
        GameObject editingLEDs = Instantiate(stripObjectCollision, new Vector3(1600f + 3f/2 * (led_length -1),250.6264f,15f), Quaternion.Euler(new Vector3(0, 0, 0)));
        GameObject viewingLEDs = Instantiate(stripObjectCollision, new Vector3(-1325f + 3f/2 * (led_length -1),250.6264f,15f), Quaternion.Euler (0f, 0f, 0f));
        
        //place holder objects -> no need for movement detection
        editingLEDs.name = "editorLEDs";
        viewingLEDs.name = "viewingLEDs";

        Destroy(editingLEDs.GetComponentInChildren<Movement>());
        Destroy(viewingLEDs.GetComponentInChildren<Movement>());

        //set name 
        // before huy 
        // stripClone.name = "Strip" + created_LEDs + "";
        
        // huys code
        stripClone.name = "LED Strip";

        created_LEDs++;

        //store individual LED's in container
        stripClone.transform.parent = stripLEDContainer.transform;

        //setting veiwing and editing containers to collider
        editingLEDs.transform.parent = stripClone.transform;
        viewingLEDs.transform.parent = stripClone.transform;

        //loop through and add set number of LEDs EDITING MODE LEDs
        for (int i = 0; i< led_length; i++) {

            //instantiate the LED's two componenets
            GameObject ClickDetectionCube = Instantiate(single_led_cube, new Vector3(1600f + i*3f,250.6264f,15f), Quaternion.Euler(new Vector3(0, 0, 0)));

            //setting music player and container
            ClickDetectionCube.GetComponent<MakeRGB>().stripLEDContainer = stripLEDContainer;
            ClickDetectionCube.GetComponent<MakeRGB>().musicPlayer = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();

            //renameing the LEDs and cube unit to match the number, as initial order
            foreach (Transform child in ClickDetectionCube.GetComponentsInChildren<Transform>()) {
                
                //CUBE doesn't really matter
                if (child.name == "CubeUnit") {
                    // print("CUBE MATCH");

                    child.name = "Cube " + (i+1); 
                }

                //red LEDs
                if (child.name.Contains("red")){
                    // print("red MATCH");

                    child.name = "red" + (i+1); 
                }

                //blue LEDs
                if (child.name.Contains("blue")){
                    // print("blue MATCH");

                    child.name = "blue" + (i+1); 
                }

                //green LEDs
                if (child.name.Contains("green")){
                    // print("green MATCH");

                    child.name = "green" + (i+1); 
                }
            }

            //Adding LED and rectangle to parent
            ClickDetectionCube.transform.parent = editingLEDs.transform;
        }

        //loop through and add set number of LEDs VIEWING LEDs
        for (int i = 0; i< led_length; i++) {

            //instantiate the LED's two componenets
            GameObject ClickDetectionCube = Instantiate(single_led_cube, new Vector3(-1325f + i*3f,250.6264f,15f), Quaternion.Euler(new Vector3(0, 0, 0)));

             //setting music player and container
            ClickDetectionCube.GetComponent<MakeRGB>().stripLEDContainer = stripLEDContainer;
            ClickDetectionCube.GetComponent<MakeRGB>().musicPlayer = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();


            //renameing the LEDs and cube unit to match the number, as initial order
            foreach (Transform child in ClickDetectionCube.GetComponentsInChildren<Transform>()) {
                
                //CUBE doesn't really matter
                if (child.name == "CubeUnit") {
                    // print("CUBE MATCH");

                    child.name = "Cube " + (i+1); 
                }

                //red LEDs
                if (child.name.Contains("red")){
                    // print("red MATCH");

                    child.name = "red" + (i+1); 
                }

                //blue LEDs
                if (child.name.Contains("blue")){
                    // print("blue MATCH");

                    child.name = "blue" + (i+1); 
                }

                //green LEDs
                if (child.name.Contains("green")){
                    // print("green MATCH");

                    child.name = "green" + (i+1); 
                }
            }

            //Adding LED and rectangle to parent
            ClickDetectionCube.transform.parent = viewingLEDs.transform;
        }
    }

    public void addLengthAnalog()
    {

        // print("ADDING new LED analog");
        // print("current LED length is: " + analog_led_length );

        // print(currentLEDAmount);
        // print(currentLEDMeter);

        //changing LED to LED gap and number of LEDs

        //1m 90 LEDs
        // 90 LEDs 1m -> i*1.83f, localScale.y * 22/36 fits 5 exact

        //setting analog led distance between and scale of LEDs (USING 90LEDs/m as scale)

        float LEDDistanceBetweenValue = 1;
        float LEDScaleFactor = 1;

        // 1m -5m -> 90 LEDs
        if (currentLEDMeter == 1 && currentLEDAmount == 90 ) {
            
            LEDDistanceBetweenValue = 183f/100 *1;
            LEDScaleFactor = 22f/36 *1;

        } else if (currentLEDMeter == 2 && currentLEDAmount == 90) {

            LEDDistanceBetweenValue = (183f/100) *2 ;
            LEDScaleFactor = (22f/36) * 2;

        } else if (currentLEDMeter == 3 && currentLEDAmount == 90) {

            LEDDistanceBetweenValue = (183f/100) *3 ;
            LEDScaleFactor = (22f/36) * 3;

        } else if (currentLEDMeter == 4 && currentLEDAmount == 90) {

            LEDDistanceBetweenValue = (183f/100) *4 ;
            LEDScaleFactor = (22f/36) * 4;

        } else if (currentLEDMeter == 5 && currentLEDAmount == 90) {

            LEDDistanceBetweenValue = (183f/100) *5 ;
            LEDScaleFactor = (22f/36) * 5;

        }

        // 1m -5m -> 60 LEDs
        if (currentLEDMeter == 1 && currentLEDAmount == 60 ) {
            
            LEDDistanceBetweenValue = (183f/100) *3/2;
            LEDScaleFactor = (22f/36) *3/2;

        } else if (currentLEDMeter == 2 && currentLEDAmount == 60) {

            LEDDistanceBetweenValue = (183f/100) *6/2;
            LEDScaleFactor = (22f/36) *6/2;

        } else if (currentLEDMeter == 3 && currentLEDAmount == 60) {

            LEDDistanceBetweenValue = (183f/100) *9/2;
            LEDScaleFactor = (22f/36) *9/2;

        } else if (currentLEDMeter == 4 && currentLEDAmount == 60) {

            LEDDistanceBetweenValue = (183f/100) *12/2 ;
            LEDScaleFactor = (22f/36) *12/2;

        } else if (currentLEDMeter == 5 && currentLEDAmount == 60) {

            LEDDistanceBetweenValue = (183f/100) *15/2 ;
            LEDScaleFactor = (22f/36) *15/2;

        }

        // 1m -5m -> 30 LEDs
        if (currentLEDMeter == 1 && currentLEDAmount == 30 ) {
 
            LEDDistanceBetweenValue = (183f/100) *3 *1;
            LEDScaleFactor = (22f/36) *3 *1;

        } else if (currentLEDMeter == 2 && currentLEDAmount == 30) {
    
            LEDDistanceBetweenValue = (183f/100) *3 *2 ;
            LEDScaleFactor = (22f/36) *3 *2;

        } else if (currentLEDMeter == 3 && currentLEDAmount == 30) {
            
            LEDDistanceBetweenValue = (183f/100) *3 *3 ;
            LEDScaleFactor = (22f/36) *3 *3;

        } else if (currentLEDMeter == 4 && currentLEDAmount == 30) {
            
            LEDDistanceBetweenValue = (183f/100) *3 *4 ;
            LEDScaleFactor = (22f/36) *3 *4;

        } else if (currentLEDMeter == 5 && currentLEDAmount == 30) {
            
            LEDDistanceBetweenValue = (183f/100) *3 *5 ;
            LEDScaleFactor = (22f/36) *3 *5;

        }

        //adding the hitbox
        GameObject stripClone = Instantiate(stripObjectCollision, new Vector3(1600f + LEDDistanceBetweenValue/2  * (currentLEDAmount -1),250.6264f,10f), Quaternion.Euler(new Vector3(0, 0, 0)));

        //change collisioin based of length
        stripClone.transform.localScale = new Vector3(stripClone.transform.localScale.x *currentLEDAmount *LEDScaleFactor ,stripClone.transform.localScale.y,stripClone.transform.localScale.z);

        //create object for Viewing LEDs and EditingLEDs
        // GameObject
        GameObject editingLEDs = Instantiate(stripObjectCollision, new Vector3(1600f + LEDDistanceBetweenValue/2  * (currentLEDAmount -1),250.6264f,15f), Quaternion.Euler(new Vector3(0, 0, 0)));
        GameObject viewingLEDs = Instantiate(stripObjectCollision, new Vector3(-1325f + LEDDistanceBetweenValue/2  * (currentLEDAmount -1),250.6264f,15f), Quaternion.Euler (0f, 0f, 0f));
        
        //place holder objects -> no need for movement detection
        editingLEDs.name = "editorLEDs";
        viewingLEDs.name = "viewingLEDs";

        //removing movement -> edge case for my code
        Destroy(editingLEDs.GetComponentInChildren<Movement>());
        Destroy(viewingLEDs.GetComponentInChildren<Movement>());

        //set name 
        stripClone.name = "Strip" + created_LEDs + "";
        created_LEDs++;

        //store individual LED's in container
        stripClone.transform.parent = stripLEDContainer.transform;

        //setting veiwing and editing containers to collider
        editingLEDs.transform.parent = stripClone.transform;
        viewingLEDs.transform.parent = stripClone.transform;

        // //setting rotation
        // stripClone.transform.rotation =  Quaternion.Euler(new Vector3(0, 0, 0));

        // print(stripLEDContainer.transform.rotation);
        // print(stripClone.transform.rotation);
        // print(editingLEDs.transform.rotation);
        // print(viewingLEDs.transform.rotation);

        //144 LED for 5m

        // GameObject testLED = Instantiate(single_led_cube, new Vector3(1600f,250.6264f,0.1f), single_led_cube.transform.rotation);
        // testLED.transform.localScale = new Vector3(testLED.transform.localScale.y * 720 * 2/3 ,testLED.transform.localScale.y,testLED.transform.localScale.z);

        //loop through and add set number of LEDs EDITING MODE LEDs
        for (int i = 0; i< currentLEDAmount; i++) {

            //instantiate the LED's two componenets
            // GameObject ClickDetectionCube = Instantiate(single_led_cube, new Vector3(1600f + i*3f,250.6264f,0.1f), single_led_cube.transform.rotation);
            GameObject ClickDetectionCube = Instantiate(single_led_cube, new Vector3(1600f + i*LEDDistanceBetweenValue ,250.6264f,15f), Quaternion.Euler(new Vector3(0, 0, 0)));
            ClickDetectionCube.transform.localScale = new Vector3(ClickDetectionCube.transform.localScale.y * LEDScaleFactor ,ClickDetectionCube.transform.localScale.y,ClickDetectionCube.transform.localScale.z);

            //setting music player and container
            ClickDetectionCube.GetComponent<MakeRGB>().stripLEDContainer = stripLEDContainer;
            ClickDetectionCube.GetComponent<MakeRGB>().musicPlayer = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();

            //renameing the LEDs and cube unit to match the number, as initial order
            foreach (Transform child in ClickDetectionCube.GetComponentsInChildren<Transform>()) {
                
                //CUBE doesn't really matter
                if (child.name == "CubeUnit") {
                    // print("CUBE MATCH");

                    child.name = "Cube " + (analog_led_pin); 
                }

                //red LEDs
                if (child.name.Contains("red")){
                    // print("red MATCH");

                    child.name = "red" + (analog_led_pin); 
                }

                //blue LEDs
                if (child.name.Contains("blue")){
                    // print("blue MATCH");

                    child.name = "blue" + (analog_led_pin); 
                }

                //green LEDs
                if (child.name.Contains("green")){
                    // print("green MATCH");

                    child.name = "green" + (analog_led_pin); 
                }
            }

            //Adding LED and rectangle to parent
            ClickDetectionCube.transform.parent = editingLEDs.transform;
        }

        //loop through and add set number of LEDs VIEWING LEDs
        for (int i = 0; i< currentLEDAmount; i++) {

            //instantiate the LED's two componenets
            GameObject ClickDetectionCube = Instantiate(single_led_cube, new Vector3(-1325f + i*LEDDistanceBetweenValue,250.6264f,15f), single_led_cube.transform.rotation);
            ClickDetectionCube.transform.localScale = new Vector3(ClickDetectionCube.transform.localScale.y * LEDScaleFactor ,ClickDetectionCube.transform.localScale.y,ClickDetectionCube.transform.localScale.z);

             //setting music player and container
            ClickDetectionCube.GetComponent<MakeRGB>().stripLEDContainer = stripLEDContainer;
            ClickDetectionCube.GetComponent<MakeRGB>().musicPlayer = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();

            //renameing the LEDs and cube unit to match the number, as initial order
            foreach (Transform child in ClickDetectionCube.GetComponentsInChildren<Transform>()) {
                
                //CUBE doesn't really matter
                if (child.name == "CubeUnit") {
                    // print("CUBE MATCH");

                    child.name = "Cube " + (analog_led_pin); 
                }

                //red LEDs
                if (child.name.Contains("red")){
                    // print("red MATCH");

                    child.name = "red" + (analog_led_pin); 
                }

                //blue LEDs
                if (child.name.Contains("blue")){
                    // print("blue MATCH");

                    child.name = "blue" + (analog_led_pin); 
                }

                //green LEDs
                if (child.name.Contains("green")){
                    // print("green MATCH");

                    child.name = "green" + (analog_led_pin); 
                }
            }

            //Adding LED and rectangle to parent
            ClickDetectionCube.transform.parent = viewingLEDs.transform;
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //current selected text updates
        if (selectedLED != null && this.gameObject.name == "EditMOde") 
        {

            currentSelectedLEDText.text = "Selected LED Strip has Length : " + selectedLEDlength;

        } else if (selectedLED == null && this.gameObject.name == "EditMOde"){

            currentSelectedLEDText.text = "Selected LED Strip has Length : ???";

        }
    }
}