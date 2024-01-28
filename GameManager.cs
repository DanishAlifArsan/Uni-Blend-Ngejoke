using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int maxAttemps = 3;
    private int numberOfAttemps;

    public static GameManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        numberOfAttemps = 0;
    }

    public bool CheckForAttempt() {
        return numberOfAttemps >= maxAttemps;
    } 

    public void IncreaseAttempt(int i) {
        numberOfAttemps += i;
    }
    public void QuitButtonClick()
    {
        Debug.Log("Aplikasi telah ditutup.");
        Application.Quit();
    }
    public void play()
    {
        SceneManager.LoadScene("game");
    }
    public void backtomenu()
    {
        SceneManager.LoadScene("mainmenu");
    }
}
