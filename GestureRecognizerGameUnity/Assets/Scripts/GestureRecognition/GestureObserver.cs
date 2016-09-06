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

    void OnGUI ()
    {
	    if(GestureRecognizer.recordDone == 1)
        { 
		    GUI.Window (0, new Rect (350, 220, 300, 100), DoMyWindow, "Save the template?");
	    }
    }

    void DoMyWindow (int windowID)
    {
        GestureRecognizer.stringToEdit = GUILayout.TextField(GestureRecognizer.stringToEdit);

        if (GUI.Button (new Rect (100,50,50,20), "Save"))
        {
            var temp = new ArrayList();
            var a = (ArrayList)GestureTemplates.Templates[GestureTemplates.Templates.Count - 1];

            for (var i = 0; i < GestureRecognizer.newTemplateArr.Count; i++)
                temp.Add(GestureRecognizer.newTemplateArr[i]);

            GestureTemplates.Templates.Add(temp);
            GestureTemplates.TemplateNames.Add(GestureRecognizer.stringToEdit);
            GestureRecognizer.recordDone = 0;
            GestureRecognizer.newTemplateArr.Clear();

            _textMesh.text = "TEMPLATE: " + GestureRecognizer.stringToEdit + "\n STATUS: SAVED";
	    }

	    if (GUI.Button (new Rect (160,50,50,20), "Cancel")) 
        {
            GestureRecognizer.recordDone = 0;
            _textMesh.text = "";
	    }
    }
}
