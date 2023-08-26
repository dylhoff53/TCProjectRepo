using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneMang : MonoBehaviour
{
    public GameObject stuff;

    public float counter;
    public bool clicked = false;
    public float muti;
    public float musicCounter;
    public AudioSource menuMusicSource;

    // Update is called once per frame
    void Update()
    {
        if (clicked == true)
        {
            counter -= muti * Time.deltaTime;
            stuff.GetComponent<CanvasGroup>().alpha = counter;
            if (counter <= 0f)
            {
                Invoke("Change", 1.5f);
            }
            menuMusicSource.volume -= Time.deltaTime * musicCounter;
        }

    }

    public void GotClicked()
    {
        clicked = true;
    }

    public void Change()
    {
        SceneManager.LoadScene(1);
    }
}
