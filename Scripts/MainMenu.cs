using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // TextMeshPro için gerekli kütüphane
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;  // Baþlatma butonunu referans alýr
    

    private void Start()
    {
        

        // Baþlatma butonuna týklanýldýðýnda LevelScene sahnesine geçer
        startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("LevelScene");  // "LevelScene" sahnesine geçiþ yapar
    }
}
