using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    public int sceneToLoad;
    public void Loadingcene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Loadingcene();
    }
}
