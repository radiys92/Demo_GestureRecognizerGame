using UnityEngine;
using System.Collections;

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
    }

    void Update()
    {
	    if (Input.GetMouseButtonDown(1))
        {
		    _mouseDown = 1;
	    }
    	
	    if (_mouseDown == 1)
        {
		    Vector2 p = new Vector2(Input.mousePosition.x , Input.mousePosition.y);
		    _pointArr.Add(p);
	    }


	    if (Input.GetMouseButtonUp(1))
        {
		    if (Input.GetKey (KeyCode.LeftControl))
            {
			    // if CTRL is held down, the script will record a gesture. 
			    _mouseDown = 0;
			    GestureRecognizer.recordTemplate(_pointArr);
    		
		    }
            else
            {
			    _mouseDown = 0;

			    // start recognizing! 
			    GestureRecognizer.startRecognizer(_pointArr);

			    _pointArr.Clear();
    		
		    }
    		
	    }
    	
    } 
}
