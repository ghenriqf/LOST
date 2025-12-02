using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterTMP : MonoBehaviour
{
    public TMP_Text dialogueText;   // Seu componente TextMeshPro
    public float typingSpeed = 0.05f; // Velocidade de digitação

    [TextArea]
    public string fullText; // O texto que vai aparecer

    void OnEnable()
    {
        // Começa a animação quando o objeto é ativado
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        dialogueText.text = "";
        foreach (char letter in fullText)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}