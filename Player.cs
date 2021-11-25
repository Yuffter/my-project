using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    //左右移動の力
    [SerializeField] float power = 10;
    //ジャンプ力
    [SerializeField] float jump_power = 20;
    //何段ジャンプまでできるか
    [SerializeField] int max_jump_count = 2;
    int jump_count = 0;
    //outになるy座標
    readonly float border_y = -9.0f;
    //スタート位置の座標
    readonly Vector3 start_position = new Vector3(-9.5f, -0.2f,0);
    readonly float slowest_speed = 0.5f;
    readonly float slowest_speed_x = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
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
        /*
        **********コントローラーの操作*************
        float x = Input.GetAxis("Horizontal");
        rb.AddForce(new Vector3(x * power,0,0));  //パターン1
        
        if (x > 0) {      //パターン2
            rb.AddForce(Vector3.Right * power);
        }
        else if (x < 0) {
            rb.AddForce(Vector3.Left * power);
        }
        
        if (Input.GetButtonDown("Jump")) {
            jump_count++;
            if (jump_count <= max_jump_count) rb.AddForce(Vector3.Up * jump_power);
        }
        */
        if (transform.position.y < border_y)
        {
            transform.position = start_position;
        }
        if (rb.velocity.x == 0 && Mathf.Abs(rb.velocity.y) < slowest_speed)
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
