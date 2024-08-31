using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class LevelScene : MonoBehaviour
{
    public Button levelButton1;  // Bir önceki seviye (ilk seviyede gizli olacak)
    public Button levelButton2;  // Bulunduðumuz seviye, bu buton týklanabilir olacak
    public Button levelButton3;  // Bir sonraki seviye
    public Button levelButton4;  // Bir sonraki seviye

    public TextMeshProUGUI levelText1;
    public TextMeshProUGUI levelText2;
    public TextMeshProUGUI levelText3;
    public TextMeshProUGUI levelText4;

    public Button mainMenuButton; // Ana menüye dönmek için buton

    public TextMeshProUGUI flashingText; // Yanýp sönecek metin

    private int currentLevel;

    private void Start()
    {
        // Mevcut seviyeyi alýyoruz
        currentLevel = PlayerPrefs.GetInt("CurrentLevelIndex", 1);

        // Seviye metinlerini güncelliyoruz
        UpdateLevelButtons();

        // Buton iþlevlerini tanýmlýyoruz
        levelButton2.onClick.AddListener(LoadGameScene);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu); // Ana menü butonu iþlevi

        // Yanýp sönme coroutine'i baþlat
        if (flashingText != null)
        {
            StartCoroutine(FlashText());
        }
    }

    private void UpdateLevelButtons()
    {
        // Ýlk buton (Bir önceki seviye)
        if (currentLevel > 1)
        {
            levelButton1.gameObject.SetActive(true);
            levelText1.text = "Level " + (currentLevel - 1);
        }
        else
        {
            levelButton1.gameObject.SetActive(false);
        }

        // Ýkinci buton (Mevcut seviye)
        levelText2.text = "Level " + currentLevel;

        // Üçüncü buton (Bir sonraki seviye)
        levelText3.text = "Level " + (currentLevel + 1);

        // Dördüncü buton (Bir sonraki seviye)
        levelText4.text = "Level " + (currentLevel + 2);
    }

    private void LoadGameScene()
    {
        // Mevcut seviyeyi PlayerPrefs'e kaydediyoruz
        PlayerPrefs.SetInt("CurrentLevelIndex", currentLevel);

        // GameScene sahnesine geçiþ yapýyoruz
        SceneManager.LoadScene("GameScene");
    }

    private void ReturnToMainMenu()
    {
        // Ana menü sahnesine geçiþ yapýyoruz
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator FlashText()
    {
        while (true)
        {
            flashingText.enabled = !flashingText.enabled; // Metni açýp kapat
            yield return new WaitForSeconds(0.5f); // Yarým saniye bekle
        }
    }
}
