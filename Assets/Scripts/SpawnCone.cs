using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Spawns in cones, when called after a delay
public class SpawnCone : MonoBehaviour
{

    public GameObject cone;

    public void CanSpawn()
    {
        Instantiate(cone, transform);
        Invoke("SetDeathPad", 0.25f);
    }

    //Sets the pads tag to deathPad
    void SetDeathPad()
    {
        transform.tag = "DeathPad";
    }
}
