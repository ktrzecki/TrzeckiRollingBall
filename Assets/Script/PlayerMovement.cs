using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRb;

    public float speed = 500.0f;
    public Transform cam;
    public bool isPoweredUp;


    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity;
    Vector3 velovity;

    private bool isJumping;


    [SerializeField] private LayerMask ground;


    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.5f);
    }

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {

    }


    private void Update()
    {
        Debug.DrawRay(transform.position, -Vector3.up, Color.red);
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log(isGrounded());
        }
    }




    //physics calculations every 3 frames
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * speed;
        float moveVertical = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (movement.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;


            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            playerRb.AddForce(moveDir * speed * Time.deltaTime);
        }

        //deltatime is in game time
        //playerRb.AddForce(movement * speed * Time.deltaTime);
    }

    private void Jump()
    {
        if (!isGrounded() == true)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void SetMoveSpeed(float newSpeedAdjustment)
    {
        speed += newSpeedAdjustment;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUps"))
        {
            isPoweredUp = true;
            CollectItem(other.gameObject);


        }




        void CollectItem(GameObject item)
        {
            item.SetActive(false);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy") && isPoweredUp)
        {
            Destroy(collision.gameObject);
        }

     
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

      
    }

  
}

