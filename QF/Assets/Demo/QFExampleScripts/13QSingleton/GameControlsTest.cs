using UnityEngine;
public class GameControlsTest
{
    [RuntimeInitializeOnLoadMethod]
    static void Awake()
    {
        GameControls.Instance.Creat();
    }
}
