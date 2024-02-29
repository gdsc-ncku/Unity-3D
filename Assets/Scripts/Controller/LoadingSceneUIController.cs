using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneUIController : MonoBehaviour
{
    [SerializeField] GameStatus gameStatus;
    [SerializeField] LoadingSceneUIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        uiManager.loadingUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Add listener event in OnEnable and remove in OnDisable and OnDestroy to avoid memory leak
    private void OnEnable()
    {
        gameStatus.loadingSceneHandleChange.AddListener(HandleLoadingEvent);
    }

    private void OnDisable()
    {
        gameStatus.loadingSceneHandleChange.RemoveListener(HandleLoadingEvent);
    }

    private void OnDestroy()
    {
        gameStatus.loadingSceneHandleChange.RemoveListener(HandleLoadingEvent);
    }

    //If event happen excute the function which using IEnumerator to excute function async
    void HandleLoadingEvent(AsyncOperationHandle handle)
    {
        StartCoroutine(DisplayLoadingBar(handle));
    }

    IEnumerator DisplayLoadingBar(AsyncOperationHandle handle)
    {
        //handle is loading scene, if its isValid then close this function or its enter the "while" that can update the loading bar 
        if(!handle.IsValid())
            yield break;

        uiManager.loadingUI.enabled = true;
        while (!handle.IsDone)
        {
            uiManager.slider.value = handle.PercentComplete;
            yield return null;
        }

        //Wait for DataManager catch the player data
        while (!gameStatus.CatchData)
        {
            yield return null;
        }

        //When handle is done, handle.PercentComplete = 1 but it won't enter the "while", thus we need set it to 1;
        yield return new WaitForSeconds(0.5f);
        uiManager.slider.value = 1;
        //Disable loading UI
        yield return new WaitForSeconds(1f);
        uiManager.loadingUI.enabled = false;
        uiManager.slider.value = 0;
    }
}
