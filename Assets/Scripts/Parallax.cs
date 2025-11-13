using System;
using System.Collections;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    [SerializeField] private float animationSpeed;

    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    
    public IEnumerator BreakParallaxSmooth()
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
