using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : Singleton<MainMenu>
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI version;
    public GameObject mainMenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.productName);
        Debug.Log(Application.version);
        title.text = Application.productName;
        version.text = "VER. " + Application.version;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ExitGame(){

        Debug.Log("Exit game");
        Application.Quit();
    }

    private void StartGame(){
        Debug.Log("Start game");
        mainMenuCanvas.SetActive(false);
        SceneLoader.LoadScene(SceneNames.InsideSubway);
    }
}
