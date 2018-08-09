using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shared : MonoBehaviour {

    public GameObject earth;
    public GameObject moon;
    public GameObject moon2;

    public static ArrayList moons = new ArrayList();

    private float G = 6.67f * Mathf.Pow(10,-11);

	// Use this for initialization
	void Start () {
        earth.GetComponent<Rigidbody>().mass = 10000000000;
        moon.GetComponent<Rigidbody>().mass = 10;
        moon2.GetComponent<Rigidbody>().mass = 100;

        earth.transform.position = new Vector3(0,-0.5f,2);
        moon.transform.position = new Vector3(0, -0.5f, 2.6f);
        moon2.transform.position = new Vector3(0.4f,-0.5f,2);

        earth.transform.localScale = new Vector3(0.32f, 0.32f, 0.32f);
        moon.transform.localScale = new Vector3(0.08f,0.08f,0.08f);
        moon2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        moon.GetComponent<Rigidbody>().velocity = new Vector3(0.32f,0.1f,0);
        moon2.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, -0.3f, 0.2f);

        moons.Add(moon);
        moons.Add(moon2);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        CleanMoons();

        foreach(GameObject m in moons)
        {
            ApplyGravity(earth,m);
            ApplyGravity(m, earth);
            foreach (GameObject m1 in moons)
            {
                if (!m1.Equals(m))
                {
                    ApplyGravity(m, m1);
                    ApplyGravity(m1, m);
                }
            }
        }
    }

    private void CleanMoons()
    {
        for(int i = 0; i < moons.Count; i++)
        {
            if (moons[i] == null)
            {
                moons.Remove(moons[i]);
            }
        }
    }

    private void ApplyGravity(GameObject CenterObject, GameObject OrbitObject)
    {
        if (CenterObject != null && OrbitObject != null)
        {
            float distance = Vector3.Distance(CenterObject.transform.position, OrbitObject.transform.position);

            float gravityFoce = (G * CenterObject.GetComponent<Rigidbody>().mass * OrbitObject.GetComponent<Rigidbody>().mass) / (distance * distance);

            Vector3 vectorForce = GetVectorForce(CenterObject.transform.position, OrbitObject.transform.position, gravityFoce);

            OrbitObject.GetComponent<Rigidbody>().AddForce(vectorForce);
        }
    }

    public static Vector3 GetVectorForce(Vector3 position1, Vector3 position2, float force)
    {
        Vector3 diff = position1 - position2;
        diff.Normalize();
        return diff * force;
    }
}
