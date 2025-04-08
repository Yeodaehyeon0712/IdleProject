using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : TSingletonMono<BackgroundManager>
{
    #region Fields
    Background[] backgrounds = new Background[GameConst.MaxBackgroundCount];
    Queue<int> backgroundOrderQueue = new Queue<int>(GameConst.MaxBackgroundCount);
    Transform followTarget;
    int bgMoveThreshold;
    bool isActive;
    #endregion

    #region Init Method
    protected override void OnInitialize()
    {
        GenerateBackground();
        bgMoveThreshold = GameConst.BackgroundOffsetX;
        IsLoad = true;
    }
    void GenerateBackground()
    {
        var prefab = Resources.Load<Background>("Background/Background");

        for (int i = 0; i < GameConst.MaxBackgroundCount; i++)
        {
            var position = new Vector3(GameConst.BackgroundOffsetX * (i - 1), 0, 0);
            backgrounds[i] = Instantiate(prefab, position, Quaternion.identity, transform);
            backgrounds[i].Initialize();
            backgroundOrderQueue.Enqueue(i);
        }
    }
    #endregion

    #region Unity Method
    void Update()
    {
        if (isActive == false||followTarget==null) return;
        CheckMoveBackground();
    }

    #endregion

    #region Background Method
    public void SetupBackground(Transform followTarget,string backgroundKey)
    {
        isActive = true;
        this.followTarget = followTarget;

        Sprite bgSprite = AddressableSystem.GetBackground(backgroundKey);
        foreach (var background in backgrounds)
            background.SetupBackground(bgSprite);
        backgrounds[backgroundOrderQueue.Peek()].Disable();
    }
    public void CleanBackground()
    {
        isActive = false;
        this.followTarget = null;
        bgMoveThreshold = 0;
        foreach (var background in backgrounds)
            background.CleanBackground();
    }
    void CheckMoveBackground()
    {
        if (followTarget.transform.position.x >= bgMoveThreshold)
            MoveBackground();
    }
    void MoveBackground()
    {
        bgMoveThreshold += GameConst.BackgroundOffsetX;

        var moveIndex = backgroundOrderQueue.Dequeue();
        backgrounds[moveIndex].gameObject.SetActive(true);
        backgrounds[moveIndex].transform.position = new Vector3(bgMoveThreshold , 0, 0);
        backgroundOrderQueue.Enqueue(moveIndex);

        var inactiveIndex = backgroundOrderQueue.Peek();
        backgrounds[inactiveIndex].gameObject.SetActive(false);
    }
    #endregion

}