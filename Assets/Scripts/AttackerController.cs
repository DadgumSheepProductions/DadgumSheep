using UnityEngine;
using System.Collections;
using System.Linq;

public class AttackerController : MonoBehaviour {

	GameObject waypoints;
	Vector3 targetWaypoint;
	int target;

	// Use this for initialization
	void Start () {
		target = 2;
		waypoints = GameObject.FindGameObjectsWithTag("Waypoint")[0];
		targetWaypoint = (Vector2)
			(waypoints.GetComponentsInChildren<Transform>().Select(c => c.transform.position).ToList()[target]);
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 diff = targetWaypoint - this.transform.position;
		if (diff.magnitude < 0.01) {
			target++;
			if(target >= waypoints.GetComponentsInChildren<Transform>().Select(c => c.transform.position).ToList().Count) {
				GameObject.Destroy(this.gameObject);
			}
			else {
				targetWaypoint = (Vector2)
					(waypoints.GetComponentsInChildren<Transform>().Select(c => c.transform.position).ToList()[target]);
			}
		}
		diff.Normalize();
		rigidbody2D.velocity = diff;
	}
}
