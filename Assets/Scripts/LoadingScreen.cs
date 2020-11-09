using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{

    public static LoadingScreen Instance;
    public GameObject bar;
    public float minTimeToShow = 1f;
    public LeanTweenType fadeIn = LeanTweenType.linear , fadeOut = LeanTweenType.linear;
    public float fadeInTime = 0.5f, fadeOutTime = 0.5f;

    private AsyncOperation currentLoadingOperation;
    private bool isLoading;
    private float timeElapsed;
    private CanvasGroup canvasGroup;
    private RectTransform barTransform;

    void Awake()
    {

        if (Instance is null)
        {
            Instance = this;
            canvasGroup = GetComponent<CanvasGroup>();
            barTransform = bar.GetComponent<RectTransform>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Close();

    }

    private void Close()
    {
        gameObject.SetActive(false);
        currentLoadingOperation = null;
        LeanTween.cancel(bar);
        barTransform.localScale = new Vector3(0f,1f,1f);
        isLoading = false;
        canvasGroup.alpha = 0f;
    }

    private void FadeOut() => canvasGroup.LeanAlpha(0f, fadeOutTime).setEase(fadeOut).setOnComplete(Close);

    // Update is called once per frame
    void Update()
    {
        if (isLoading)
        {
            SetCurrentProgress(currentLoadingOperation.progress);

            if (currentLoadingOperation.isDone)
            {
                LeanTween.scaleX(bar, 1f, 0.1f * minTimeToShow).setOnComplete(FadeOut);
            }
            else
            {
                timeElapsed += Time.deltaTime;
                if (timeElapsed >= minTimeToShow)
                    currentLoadingOperation.allowSceneActivation = true;
            }

        }

    }

    private void SetCurrentProgress(float progress)
    {
        // Scalar a barra
        bar.LeanScaleX(progress, currentLoadingOperation.allowSceneActivation? Time.deltaTime : minTimeToShow );
    }

    private void StartLoading() => isLoading = true;

    public void DisplayLoadingScreen(AsyncOperation loadingOperation)
    {
        gameObject.SetActive(true);

        currentLoadingOperation = loadingOperation;
        currentLoadingOperation.allowSceneActivation = false;

        timeElapsed = .0f;

        canvasGroup.LeanAlpha(1f, fadeInTime).setEase(fadeIn).setOnComplete(StartLoading);
    }

}
