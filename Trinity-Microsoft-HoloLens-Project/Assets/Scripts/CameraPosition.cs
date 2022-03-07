using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 vec = new Vector3(30, 5);
        Camera.main.transform.Rotate(vec);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
