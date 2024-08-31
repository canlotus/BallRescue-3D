using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelWin;

    public GameObject gameOverPanel;
    public GameObject levelWinPanel;

    public static int CurrentLevelIndex;
    public static int noOfPassingRings;

    public TextMeshProUGUI CurrentLevelText;
    public TextMeshProUGUI NextLevelText;

    public Slider ProggressBar;

    private GecisliReklam gecisliReklam; // GecisliReklam scriptine referans
    private HelixManager helixManager; // HelixManager referansý

    public void Awake()
    {
        CurrentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
        gecisliReklam = FindObjectOfType<GecisliReklam>(); // GecisliReklam scriptine eriþim
        helixManager = FindObjectOfType<HelixManager>(); // HelixManager scriptine eriþim
    }

    private void Start()
    {
        Time.timeScale = 1f;
        noOfPassingRings = 0;
        gameOver = false;
        levelWin = false;

        // Eðer CurrentLevelIndex tek sayýysa reklamý yükle
        if (CurrentLevelIndex % 2 != 0 && gecisliReklam != null)
        {
            gecisliReklam.LoadInterstitialAd();
        }
    }

    private void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0; // Oyunu durdur
            gameOverPanel.SetActive(true); // Game Over panelini göster

            if (Input.GetMouseButtonDown(0)) // Fareye týklanýrsa
            {
                RestartGame(); // Oyunu yeniden baþlat
            }
        }

        CurrentLevelText.text = CurrentLevelIndex.ToString();
        NextLevelText.text = (CurrentLevelIndex + 1).ToString();

        // Slider'ý güncelle
        int progress = noOfPassingRings * 100 / FindObjectOfType<HelixManager>().noOfRings;
        ProggressBar.value = progress;

        if (levelWin)
        {
            levelWinPanel.SetActive(true);
            if (Input.GetMouseButtonDown(0)) // Fareye týklanýrsa
            {
                AdvanceToNextLevel(); // Bir sonraki seviyeye geç
            }
        }
    }

    private void RestartGame()
    {
        // Mevcut sahneyi yeniden yükle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void AdvanceToNextLevel()
    {
        PlayerPrefs.SetInt("CurrentLevelIndex", CurrentLevelIndex + 1);

        // Eðer NextLevelIndex tek sayýysa reklamý göster
        if ((CurrentLevelIndex + 1) % 2 != 0 && gecisliReklam != null)
        {
            gecisliReklam.ShowInterstitialAd();
        }

        // Mevcut sahneyi yeniden yükle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
