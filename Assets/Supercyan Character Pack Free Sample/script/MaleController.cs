using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleController : MonoBehaviour
{
    Animator animator;
    AudioSource footSteps;
    [SerializeField] private float speed;
    [SerializeField] private ParticleSystem PS;

    float max_speed = 5.0f;
    float min_speed = -5.0f;

    bool InPlay;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        footSteps = GetComponent<AudioSource>();

        InPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Q)) {
            speed = 0;
            animator.SetTrigger("wave");
        }

        if (Input.GetKey(KeyCode.W))
        {
            speed = Mathf.Clamp(speed + Time.deltaTime, min_speed, max_speed);
            animator.SetFloat("speed", speed);
        }
        else if (Input.GetKey(KeyCode.S)) {
            speed = Mathf.Clamp(speed - Time.deltaTime, min_speed, max_speed);
            animator.SetFloat("speed", speed);
        }
        else
        {
            if (speed > 0)
            {
                speed = Mathf.Clamp(speed - Time.deltaTime, 0, max_speed);
                animator.SetFloat("speed", speed);
            }
            else if(speed < 0)
            {
                speed = Mathf.Clamp(speed + Time.deltaTime, min_speed, 0);
                animator.SetFloat("speed", speed);
            }
        }

        // 腳步聲(停止&移動控制)
        if ((speed == 0 && InPlay == true) || !animator.GetBool("Grounded"))
        {
            footSteps.Stop();
            InPlay = false;
        }
        else if(speed != 0 && InPlay == false)
        {
            footSteps.Play();
            InPlay = true;
        }

        // 腳步聲(速度控制)
        if(InPlay == true) {
            float speedAbs = Mathf.Abs(speed);
            if (speedAbs > 0 && speedAbs <= 2f)
            {
                footSteps.pitch = 0.9f;
            }
            else if(speedAbs > 2f && speedAbs < 5)
            {
                footSteps.pitch = 2f;
            }
            else if(speedAbs == 5)
            {
                footSteps.pitch = 2.5f;
            }
        }

        // 跳躍
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("jump");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetTrigger("land");
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("wave"))
        {
            animator.SetBool("Grounded", true);
        }
        else
        {
            animator.SetBool("Grounded", false);
        }
    }

    void Move() {
        if (speed > 0)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        } else if (speed < 0)
        {
            transform.position -= Vector3.back * speed * Time.deltaTime;
        }
    }

    void Land()
    {
        PS.Play();
    }
}
