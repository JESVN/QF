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
    
    
    // Generate Id:a951b945-c1af-4cfd-b218-b64a52a92cdd
    public partial class GameEvent
    {
        
        public const string NAME = "Game";
        
        // 游戏界面管理器
        [SerializeField()]
        public GameMainController Main;
        
        // 游戏进行中管理器
        [SerializeField()]
        public GameingController Gameing;
        
        private GameEventData mPrivateData = null;
        
        public GameEventData Data
        {
            get
            {
                return mData;
            }
        }
        
        GameEventData mData
        {
            get
            {
                return mPrivateData ?? (mPrivateData = new GameEventData());
            }
            set
            {
                mUIData = value;
                mPrivateData = value;
            }
        }
        
        protected override void ClearUIComponents()
        {
            Main = null;
            Gameing = null;
            mData = null;
        }
    }
}
