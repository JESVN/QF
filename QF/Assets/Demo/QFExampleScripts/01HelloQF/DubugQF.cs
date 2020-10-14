using UnityEngine;
using QFramework;
public class DubugQF : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"{"UnityLog"}");
        "QFLog0".LogInfo();
        Log.I("QFLog1");
    }
}
