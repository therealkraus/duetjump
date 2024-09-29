using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that deals with player movement and touch controls
public class PlayerMovement : MonoBehaviour
{

    public GameObject sceneManager;
    SceneController sceneController;

    public float rotationSpeed;
    public float maxTerminalVelocity;

    Rigidbody rb;
    Vector3 eulerAngleVelocity;

    // Use this for initialization
    private void Start()
    {
        //Get scene controller script from sceneManager object
        sceneController = sceneManager.GetComponent<SceneController>();

        //Get Player gameobject rigidbody
        rb = GetComponent<Rigidbody>();

        //Set the velocity of the rotation on y-axis
        eulerAngleVelocity = new Vector3(0, rotationSpeed, 0);
    }

    // Update is called once per frame
    private void Update()
    {
        //Clamps the players falling speed set to maxTerminal Velocity variable
        if (rb.velocity.magnitude > maxTerminalVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxTerminalVelocity;
        }

        //Checks to see if player is alive
        if (sceneController.IsPlayerAlive == true)
        {
            if (Input.touchCount > 0)
            {
                //Uses touch controls to move left, right along the x-axis
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    sceneController.Play();

                    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                    Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime * -touchDeltaPosition.x);

                    rb.MoveRotation(rb.rotation * deltaRotation);
                }
            }
        }

        //Sets the players angular velocity to zero
        rb.angularVelocity = Vector3.zero;
    }

    // Will be removed from FINAL VERSION// FOR COMPUTER TESTING ONLY
    private void FixedUpdate()
    {
        if (sceneController.IsPlayerAlive == true)
        {
            if (Input.GetButton("Horizontal"))
            {
                sceneController.Play();

                rotationSpeed = 300;
                eulerAngleVelocity = new Vector3(0, rotationSpeed, 0);
                Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime * Input.GetAxisRaw("Horizontal"));
                rb.MoveRotation(rb.rotation * deltaRotation);
            }
        }
        rb.angularVelocity = Vector3.zero;
    }
}
