using UnityEngine;

public class ScaneController : MonoBehaviour
{
    [SerializeField] private GameObject car;
    [SerializeField] private AudioSource audioSource;
  
    private void BrakeCar()
    {
        car.GetComponent<Animator>().enabled = false;
        audioSource.Play();
    }
    
    void Start()
    {
        Invoke("BrakeCar", 2f);
    }
}
