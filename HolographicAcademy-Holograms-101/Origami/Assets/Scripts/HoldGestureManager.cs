using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldGestureManager : MonoBehaviour {

    public static bool holding = false;


    public static void OnHoldStart()
    {
        holding = true;
        //SpatialMapping.Instance.DrawVisualMeshes = true;
    }

    public static void OnHoldCompleted()
    {
        //SpatialMapping.Instance.DrawVisualMeshes = false;
        holding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (holding)
        {
            
            GazeGestureManager.throwPower += 0.5f*Time.deltaTime;

            //transform.parent.position = Camera.main.transform.position + Camera.main.transform.forward * 2.0f;
        }
    }


    // Use this for initialization
    void Start () {
        
	}

}
