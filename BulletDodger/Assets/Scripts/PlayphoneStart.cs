using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayphoneStart : MonoBehaviour {

	public Text status;

	void Start () {
		status.GetComponent<Text> ();
		status.text = "Initializing";
		PlayPhone.Plugin.IncrementTracking ();
		PlayPhone.Plugin.OnInit += () => {
			status.text = "";
			PlayPhone.Plugin.ShowIcon();
			PlayPhone.Plugin.GetLaunchScreen();
		};
		PlayPhone.Plugin.OnInitError += (error) => {
			status.text = "Error initializing";
			Debug.Log ("Error initializing");
		};

		PlayPhone.Plugin.Init ();
	
	}

	void OnApplicationQuit () {
		PlayPhone.Plugin.DecrementTracking ();
	}
}
