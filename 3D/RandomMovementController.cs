using UnityEngine;

public class RandomMovementController : MonoBehaviour
{
    [Header("Turning Parameters")]
    public float minTurnDegree = 45f;
    public float maxTurnDegree = 180f;
    public float minTurnInterval = 1f;
    public float maxTurnInterval = 5f;

    [Header("Movement Parameters")]
    public float minForce = 5f;
    public float maxForce = 15f;
    public float minMoveInterval = 1f;
    public float maxMoveInterval = 4f;

    private Rigidbody rb;
    private float nextTurnTime;
    private float nextMoveTime;
    private bool isTurning = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetNextTurnTime();
        SetNextMoveTime();
    }

    private void Update()
    {
        if (Time.time >= nextTurnTime && !isTurning)
        {
            StartTurning();
        }
        
        if (Time.time >= nextMoveTime && !isTurning)
        {
            MoveForward();
        }
    }

    private void StartTurning()
    {
        isTurning = true;
        float randomRotation = Random.Range(minTurnDegree, maxTurnDegree);
        float randomDirection = Random.Range(0, 2) * 2 - 1; // Returns either 1 or -1
        
        // Create a rotation around the Y axis
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, randomRotation * randomDirection, 0);
        
        // Start coroutine to smooth rotation
        StartCoroutine(RotateOverTime(targetRotation, 1f));
        
        SetNextTurnTime();
    }

    private System.Collections.IEnumerator RotateOverTime(Quaternion endRotation, float duration)
    {
        Quaternion startRotation = transform.rotation;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }
        
        transform.rotation = endRotation;
        isTurning = false;
    }

    private void MoveForward()
    {
        float randomForce = Random.Range(minForce, maxForce);
        rb.AddForce(transform.forward * randomForce, ForceMode.Impulse);
        SetNextMoveTime();
    }

    private void SetNextTurnTime()
    {
        nextTurnTime = Time.time + Random.Range(minTurnInterval, maxTurnInterval);
    }

    private void SetNextMoveTime()
    {
        nextMoveTime = Time.time + Random.Range(minMoveInterval, maxMoveInterval);
    }
}
