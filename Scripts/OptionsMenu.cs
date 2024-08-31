using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsPanel; // Panel referans�
    public Button optionsButton;    // Se�enekler butonu referans�
    public Button soundOnButton;    // Ses A� butonu referans�
    public Button soundOffButton;   // Ses Kapa butonu referans�
    public Button mainMenuButton;   // Ana Men� butonu referans�
    public Button resumeButton;     // Oyuna devam etme butonu referans�

    private bool isSoundOn = true; // Varsay�lan olarak ses a��k

    void Start()
    {
        // Buton t�klama olaylar�n� ekle
        optionsButton.onClick.AddListener(ToggleOptionsPanel);
        soundOnButton.onClick.AddListener(TurnSoundOn);
        soundOffButton.onClick.AddListener(TurnSoundOff);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        resumeButton.onClick.AddListener(ResumeGame); // Resume butonu i�levini ekle

        // Ba�lang��ta paneli kapal� tut
        optionsPanel.SetActive(false);
        UpdateSoundButtons();
    }

    void ToggleOptionsPanel()
    {
        bool isActive = !optionsPanel.activeSelf; // Panelin mevcut durumunu tersine �evir

        optionsPanel.SetActive(isActive); // Paneli a�/kapa

        // Panel a��ld���nda oyunu durdur, kapand���nda devam et
        Time.timeScale = isActive ? 0 : 1;
    }

    void TurnSoundOn()
    {
        isSoundOn = true; // Sesi a�
        AudioListener.volume = 1;
        UpdateSoundButtons(); // Butonlar� g�ncelle
    }

    void TurnSoundOff()
    {
        isSoundOn = false; // Sesi kapat
        AudioListener.volume = 0;
        UpdateSoundButtons(); // Butonlar� g�ncelle
    }

    void UpdateSoundButtons()
    {
        // Sese g�re butonlar�n t�klanabilirli�ini ayarla
        soundOnButton.interactable = !isSoundOn;
        soundOffButton.interactable = isSoundOn;
    }

    void GoToMainMenu()
    {
        // Oyunu tekrar devam ettir, ard�ndan ana men�ye d�n
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelScene");
    }

    void ResumeGame()
    {
        // Oyunu devam ettir ve paneli kapat
        optionsPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
