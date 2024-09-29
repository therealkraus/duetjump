using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadSpawnRules : MonoBehaviour {

    public bool canSpawnCircle = true;
    public bool canSpawnCone = true;
    //public bool canSpawnEmpty;


    private void Awake()
    {
        if (transform.localEulerAngles.y > 21 && transform.localEulerAngles.y < 23)
        {
            //Debug.Log(transform.eulerAngles.y);
            canSpawnCone = false;
        }
        else if (transform.localEulerAngles.y > 66 && transform.localEulerAngles.y < 68)
        {
            //Debug.Log(transform.eulerAngles.y);
            canSpawnCone = false;
        }
        else if (transform.localEulerAngles.y == 0)
        {
            //Debug.Log(transform.eulerAngles.y);
            canSpawnCircle = false;
        }
        else if (transform.localEulerAngles.y == 90)
        {

            canSpawnCircle = false;
        }
        else
        {
            //Debug.Log(transform.localEulerAngles.y);
            //Debug.Log(transform.localRotation.eulerAngles.y);
            //Debug.Log(transform.rotation.eulerAngles.y);
        }
    }
    // Use this for initialization
    void Start () {



    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
