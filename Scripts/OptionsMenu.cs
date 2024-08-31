using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsPanel; // Panel referansý
    public Button optionsButton;    // Seçenekler butonu referansý
    public Button soundOnButton;    // Ses Aç butonu referansý
    public Button soundOffButton;   // Ses Kapa butonu referansý
    public Button mainMenuButton;   // Ana Menü butonu referansý
    public Button resumeButton;     // Oyuna devam etme butonu referansý

    private bool isSoundOn = true; // Varsayýlan olarak ses açýk

    void Start()
    {
        // Buton týklama olaylarýný ekle
        optionsButton.onClick.AddListener(ToggleOptionsPanel);
        soundOnButton.onClick.AddListener(TurnSoundOn);
        soundOffButton.onClick.AddListener(TurnSoundOff);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        resumeButton.onClick.AddListener(ResumeGame); // Resume butonu iþlevini ekle

        // Baþlangýçta paneli kapalý tut
        optionsPanel.SetActive(false);
        UpdateSoundButtons();
    }

    void ToggleOptionsPanel()
    {
        bool isActive = !optionsPanel.activeSelf; // Panelin mevcut durumunu tersine çevir

        optionsPanel.SetActive(isActive); // Paneli aç/kapa

        // Panel açýldýðýnda oyunu durdur, kapandýðýnda devam et
        Time.timeScale = isActive ? 0 : 1;
    }

    void TurnSoundOn()
    {
        isSoundOn = true; // Sesi aç
        AudioListener.volume = 1;
        UpdateSoundButtons(); // Butonlarý güncelle
    }

    void TurnSoundOff()
    {
        isSoundOn = false; // Sesi kapat
        AudioListener.volume = 0;
        UpdateSoundButtons(); // Butonlarý güncelle
    }

    void UpdateSoundButtons()
    {
        // Sese göre butonlarýn týklanabilirliðini ayarla
        soundOnButton.interactable = !isSoundOn;
        soundOffButton.interactable = isSoundOn;
    }

    void GoToMainMenu()
    {
        // Oyunu tekrar devam ettir, ardýndan ana menüye dön
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
