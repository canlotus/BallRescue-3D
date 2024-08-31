using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // TextMeshPro i�in gerekli k�t�phane
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;  // Ba�latma butonunu referans al�r
    

    private void Start()
    {
        

        // Ba�latma butonuna t�klan�ld���nda LevelScene sahnesine ge�er
        startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("LevelScene");  // "LevelScene" sahnesine ge�i� yapar
    }
}
