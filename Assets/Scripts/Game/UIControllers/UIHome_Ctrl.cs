using System;
using UnityEngine;
using System.Collections;
using Framework.Managers;
using Framework.Utils;
using TMPro;

public class UIHome_Ctrl : UICtrl{
    protected override void Awake()
    {
        base.Awake();
        AddButtonListener("StartButton", () => Debug.Log("Start game button pressed."));
        TimerManager.Instance.Schedule(0, 0, 1, false, null, (int _, object _) =>
        {
            Debug.Log("update fps");
            view["FPSText"].GetComponent<TMP_Text>().text = ((int)(1f / Time.unscaledDeltaTime)).ToString();
        });
    }

    void Update()
    {
        // 错误示范（
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 0.5f;
        }
    }
}
