using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {

    private float forcetime = 5000;
    private float startime;

	// Use this for initialization
	void Start () {
        startime = Time.time;
        GetComponentInParent<Rigidbody>().AddForce(new Vector3(2, 0.5f, 1));
    }
	
	// Update is called once per frame
	void Update () {
        //transform.Rotate(new Vector3(24,35,10)*Time.deltaTime);



	}
}
