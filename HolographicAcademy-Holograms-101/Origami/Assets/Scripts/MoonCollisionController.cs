using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonCollisionController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>() && gameObject!=null)
        {
            Debug.LogError("Entering this parrt");
            if (other.gameObject.GetComponent<Rigidbody>().mass > GetComponent<Rigidbody>().mass)
            {
                other.gameObject.GetComponent<Rigidbody>().mass += GetComponent<Rigidbody>().mass;
                Destroy(gameObject);
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().mass += other.GetComponent<Rigidbody>().mass;
                Destroy(other.gameObject);
            }
        }
    }
}
