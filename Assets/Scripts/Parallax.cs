using System;
using System.Collections;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    [SerializeField] private float animationSpeed;
    private float initialAnimationSpeed;
    private Vector2 initialOffset;

    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer = GetComponent<MeshRenderer>();
        initialAnimationSpeed = animationSpeed;
        initialOffset = _meshRenderer.material.mainTextureOffset;
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
    
    public void ResetParallaxInstant()
    {
        animationSpeed = initialAnimationSpeed;
        _meshRenderer.material.mainTextureOffset = initialOffset;
    }
    
    void Update()
    {
        _meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }
}