/* 		
		Author : Farhan
		Skype : farhan.blu
		Email : farhan.blu@gmail.com
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LoadingUI : UIMonoBehaviour 
{
	public Text _tipMessage;

	private static List<string> _tipMessages;
	static LoadingUI()
	{
		_tipMessages = new List<string>();

		// The text added here should ideally be just keys against which there are localized string values predefined in the 
		// LocalizationManager editor (Window>LocalizationManager).
		_tipMessages.Add("Yes");
		_tipMessages.Add("Relax");
		_tipMessages.Add("Flaming is discouraged.");
	}


	protected void Start()
	{
		_tipMessage.text = LocalizationManager._instance.GetLocalizedString(_tipMessages.GetRandom());
	}


	public void Show()
	{
		Show (_tipMessages.GetRandom());
	}


	private void Show(string tipMessage)
	{
		if(!gameObject.activeInHierarchy)
		{
			gameObject.SetActive(true);
			_tipMessage.text = LocalizationManager._instance.GetLocalizedString(tipMessage);
		}
	}
}
