/****************************************************************************
 * 2020.10 DESKTOP-S3DDJ2V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public partial class GameingController
	{
		[SerializeField] public UnityEngine.UI.Image BG;
		[SerializeField] public UnityEngine.UI.Text TileText;
		[SerializeField] public UnityEngine.UI.Button ExitButton;

		public void Clear()
		{
			BG = null;
			TileText = null;
			ExitButton = null;
		}

		public override string ComponentName
		{
			get { return "GameingController";}
		}
	}
}
