using UnityEngine;
using System.Collections;

public class StartManager : MonoBehaviour {

	public GameObject enemy;
	private float timer;

	void Start () {
		timer = 0;
	}
	
	void Update () {
		if (timer < Time.timeSinceLevelLoad) {
			Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width), Screen.height, 1));
			Instantiate (enemy, screenPosition, Quaternion.identity);
			timer = Time.timeSinceLevelLoad + 0.2f;
		}
	}

	public void StartGame(){
		Application.LoadLevel ("GameScene");
	}
}
