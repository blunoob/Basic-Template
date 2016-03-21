using System;
using UnityEngine;
using System.Collections;

public class UIMonoBehaviour : MonoBehaviour 
{
	protected void ExecuteAction(Action action)
	{
		if(action != null)
			action();
	}

}
