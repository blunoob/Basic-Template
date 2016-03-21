using UnityEngine;
using System.Collections;

/// <summary>
/// Game Manager is the main entry point to a game.
/// </summary>
public class GameManager : MonoBehaviour 
{
	public GameManager _instance	{	get;	private set;}

	protected void Awake()
	{
		if(_instance == null) {
			_instance = this;
			DontDestroyOnLoad(gameObject);
		} else
			Destroy(gameObject);
	}

	protected void Start()
	{

	}
}
