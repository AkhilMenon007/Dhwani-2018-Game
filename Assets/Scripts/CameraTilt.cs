using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CameraTilt : MonoBehaviour {


    [SerializeField]
    float rotationLimit=15f;

    [SerializeField]
    float multiplier = 5f;

    float moveHorizontal = 0f;

    float rotationAmount=0f;

    float targetRotationAmount = 0f;

    void Update ()
    {
        
        moveHorizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");

        targetRotationAmount = -moveHorizontal * rotationLimit;

        rotationAmount = Mathf.Lerp(rotationAmount, targetRotationAmount, Time.deltaTime * multiplier);

        Mathf.Clamp(rotationAmount, -rotationLimit, rotationLimit);

        transform.localRotation = Quaternion.Euler(0f, rotationAmount , 0f);
        
    }
}
