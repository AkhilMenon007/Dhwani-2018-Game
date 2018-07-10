using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMover : MonoBehaviour {


    public float speedBonus = 5f;
    public float initialSpeed=5f;
    public float speed;
    public float turning = 90f;

    [SerializeField]
    float positiveInputMultiplier = 2f;

    [SerializeField]
    float negativeInputMultiplier = 0.5f;

    public float movementOffset= 1;

    float moveHorizontal;
    float moveVertical;

    Vector3 target;
    Rigidbody rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        target = new Vector3();
        speed = initialSpeed;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal") + Input.GetAxis("Horizontal");
        if (CrossPlatformInputManager.GetAxis("Vertical") > 0)
        {
            moveVertical = movementOffset + positiveInputMultiplier * (CrossPlatformInputManager.GetAxis("Vertical") + Input.GetAxis("Vertical"));
        }
        else
        {
            moveVertical = movementOffset + negativeInputMultiplier * (CrossPlatformInputManager.GetAxis("Vertical") + Input.GetAxis("Vertical"));
        }

        GameController.gameController.scoreMultiplier = 1f+ Mathf.Clamp((moveVertical-1f)*speedBonus,0f,1000f);

        target = transform.forward * moveVertical * speed;

        
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + moveHorizontal * turning * Time.fixedDeltaTime, 0f);

        rb.velocity = target;

	}
}
