using UnityEngine;

public class PrologueManager : MonoBehaviour
{
    [SerializeField] private GameObject[] texts;
    private int state=0;

    public void OnNext()
    {
        texts[state].SetActive(false);
        state++;
        texts[state].SetActive(true);
    }
    public void OnPrevious()
    {
        texts[state].SetActive(false);
        state--;
        texts[state].SetActive(true);
    }
}
