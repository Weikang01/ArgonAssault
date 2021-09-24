using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    [Header("General")]
    [SerializeField] GameObject[] laserTransmitter;

    [Header("Translation")]
    [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 150f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 60f;
    [SerializeField] float limitXPos = 6.2f;
    [SerializeField] float limitYPos = 3.63f;

    [Header("Rotation")]
    [SerializeField] float positionPitchFactor = -4.65f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = 5.05f;
    [SerializeField] float controlRollFactor = -50f;

    float xThrow, yThrow;
    bool isDead = false;

    // Update is called once per frame
    void Update ()
    {
        if(isDead == false)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffset;

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffset;

        transform.localPosition = new Vector3(Mathf.Clamp(rawNewXPos, -limitXPos, limitXPos), Mathf.Clamp(rawNewYPos, -limitYPos, limitYPos), transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            foreach(GameObject laser in laserTransmitter)
            {
                laser.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject laser in laserTransmitter)
            {
                laser.SetActive(false);
            }
        }
    }

    void OnPlayerDeath()
    {
        isDead = true;
    }
}
