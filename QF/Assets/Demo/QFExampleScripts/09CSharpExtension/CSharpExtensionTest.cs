using QFramework;
using UnityEngine;
public class CSharpExtensionTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.Repeat()
            .Until(() => Input.GetKeyDown(KeyCode.Mouse0))
            .Event(() =>
            {
                "打开".LogInfo();
                //IOExtension.OpenFolder(@"F:\FTP_Server");
            })
            .Begin();
        // traditional style
        var playerPrefab = Resources.Load<GameObject>("playerPrefab");
        var playerObj = Instantiate(playerPrefab);
        playerObj.transform.SetParent(null);
        playerObj.transform.localRotation = Quaternion.identity;
        playerObj.transform.localPosition = Vector3.left;
        playerObj.transform.localScale = Vector3.one;
        playerObj.layer = 1;
        playerObj.layer = LayerMask.GetMask("Default");
        Debug.Log("playerPrefab instantiated");

// Extension's Style,same as above 
        Resources.Load<GameObject>("playerPrefab")
            .Instantiate()
            .transform
            .Parent(null)
            .LocalRotationIdentity()
            .LocalPosition(Vector3.left)
            .LocalScaleIdentity()
            .Layer(1)
            .Layer("Default")
            .ApplySelfTo(_ => { Debug.Log("playerPrefab instantiated"); });
    }
}
