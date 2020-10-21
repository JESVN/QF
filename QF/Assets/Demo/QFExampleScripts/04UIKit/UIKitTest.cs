using System.Collections;
using System.Collections.Generic;
using QFramework;
using QFramework.Example;
using UnityEngine;
/// <summary>
/// 1.UIKit目前看起来还行，不过若是一个场景里面有多个UIRoot可能就不太好操作了，这是缺点
/// 2.UIKit合适用来做静态UI，动态UI需要自己实现一套或者找找插件
/// </summary>
public class UIKitTest : MonoBehaviour
{
    [System.Serializable]
    public class UIPanelTesterInfo
    {
        /// <summary>
        /// 页面的名字
        /// </summary>
        public string PanelName;

        /// <summary>
        /// 层级名字
        /// </summary>
        public UILevel Level;
    }

    /// <summary>
    /// 页面的名字
    /// </summary>
    public string PanelName;

    /// <summary>
    /// 层级名字
    /// </summary>
    public UILevel Level;

    [SerializeField] private List<UIPanelTesterInfo> mOtherPanels;

    private void Awake()
    {
        ResMgr.Init();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        //Open();
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width/2,0,150,50),"打开UIHomePanel" ))
        {
             UIKit.OpenPanel<UIHomePanel>(new UIHomePanelData() {versions = "[V1.0.0]", tile = "EVGame"});
        }
        if (GUI.Button(new Rect(Screen.width/2,50,150,50),"卸载UIHomePanel" ))
        {
            UIKit.ClosePanel<UIHomePanel>();
        }
    }

    private void Open()
    {
        //1.默认打开
        //UIKit.OpenPanel(PanelName, Level);
        //2.打开指定UI界面并初始化参数
        UIKit.OpenPanel<UIHomePanel>(new UIHomePanelData() {versions = "[V1.0.0]", tile = "EVGame"});
        mOtherPanels.ForEach(panelTesterInfo => { UIKit.OpenPanel(panelTesterInfo.PanelName, panelTesterInfo.Level); });
    }
    private void Close()
    {
        UIKit.HidePanel<UIHomePanel>();
    }
}