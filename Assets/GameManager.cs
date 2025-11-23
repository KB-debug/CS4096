using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDelayedAction()
    {
        // Start the coroutine on the GameManager host
        StartCoroutine(DelayedActionCoroutine());
    }


    private IEnumerator DelayedActionCoroutine()
    {
        Debug.Log("Stated end");
         yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Victory");
    }


    public void RestartGame()
    {
        SceneManager.LoadScene("StartScene");
    }


    public void ShowHelp()
    {
        SceneManager.LoadScene("Help");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }
}
