using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;

public class TitleScene : BaseScene
{
    public bool isTestMode;
    Button button_NextScene;
    //나중에 퍼센테이지로 변경할것 .
    TextMeshProUGUI text_Description;

    #region Scene Method
    protected override void OnStartScene()
    {
        button_NextScene = transform.GetComponentInChildren<Button>();
        button_NextScene.onClick.AddListener(() => SceneChange());
        button_NextScene.gameObject.SetActive(false);
        text_Description = transform.Find("TitleUI/Panel_Progress/Text_Progress").GetComponent<TextMeshProUGUI>();
        InitManager().Forget();
    }
    protected override void OnStopScene()
    {

    }
    #endregion
    async UniTask InitManager()
    {
        text_Description.text = "Waiting ... ";
        SnapShotDataProperty.Instance.Initialize();
        await UniTask.WaitUntil(() => SnapShotDataProperty.Instance.IsLoad);

        TimeManager.Instance.Initialize();
        await UniTask.WaitUntil(() => TimeManager.Instance.IsLoad);

        DataManager.Instance.InitAddressableSystem();
        await UniTask.WaitUntil(() => DataManager.AddressableSystem.IsLoad);

        DataManager.Instance.Initialize();
        await UniTask.WaitUntil(() => DataManager.Instance.IsLoad);

        CameraManager.Instance.Initialize();
        await UniTask.WaitUntil(() => CameraManager.Instance.IsLoad);

        LocalizingManager.Instance.Initialize();
        await UniTask.WaitUntil(() => LocalizingManager.Instance.IsLoad);

        UIManager.Instance.Initialize();
        await UniTask.WaitUntil(() => UIManager.Instance.IsLoad);

        StageManager.Instance.Initialize();
        await UniTask.WaitUntil(() => StageManager.Instance.IsLoad);

        BackgroundManager.Instance.Initialize();
        await UniTask.WaitUntil(() => BackgroundManager.Instance.IsLoad);

        ActorManager.Instance.Initialize();
        await UniTask.WaitUntil(() => ActorManager.Instance.IsLoad);

        text_Description.text = "Complete";
        button_NextScene.gameObject.SetActive(true);
    }
    void SceneChange()
    {
        if (isTestMode)
            SceneManager.Instance.AsyncSceneChange<TestScene>().Forget();
        else
            SceneManager.Instance.AsyncSceneChange<BattleScene>().Forget();
    }
}
