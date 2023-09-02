using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// by @kurtdekker - to make a simple Unity singleton that has no
// predefined data associated with it, eg, a high score manager.
//
// To use: access with GameManager.Instance
//
// To set up:
//	- Copy this file (duplicate it)
//	- rename class GameManager to your own classname
//	- rename CS file too
//
// DO NOT PUT THIS IN ANY SCENE; this code auto-instantiates itself once.
//
// I do not recommend subclassing unless you really know what you're doing.

public class GameManager : MonoBehaviour
{
    // This is really the only blurb of code you need to implement a Unity singleton
    private static GameManager _Instance;
    public static GameManager Instance
    {
        get
        {
            if (!_Instance)
            {
                _Instance = new GameObject().AddComponent<GameManager>();
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
    GameObject doors;
    [SerializeField]
    Animator animator;

    bool closingTime = false;

    private void Awake()
    {
        doors.SetActive(false);
    }

    private void Update()
    {
        if (closingTime)
        {
            doors.SetActive(true);
            animator.SetBool("closing_is_true", true);
        }
    }

    public void Debug_Tool()
    {
        // change according to need
        closingTime = true;
    }
}