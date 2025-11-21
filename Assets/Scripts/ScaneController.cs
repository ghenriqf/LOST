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
    public Transform house;

    [Header("Tempos da Cutscene")]
    public float fadeDuration = 1f;
    public float timeVisible = 1f;
    public float parallaxDuration = 10f; 
    public float houseMoveDuration = 3f;

    [Header("Posições da Casa")]
    public float houseEndX = 0.8f; 
    private Vector3 houseStartPos;

    void Awake()
    {
        parallax = FindObjectsOfType<Parallax>();
        houseStartPos = new Vector3(1.858f, -0.174f, -1.25f);
        house.position = houseStartPos;
    }

    void Start()
    {
        foreach (var sr in sprites)
            SetAlpha(sr, 0);
        SetAlpha(blackBackground, 0);

        foreach (var p in parallax)
            p.enabled = true;

        StartCoroutine(CutsceneSequence());
    }

    IEnumerator CutsceneSequence()
    {
        yield return new WaitForSeconds(parallaxDuration);

        yield return StartCoroutine(PlayImageSequence());

        yield return new WaitForSeconds(parallaxDuration);

        yield return StartCoroutine(MoveHouseX(houseStartPos.x, houseEndX, houseMoveDuration));

        foreach (var p in parallax)
            p.enabled = false;
    }

    IEnumerator PlayImageSequence()
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

    IEnumerator MoveHouseX(float startX, float endX, float duration)
    {
        float t = 0;
        Vector3 currentPos = house.position;

        while (t < duration)
        {
            t += Time.deltaTime;
            float newX = Mathf.Lerp(startX, endX, t / duration);
            house.position = new Vector3(newX, currentPos.y, currentPos.z);
            yield return null;
        }

        house.position = new Vector3(endX, currentPos.y, currentPos.z);
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
            p.enabled = true;
        }

        foreach (var sr in sprites)
            SetAlpha(sr, 0);

        SetAlpha(blackBackground, 0);

        house.position = houseStartPos;
    }

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ResetScene();
        }
    }
}