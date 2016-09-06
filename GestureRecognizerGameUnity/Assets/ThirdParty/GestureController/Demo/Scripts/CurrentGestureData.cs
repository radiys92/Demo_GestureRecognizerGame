using UnityEngine;
using GCon;

[RequireComponent(typeof(TextMesh))]
public class CurrentGestureData : MonoBehaviour 
{
	TextMesh _text;

	/// <summary>
	/// Init
	/// </summary>
	void Awake()
	{
		GestureController.OnGestureStart+=OnGestureStart;
		GestureController.OnGestureEnd+=OnGestureEnd;
		_text = GetComponent<TextMesh>();
	}

	/// <summary>
	/// Finalize	
	/// </summary>
	void OnDestroy()
	{
		GestureController.OnGestureStart+=OnGestureStart;
		GestureController.OnGestureEnd-=OnGestureEnd;
	}

	/// <summary>
	/// Gets the gesture info.
	/// </summary>
	string GetGestureInfo(Gesture g)
	{
		return string.Format("Current (last) gesture data:\r\nID [{0}]" +
			"\r\nStart point [{1}]\r\nCenter point [{2}]\r\nEnd point [{3}]\r\n" +
		                     "Gesture time [{4}]\r\nDistance [{5}]\r\nCode [{6}]" +
		                     "\r\nAngle between first/last points [{7}]\r\nFrames count [{8}]",
							g.ID,
							g.StartPoint,
							g.CenterPoint,
							g.EndPoint,
							g.GestureTime,
							g.Distance,
							g.Code,
		                     g.FirsLastAngle(),
							g.FramesCount);
	}

	/// <summary>
	/// Raises the gesture start event.
	/// </summary>
	void OnGestureStart (Gesture g)
	{
		g.OnGestureStay += OnGestureStay;
		_text.text = GetGestureInfo(g);
	}

	/// <summary>
	/// Raises the gesture stay event.
	/// </summary>
	void OnGestureStay (Gesture g)
	{
		_text.text = GetGestureInfo(g);
	}

	/// <summary>
	/// Raises the gesture end event.
	/// </summary>
	void OnGestureEnd (Gesture g)
	{
//		_text.text = "";
	}
}
