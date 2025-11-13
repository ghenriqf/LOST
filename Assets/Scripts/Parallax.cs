using System;
using System.Collections;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    [SerializeField] private float animationSpeed;

    private void Start()
    {
        Invoke(nameof(StartBreakSmooth), 2f);
    }

    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    
    private void StartBreakSmooth()
    {
        StartCoroutine(BreakParallaxSmooth());
    }
    
    private IEnumerator BreakParallaxSmooth()
    {
        while (animationSpeed > 0)
        {
            animationSpeed -= 0.1f * Time.deltaTime;
            yield return null;
        }
        animationSpeed = 0;
    }
    
    void Update()
    {
        _meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }
}
