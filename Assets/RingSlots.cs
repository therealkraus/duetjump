using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSlots : MonoBehaviour {

    public List<GameObject> circleList;
    public List<GameObject> coneList;


    public void FindSlots()
    {
        Transform[] transforms = transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in transforms)
        {
            if (t.name == "Cone")
            {
                coneList.Add(t.gameObject);
            }
            else if (t.name == "TouchCircle")
            {
                circleList.Add(t.gameObject);
            }
        }
    }
}
