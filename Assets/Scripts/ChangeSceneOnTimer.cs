using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTimer : MonoBehaviour
{
    public float timeToChange;
    public string sceneName;
    
    // Update is called once per frame
    void Update()
    {
        timeToChange -= Time.deltaTime;
        if (timeToChange <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
