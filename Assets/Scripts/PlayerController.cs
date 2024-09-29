using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("Player Attributes")]
    public float jumpHeight;

    [Space(10)]
    Transform leftSphere;
    Transform rightSphere;
    public GameObject sceneManager;
    public GameObject floatingTextScore;
    Animator animatorFloatingTextScore;
    Text textScore;
    FloatingText floatingText;

    public GameObject MainCamera;
    CameraController cameraController;

    SceneController sceneController;
    PlayerSettings playerSettings;
    RingSpawner ringSpawner;

    ParticleSystem[] playerParticleSystems;

    Rigidbody rb;
    Animator animatorLeftSphere;
    Animator animatorRightSphere;

    //Vector3 doffset = new Vector3(0, -0.05f, 0);

    float rayLength;
    float rayOffset = 0.05f;
    int layerMask = 1 << 8;

    Collider leftSphereCollider;
    Collider rightSphereCollider;


    //[Header("Debug")]
    //Debug Tool, to slow down time
    //[Range(0f, 1f)]
    //public float time;
    Transform ringHit;

    bool rayCheck = true;

    int ExamineRayHitCounter = 0;

    int circleCount = 1;
    int whenToSpawn = 7;
    int triggerCounter = 0;

    private void Awake()
    {
        //Get playerSettings, ringSpawner, sceneController script from sceneManager
        playerSettings = sceneManager.GetComponent<PlayerSettings>();
        ringSpawner = sceneManager.GetComponent<RingSpawner>();
        sceneController = sceneManager.GetComponent<SceneController>();
        cameraController = MainCamera.GetComponent<CameraController>();

        //Set isPlayerAlive and IsPaused to true at the start of the game
        sceneController.IsPlayerAlive = true;
        sceneController.IsPaused = true;
    }

    // Use this for initialization
    private void Start()
    {
        //Get Player rigidbody
        rb = GetComponent<Rigidbody>();

        //Get leftSphere and rightSphere child transforms
        leftSphere = transform.GetChild(0);
        rightSphere = transform.GetChild(1);

        //Get animator component from child objects, leftSphere and rightSphere
        animatorLeftSphere = leftSphere.GetComponent<Animator>();
        animatorRightSphere = rightSphere.GetComponent<Animator>();

        animatorFloatingTextScore = floatingTextScore.GetComponent<Animator>();
        floatingText = floatingTextScore.GetComponent<FloatingText>();
        textScore = floatingTextScore.GetComponent<Text>();

        //Get collider component from child objects, leftSphere and rightSphere
        leftSphereCollider = leftSphere.GetComponent<Collider>();
        rightSphereCollider = rightSphere.GetComponent<Collider>();

        //All child particle systems of Player gameobject are found and assigned to array
        playerParticleSystems = GetComponentsInChildren<ParticleSystem>();

        //Length of ray based on sphere radius plus offset, 0.05f
        rayLength = leftSphere.GetComponent<Collider>().bounds.extents.y;
        rayLength += rayOffset;
        layerMask = ~layerMask;

    }

    private void FixedUpdate()
    {
        if (sceneController.IsPlayerAlive == true)
        {
            Rays(leftSphereCollider);
          //Rays(rightSphereCollider);        
        }

    }

    private void Rays(Collider collider)
    {
        RaycastHit centerRay;
        RaycastHit leftRay;
        RaycastHit forwardRay;
        RaycastHit backwardRay;
        RaycastHit rightRay;

        Vector3 centerOrigin = collider.bounds.center;
        Vector3 boundPoint1 = collider.bounds.min;
        Vector3 boundPoint2 = collider.bounds.max;
        Vector3 boundPoint3 = new Vector3(boundPoint1.x, boundPoint1.y, boundPoint2.z);
        Vector3 boundPoint4 = new Vector3(boundPoint1.x, boundPoint2.y, boundPoint1.z);
        Vector3 boundPoint5 = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint1.z);
        Vector3 boundPoint6 = new Vector3(boundPoint1.x, boundPoint2.y, boundPoint2.z);
        Vector3 boundPoint7 = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint2.z);
        Vector3 boundPoint8 = new Vector3(boundPoint2.x, boundPoint2.y, boundPoint1.z);

        if (Physics.Raycast(centerOrigin, Vector3.down, out centerRay, rayLength, layerMask))
        {
            //Debug.Log("centerRay");
            ExamineRayHit(centerRay);
        }
        else if (Physics.Raycast(FindMedianPoint(collider, boundPoint1, boundPoint3), Vector3.down, out leftRay, rayLength, layerMask))
        {
            // Debug.Log("leftRay");
            ExamineRayHit(leftRay);
        }
        else if (Physics.Raycast(FindMedianPoint(collider, boundPoint2, boundPoint6), Vector3.down, out forwardRay, rayLength, layerMask))
        {
            // Debug.Log("forwardRay");
            ExamineRayHit(forwardRay);
        }
        else if (Physics.Raycast(FindMedianPoint(collider, boundPoint4, boundPoint8), Vector3.down, out backwardRay, rayLength, layerMask))
        {
            // Debug.Log("backwardRay");
            ExamineRayHit(backwardRay);
        }
        else if (Physics.Raycast(FindMedianPoint(collider, boundPoint5, boundPoint7), Vector3.down, out rightRay, rayLength, layerMask))
        {
            // Debug.Log("rightRay");
            ExamineRayHit(rightRay);
        }
    }

    //Finds the median point between two points
    private Vector3 FindMedianPoint(Collider collider, Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 medianPoint = Vector3.Lerp(startPoint, endPoint, 0.5f);
        medianPoint.y = collider.bounds.center.y;
        return medianPoint;
    }

    //Function that examines what the rays hit
    private void ExamineRayHit(RaycastHit ray)
    {
        if (ray.collider != null)
        {
            //If collider that ray hit is not a trigger
            if (!ray.collider.isTrigger)
            {
                ExamineRayHitCounter++;
                if (ExamineRayHitCounter == 1)
                {

                    if (ray.collider.transform.parent.tag == "DeathPad" && tag != "Invulnerable")
                    {
                        circleCount = 1;
                        //Debug.Log(ray.collider.transform.parent.parent.parent);
                        if (ray.collider.transform.parent.parent.parent.tag == "BaseRing")
                        {
                            ray.collider.transform.parent.parent.parent.GetComponent<RingSlots>().FindSlots();
                            List<GameObject> circleList = ray.collider.transform.parent.parent.parent.GetComponent<RingSlots>().circleList;
                            List<GameObject> coneList = ray.collider.transform.parent.parent.parent.GetComponent<RingSlots>().coneList;

                            ray.collider.transform.parent.parent.parent.GetComponent<RingRotation>().enabled = false;
                            transform.eulerAngles = ray.collider.transform.parent.parent.parent.eulerAngles;
                            for (int i = circleList.Count - 1; i >= 0; i--)
                            {
                                circleList[i].transform.parent.tag = "EmptyPad";
                                Destroy(circleList[i].gameObject);
                            }

                            for (int i = coneList.Count - 1; i >= 0; i--)
                            {
                                coneList[i].transform.parent.tag = "EmptyPad";
                                Destroy(coneList[i].gameObject);
                            }

                            //Transform[] transforms = ray.collider.transform.parent.parent.parent.GetComponentsInChildren<Transform>();
                            //ray.collider.transform.parent.parent.parent.GetComponent<RingRotation>()._PingPong = false;
                            //transform.eulerAngles = ray.collider.transform.parent.parent.parent.eulerAngles;
                            //foreach (Transform t in transforms)
                            //{
                            //    if (t.name == "Cone(Clone)")
                            //    {
                            //        Destroy(t.gameObject);
                            //    }
                            //    else if (t.name == "TouchCircle(Clone)")
                            //    {
                            //        Destroy(t.gameObject);
                            //    }
                            //    else if (t.name == "SinglePad")
                            //    {
                            //        t.tag = "EmptyPad";
                            //    }
                            //}
                        }
                        OnDeathPad();
                    }
                    else if (ray.collider.transform.parent.tag == "ScorePad")
                    {
                        //Plays the particles
                        ParticlePlay();
                        ringHit = ray.collider.transform.parent;
                       // sceneController.UpdateGravity();
                        //Debug.Log("Circle count is != 1,2,3,4,5" + circleCount);
                        switch (circleCount)
                        {
                            //case 29:
                            //    playerSettings.PlayPianoSound29();
                            //    sceneController.AddScore(5);
                            //    ShowFloatingText(ringHit, 5);
                            //    break;
                            //case 28:
                            //    playerSettings.PlayPianoSound28();
                            //    sceneController.AddScore(5);
                            //    ShowFloatingText(ringHit, 5);
                            //    break;
                            //case 27:
                            //    playerSettings.PlayPianoSound27();
                            //    sceneController.AddScore(5);
                            //    ShowFloatingText(ringHit, 5);
                            //    break;
                            //case 26:
                            //    playerSettings.PlayPianoSound26();
                            //    sceneController.AddScore(5);
                            //    ShowFloatingText(ringHit, 5);
                            //    break;
                            //case 25:
                            //    playerSettings.PlayPianoSound25();
                            //    sceneController.AddScore(5);
                            //    ShowFloatingText(ringHit, 5);
                            //    break;
                            //case 24:
                            //    playerSettings.PlayPianoSound24();
                            //    sceneController.AddScore(5);
                            //    ShowFloatingText(ringHit, 5);
                            //    break;
                            //case 23:
                            //    playerSettings.PlayPianoSound23();
                            //    sceneController.AddScore(5);
                            //    ShowFloatingText(ringHit, 5);
                            //    break;
                            //case 22:
                            //    playerSettings.PlayPianoSound22();
                            //    sceneController.AddScore(5);
                            //    ShowFloatingText(ringHit, 5);
                            //    break;
                            case 21:
                                playerSettings.PlayPianoSound21();
                                sceneController.AddScore(5);
                                ShowFloatingText(ringHit, 5);
                                break;
                            case 20:
                                playerSettings.PlayPianoSound20();
                                sceneController.AddScore(5);
                                ShowFloatingText(ringHit, 5);
                                break;
                            case 19:
                                playerSettings.PlayPianoSound19();
                                sceneController.AddScore(5);
                                ShowFloatingText(ringHit, 5);
                                break;
                            case 18:
                                playerSettings.PlayPianoSound18();
                                sceneController.AddScore(5);
                                ShowFloatingText(ringHit, 5);
                                break;
                            case 17:
                                playerSettings.PlayPianoSound17();
                                sceneController.AddScore(5);
                                ShowFloatingText(ringHit, 5);
                                break;
                            case 16:
                                playerSettings.PlayPianoSound16();
                                sceneController.AddScore(5);
                                ShowFloatingText(ringHit, 5);
                                break;
                            case 15:
                                playerSettings.PlayPianoSound15();
                                sceneController.AddScore(5);
                                ShowFloatingText(ringHit, 5);
                                break;
                            case 14:
                                playerSettings.PlayPianoSound14();
                                sceneController.AddScore(5);
                                ShowFloatingText(ringHit, 5);
                                break;
                            case 13:
                                playerSettings.PlayPianoSound13();
                                sceneController.AddScore(5);
                                ShowFloatingText(ringHit, 5);
                                break;
                            case 12:
                                playerSettings.PlayPianoSound12();
                                sceneController.AddScore(5);
                                ShowFloatingText(ringHit, 5);
                                break;
                            case 11:
                                playerSettings.PlayPianoSound11();
                                sceneController.AddScore(5);
                                ShowFloatingText(ringHit, 5);
                                break;
                            case 10:
                                playerSettings.PlayPianoSound10();
                                sceneController.AddScore(5);
                                ShowFloatingText(ringHit, 5);
                                break;
                            case 9:
                                playerSettings.PlayPianoSound9();
                                sceneController.AddScore(5);
                                ShowFloatingText(ringHit, 5);
                                break;
                            case 8:
                                playerSettings.PlayPianoSound8();
                                sceneController.AddScore(5);
                                ShowFloatingText(ringHit, 5);
                                break;
                            case 7:
                                playerSettings.PlayPianoSound7();
                                sceneController.AddScore(5);
                                ShowFloatingText(ringHit, 5);
                                break;
                            case 6:
                                playerSettings.PlayPianoSound6();
                                sceneController.AddScore(5);
                                ShowFloatingText(ringHit, 5);
                                break;
                            case 5:
                                playerSettings.PlayPianoSound5();
                                sceneController.AddScore(5);
                                ShowFloatingText(ringHit, 5);
                                break;
                            case 4:
                                playerSettings.PlayPianoSound4();
                                sceneController.AddScore(4);
                                ShowFloatingText(ringHit, 4);
                                break;
                            case 3:
                                playerSettings.PlayPianoSound3();
                                sceneController.AddScore(3);
                                ShowFloatingText(ringHit, 3);
                                break;
                            case 2:
                                playerSettings.PlayPianoSound2();
                                sceneController.AddScore(2);
                                ShowFloatingText(ringHit, 2);
                                break;
                            case 1:
                                playerSettings.PlayPianoSound1();
                                sceneController.AddScore(1);
                                ShowFloatingText(ringHit, 1);
                                break;
                            default:
                                Debug.Log("Circle count is != 1,2,3,4,5" + circleCount);
                                break;
                        }
                        if (circleCount < 21)
                        {
                            circleCount += 1;
                        }
                        //GETS CALLED EVERYTIME THE BALL HITS THE PAD!
                        if (ray.collider.transform.parent.parent.parent.tag == "BaseRing")
                        {
                            foreach (GameObject g in ray.collider.transform.parent.parent.parent.GetComponent<RingSlots>().circleList)
                            {
                                //Debug.Log(g.transform.parent);
                                ray.collider.transform.parent.tag = "EmptyPad";
                                g.SetActive(false);
                            }
                        }


                        //Transform objt = ray.collider.transform.parent.Find("TouchCircle(Clone)");
                        //objt.gameObject.SetActive(false);
                        //ray.collider.transform.parent.tag = "EmptyPad";

                    }
                    else if (ray.collider.transform.parent.tag == "EmptyPad")
                    {
                        //sceneController.ResetGravity();
                        //GETS CALLED EVERYTIME THE BALL HITS THE PAD!
                        if (ray.collider.transform.parent.parent.parent.tag == "BaseRing")
                        {
                            foreach (GameObject g in ray.collider.transform.parent.parent.parent.GetComponent<RingSlots>().circleList)
                            {
                                if (g != null && g.activeSelf != false)
                                {
                                    g.transform.parent.GetComponent<SpawnCone>().CanSpawn();
                                    //ray.collider.transform.parent.parent.parent.GetComponent<RingSlots>().FindSlots();
                                    g.SetActive(false);
                                }

                            }
                        }

                        //Transform[] transforms = ray.collider.transform.parent.parent.parent.GetComponentsInChildren<Transform>();
                        //foreach (Transform t in transforms)
                        //{
                        //    if (t.name == "TouchCircle(Clone)")
                        //    {
                        //        t.parent.tag = "EmptyPad";
                        //        t.parent.GetComponent<SpawnCone>().CanSpawn();
                        //        Destroy(t.gameObject);
                        //    }

                        //}

                        //playerSettings.PlaySound();
                        circleCount = 1;
                        //ray.collider.transform.parent.GetComponent<SpawnCone>().CanSpawn();
                    }

                    //Debug.Log("Hit Empty");
                    OnEmptyPad();

                }
                else if (ExamineRayHitCounter > 1)
                {
                    ExamineRayHitCounter = 0;
                }

            }
        }




    }

    //Function that is played when the player bounces on a death pad
    private void OnDeathPad()
    {
        Handheld.Vibrate();
        //Player is killed, game is paused, and the player rigidbody is disabled
        sceneController.IsPlayerAlive = false;
        sceneController.IsPaused = true;
        //rb.isKinematic = true;
    }

    //Function that is called when the player bounces on a empty pad
    private void OnEmptyPad()
    {

        //Debug.Log("1");
        //circleCount = 0;


        //Disallows the camera to follow the player
        sceneController.FollowCamera(false);

        //Plays a splat sound
       // playerSettings.PlaySound();

        //Check animator trigger, animation plays
        animatorLeftSphere.SetTrigger("Bounce");
        animatorRightSphere.SetTrigger("Bounce");

        

        //Adds force to the player to jump
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);

        //Invoke("EnableRayCheck", 0.1f);
    }

    //Function that plays particles
    private void ParticlePlay()
    {
        //For each loop cycles through array of particles
        foreach (ParticleSystem particleSystem in playerParticleSystems)
        {
            //Randomizes particle rotation on the y-axis
            RandomParticleRotationYAxis(particleSystem.main);
            //Plays the particle
            particleSystem.Play();
        }
    }

    //Function that clear particles
    private void ParticleClear()
    {
        //For each loop cycles through array of particles
        foreach (ParticleSystem particleSystem in playerParticleSystems)
        {
            //Clears the particle
            particleSystem.Clear();
        }
    }

    //Function that randomizes a particle system start rotation on the y-axis
    private void RandomParticleRotationYAxis(ParticleSystem.MainModule particleMain)
    {
        if (particleMain.startRotation3D)
        {
            particleMain.startRotationY = Random.Range(0f, 3.14159f);
        }

    }

    //Function that shows how much was added to the score
    private void ShowFloatingText(Transform hitTransform, int score)
    {
        floatingTextScore.transform.localPosition = new Vector3(Random.Range(-40, 40), Random.Range(260, 340), 0);
        floatingTextScore.transform.localEulerAngles = new Vector3(0, 0, Random.Range(-10, 10));
        // float scale = Random.Range(1, 1.4f);
        // floatingTextScore.transform.localScale = new Vector3(scale, scale, scale);
        animatorFloatingTextScore.SetTrigger("PopUp");
        // floatingTextScore.transform.rotation = Quaternion.Euler(new Vector3(40, 0, 0));

        if (score == 5)
        {
            textScore.fontSize = 200;
            Debug.Log(textScore.fontSize);
        }else{
            textScore.fontSize = 150;
        }

        textScore.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        textScore.text = "+" + score.ToString();


        floatingText.ResetText();
    }

    private IEnumerator DestroyPad(GameObject pad, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(pad);
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerCounter++;
        if (triggerCounter == 1)
        {
            if (other.tag == "RingPass")
            {
               
                //Debug.Log(whenToSpawn);
                sceneController.RingCounter += 1;
                //Checks if the player has passed 5 rings, if he has then spawn in 10 more
                if (sceneController.RingCounter == 2)
                {
                    //Spawns in rings, passes 1 and 3. 1 and 3 is used to determine how many cone pairs are spawned on each ring, with 1 cone pair being the min and with 3 cone pair being the max
                    ringSpawner.SpawnRingDifficulty1(5);
                    //ringSpawner.SpawnRings(1, 2, 5);
                    //Randomly chooses rings to ping pong, passed 2 and 10. 2 is used to determine which of the 2 out of 10 rings are rotated. 10 is used for the speed of the rotation
                    ringSpawner.RandomPingPongDifficulty0(Random.Range(1, 5));
                    ringSpawner.ClearList();
                    //whenToSpawn += 5;
                }
                //Checks if the player has passed whenToSpawn. whenToSpawn is 15 at first, each time the rings are spawned 10 is added to it. This way rings are always spawned on the 10th. 
                //at the 5th ring it spawns 10, then on the 15th ring it spawns another 10, then on the 25th ring it spawns another 10 and so on
                else if (sceneController.RingCounter == 7)
                {
                    //Spawns in rings, passes 2 and 4. 2 and 4 is used to determine how many cone pairs are spawned on each ring, with 2 cone pair being the min and with 4 cone pair being the max
                    ringSpawner.SpawnRingDifficulty2(10);
                    //ringSpawner.SpawnRings(2, 3, 10);
                    //Randomly chooses rings to ping pong, passed 3 and 20. 3 is used to determine which of the 3 out of 10 rings are rotated. 20 is used for the speed of the rotation
                    ringSpawner.RandomPingPongDifficulty1(Random.Range(4,8));
                    ringSpawner.ClearList();
                    whenToSpawn += 10;
                }
                else if (sceneController.RingCounter == whenToSpawn)
                {

                    //Spawns in rings, passes 2 and 4. 2 and 4 is used to determine how many cone pairs are spawned on each ring, with 2 cone pair being the min and with 4 cone pair being the max
                    ringSpawner.SpawnRingDifficulty3(10);
                    //ringSpawner.SpawnRings(3, 5, 10);
                    //Randomly chooses rings to ping pong, passed 3 and 20. 3 is used to determine which of the 3 out of 10 rings are rotated. 20 is used for the speed of the rotation
                    ringSpawner.RandomPingPongDifficulty2(Random.Range(11, 13));
                    ringSpawner.ClearList();
                    whenToSpawn += 10;
                }
                //Inceases gravity through each ring pass
                sceneController.UpdateGravity();

                //Clears any particles
                ParticleClear();


                //Allows the camera to follow the player

                sceneController.FollowCamera(true);

                //EnableRayCheck();
                //Adds +1 to the score
                sceneController.AddScore(1);

                //ShowFloatingText(other.transform, 1);

                //Destroys the ring that was passed
                StartCoroutine(DestroyPad(other.transform.parent.gameObject, 0.25f));
            }
        }
        else if (triggerCounter > 1)
        {
            triggerCounter = 0;
        }


    }










































    //if (canvas.GetComponent<SceneManagement>().IsAlivePlayer == true)
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        foreach (Touch touch in Input.touches)
    //        {
    //            if (touch.phase == TouchPhase.Stationary)
    //            {
    //                if (touch.position.x > screenCenterX)
    //                {
    //                    Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime * 1);
    //                    rb.MoveRotation(rb.rotation * deltaRotation);
    //                }
    //                else if (touch.position.x < screenCenterX)
    //                {
    //                    Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime * -1);
    //                    rb.MoveRotation(rb.rotation * deltaRotation);
    //                }
    //            }
    //        }
    //    }
    //}




    // Debug.DrawRay(centerOrigin, Vector3.down + doffset);
    // Debug.DrawRay(CalcOrigin(boundPoint1, boundPoint3), Vector3.down + doffset);
    // Debug.DrawRay(CalcOrigin(boundPoint2, boundPoint6), Vector3.down + doffset);
    //// Debug.DrawRay(CalcOrigin(boundPoint4, boundPoint8), Vector3.down + doffset);
    //Debug.DrawRay(CalcOrigin(boundPoint5, boundPoint7), Vector3.down + doffset);





    //private void OnDrawGizmosSelected()
    //{
    //    Vector3 boundPoint1 = leftSphere.GetComponent<Collider>().bounds.min;
    //    Vector3 boundPoint2 = leftSphere.GetComponent<Collider>().bounds.max;
    //    Vector3 boundPoint3 = new Vector3(boundPoint1.x, boundPoint1.y, boundPoint2.z);
    //    Vector3 boundPoint4 = new Vector3(boundPoint1.x, boundPoint2.y, boundPoint1.z);
    //    Vector3 boundPoint5 = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint1.z);
    //    Vector3 boundPoint6 = new Vector3(boundPoint1.x, boundPoint2.y, boundPoint2.z);
    //    Vector3 boundPoint7 = new Vector3(boundPoint2.x, boundPoint1.y, boundPoint2.z);
    //    Vector3 boundPoint8 = new Vector3(boundPoint2.x, boundPoint2.y, boundPoint1.z);

    //    //Gizmos.color = Color.red;
    //    //Gizmos.DrawWireCube(leftSphere.GetComponent<Collider>().bounds.center, leftSphere.GetComponent<Collider>().bounds.size);
    //    //Gizmos.color = Color.white;
    //    //Gizmos.DrawWireCube(boundPoint1, new Vector3(0.1f, 0.1f, 0.1f));
    //    //Gizmos.DrawWireCube(boundPoint3, new Vector3(0.1f, 0.1f, 0.1f));
    //    //Gizmos.DrawWireCube(boundPoint5, new Vector3(0.1f, 0.1f, 0.1f));
    //    //Gizmos.DrawWireCube(boundPoint7, new Vector3(0.1f, 0.1f, 0.1f));
    //    //Debug.Log(boundPoint1);

    //    Vector3 boundPoint13 = Vector3.Lerp(boundPoint1, boundPoint3, 0.5f);
    //    boundPoint13.y = leftSphere.GetComponent<Collider>().bounds.center.y;
    //    Gizmos.DrawWireCube(boundPoint13, new Vector3(0.1f, 0.1f, 0.1f));

    //    Vector3 boundPoint48 = Vector3.Lerp(boundPoint4, boundPoint8, 0.5f);
    //    boundPoint48.y = leftSphere.GetComponent<Collider>().bounds.center.y;
    //    Gizmos.DrawWireCube(boundPoint48, new Vector3(0.1f, 0.1f, 0.1f));

    //    Vector3 boundPoint26 = Vector3.Lerp(boundPoint2, boundPoint6, 0.5f);
    //    boundPoint26.y = leftSphere.GetComponent<Collider>().bounds.center.y;
    //    Gizmos.DrawWireCube(boundPoint26, new Vector3(0.1f, 0.1f, 0.1f));

    //    Vector3 boundPoint57 = Vector3.Lerp(boundPoint5, boundPoint7, 0.5f);
    //    boundPoint57.y = leftSphere.GetComponent<Collider>().bounds.center.y;
    //    Gizmos.DrawWireCube(boundPoint57, new Vector3(0.1f, 0.1f, 0.1f));

    //    Gizmos.DrawWireCube(leftSphere.GetComponent<Collider>().bounds.center, new Vector3(0.1f, 0.1f, 0.1f));

    //}
}