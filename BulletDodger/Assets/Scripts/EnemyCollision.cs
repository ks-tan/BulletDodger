using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyCollision : MonoBehaviour {

	void OnCollisionEnter(Collision other){

		if (other.collider.name == "Player") {
			GameScript.health--;
			Vector3 position = gameObject.transform.position;
			SpecialEffectsScript.MakeExplosion ((position));
			Destroy (gameObject);
		}
	}

	void OnBecameInvisible() {
		Destroy (gameObject);
	}
}
