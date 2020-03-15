using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMale : MonoBehaviour
{
    [SerializeField] private GameObject male;
    Vector3 relativePos = new Vector3(0, 1.6f, -3.5f);
    // Start is called before the first frame update
    void Start()
    {
        transform.position = male.transform.position + relativePos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = male.transform.position + relativePos;
    }
}
