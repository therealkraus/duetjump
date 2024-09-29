using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that spawns rings for playing
public class RingSpawner : MonoBehaviour
{
    public int heightOffset;
    public List<GameObject> ringPrefabs = new List<GameObject>();
    public GameObject startRing;
    public float[] ringChances;

    public List<GameObject> ringDifficulty0Prefabs = new List<GameObject>();
    public List<GameObject> ringDifficulty1Prefabs = new List<GameObject>();
    public List<GameObject> ringDifficulty2Prefabs = new List<GameObject>();
    public List<GameObject> ringDifficulty3Prefabs = new List<GameObject>();

    public float[] ringDifficulty0PrefabsChances;
    public float[] ringDifficulty1PrefabsChances;
    public float[] ringDifficulty2PrefabsChances;
    public float[] ringDifficulty3PrefabsChances;


    int a = 0;
    int c = 0;
    public int ringPosition;
    public List<GameObject> cloneList = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        //Spawns the starting ring
        Instantiate(startRing, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)), transform);

        ringPosition += heightOffset;

        //Spawns the first ten rings, with 1 to 2 cone pairs on them
        //SpawnRings(0, 1, 5);
       


    }

    public void SpawnStart()
    {
        SpawnRingDifficulty0(5);
        RandomPingPongDifficulty0(2);
        ClearList();
    }

    //Function that spawns rings, takes in two values that determine how many cone pairs spawn on those rings
    public void SpawnRings(int a, int b, int ringsToSpawn)
    {
        for (int i = 0; i < ringsToSpawn; i++)
        {
            GameObject clone = Instantiate(ringPrefabs[Choose(ringChances)], new Vector3(0, ringPosition, 0), RandomRotation(), transform);
            cloneList.Add(clone);

            //GenerateSlots generateSlots = cloneList[i].GetComponentInChildren<GenerateSlots>();
            //c = generateSlots.FillUpSlots(a, b, c);

            ringPosition += heightOffset;
            RingSlots ringSlots = cloneList[i].GetComponentInChildren<RingSlots>();
            ringSlots.FindSlots();
        }

    }

    public void SpawnRingDifficulty0(int ringsToSpawn)
    {
        for (int i = 0; i < ringsToSpawn; i++)
        {
            GameObject clone = Instantiate(ringDifficulty0Prefabs[Choose(ringDifficulty0PrefabsChances)], new Vector3(0, ringPosition, 0), RandomRotation(), transform);
            cloneList.Add(clone);

            ringPosition += heightOffset;
            RingSlots ringSlots = cloneList[i].GetComponentInChildren<RingSlots>();
            ringSlots.FindSlots();
        }
    }

    public void SpawnRingDifficulty1(int ringsToSpawn)
    {
        for (int i = 0; i < ringsToSpawn; i++)
        {
            GameObject clone = Instantiate(ringDifficulty1Prefabs[Choose(ringDifficulty1PrefabsChances)], new Vector3(0, ringPosition, 0), RandomRotation(), transform);
            cloneList.Add(clone);

            ringPosition += heightOffset;
            RingSlots ringSlots = cloneList[i].GetComponentInChildren<RingSlots>();
            ringSlots.FindSlots();
        }
    }

    public void SpawnRingDifficulty2(int ringsToSpawn)
    {
        for (int i = 0; i < ringsToSpawn; i++)
        {
            GameObject clone = Instantiate(ringDifficulty2Prefabs[Choose(ringDifficulty2PrefabsChances)], new Vector3(0, ringPosition, 0), RandomRotation(), transform);
            cloneList.Add(clone);

            ringPosition += heightOffset;
            RingSlots ringSlots = cloneList[i].GetComponentInChildren<RingSlots>();
            ringSlots.FindSlots();
        }
    }

    public void SpawnRingDifficulty3(int ringsToSpawn)
    {
        for (int i = 0; i < ringsToSpawn; i++)
        {
            GameObject clone = Instantiate(ringDifficulty3Prefabs[Choose(ringDifficulty3PrefabsChances)], new Vector3(0, ringPosition, 0), RandomRotation(), transform);
            cloneList.Add(clone);

            ringPosition += heightOffset;
            RingSlots ringSlots = cloneList[i].GetComponentInChildren<RingSlots>();
            ringSlots.FindSlots();
        }
    }
    public void RandomPingPongDifficulty0(int b)
    {
        int[] directions = new int[] { -1, 1 };
        float[] degrees = new float[] { 10, 15, 20 };
        int[] speed = new int[] { 10, 15, 20 };

        for (int i = 0; i < b; i++)
        {
            GameObject clone = cloneList[Random.Range(0, cloneList.Count)];
            RingRotation ringRotation = clone.GetComponent<RingRotation>();
            //ringRotation.speed = Random.Range(20,40);

            if (clone.transform.eulerAngles.y == 70)
            {
                ringRotation.direction = 1;
            }
            else
            {
                ringRotation.direction = -1;
            }
            // ringRotation.direction = directions[Random.Range(0, directions.Length)];

            //ringRotation.degrees = Random.Range(20,60);
            //float selectedSpeed = speed[Random.Range(0, speed.Length)];
            float selectedDegrees = degrees[Random.Range(0, degrees.Length)];

            if (selectedDegrees == 20)
            {
                ringRotation.speed = Random.Range(15, 25);
            }
            else if (selectedDegrees == 15)
            {
                ringRotation.speed = Random.Range(10, 20);
            }
            else if (selectedDegrees == 10)
            {
                ringRotation.speed = Random.Range(10, 15);
            }
            else
            {
                ringRotation.speed = speed[Random.Range(0, speed.Length)];
            }

            ringRotation.degrees = degrees[Random.Range(0, degrees.Length)];

            ringRotation._PingPong = true;

        }
    }

    //Function that adds the ring rotation script to rings, takes in two values. b and s. b is used to determine how many times ringRotation script is added to the rings. s is the speed of the rotation.
    public void RandomPingPongDifficulty1(int b)
    {
        int[] directions = new int[] { -1, 1 };
        float[] degrees = new float[] { 20, 30};
        int[] speed = new int[] { 20, 30};

        for (int i = 0; i < b; i++)
        {
            GameObject clone = cloneList[Random.Range(0, cloneList.Count)];
            RingRotation ringRotation = clone.GetComponent<RingRotation>();
            //ringRotation.speed = Random.Range(20,40);

            if (clone.transform.eulerAngles.y == 70)
            {
                ringRotation.direction = 1;
            }
            else
            {
                ringRotation.direction = -1;
            }
            // ringRotation.direction = directions[Random.Range(0, directions.Length)];

            //ringRotation.degrees = Random.Range(20,60);
            //float selectedSpeed = speed[Random.Range(0, speed.Length)];
            float selectedDegrees = degrees[Random.Range(0, degrees.Length)];

            if (selectedDegrees == 30)
            {
                ringRotation.speed = Random.Range(25, 35);
            }
            else if (selectedDegrees == 20)
            {
                ringRotation.speed = Random.Range(15, 25);
            }
            else
            {
                ringRotation.speed = speed[Random.Range(0, speed.Length)];
            }

            ringRotation.degrees = degrees[Random.Range(0, degrees.Length)];

            ringRotation._PingPong = true;

        }
    }

    public void RandomPingPongDifficulty2(int b)
    {
        int[] directions = new int[] { -1, 1 };
        float[] degrees = new float[] { 20, 30, 35};
        int[] speed = new int[] { 30, 40, 50 };

        foreach (GameObject clone in cloneList)
        {
            //= cloneList[Random.Range(0, cloneList.Count)];
            RingRotation ringRotation = clone.GetComponent<RingRotation>();
            //ringRotation.speed = Random.Range(20,40);

            if (clone.transform.eulerAngles.y == 70)
            {
                ringRotation.direction = 1;
            }
            else
            {
                ringRotation.direction = -1;
            }
            // ringRotation.direction = directions[Random.Range(0, directions.Length)];

            //ringRotation.degrees = Random.Range(20,60);
            //float selectedSpeed = speed[Random.Range(0, speed.Length)];
            float selectedDegrees = degrees[Random.Range(0, degrees.Length)];

            if (selectedDegrees == 35)
            {
                ringRotation.speed = Random.Range(40, 55);
            }
            else if (selectedDegrees == 30)
            {
                ringRotation.speed = Random.Range(25, 35);
            }
            else if (selectedDegrees == 20)
            {
                ringRotation.speed = Random.Range(15, 25);
            }
            else
            {
                ringRotation.speed = speed[Random.Range(0, speed.Length)];
            }

            ringRotation.degrees = degrees[Random.Range(0, degrees.Length)];

            ringRotation._PingPong = true;
        }


    }






    //Function that adds the ring rotation script to rings, takes in two values. b and s. b is used to determine how many times ringRotation script is added to the rings. s is the speed of the rotation.
    public void RandomPingPong(int b)
    {
        int[] directions = new int[] { -1, 1 };
        float[] degrees = new float[] { 20, 30, 35 };
        int[] speed = new int[] { 20, 30, 40 };

        for (int i = 0; i < b; i++)
        {
            GameObject clone = cloneList[Random.Range(0, cloneList.Count)];
            RingRotation ringRotation = clone.GetComponent<RingRotation>();
            //ringRotation.speed = Random.Range(20,40);

            if (clone.transform.eulerAngles.y == 70)
            {
                ringRotation.direction = 1;
            }
            else
            {
                ringRotation.direction = -1;
            }
           // ringRotation.direction = directions[Random.Range(0, directions.Length)];

            //ringRotation.degrees = Random.Range(20,60);
            //float selectedSpeed = speed[Random.Range(0, speed.Length)];
            float selectedDegrees = degrees[Random.Range(0, degrees.Length)];

            if (selectedDegrees == 35)
            {
                ringRotation.speed = Random.Range(30,40);
            }
            else if (selectedDegrees == 30)
            {
                ringRotation.speed = Random.Range(25, 35);
            }
            else if (selectedDegrees == 20)
            {
                ringRotation.speed = Random.Range(20, 30);
            }
            else
            {
                ringRotation.speed = speed[Random.Range(0, speed.Length)];
            }

            ringRotation.degrees = degrees[Random.Range(0, degrees.Length)];

            ringRotation._PingPong = true;

        }
    }

    //Clears the list of all clones of rings
    public void ClearList()
    {
        cloneList.Clear();
    }

    //Function that is called when rings are spawned, it randomly changes their rotation between two values
    Quaternion RandomRotation()
    {
        if (a == 0)
        {
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 70, 0));
            a = 1;
            return rotation;
        }
        else if (a == 1)
        {
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            a = 0;
            return rotation;
        }
        else
        {
            return transform.rotation;
        }
    }

    //WILL BE REMOVED
    int Choose(float[] probs)
    {

        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }

}
