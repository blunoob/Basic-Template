/* 		
		Author : Farhan
		Skype : farhan.blu
		Email : farhan.blu@gmail.com
*/

using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TwoButtonPopup : MonoBehaviour 
{
	[SerializeField]
	private Text _messageBox;

	[SerializeField]
	private Text _button1Text;

	[SerializeField]
	private Text _button2Text;

	private Action _button1Action;
	private Action _button2Action;

	public void Show(string message, string button1Text, Action button1Action, string button2Text, Action button2Action)
	{
		_messageBox.text = message;
		_button1Text.text = button1Text;
		_button2Text.text = button2Text;

		_button1Action = button1Action;
		_button2Action = button2Action;
	}

	public void OnClickButton1()
	{
		if(_button1Action != null)
			_button1Action();
	}

	public void OnClickButton2()
	{
		if(_button2Action != null)
			_button2Action();
	}
}
