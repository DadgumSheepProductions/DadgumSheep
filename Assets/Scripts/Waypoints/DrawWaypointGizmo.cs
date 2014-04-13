using UnityEngine;
using System.Collections;

public class DrawWaypointGizmo : MonoBehaviour {

	public bool Enabled = true;

	void OnDrawGizmos()
	{
		if(Enabled)
		{
			Gizmos.DrawIcon(transform.position, "waypointstar.png");
		}
	}
}
