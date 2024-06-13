using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : Singleton<PauseMenu>
{
    public GameObject pauseMenu;
    private InputAction pauseAction;

    protected override void Awake()
    {
        base.Awake();
        var inputActionAsset = Resources.Load<InputActionAsset>("PlayerInput");
        pauseAction = inputActionAsset.FindActionMap("UI").FindAction("Pause");

        pauseAction.performed += OnPause;
        pauseAction.Enable();
    }

    void OnPause(InputAction.CallbackContext context)
    {
        if (pauseMenu.activeSelf)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneLoader.LoadScene(SceneNames.MainMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
        // TODO: No me convense
    }

    private void OnDestroy()
    {
        pauseAction.performed -= OnPause;
    }
}
