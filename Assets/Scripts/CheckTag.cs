using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTag : MonoBehaviour {

    GameObject sceneController;
    ColorController colorController;

	// Use this for initialization
	void Start () {

        sceneController = GameObject.Find("_SceneManager");
        colorController = sceneController.GetComponent<ColorController>();

        if (transform.tag == "DeathPad")
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material = colorController.deathMaterial;
            }
        }	
	}
}
