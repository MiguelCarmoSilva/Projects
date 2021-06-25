using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	
	private Transform target;
	public float trackSpeed = 25;
	

	public void SetTarget(Transform t) {
		target = t;
		transform.position = new Vector3(t.position.x,t.position.y,transform.position.z);
	}

	void LateUpdate() {
		if (target) {
			float x = IncrementTowards(transform.position.x, target.position.x, trackSpeed);
			float y = IncrementTowards(transform.position.y, target.position.y, trackSpeed);
			transform.position = new Vector3(x,y, transform.position.z);
		}
	}

	private float IncrementTowards(float n, float target, float a) {
		if (n == target) {
			return n;	
		}
		else {
			float dir = Mathf.Sign(target - n); 
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target-n))? n: target; 
		}
	}
}
