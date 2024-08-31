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
    private HelixManager helixManager; // HelixManager referans�

    public void Awake()
    {
        CurrentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
        gecisliReklam = FindObjectOfType<GecisliReklam>(); // GecisliReklam scriptine eri�im
        helixManager = FindObjectOfType<HelixManager>(); // HelixManager scriptine eri�im
    }

    private void Start()
    {
        Time.timeScale = 1f;
        noOfPassingRings = 0;
        gameOver = false;
        levelWin = false;

        // E�er CurrentLevelIndex tek say�ysa reklam� y�kle
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
            gameOverPanel.SetActive(true); // Game Over panelini g�ster

            if (Input.GetMouseButtonDown(0)) // Fareye t�klan�rsa
            {
                RestartGame(); // Oyunu yeniden ba�lat
            }
        }

        CurrentLevelText.text = CurrentLevelIndex.ToString();
        NextLevelText.text = (CurrentLevelIndex + 1).ToString();

        // Slider'� g�ncelle
        int progress = noOfPassingRings * 100 / FindObjectOfType<HelixManager>().noOfRings;
        ProggressBar.value = progress;

        if (levelWin)
        {
            levelWinPanel.SetActive(true);
            if (Input.GetMouseButtonDown(0)) // Fareye t�klan�rsa
            {
                AdvanceToNextLevel(); // Bir sonraki seviyeye ge�
            }
        }
    }

    private void RestartGame()
    {
        // Mevcut sahneyi yeniden y�kle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void AdvanceToNextLevel()
    {
        PlayerPrefs.SetInt("CurrentLevelIndex", CurrentLevelIndex + 1);

        // E�er NextLevelIndex tek say�ysa reklam� g�ster
        if ((CurrentLevelIndex + 1) % 2 != 0 && gecisliReklam != null)
        {
            gecisliReklam.ShowInterstitialAd();
        }

        // Mevcut sahneyi yeniden y�kle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
