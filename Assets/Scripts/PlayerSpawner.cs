using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private void Start()
    {
        string spawnName = PlayerPrefs.GetString("SpawnPoint", "");

        if (spawnName != "")
        {
            GameObject spawnPoint = GameObject.Find(spawnName);
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (spawnPoint != null && player != null)
            {
                player.transform.position = spawnPoint.transform.position;
            }
        }
    }
}
