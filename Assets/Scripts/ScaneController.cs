using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScaneController : MonoBehaviour
{
    [SerializeField] private GameObject car;
    [SerializeField] private AudioSource audioSource;
    private Parallax[] parallax;

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
    }
    
    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ResetScene();
        }
    }
}