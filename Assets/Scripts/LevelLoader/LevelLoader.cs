using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f; 
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex +1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Animasyonu Oynat
        transition.SetTrigger("Start");
        //Bekle
        yield return new WaitForSeconds(transitionTime);
        //Sahneyi Y�kle
        SceneManager.LoadScene(levelIndex);
    }
}
