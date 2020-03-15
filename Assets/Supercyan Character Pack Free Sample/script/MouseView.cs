using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseView : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes m_axes = RotationAxes.MouseXandY;
    public float m_sensitivityX;
    public float m_sensitivityY;

    // 水平方向
    public float m_minimumX;
    public float m_maximumX;
    // 垂直方向
    public float m_minimumY;
    public float m_maximumY;

    float m_rotationY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Rigidbody>()) {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (m_axes == RotationAxes.MouseXandY)
            {
                float m_rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * m_sensitivityX;
                m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivityY;
                m_rotationY = Mathf.Clamp(m_rotationY, m_minimumY, m_maximumY);

                transform.localEulerAngles = new Vector3(-m_rotationY, m_rotationX, 0);
            }
            else if (m_axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * m_sensitivityX, 0);
            }
            else
            {
                m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivityY;
                m_rotationY = Mathf.Clamp(m_rotationY, m_minimumY, m_maximumY);

                transform.localEulerAngles = new Vector3(-m_rotationY, transform.localEulerAngles.y, 0);
            }
        }
    }
}
