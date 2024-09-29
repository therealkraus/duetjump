using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorController : MonoBehaviour
{
    public static ColorController instance = null;
    public bool changeColor = false;
    public GameObject player;
    Transform[] playerChildObjects;

    public Material noDeathMaterial;
    public Material deathMaterial;
    public Material skyMaterial;

    ColorSchemes colorSchemes;

    public List<int> randomList = new List<int>();
    int a = 0;

    public static ColorSchemes[] ColorSchemes = new ColorSchemes[]
   {
        new ColorSchemes(
            new Color32(0,0,0,255),
           new Color32(0,0,0,255),
           new Color32(249,202,92,255),
           new Color32(54,218,255,255),
           new Color32(159,189,181,255),
           new Color32(169,145,71,255)
            ),
       new ColorSchemes(
                new Color32(0,0,0,255),
           new Color32(0,0,0,255),
           new Color32(203,101,101,255),
           new Color32(117,236,117,255),
           new Color32(140,199,198,255),
           new Color32(99,92,155,255)
            ),
       new ColorSchemes(
              new Color32(0,0,0,255),
           new Color32(0,0,0,255),
           new Color32(150,115,204,255),
           new Color32(173,255,47,255),
           new Color32(155,140,199,255),
           new Color32(95,155,92,255)
            ),
        new ColorSchemes(
           new Color32(0,0,0,255),
           new Color32(0,0,0,255),
           new Color32(183,111,73,255),
           new Color32(119,136,153,255),
           new Color32(178,199,140,255),
           new Color32(92,155,134,255)
            )
   };

    int PickRandom()
    {
        if (a == 0)
        {
            return a;
        }
        else if (a == 1)
        {
            return a;
        }
        else if (a == 2)
        {
            return a;
        }
        else if (a == 3)
        {
            return a;
        }
        else
        {
            a = 0;
            return 0;
        }
        //if (randomList.Count == 0)
        //{
        //    randomList.Add(0);
        //    randomList.Add(1);
        //    randomList.Add(2);
        //    randomList.Add(3);
        //}


        //a = Random.Range(0, randomList.Count);
        //randomList.Add(a);
        
        //Debug.Log(a);
        //return a;
    }

    private void Awake()
    {

        if (instance == null)
            instance = this;
        else if (instance != this)           
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //player = GameObject.Find("Player");
        //colorSchemes = ColorSchemes[PickRandom()];

        //    playerChildObjects = player.GetComponentsInChildren<Transform>();

        //    foreach (Transform objt in playerChildObjects)
        //    {
        //        if (objt.tag == "LeftSphere")
        //        {
        //            UpdatePlayerColor(objt, colorSchemes.leftSphereColor);
        //        }

        //        else if (objt.tag == "RightSphere")
        //        {
        //            UpdatePlayerColor(objt, colorSchemes.rightSphereColor);
        //        }
        //    }

        //    noDeathMaterial.color = colorSchemes.noDeathPadColor;
        //    deathMaterial.color = colorSchemes.deathPadColor;

        //    skyMaterial.SetColor("_TopColor", colorSchemes.skyGradientColorTop);
        //    skyMaterial.SetColor("_BottomColor", colorSchemes.skyGradientColorBottom);
    }

    private void Update()
    {
        if (changeColor == true)
        {
            GameObject[] goArray = SceneManager.GetSceneByName("main").GetRootGameObjects();
            foreach (GameObject item in goArray)
            {
                
                if (item.name == "Player")
                {
                    Debug.Log(item.name);
                    player = item.gameObject;
                }
            }
            colorSchemes = ColorSchemes[a];
            a++;
            if (a == 4)
            {
                a = 0;
            }

            playerChildObjects = player.GetComponentsInChildren<Transform>();

            foreach (Transform objt in playerChildObjects)
            {
                if (objt.tag == "LeftSphere")
                {
                    UpdatePlayerColor(objt, colorSchemes.leftSphereColor);
                }

                else if (objt.tag == "RightSphere")
                {
                    UpdatePlayerColor(objt, colorSchemes.rightSphereColor);
                }
            }

            noDeathMaterial.color = colorSchemes.noDeathPadColor;
            deathMaterial.color = colorSchemes.deathPadColor;

            skyMaterial.SetColor("_TopColor", colorSchemes.skyGradientColorTop);
            skyMaterial.SetColor("_BottomColor", colorSchemes.skyGradientColorBottom);

            changeColor = false;
        }
    }

    void UpdatePlayerColor(Transform objt, Color32 playerColor)
    {
        if (objt.GetComponent<SkinnedMeshRenderer>() != null)
        {
            objt.GetComponent<SkinnedMeshRenderer>().material.color = playerColor;
        }
        else if (objt.GetComponent<ParticleSystem>() != null)
        {
            ParticleSystem particleSystem = objt.GetComponent<ParticleSystem>();

            ParticleSystem.MainModule mainModule = particleSystem.main;
            ParticleSystem.TrailModule trailModule = particleSystem.trails;

            mainModule.startColor = new ParticleSystem.MinMaxGradient(playerColor);

            if (trailModule.enabled)
            {
                trailModule.colorOverTrail = new ParticleSystem.MinMaxGradient(playerColor);
            }
        }
        else if (objt.GetComponent<TrailRenderer>() != null)
        {
            TrailRenderer trailRenderer = objt.GetComponent<TrailRenderer>();
            trailRenderer.startColor = playerColor;
        }
    }


}
