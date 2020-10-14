using UnityEngine;
public class Back : MonoBehaviour
{
    void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,150,50),"返回" ))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
