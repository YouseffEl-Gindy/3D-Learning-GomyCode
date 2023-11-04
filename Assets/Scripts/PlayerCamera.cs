using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    #region References
    [SerializeField] Transform player;
    [SerializeField] Transform tragetTransform;

    #endregion

    #region Variables
    [SerializeField] float senX;
    [SerializeField] float senY;

    [SerializeField] float minX;
    [SerializeField] float maxX;

    float xRotation;
    float yRotation;
    #endregion

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        transform.position = tragetTransform.position;

        float mouseX = Input.GetAxis("Mouse X") * senX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * senY * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, minX, maxX);

        player.rotation = Quaternion.Euler(0, yRotation, 0);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);


        
    }
}
