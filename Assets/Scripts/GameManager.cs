using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // This is really the only blurb of code you need to implement a Unity singleton
    public static GameManager Instance { get; private set; }

    // implement your Awake, Start, Update, or other methods here...

    // properties
    [SerializeField] GameObject doors;
    [SerializeField] Animator animator;
    [SerializeField] GameObject enemyGuy;
    [SerializeField] float enemyDestination = -0.3f;

    // scene transition stuff
    [SerializeField] Color transitionColor = Color.black;
    [SerializeField] float multiplier = 2f;
    // flag for whether we are in a level or menu
    bool inMenu;
    // for tracking which level we're theoretically still in
    string lastSceneName;


    public Material buttonUnlitMat;
    // public Material buttonLitMat;


    bool canInteract;
    GameObject currentHoveredObject;
    bool closingTime;

    private void ResetValues()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        // bools and such
        CheckIfInLevel();
        canInteract = false;
        closingTime = false;
        currentHoveredObject = null;

        Debug.Log("awake this late???");
        if (!inMenu)
        {
            doors.SetActive(false);
            animator.SetBool("closing_is_true", false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Awake()
    {
        ResetValues();
        SceneManager.activeSceneChanged += HandleSceneChange;
    }

    private void Update()
    {
        if (!inMenu)
        {

            if (closingTime)
            {
                doors.SetActive(true);
                animator.SetBool("closing_is_true", true);
            }

            CheckLoseCondition();
        }
    }

    void HandleSceneChange(Scene current, Scene next)
    {
        CheckIfInLevel();
    }

    void CheckIfInLevel()
    {
        if (SceneManager.GetActiveScene().name.Contains("Level_"))
        {
            inMenu = false;
        }
        else
        {
            inMenu = true;
        }
    }

    void CheckLoseCondition()
    {
        if (enemyGuy.transform.position.x > enemyDestination)
        {
            Debug.Log("Should be Game Over");
            lastSceneName = SceneManager.GetActiveScene().name;
            Initiate.Fade("GameLoss", transitionColor, multiplier / 3f);
        }
    }

    public void ToggleInteract(GameObject hoveredObject = null)
    {
        if (hoveredObject == null)
        {
            canInteract = false;
        }
        else
        {
            canInteract = true;
            currentHoveredObject = hoveredObject;
        }
    }

    public void Interact() // WIN CONDITION IS IN THIS BLOCK
    {
        if (canInteract)
        {
            // TODO: to have more interactions would need a better solution than checking for our one specific gameobject at the moment
            if (currentHoveredObject.CompareTag("winButton"))
            {
                Debug.Log("it is indeed the button...");
                buttonUnlitMat.EnableKeyword("_EMISSION");
                closingTime = true;
            }
        }
    }

    public void RestartLevel()
    {
        ResetValues();
        Initiate.Fade(lastSceneName, transitionColor, multiplier);
    }


    public void BackToStartMenu()
    {
        Initiate.Fade("Start_Menu", transitionColor, multiplier);
    }

    public void ExitGame()
    {
        Debug.Log("Quit called");
        Application.Quit();
    }

}