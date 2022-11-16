using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;
using DG.Tweening;

public enum SceneType
{
    None,
    IpSelect,
    SplashScene,
    TitleScene,
    Loading,
    LobbyV2,
    IngameV2,
}

public class SceneController : Singleton<SceneController>
{
    #region Variables
    private readonly float FadeDuration = 0.5f;

    protected override bool DontDestroyOnload => true;

    [SerializeField]
    private Image imageCurtain;
    [SerializeField]
    private TMP_Text textCalculate;

    public Image imageCalculateCurtain;

    private GameObject curtainParent;

    private SceneType currentSceneType;

    public SceneType NextLoadingSceneType { get; private set; }
    public bool IsWait { get; private set; }

    private Action loadingAction;
    #endregion Variables

    #region Unity Methods
    protected override void OnAwake()
    {
        curtainParent = imageCurtain.transform.parent.gameObject;
        curtainParent.SetActive(false);

        SceneManager.sceneLoaded += OnLoadSceneFinish;
    }

    protected override void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnLoadSceneFinish;

        base.OnDestroy();
    }
    #endregion Unity Methods

    #region Main Methods
    private static readonly Dictionary<float, WaitForSeconds> dicWaitForSeconds;

    static SceneController()
    {
        dicWaitForSeconds = new Dictionary<float, WaitForSeconds>();
    }

    public static WaitForSeconds Get_WaitForSeconds(float second)
    {
        if (!dicWaitForSeconds.ContainsKey(second))
            dicWaitForSeconds.Add(second, new WaitForSeconds(second));
        return dicWaitForSeconds[second];
    }


    private void OnLoadSceneFinish(Scene scene, LoadSceneMode sceneMode)
    {
        if (!Enum.TryParse<SceneType>(scene.name, out SceneType finishSceneType))
        {
            throw new Exception("Not Exist Scene");
        }

        currentSceneType = finishSceneType;
    }

    public void SetNextLoadingScene(SceneType nextScene)
    {
        ResetWaitFlag();

        NextLoadingSceneType = nextScene;
        LoadScene(SceneType.Loading, false);
    }

    public void JustLoadScene(SceneType sceneType)
    {
        IsWait = true;
        LoadScene(sceneType, false);
    }

    public void LoadScene(SceneType loadScene, bool isAsync)
    {

        if (isAsync)
        {
            StopAllCoroutines();

            StartCoroutine(AsyncLoadScene(loadScene));
        }
        else
        {

            SceneManager.LoadScene(loadScene.ToString());
        }
    }

    private IEnumerator AsyncLoadScene(SceneType sceneType)
    {
        var operate = SceneManager.LoadSceneAsync(sceneType.ToString());

        operate.allowSceneActivation = false;

        while (!operate.isDone)
        {
            if (operate.progress >= 0.9f)
            {
                break;
            }

            yield return null;
        }

        if (loadingAction != null)
        {
            loadingAction?.Invoke();
            yield return Get_WaitForSeconds(1.5f);
        }

        ShowCurtain();
        yield return Get_WaitForSeconds(0.5f);
        operate.allowSceneActivation = true;
    }

    public void SetLoadingAction(Action action) => loadingAction = action;
    public void RemoveLoadingAction() => loadingAction = null;

    private void ShowCurtain()
    {
        if (currentSceneType == SceneType.Loading
            || currentSceneType == SceneType.TitleScene)
        {
            imageCurtain.DOFade(1.0f, FadeDuration).OnStart(() => curtainParent.SetActive(true));
        }
    }

    public void CloseCurtain()
    {
        Sequence sequence = DOTween.Sequence();
        if (loadingAction != null)
        {
            sequence.AppendInterval(1.0f);
        }
        sequence.Append(imageCurtain.DOFade(0.0f, FadeDuration).OnComplete(() => curtainParent.SetActive(false)));
    }

    public void ResetWaitFlag()
    {
        IsWait = false;
    }
    #endregion Main Methods
}
