using UnityEngine;
using System.Collections;
using GCon;

// Need to add at object with camera
[RequireComponent(typeof(Camera))]
public class Throw2dObject : MonoBehaviour 
{
	public float ThrowForce = 400;
	// layer mask of objects which can be throwed
	public LayerMask ThrowMask;

	// current throw target
	private Transform _target;
	// current gesture ID
	private int _gID = -1;

	// Init
	void Awake()
	{
		GestureController.OnGestureStart+=OnGestureStart;
		GestureController.OnGestureEnd+=OnGestureEnd;
	}

	// Finalize
	void OnDestroy()
	{
		// It's need to safely load enother level
		GestureController.OnGestureStart+=OnGestureStart;
		GestureController.OnGestureEnd-=OnGestureEnd;
	}

	/// Raises the gesture start event.
	void OnGestureStart (Gesture g)
	{
		// will remember gesture ID
		if (_gID == -1)
		{
			// if gesture start from any object
			Ray r = GetComponent<Camera>().ScreenPointToRay(g.StartPoint);
			RaycastHit2D hit = Physics2D.Raycast(r.origin,r.direction,100,ThrowMask);
			if (hit)
			{
				// remember it
				_target = hit.rigidbody.transform;
				g.OnGestureStay += OnGestureStay;
				_gID = g.ID;
			}
		}
	}

	/// Raises the gesture stay event.
	void OnGestureStay (Gesture g)
	{
		// for example, we need only short gestures
		// (about 0.1 seconds)
		if (g.GestureTime > 0.1f && g.StartPoint != g.EndPoint) 
			GestureController.StopGesture(g);
	}

	/// Raises the gesture end event.
	void OnGestureEnd (Gesture g)
	{
		// if it our membered gesture
		if (_gID == g.ID)
		{
			if (_target != null && _target.GetComponent<Rigidbody2D>() != null)
			{
				Vector3 start = GetComponent<Camera>().ScreenToWorldPoint(g.StartPoint);
				Vector3 end = GetComponent<Camera>().ScreenToWorldPoint(g.EndPoint);
				// than throw it
				_target.GetComponent<Rigidbody2D>().AddForce(ThrowForce * (end - start).normalized);
			}
			_gID = -1;
		}
	}
}
