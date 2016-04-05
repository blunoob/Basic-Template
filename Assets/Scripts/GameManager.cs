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
			LocalizationManager._instance.LoadLanguage(SystemLanguage.English);
			DontDestroyOnLoad(gameObject);
		} else
			Destroy(gameObject);
	}

	protected void Start()
	{
		UIManager._instance.LoadUI<LoadingUI>(true);
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
		UIManager._instance.LoadUI<TwoButtonPopup>().Show("You sure?", LocalizationManager._instance.GetLocalizedString("Yes"), () => {
			UIManager._instance.DestroyUI<TwoButtonPopup>();
			ShowMenu();
		}, LocalizationManager._instance.GetLocalizedString("No"), () => {
			UIManager._instance.DestroyUI<TwoButtonPopup>();
			ShowMenu();
		});
	}
}
