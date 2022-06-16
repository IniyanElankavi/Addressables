using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour
{

    Vector3 RotateAmount;  // degrees per second to rotate in each axis. Set in inspector.

    private void Start()
    {
        RotateAmount = new Vector3(10, 10, 10);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotateAmount * Time.deltaTime);
    }
}
