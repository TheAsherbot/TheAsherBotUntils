using TheAshBot;

using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Use this to log anything");
        this.Log("Use this to log anything");
        Debug.Log("Use this to log successes");
        this.LogSuccess("Use this to log successes");
        Debug.LogWarning("Use this to log warnings");
        this.LogWarning("Use this to log warnings");
        Debug.LogError("Use this to log errors");
        this.LogError("Use this to log errors");
    }
}
