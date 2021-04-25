using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadHelper : MonoBehaviour
{
    public string levelName;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(levelName);
        }
    }
}
