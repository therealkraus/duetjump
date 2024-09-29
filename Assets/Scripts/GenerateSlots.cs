using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSlots : MonoBehaviour
{

    public GameObject cone;
    public GameObject circle;
    public GameObject ring;
    RingSlots ringSlots;
    public List<Transform> slots = new List<Transform>();
    GameObject RightSide;

    int d;
    int numOfSlots;

    private void Awake()
    {
        ringSlots = ring.GetComponent<RingSlots>();
    }

    public int FillUpSlots(int a, int b, int c)
    {
        d = c;
        if (transform.name == "LeftSide")
        {
            numOfSlots = transform.childCount;

            for (int i = 0; i < numOfSlots; i++)
            {
                slots.Add(transform.GetChild(i));
            }

            int slotsLeft = Random.Range(2, 3);
            Instantiate(circle, slots[slotsLeft]);
            slots[slotsLeft].tag = "ScorePad";
            slots.RemoveAt(slotsLeft);

            //slots.RemoveAt(RandomSlotRemoved());
            //for (int i = 0; i < slots.Count; i++)
            //{
            //    PadSpawnRules padSpawnRules = slots[i].GetComponent<PadSpawnRules>();
            //    if (padSpawnRules.canSpawnCone == true)
            //    {
            //        Debug.Log(slots[i].localEulerAngles + "canSpawnCone == true");

            //    }
            //    else if (padSpawnRules.canSpawnCone == false)
            //    {
            //        Debug.Log(slots[i].localEulerAngles + "canSpawnCone == false");
            //        slots.RemoveAt(i);
            //    }

            //}

            slotsLeft = Random.Range(0, slots.Count);
            for (int i = 0; i < Random.Range(a, b); i++)
            {
                Instantiate(cone, slots[slotsLeft]);
                slots[slotsLeft].tag = "DeathPad";
                slots.RemoveAt(slotsLeft);
                slotsLeft = Random.Range(0, slots.Count);
            }

            Transform clone2 = Instantiate(transform, transform.position, transform.rotation, transform.parent);
            clone2.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
            clone2.name = "RightSide";
        }
        c = d;
        return c;
    }

    int RandomSlotRemoved()
    {
        if (d == 0)
        {
            //Debug.Log(d);
            int itemIndex = 2;
            d = 1;
            return itemIndex;
        }
        else if (d == 1)
        {
            //Debug.Log(d);
            int itemIndex = 1;
            d = 0;
            return itemIndex;
        }
        else
        {
            return 1;
        }
    }
}
