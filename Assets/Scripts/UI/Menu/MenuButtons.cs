using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject loadButton;

    private void Start()
    {
        if (SaveSystem.SaveDataExists())
        {
            loadButton.SetActive(true);
        }
    }

    public void NewGame()
    {
        SaveSystem.CreateNewSaveFile();
        SceneManager.LoadScene("Game");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

}
