using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PlayPhone.MiniJSON;
using System.Linq;

public class ScoreManager : MonoBehaviour {

	public Text status;
	
	private readonly List<PlayPhone.Billing.PurchaseDetails> restoredPurchaseDetails 
		= new List<PlayPhone.Billing.PurchaseDetails>();

	void Start () {

		status.GetComponent<Text> ();

		GetLeaderboard ();

		PlayPhone.Plugin.OnLeaderboardsData += (json) => {
			status.text = "HIGHEST SCORE\n" + (string)json.Substring(22,2);
//			var dict = Json.Deserialize((string)json) as Dictionary<string,object>;
//			Dictionary<string,object> new_dict = (Dictionary<string,object>)((List<object>) dict["all_time"])[0];
//			string highestScore = new_dict["score"].ToString();
//			status.text = "HIGHEST SCORE\n" + highestScore;
		};
		PlayPhone.Plugin.OnLeaderboardsDataError += (json) => {
			status.text = "Error getting data";
		};

		PlayPhone.Billing.OnSuccess += (purchaseDetails) => 
		{ 
			GameScript.health++;
			status.text = "Purchased: " + purchaseDetails.Name + "\n" +
				"Current health: " + GameScript.health;
		};
		PlayPhone.Billing.OnError += (error) => 
		{ 
			status.text = "Error: " + error;
		};
		PlayPhone.Billing.OnCancel += () => 
		{ 
			status.text = "Canceled";
		};
		PlayPhone.Billing.OnRestore += (purchaseDetails) => 
		{
			restoredPurchaseDetails.Add(purchaseDetails);
			var names = (from p in restoredPurchaseDetails select string.Format("{0}(id={1})", p.Name, p.ItemId)).ToArray();
			status.text = "Restored: " + string.Join(", ", names);
		};
	}

	public void PlayAgain(){
		Application.LoadLevel ("GameScene");
	}

	public void PurchaseHealth(){
		status.text = "Purchasing health";
		PlayPhone.Billing.Purchase ("19310");
	}

	void GetLeaderboard(){
		PlayPhone.Plugin.GetLeaderboardData(1338);
		Debug.Log ("Getting leaderboard data");
		status.text = "GETTING\nHIGH\nSCORE";
	}
}
