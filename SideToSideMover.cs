using UnityEngine;

public class SideToSideMover : MonoBehaviour
{
    public float distance = 2f; // Maximum distance from the center
    public float frequency = 1f; // Speed of movement

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float offset = Mathf.Sin(Time.time * frequency) * distance;
        transform.position = startPosition + new Vector3(offset, 0f, 0f);
    }
}
