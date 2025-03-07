using UnityEngine;

public class RestoreObjects : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // RestoreByTag, takes a string as a parameter
    public void RestoreByTag(string tag)
    {
        // Find all objects with the tag
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        // For each object in the array
        foreach (GameObject obj in objects)
        {
            // Set the object to active
            obj.SetActive(true);
        }
    }
}
