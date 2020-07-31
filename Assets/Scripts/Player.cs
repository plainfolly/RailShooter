using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")][SerializeField] float speed = 20f; //tooltip if u need units
    [Tooltip("In m")] [SerializeField] float xRange = 6f;
    [Tooltip("In m")] [SerializeField] float yRange = 4f;
    [SerializeField] float positionPitchFactor = -5f; // no tooltip cause no unit
    [SerializeField] float controlPitchFactor = -20f;

    [SerializeField] float positionYawFactor = 5f; 

    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = yThrow * speed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);
        
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);    
    }
    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor; // pitch due position and control

        float yaw = transform.localPosition.x * positionYawFactor; //yaw due position
        
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll); //static function
    }
}
