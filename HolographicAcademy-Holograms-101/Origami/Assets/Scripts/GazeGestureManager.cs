using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

public class GazeGestureManager : MonoBehaviour
{
    public static GazeGestureManager Instance { get; private set; }

    public GameObject moon;

    public Slider speedSlider;
    public Text speedText;
    public Slider massSlider;
    public Text massText;

    public bool holding;
    public bool gotMass;

    public static float throwPower=0;
    public static float throwMass = 10;

    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }

    GestureRecognizer recognizer;

    // Use this for initialization
    void Awake()
    {
        Instance = this;
        speedSlider.value = 0;
        massSlider.value = 0;
        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Hold | GestureSettings.Tap);


        recognizer.Tapped += (args) =>
        {
            Debug.Log("Tapped***************!");
        };
        recognizer.HoldStartedEvent += (source, ray) =>
        {
            Debug.Log("Hold Started-----------!");
            holding = true;
        };
        recognizer.HoldCompletedEvent += (source, ray) =>
        {
            Debug.Log("Hold Ended>>>>>>>>>>>>>>>>>!");
            if (gotMass)
            {
                holding = false;
                gotMass = false;
                ThrowMoon();
                throwPower = 0;
                throwMass = 10;
                speedText.text = "0 M/S";
                massText.text = "10 kg";
                speedSlider.value = 0;
                massSlider.value = 0;
            }
            else
            {
                gotMass = true;
                holding = false;
            }

        };
        recognizer.HoldCanceledEvent += (source, ray) =>
        {
            Debug.Log("Hold Ended>>>>>>>>>>>>>>>>>!");
            if (gotMass)
            {
                holding = false;
                gotMass = false;
                ThrowMoon();
                throwPower = 0;
                throwMass = 10;
                speedText.text = "0 M/S";
                massText.text = "10 kg";
                speedSlider.value = 0;
                massSlider.value = 0;
            }
            else
            {
                gotMass = true;
                holding = false;
            }
        };

        Debug.Log("Starting REcognizing");
        recognizer.StartCapturingGestures();
    }


    public void ThrowMoon()
    {
        GameObject mo = Instantiate(moon, Camera.main.transform.position+Camera.main.transform.forward, new Quaternion());
        mo.GetComponent<Rigidbody>().velocity = Shared.GetVectorForce(new Vector3(), Camera.main.transform.forward, -throwPower);
        mo.GetComponent<Rigidbody>().mass = throwMass;
        Shared.moons.Add(mo);
    }

    // Update is called once per frame
    void Update()
    {
        // Figure out which hologram is focused this frame.
        GameObject oldFocusObject = FocusedObject;

        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram, use that as the focused object.
            FocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            // If the raycast did not hit a hologram, clear the focused object.
            FocusedObject = null;
        }

        // If the focused object changed this frame,
        // start detecting fresh gestures again.
        //if (FocusedObject != oldFocusObject)
        //{
        //    recognizer.CancelGestures();
        //    recognizer.StartCapturingGestures();
        //}

        if (holding)
        {
            if (gotMass)
            {
                throwPower += 0.5f * Time.deltaTime;
                speedSlider.value = throwPower;
                speedText.text = throwPower + " M/S";
            }
            else
            {
                throwMass += 500*Time.deltaTime;
                massSlider.value = throwMass;
                massText.text = throwMass + " Kg";
            }
        }
    }
}
