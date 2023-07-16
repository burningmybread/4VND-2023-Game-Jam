using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIScript : MonoBehaviour
{
    [SerializeField] private GameObject pausepanel;
    private bool paused = false;

    Scene scene;
    string sceneName;

    [Header("Ammo & PuzzleCollect")]
    [SerializeField] private TextMeshProUGUI ammoCount;
    [SerializeField] private TextMeshProUGUI sphereCount;

    private Turret turretscript;
    private Hull hullscript;

    private void OnEnable()
    {
        Turret.ReduceAmmo += UpdateAmmo;
        Hull.AddSphere += UpdateCollect;
    }

    private void OnDisable()
    {
        Turret.ReduceAmmo -= UpdateAmmo;
        Hull.AddSphere -= UpdateCollect;
    }

    private void UpdateAmmo()
    {
        ammoCount.text = $"x{turretscript.currentAmmo}";
    }

    private void UpdateCollect()
    {
        sphereCount.text = $"x{hullscript.numberOfSphere}";
    }

    void Start()
    {
        turretscript = GameObject.FindGameObjectWithTag("Player").GetComponent<Turret>();
        hullscript = GameObject.FindGameObjectWithTag("Player").GetComponent<Hull>();
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        paused = true;
        pausepanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        paused = false;
        pausepanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        paused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
