using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScaneController : MonoBehaviour
{
    [SerializeField] private GameObject car;
    [SerializeField] private AudioSource audioSource;
    private Parallax[] parallax;
    public SpriteRenderer[] sprites;
    public SpriteRenderer blackBackground;

    public float fadeDuration = 1f;
    public float timeVisible = 1f;

    void Awake()
    {
        parallax = FindObjectsOfType<Parallax>();
    }

    private IEnumerator BrakeCar(float time)
    {
        yield return new WaitForSeconds(time);
        car.GetComponent<Animator>().enabled = false;
        audioSource.Play();

        foreach (var p in parallax)
        {
            StartCoroutine(p.BreakParallaxSmooth());
        }
    }

    IEnumerator PlaySequence()
    {
        SetAlpha(blackBackground, 0);

        for (int i = 0; i < sprites.Length; i++)
        {
            SpriteRenderer sr = sprites[i];

            yield return StartCoroutine(FadeIn(sr, fadeDuration));
            yield return new WaitForSeconds(timeVisible);

            SetAlpha(blackBackground, 1);

            yield return StartCoroutine(FadeOut(sr, fadeDuration));

            if (i == sprites.Length - 1)
            {
                yield return StartCoroutine(FadeOut(blackBackground, fadeDuration));
            }
        }
    }

    IEnumerator FadeIn(SpriteRenderer sr, float duration)
    {
        float t = 0;
        Color c = sr.color;
        c.a = 0;
        sr.color = c;

        while (t < duration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0, 1, t / duration);
            sr.color = c;
            yield return null;
        }
    }

    IEnumerator FadeOut(SpriteRenderer sr, float duration)
    {
        float t = 0;
        Color c = sr.color;
        c.a = 1;
        sr.color = c;

        while (t < duration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1, 0, t / duration);
            sr.color = c;
            yield return null;
        }
    }

    void SetAlpha(SpriteRenderer sr, float a)
    {
        Color c = sr.color;
        c.a = a;
        sr.color = c;
    }

    public void ResetScene()
    {
        car.GetComponent<Animator>().enabled = true;
        foreach (var p in parallax)
        {
            p.ResetParallaxInstant();
        }
    }

    void Start()
    {
        StartCoroutine(BrakeCar(10));

        foreach (var sr in sprites)
        {
            SetAlpha(sr, 0);
        }

        SetAlpha(blackBackground, 0);

        StartCoroutine(PlaySequence());
    }

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ResetScene();
        }
    }
}