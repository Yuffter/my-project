using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    public float power = 10;
    public float jump_power = 20;
    //‰½’iƒWƒƒƒ“ƒv‚Ü‚Å‚Å‚«‚é‚©
    public int max_jump_count = 2;
    int jump_count = 0;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(new Vector3(power, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(new Vector3(-power, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump_count++;
            if (jump_count <= max_jump_count) rb.AddForce(new Vector3(0, jump_power, 0));
            
        }
        if (transform.position.y < -9)
        {
            transform.position = new Vector3(-9.5f, -0.2f,0);
        }
        if (rb.velocity.x == 0 && Mathf.Abs(rb.velocity.y) < 0.5f)
        {
            GetComponent<TrailRenderer>().enabled = false;
        }
        else
        {
            GetComponent<TrailRenderer>().enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.parent = collision.transform;
        jump_count = 0;
    }

    private void OnCollisionExit(Collision collision)
    {
        transform.parent = null;
    }
}
