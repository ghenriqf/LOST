using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ScaneController : MonoBehaviour
{
    [SerializeField] private GameObject car;
    [SerializeField] private AudioSource audioSource;

    private Parallax[] parallax;

    public SpriteRenderer[] sprites;
    public SpriteRenderer blackBackground;
    public Transform house;

    public float fadeDuration = 1f;
    public float timeVisible = 1f;
    public float parallaxDuration = 10f;
    public float houseMoveDuration = 3f;

    public float houseEndX = 0.8f;
    private Vector3 houseStartPos;

    public TextMeshProUGUI dialogueText;
    public float dialogueFadeDuration = 0.5f;
    public float typeSpeed = 0.04f;

    void Awake()
    {
        parallax = FindObjectsOfType<Parallax>();
        houseStartPos = new Vector3(1.98f, -0.28f, -1.25f);
        house.position = houseStartPos;
    }

    void Start()
    {
        foreach (var sr in sprites)
            SetAlpha(sr, 0);

        SetAlpha(blackBackground, 0);

        foreach (var p in parallax)
            p.enabled = true;

        Color c = dialogueText.color;
        c.a = 0;
        dialogueText.color = c;

        StartCoroutine(CutsceneSequence());
    }

    IEnumerator CutsceneSequence()
    {
        yield return StartCoroutine(ShowDialogueTypewriter("No interior do Texas, um jovem detetive dirigia, a procura do hotel para passar a sua noite.\n"));
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(HideDialogue());

        yield return new WaitForSeconds(parallaxDuration);
        yield return StartCoroutine(ShowDialogueTypewriter("Já era de madrugada. Para melhorar, a bússola andava oscilando, incapaz de apontar qualquer direção estável. E esse mapa, pelo amor de Deus, ele não ajudava nem um pouco.\n"));
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(HideDialogue());
        yield return StartCoroutine(PlayImageSequence());

        yield return new WaitForSeconds(parallaxDuration);

        yield return StartCoroutine(ShowDialogueTypewriter("Em sua ardente busca, nota uma casa ao fundo da planície escura."));
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(HideDialogue());
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

    IEnumerator ShowDialogueTypewriter(string text)
    {
        dialogueText.text = "";

        float t = 0;
        Color c = dialogueText.color;
        c.a = 0;
        dialogueText.color = c;

        while (t < dialogueFadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0, 1, t / dialogueFadeDuration);
            dialogueText.color = c;
            yield return null;
        }

        yield return StartCoroutine(Typewriter(text));
    }

    IEnumerator Typewriter(string text)
    {
        dialogueText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    IEnumerator HideDialogue()
    {
        float t = 0;
        Color c = dialogueText.color;
        c.a = 1;

        while (t < dialogueFadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1, 0, t / dialogueFadeDuration);
            dialogueText.color = c;
            yield return null;
        }
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

        Color c = dialogueText.color;
        c.a = 0;
        dialogueText.color = c;

        house.position = houseStartPos;
    }
}