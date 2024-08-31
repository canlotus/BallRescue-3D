using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class LevelScene : MonoBehaviour
{
    public Button levelButton1;  // Bir �nceki seviye (ilk seviyede gizli olacak)
    public Button levelButton2;  // Bulundu�umuz seviye, bu buton t�klanabilir olacak
    public Button levelButton3;  // Bir sonraki seviye
    public Button levelButton4;  // Bir sonraki seviye

    public TextMeshProUGUI levelText1;
    public TextMeshProUGUI levelText2;
    public TextMeshProUGUI levelText3;
    public TextMeshProUGUI levelText4;

    public Button mainMenuButton; // Ana men�ye d�nmek i�in buton

    public TextMeshProUGUI flashingText; // Yan�p s�necek metin

    private int currentLevel;

    private void Start()
    {
        // Mevcut seviyeyi al�yoruz
        currentLevel = PlayerPrefs.GetInt("CurrentLevelIndex", 1);

        // Seviye metinlerini g�ncelliyoruz
        UpdateLevelButtons();

        // Buton i�levlerini tan�ml�yoruz
        levelButton2.onClick.AddListener(LoadGameScene);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu); // Ana men� butonu i�levi

        // Yan�p s�nme coroutine'i ba�lat
        if (flashingText != null)
        {
            StartCoroutine(FlashText());
        }
    }

    private void UpdateLevelButtons()
    {
        // �lk buton (Bir �nceki seviye)
        if (currentLevel > 1)
        {
            levelButton1.gameObject.SetActive(true);
            levelText1.text = "Level " + (currentLevel - 1);
        }
        else
        {
            levelButton1.gameObject.SetActive(false);
        }

        // �kinci buton (Mevcut seviye)
        levelText2.text = "Level " + currentLevel;

        // ���nc� buton (Bir sonraki seviye)
        levelText3.text = "Level " + (currentLevel + 1);

        // D�rd�nc� buton (Bir sonraki seviye)
        levelText4.text = "Level " + (currentLevel + 2);
    }

    private void LoadGameScene()
    {
        // Mevcut seviyeyi PlayerPrefs'e kaydediyoruz
        PlayerPrefs.SetInt("CurrentLevelIndex", currentLevel);

        // GameScene sahnesine ge�i� yap�yoruz
        SceneManager.LoadScene("GameScene");
    }

    private void ReturnToMainMenu()
    {
        // Ana men� sahnesine ge�i� yap�yoruz
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator FlashText()
    {
        while (true)
        {
            flashingText.enabled = !flashingText.enabled; // Metni a��p kapat
            yield return new WaitForSeconds(0.5f); // Yar�m saniye bekle
        }
    }
}
