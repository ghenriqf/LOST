using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // <--- IMPORTANTE

public class InteractionTrigger2D : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject spritePressE;

    bool canInteract = false;

    private void Start()
    {
        if (spritePressE != null)
            spritePressE.SetActive(false);
    }

    private void Update()
    {
        if (canInteract && Keyboard.current.eKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            if (spritePressE != null) spritePressE.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            if (spritePressE != null) spritePressE.SetActive(false);
        }
    }
}