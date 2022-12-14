using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public float maxSpeed = 10;
    public float jumpSpeed = 9f;
    public float jumpPower = 20f;
    public bool grounded;
    public float jumpRate = 1f;
    public float nextJumpPress = 0.0f;
    private Rigidbody2D rigidbody2D;
    private Physics2D physics2D;
    Animator animator;
    public int playerHealth = 100;


    void Start()
    {
        rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Grounded", true);
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        if (Input.GetAxis("Horizontal") < - 0.1f)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
        }else if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            transform.SetPositionAndRotation(new Vector2(-2, 0), transform.rotation);
        }
        
        if (Input.GetKey(KeyCode.Z) && Time.time > nextJumpPress)
        {
            animator.SetBool("Jump", true);
            nextJumpPress = Time.time + jumpRate;
            rigidbody2D.AddForce(jumpSpeed * (Vector2.up * jumpPower));
        } else {
            animator.SetBool("Jump", false);
        }


    }
}
