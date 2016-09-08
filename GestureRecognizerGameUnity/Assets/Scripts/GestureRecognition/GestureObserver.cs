using UnityEngine;
using System.Collections;
using System.Linq;
using GCon;

public class GestureObserver : MonoBehaviour
{
    public static TextMesh _textMesh;

    ArrayList _pointArr;
    static int _mouseDown;

    // runs when game starts - main function
    void Start ()
    {
	    _pointArr = new ArrayList();
        _textMesh = GetComponent<TextMesh>();
        _textMesh.text = "Templates loaded: " + GestureTemplates.Templates.Count;

        GestureController.OnGestureEnd += OnGestureEnd;
    }
    
    private void OnGestureEnd(Gesture g)
    {
        GestureRecognizer.startRecognizer(g.Frames.Select(i => i.position).ToList());
    }
}
