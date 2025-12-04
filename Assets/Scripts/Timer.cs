using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public float tempo = 10f;
    public TextMeshProUGUI texto;

    void Update()
    {
        tempo -= Time.deltaTime;

        if (tempo <= 0)
        {
            tempo = 0;
            SceneManager.LoadScene("CutsceneGameOver");
        }

        texto.text = Mathf.CeilToInt(tempo).ToString();
    }
}