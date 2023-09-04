using UnityEngine;
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


    public Material buttonUnlitMat;
    // public Material buttonLitMat;

    bool canInteract = false;
    GameObject currentHoveredObject;
    bool closingTime = false;

    private void Awake()
    {
        Instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("awake this late???");
        doors.SetActive(false);
    }

    private void Update()
    {
        if (closingTime)
        {
            doors.SetActive(true);
            animator.SetBool("closing_is_true", true);
        }

        CheckLoseCondition();
    }

    void CheckLoseCondition()
    {
        if (enemyGuy.transform.position.x > enemyDestination) {
            Debug.Log("Should be Game Over");
        }
    }

    public void Debug_Tool()
    {
        // change according to need
        closingTime = true;
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

    public void Interact()
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
}