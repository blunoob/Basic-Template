using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingUI : MonoBehaviour 
{
	public Text _tipMessage;

	public void Show(string tipMessage)
	{
		if(!gameObject.activeInHierarchy)
		{
			gameObject.SetActive(true);
			_tipMessage.text = tipMessage;
		}
	}
}
