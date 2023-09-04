using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{// This is really the only blurb of code you need to implement a Unity singleton
    private static StartMenuManager _Instance;
    public static StartMenuManager Instance
    {
        get
        {
            if (!_Instance)
            {
                _Instance = new GameObject().AddComponent<StartMenuManager>();
                // name it for easy recognition
                _Instance.name = _Instance.GetType().ToString();
                // mark root as DontDestroyOnLoad();
                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }

    // implement your Awake, Start, Update, or other methods here...

    // properties
    [SerializeField]
    Color transitionColor = Color.black;
    [SerializeField]
    float multiplier = 0.5f;
    [SerializeField]
    GameObject aboutPanel;

    void Awake()
    {
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }
        if (aboutPanel.activeInHierarchy)
        {
            aboutPanel.SetActive(false);
        }
    }

    public void StartLevel(int levelNum)
    {
        string sceneString = "Level_" + levelNum;
        Initiate.Fade(sceneString, transitionColor, multiplier);
    }

    public void About(bool should_be_active)
    {
        switch (should_be_active)
        {
            case true:
                aboutPanel.SetActive(true);
                break;
            case false:
                aboutPanel.SetActive(false);
                break;
            default:
        }
    }

    public void ExitGame()
    {
        Debug.Log("Quit called");
        Application.Quit();
    }
}
