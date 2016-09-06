using UnityEngine;
using System.Collections;
using GCon;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(LineRenderer))]
public class LineCreator : MonoBehaviour 
{
	LineRenderer _lr = null;
	int _vertCount = 0;

	void Awake()
	{
		// Just subscribe to gesture start delegate
		GestureController.OnGestureStart += OnGestureStart;
		// and gesture end delegate
		GestureController.OnGestureEnd += OnGestureEnd;
		_lr = GetComponent<LineRenderer>();
	}

	void OnDestroy()
	{
		// for security at next scene loading
		GestureController.OnGestureStart -= OnGestureStart;
		GestureController.OnGestureEnd -= OnGestureEnd;
	}

	void OnGestureStart (Gesture g)
	{
		// What do when gesture start?
		_lr.SetVertexCount(1);
		Vector3 pos = GetComponent<Camera>().ScreenToWorldPoint(g.StartPoint);
		pos.z = 0;
		_lr.SetPosition(0,pos);
		_vertCount = 1;
		g.OnGestureStay+=OnGestureStay;
	}

	void OnGestureStay (Gesture g)
	{
		// What do when gesture updated?
		_lr.SetVertexCount(_vertCount+1);
		Vector3 pos = GetComponent<Camera>().ScreenToWorldPoint(g.EndPoint);
		pos.z = 0;
		_lr.SetPosition(_vertCount,pos);
		_vertCount++;
	}

	void OnGestureEnd (Gesture g)
	{
		// Finish him!
		_lr.SetVertexCount(0);
		_vertCount = 0;
	}
}
