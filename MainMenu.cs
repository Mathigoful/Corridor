using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public AudioSource _source;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine("LoadingLevel");
    }

    public void Quit()
    {
        StartCoroutine("QuitGame");
    }

    IEnumerator LoadingLevel()
    {
        _source.Play(0);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Level1");
    }

    IEnumerator QuitGame()
    {
        _source.Play(0);
        yield return new WaitForSeconds(1);
        Debug.Log("Application Quit");
        Application.Quit();
    }
}
