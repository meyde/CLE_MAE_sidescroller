using UnityEngine;
using UnityEngine.UI;

public class SceneChoice : MonoBehaviour
{
    [SerializeField] private LoadScene sceneLoader;
    [SerializeField] private int sceneToChose;
    [SerializeField] public Image btnsr;
    [SerializeField] private SceneChoice[] allChoices;


    public void ChangeSceneToLoad()
    {
        sceneLoader.sceneToLoad = sceneToChose;
        foreach (var choice in allChoices)
        {
            choice.btnsr.color = Color.white;
        }
        btnsr.color = Color.red;
    }


}
