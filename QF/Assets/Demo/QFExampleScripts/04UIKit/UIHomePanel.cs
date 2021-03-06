﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QFramework.Example
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;
    public class UIHomePanelData : QFramework.UIPanelData
    {
        public string tile;
        public string versions;
    }
    
    public partial class UIHomePanel : QFramework.UIPanel
    {
        
        protected override void ProcessMsg(int eventId, QFramework.QMsg msg)
        {
            throw new System.NotImplementedException ();
        }
        
        protected override void OnInit(QFramework.IUIData uiData)
        {
            mData = uiData as UIHomePanelData ?? new UIHomePanelData();
            // please add init code here
            this.Main.CloseButton.onClick.AddListener(() =>
            {
                this.Hide();
            });
            this.Main.StartButton.onClick.AddListener(() =>
            {
                ResKitSceneLoad.Instance.SceneLoadAsyn("ResKitScene");
            });
            this.Main.ExitButton.onClick.AddListener(() =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            });
            this.Main.TileText.text = mData.tile;
            this.Main.VersionsText.text = mData.versions;
        }
        
        protected override void OnOpen(QFramework.IUIData uiData)
        {
            Debug.Log($"打开");
        }
        
        protected override void OnShow()
        {
            Debug.Log($"显示");
        }
        
        protected override void OnHide()
        {
            Debug.Log($"隐藏");
        }
        
        protected override void OnClose()
        {
            Debug.Log($"关闭");
        }
        protected new void OnDestroy()
        {
            this.CloseSelf();
        }
    }
}
