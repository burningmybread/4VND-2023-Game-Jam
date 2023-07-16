using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoCount;
    [SerializeField] private TextMeshProUGUI sphereCount;
    private Turret turretscript;
    private Hull hullscript;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameoverPanel;
    Scene scene;
    string sceneName;

    private void OnEnable()
    {
        Turret.UseAmmo += UpdateAmmo;
        Turret.ReloadDisplay += ReloadDisplay;
        Hull.AddSphere += UpdateSphere;
        PlayerHealth.TriggerGameOver += GameOver;
    }

    private void OnDisable()
    {
        Turret.ReloadDisplay -= ReloadDisplay;
        Turret.UseAmmo -= UpdateAmmo;
        Hull.AddSphere -= UpdateSphere;
        PlayerHealth.TriggerGameOver -= GameOver;
    }

    private void UpdateAmmo()
    {
        ammoCount.text = $"x{turretscript.currentAmmo}";
    }

    private void ReloadDisplay()
    {
        ammoCount.text = $"RELOADING";
    }

    private void UpdateSphere()
    {
        sphereCount.text = $"{hullscript.numberOfSphere}";
    }

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
    
        turretscript = GameObject.Find("Turret").GetComponent<Turret>();
        hullscript = GameObject.Find("Tank").GetComponent<Hull>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainLevel");
    }

    public void GameOver()
    {
        gameoverPanel.SetActive(true);
    }
}
