using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadMenu", 3f);
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
