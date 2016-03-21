/* 		
		Author : Farhan
		Skype : farhan.blu
		Email : farhan.blu@gmail.com
*/

/// <summary>
/// Meant for use with UGUI in 3D games only. Instantiates, deletes, and organizes your canvases inside a single parent,
/// increasing readibility and ease of management.
/// 
/// There were some assumptions made in writing this code and to me, they add more ease than trouble.
/// See "remarks" and method summaries.
/// </summary>

/// <remarks>
/// Meant only for 3d games and requires a GameObject, with this script on it, in the VERY first scene.
/// This gameobject is not supposed to have any UI widgets on it, it serves only as a container for your
/// UI in the 3d game.
/// 
/// Also, it is not meant to have any child before run-time. 
/// 
/// Assumes that all canvases to be added later have their prefab inside "Assets/Resources/Prefabs/UI" folder.
/// </remarks>


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour 
{
	private const string UI_PREFAB_PATH = "Prefabs/UI/";

	public static UIManager _instance	{	private set; get;}

	protected Dictionary<string, object> _persistentUIs;


	protected void Awake()
	{
		if(_instance == null) {
			_instance = this;
			_persistentUIs = new Dictionary<string, object>();
			UnityEngine.Object.DontDestroyOnLoad (gameObject);
		}
		else
			Destroy(gameObject);
	}


	protected void Start()
	{
		List<string> a = new List<string>();
		a.Add("d");
		a.Add("e");
		a.Add("f");

		LazyIterator i = new LazyIterator();
		string key = i.RegisterEnumerable(a, LazyIterator.IterativeMode.Loop);

		Debug.Log(i.GetNext<string>(key));
      	Debug.Log(i.GetNext<string>(key));
    	Debug.Log(i.GetNext<string>(key));
      	Debug.Log(i.GetNext<string>(key));
	}


	/// <summary>
	/// Assumes that UI script attached to a canvas gameobject has the same class name as the prefab name
	/// </summary>
	public T LoadUI<T> (bool persistent = false) where T : Component
	{
		return LoadUI<T> (typeof(T).Name, persistent);
	}

	public T LoadUI<T> (string prefabName, bool persistent) where T :  Component
	{
		string typeName = typeof(T).Name;

		if(_persistentUIs.ContainsKey(typeName))
		   return _persistentUIs[typeName] as T;

		GameObject uiObject = Instantiate (Resources.Load (UI_PREFAB_PATH + prefabName)) as GameObject;
		uiObject.transform.parent = transform;

		T targetComponent = uiObject.GetComponent<T> ();

		if(persistent)
			_persistentUIs[typeName] = targetComponent;

		return targetComponent;
    }


	public bool IsShowingUI<T> () where T : Component
	{
		string typeName = typeof(T).Name;
		if(_persistentUIs.ContainsKey(typeName))
			return ((T) _persistentUIs[typeName]).gameObject.activeInHierarchy;

		Component target = transform.GetComponentInChildren (typeof(T));
		if (target != null)
			return true;
        return false;
    }


	/// <summary>
	/// Destroys non-persistent UI Canvases.
	/// </summary>
	public void DestroyUI<T> () where T : Component
	{
		string typeName = typeof(T).Name;
		if(_persistentUIs.ContainsKey(typeName)) {
			HideUI<T>();
			Debug.LogWarning(string.Format("{0} is a persistent UI, it will only be hidden", typeName));
		}

		Component [] targets = transform.GetComponentsInChildren (typeof(T));
		for (int i = 0; i < targets.Length; i++) {
			if (targets [i] != null)
				Destroy (targets [i].gameObject);
        }
    }


	/// <summary>
	/// Hides persistent UI Canvases.
	/// </summary>
	public void HideUI<T> () where T : Component
	{
		string typeName = typeof(T).Name;
		if(_persistentUIs.ContainsKey(typeName))
			((T) _persistentUIs[typeName]).gameObject.SetActive(false);
		else {
			DestroyUI<T>();
			Debug.LogWarning(string.Format("{0} is not a persistent UI, it will be deleted", typeName));
		}
	}
}
