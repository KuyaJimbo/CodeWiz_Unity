using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleRotationScript : MonoBehaviour
{
    // using math we will make this object oscillate vertically using the sin function
    public float amplitude = 1.0f;
    public float frequency = 1.0f;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        // get the initial position of the object
        Vector3 initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // calculate the new position of the object
        float newRotation = initialPosition.z + amplitude * Mathf.Sin(2 * Mathf.PI * frequency * Time.time);

        // set the new position of the object
        transform.rotation = Quaternion.Euler(0, 0, newRotation);
    }
}
