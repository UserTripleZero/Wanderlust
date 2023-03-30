using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour

{
    public float timeSpeed = 10.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * timeSpeed * Time.deltaTime);
    }
}
