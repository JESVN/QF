﻿/****************************************************************************
 * 2020.10 DESKTOP-S3DDJ2V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public partial class MainController
	{
		[SerializeField] public UnityEngine.UI.Text TileText;
		[SerializeField] public UnityEngine.UI.Button StartButton;
		[SerializeField] public UnityEngine.UI.Button ExitButton;
		[SerializeField] public UnityEngine.UI.Button CloseButton;
		[SerializeField] public UnityEngine.UI.Text VersionsText;

		public void Clear()
		{
			TileText = null;
			StartButton = null;
			ExitButton = null;
			CloseButton = null;
			VersionsText = null;
		}

		public override string ComponentName
		{
			get { return "MainController";}
		}
	}
}
