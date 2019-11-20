using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

	public static UIController instance;

	public Button startBtn;
	public Button reStartBtn;
	public Button reTryBtn;
	public Button quitBtn;
	public Text gameState;
	public Text gameScore;
	public GameObject endGamePanel;
	public GameObject startGamePanel; 

	void Awake ()
	{
		instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		startBtn.onClick.AddListener (OnStartGame);
		reStartBtn.onClick.AddListener (OnStartGame);
		reTryBtn.onClick.AddListener (OnRetryGame);
		quitBtn.onClick.AddListener (OnQuit);
		endGamePanel.gameObject.SetActive (false);
	} 

	void OnStartGame ()
	{
		Debug.Log ("start game");
		startGamePanel.SetActive (false);
		endGamePanel.gameObject.SetActive (false); 
		setScore (0);
		UserController.instance.SetStart (true);
		CameraController.instance.setEnable (true);
	}

	void OnRetryGame ()
	{
		SceneManager.LoadScene (0);
	}

	void OnQuit ()
	{
		Application.Quit ();
	}

	public void ShowEndGameState (string state)
	{
		endGamePanel.gameObject.SetActive (true);
		gameState.text = state;
		UserController.instance.SetStart (false);
		CameraController.instance.setEnable (false);

	}

	public void setScore (int score)
	{
		gameScore.text = "Score: " + score;
	} 
}
