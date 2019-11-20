using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UserController : MonoBehaviour
{

	public static UserController instance;
 
	SceneController sceneController;
	private Image img;
	private Text txt;
	private bool start;

	void Awake ()
	{
		instance = this;
	}

	void Start ()
	{ 
		img = transform.Find ("Image").GetComponent<Image> ();
		txt = transform.Find ("Text").GetComponent<Text> (); 
		sceneController = (SceneController)FindObjectOfType (typeof(SceneController));
	}

	void Update ()
	{   
		if (start && !EventSystem.current.IsPointerOverGameObject()) {
			if (Input.GetMouseButton (0)) {
				img.fillAmount += 0.5f * Time.deltaTime;
				txt.text = (int)(img.fillAmount * 100) + "%";
				if (img.fillAmount == 1) {
					OnReset ();
				}
			} else if (Input.GetMouseButtonUp (0)) {
				Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
				sceneController.sendArrow (mouseRay.direction, img.fillAmount);
				OnReset ();
			}
		}
	}

	void OnReset ()
	{ 
		img.fillAmount = 0f;
		txt.text = "Force: 0%";
	}

	public void SetStart (bool start)
	{
		this.start = start;
	}

	public bool IsStart()
	{
		return start;
	}
}