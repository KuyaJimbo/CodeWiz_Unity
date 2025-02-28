using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectCounter : MonoBehaviour
{
    public string objectTag; // The tag to search for
    public TextMeshProUGUI countText; // UI text to display the count
    public GameObject button; // The UI button to activate

    void Start()
    {
        UpdateCount();
    }

    void Update()
    {
        UpdateCount();
    }

    void UpdateCount()
    {
        int count = GameObject.FindGameObjectsWithTag(objectTag).Length; // Count objects with tag
        countText.text = "Objects Remaining: " + count; // Update UI text

        // Show the button when count reaches 0
        button.SetActive(count == 0);
    }
}
