using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] float smooth;
    [SerializeField] float swayMultiplier;

    private void Update()
    {
        //Get the Input
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        //Calculate the target rotation
        Quaternion xRotation = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion yRotation = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = xRotation * yRotation;

        //Rotate
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);


    }
}
