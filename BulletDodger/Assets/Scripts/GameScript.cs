using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class GameScript : MonoBehaviour
{

	public GameObject player;
	public GameObject enemy;
	public Text score;
	public Text status;
	public GameObject panel;

	private Dictionary<int, GameObject> trails = new Dictionary<int, GameObject>();
	private float timer;
	public static int health;

	void Start() {
		health = 3;
		panel.SetActive (false);
		timer = Time.timeSinceLevelLoad + 2;
		score.GetComponent<Text> ();
	}
	
	void Update() {

		player.transform.Rotate(Vector3.back, 200 * Time.deltaTime);

		if (health == 0) {
			Vector3 position = player.transform.position;
			SpecialEffectsScript.MakeExplosion ((position));
			SpecialEffectsScript.MakeExplosion ((position));
			SpecialEffectsScript.MakeExplosion ((position));
			panel.SetActive(true);

			health = 3;
			float currentScore = Mathf.Round (Time.timeSinceLevelLoad-1);
			PlayPhone.MyPlay.SubmitScore ("1338", (int)currentScore);
			Debug.Log ("Submitting score: " + (int) currentScore);
			if (currentScore >= 5){
				Debug.Log("Achievement unlocked");
				PlayPhone.MyPlay.UnlockAchievement("3514");
			}
			Destroy(player);
		}

		if (timer < Time.timeSinceLevelLoad && health!=0) {
			Vector2 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width), Screen.height));
			Instantiate (enemy, screenPosition, Quaternion.identity);
			timer = Time.timeSinceLevelLoad + 0.1f;
			score.text = Mathf.Round (Time.timeSinceLevelLoad-1).ToString ();
		}

		for (int i = 0; i < Input.touchCount; i++) {
			Touch touch = Input.GetTouch(i);

			if (touch.phase == TouchPhase.Began)
			{
				// Store this new value
				if (trails.ContainsKey(i) == false)
				{
					Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);
					position.z = 0; // Make sure the trail is visible
					
					GameObject trail = SpecialEffectsScript.MakeTrail(position);
					
					if (trail != null)
					{
						Debug.Log(trail);
						trails.Add(i, trail);
					}
				}
			}
			else if (touch.phase == TouchPhase.Moved)
			{
				// Move the trail
				if (trails.ContainsKey(i))
				{
					GameObject trail = trails[i];
					
					Camera.main.ScreenToWorldPoint(touch.position);
					Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);
					position.z = 0; // Make sure the trail is visible
					
					trail.transform.position = position;
				
					player.transform.position = position;
				}
			}
			else if (touch.phase == TouchPhase.Ended)
			{
				// Clear known trails
				if (trails.ContainsKey(i))
				{
					GameObject trail = trails[i];
					
					// Let the trail fade out
					Destroy(trail, trail.GetComponent<TrailRenderer>().time);
					trails.Remove(i);
				}
			}
		}
	}

	public void PlayAgain(){
		Application.LoadLevel (Application.loadedLevel);
		timer = 0;
	}

	public void ViewScores(){
		Application.LoadLevel ("ScoreScene");
	}

}