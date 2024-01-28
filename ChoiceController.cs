using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoiceController : MonoBehaviour
{
    public GameObject backgroundtext; 
    public Button[] buttonList; 
    public TextMeshProUGUI[] buttonText;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI  objectifText;
    Tuple<Manusia, string>[] randomJokesTuple;
    Manusia manusiaTag;
    public int health = 3;
   

    public void CheckJokes(Tuple<Manusia, string>[] jokesTuple, Manusia manusia) {
        foreach (var btn in buttonList) //
        {
            btn.gameObject.SetActive(true);
        }

        manusiaTag = manusia;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        System.Random random = new System.Random();
        randomJokesTuple = jokesTuple.OrderBy(pos => random.Next()).ToArray();

        for (int i = 0; i < Mathf.Min(buttonText.Length, randomJokesTuple.Length); i++)
        {
           buttonText[i].text = randomJokesTuple[i].Item2.Substring(0,randomJokesTuple[i].Item2.IndexOf("|") + 1);
        }
    }

    public void ShowDialogue(int i)  {
        StartCoroutine(PlayDialogue(randomJokesTuple[i]));
    }

    private IEnumerator PlayDialogue(Tuple<Manusia, string> jokesTuple) {
        
        foreach (var btn in buttonList)
        {
            btn.gameObject.SetActive(false);
            backgroundtext.SetActive(true);
        }
        string[] splitString = jokesTuple.Item2.Split("|");
        for (int i = 0; i < splitString.Length; i++)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            dialogueText.text = splitString[i];
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (jokesTuple.Item1 == manusiaTag)
        {
            SceneManager.LoadScene("Scene Menang");
        } else {
            dialogueText.text = ""; //
            this.gameObject.SetActive(false);
            backgroundtext.SetActive(false);
            objectifText.text = "*JOKES KAMU GARING CARI JOKES LAIN" ;
            health=  health - 1;
            if(health <= 0)
            {
                GameManager.instance.IncreaseAttempt(3);

                if (GameManager.instance.CheckForAttempt())
                {
                    SceneManager.LoadScene("Scene Kalah");
                }
            }
            
        }
    }
}
