using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class ConnectWaypointsGizmo : MonoBehaviour {

	public bool Enabled = true;

	private int _arrowHeadAngle = 20;

	private float _arrowHeadLength = 0.25f;

	private List<Transform> _children;  // Cache children

	void OnDrawGizmos()
	{
		if(_children == null){
			_children = GetComponentsInChildren<DrawWaypointGizmo>().Select(c => c.transform).ToList();
		}

		if(Enabled && _children.Count >= 2)
		{
			var waypointConfig = GetComponent<WaypointGroupConfig>() ?? new WaypointGroupConfig();

			Gizmos.color = waypointConfig.GizmoColor;

			for(var i = 0; i < _children.Count - 1; i++)
			{
				DrawConnector(
					_children[i].position,
					_children[i + 1].position);
			}

			if(waypointConfig.Loop)
			{
				DrawConnector(
					_children.Last().position,
					_children.First().position);
			}
		}
	}

	private void DrawConnector(Vector3 start, Vector3 end)
	{
		Gizmos.DrawLine(start, end);
		var direction = (end - start).normalized;
		var middle = Vector3.Lerp(start, end, 0.5f);
		
		var right = Quaternion.LookRotation(direction, new Vector3(0, 0, 1)) * Quaternion.Euler(0, 180 + _arrowHeadAngle, 0) * new Vector3(0, 0, 1);
		var left = Quaternion.LookRotation(direction, new Vector3(0, 0, 1)) * Quaternion.Euler(0, 180 - _arrowHeadAngle, 0) * new Vector3(0, 0, 1);

		Gizmos.DrawRay(middle, right * 1f);
		Gizmos.DrawRay(middle, left * 1f);
	}
}
