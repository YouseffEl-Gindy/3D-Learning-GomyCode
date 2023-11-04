using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Senglton
    public static PlayerMove Instance;

    #region References
    Rigidbody rgd;
    CapsuleCollider col;
    [SerializeField] LayerMask groundMask;
    #endregion

    #region Variables
    [SerializeField] float normalSpeed;
    [SerializeField] float highSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float gravity;

    Vector3 moveDir = Vector3.zero;
    bool grounded;
    float moveSpeed;
    bool moving;
    #endregion
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }
    private void Start()
    {
        rgd = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        Physics.gravity = Vector3.up * gravity;

        moveSpeed = normalSpeed;
    }
    private void Update()
    {

        Move();
        Jump();
    }

    void Move()
    {
        SpeedUp();

        float xDir = Input.GetAxis("Horizontal");
        float yDir = Input.GetAxis("Vertical");

        moveDir = transform.right * xDir + transform.forward * yDir;
        rgd.velocity = moveDir * moveSpeed * Time.deltaTime;
        //Solve infinite spinning issue
        rgd.angularVelocity = Vector3.zero;

        //For Foot Steps Sound
        if((xDir != 0 || yDir !=0) && !moving)
        {
            AudioManager.Instance.PlayFootStepsSFX("Foot Steps Sound");
            moving = true;
        }
        else if(IsValueBetween(xDir , -0.1f,0.1f) && IsValueBetween(yDir, -0.1f, 0.1f))
        {
            AudioManager.Instance.StopFootStepsSFX("Foot Steps Sound");
            moving = false;
        }
    }   
    void SpeedUp()
    {
        if(Input.GetKey(KeyCode.LeftShift)) moveSpeed = highSpeed;
        else moveSpeed = normalSpeed;
    }
    void Jump()
    {
        grounded = IsGrounded();
        if (grounded && Input.GetKey(KeyCode.Space))
        {
            rgd.AddForce(transform.up*jumpForce * Time.deltaTime, ForceMode.Impulse);
            Debug.Log("Jump", this);
        }

    }
    bool IsGrounded()
    {

        return Physics.CheckCapsule(col.bounds.center, 
               new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), 
               col.radius * 0.1f, groundMask);
    }
    bool IsValueBetween(float value, float minValue, float maxValue)
    {
        return value >= minValue && value <= maxValue;
    }

}
