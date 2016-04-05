/* 		
		Author : Farhan
		Skype : farhan.blu
		Email : farhan.blu@gmail.com
*/

using System;
using UnityEngine;
using System.Collections;

public class MainMenu : UIMonoBehaviour 
{
	private Action _playAction;
	private Action _settingsAction;

	public void Show(Action onPlayCB, Action onSettingsCB = null)
	{
		_playAction = onPlayCB;
		_settingsAction = onSettingsCB;
	}

	#region UI Interface callback
	public void OnPlay()
	{
		ExecuteAction(_playAction);
	}

	public void OnSettings()
	{
		ExecuteAction(_settingsAction);
	}
	#endregion
}
