/* 		
		Author : Farhan
		Skype : farhan.blu
		Email : farhan.blu@gmail.com
*/

using UnityEngine;
using System.Collections;

/// <summary>
/// Game Manager is the main entry point to a game.
/// </summary>
public class GameManager : MonoBehaviour 
{
	public static GameManager _instance	{	get;	private set;}

	protected void Awake()
	{
		if(_instance == null) {
			_instance = this;

			//It is important to initialize localization as the first step in your game. 
			LocalizationManager._instance.LoadLanguage();
			DontDestroyOnLoad(gameObject);
		} else
			Destroy(gameObject);
	}


	protected void Start()
	{
		
		//It will be a good idea to just save a reference to the uimanager here and use it henceforth.
		UIManager._instance.LoadUI<LoadingUI>(true);

		//You might want to do something different here.
		this.PerformActionWithDelay(2f, ContinueGame);
	}
		

	protected void ContinueGame ()
	{
		UIManager._instance.DestroyUI<LoadingUI>();
		ShowMenu();
	}


	protected void ShowMenu()
	{
		UIManager._instance.LoadUI<MainMenu>().Show(PlayGame);
	}


	protected void PlayGame()
	{
		//TODO ... Put your code here. You may want to remove the following code.

		UIManager._instance.DestroyUI<MainMenu>();

		//Sample code on how you could define in-line actions for your UIs.
		UIManager._instance.LoadUI<TwoButtonPopup>().Show("You sure?", LocalizationManager._instance.GetLocalizedString("Yes"), () => {
			UIManager._instance.DestroyUI<TwoButtonPopup>();
			ShowMenu();
		}, LocalizationManager._instance.GetLocalizedString("No"), () => {
			UIManager._instance.DestroyUI<TwoButtonPopup>();
			ShowMenu();
		});


	}
}
