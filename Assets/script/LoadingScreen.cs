﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public GameObject[] loadingPages;
    public GameObject loading;


    IEnumerator loadingNewScene(string scene, string unScene)
    {

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        while (!ao.isDone)
        {
            yield return StartCoroutine(loadingThings());
            // [0, 0.9] > [0, 1]
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            Debug.Log("ao.progress" + ao.progress); //this only shows 0 and 0.9??? wtf???hello??? wheres the number in between???
        }
        SceneManager.UnloadSceneAsync(unScene);
        doneLoading();
    }

    IEnumerator loadingThings()
    {
        for (int i = 0; i < 8; i++)
            {
                yield return new WaitForSeconds(0.1f);
                loadingPages[i].SetActive(true);
                if (i == 0)
                {
                    loadingPages[7].SetActive(false);
                }
                else
                {
                    loadingPages[i - 1].SetActive(false);
                }
            }
        yield return null;
    }

    void doneLoading()
    {
        loading.SetActive(false);
    }

    public void load(string scene, string unScene)
    {
        loading.SetActive(true);
        var resizing = loading.transform as RectTransform;
        //var resizing = loading.GetComponent<RectTransform>();
        resizing.sizeDelta = new Vector2(Screen.width, Screen.height);

        StartCoroutine(loadingNewScene(scene,unScene));
    }

    void loadPage(int i)
    {
        loadingPages[i].SetActive(true);
        if (i == 0)
        {
            loadingPages[7].SetActive(false);
        }
        else
        {
            loadingPages[i - 1].SetActive(false);
        }
    }
}
