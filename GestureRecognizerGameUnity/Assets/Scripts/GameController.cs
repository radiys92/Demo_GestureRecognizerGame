using System;
using UnityEngine;
using System.Collections.Generic;
using GCon;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private enum States
    {
        line1,
        line2
    }

    public Material[] LineMaterials;
    public Text statusBar;

    private States state = States.line1;
    private readonly List<LineRenderer> _lines = new List<LineRenderer>();
    private readonly List<Gesture> _gestures = new List<Gesture>(); 
    private LineRenderer _lr;
    private int _vertCount = 0;

    private void Awake()
    {
        GestureController.OnGestureStart += OnGestureStart;
        GestureController.OnGestureEnd += OnGestureEnd;
    }

    private LineRenderer CreateLine()
    {
        var go = new GameObject("~Line");
        var lr = go.AddComponent<LineRenderer>();
        lr.material = LineMaterials[_lines.Count%LineMaterials.Length];
        return lr;
    }

    private void OnDestroy()
    {
        // for security at next scene loading
        GestureController.OnGestureStart -= OnGestureStart;
        GestureController.OnGestureEnd -= OnGestureEnd;
    }

    private void OnGestureStart(Gesture g)
    {
        if (state == States.line1)
            ClearLines();

        _lr = CreateLine();
        _lines.Add(_lr);

        // What do when gesture start?
        _lr.SetVertexCount(1);
        var pos = Camera.main.ScreenToWorldPoint(g.StartPoint);
        pos.z = 0;
        _lr.SetPosition(0, pos);
        _vertCount = 1;
        g.OnGestureStay += OnGestureStay;
    }

    private void ClearLines()
    {
        foreach (var line in _lines)
        {
            Destroy(line.gameObject);
        }
        _lines.Clear();
    }

    private void OnGestureStay(Gesture g)
    {
        // What do when gesture updated?
        _lr.SetVertexCount(_vertCount + 1);
        var pos = Camera.main.ScreenToWorldPoint(g.EndPoint);
        pos.z = 0;
        _lr.SetPosition(_vertCount, pos);
        _vertCount++;
    }

    private void OnGestureEnd(Gesture g)
    {
        _gestures.Add(g);
        switch (state)
        {
            case States.line1:
                statusBar.text = "Draw line 2";
                break;
            case States.line2:
                statusBar.text = "Finished!\n";

                var score = GestureRecognizer.Compare(_gestures[0], _gestures[1], 64);
                statusBar.text += string.Format(" Comparation score = {0}, points = {2}. {1} result.\n",
                    score,
                    score < .7f
                        ? "Bad"
                        : score < 0.9f
                            ? "Good"
                            : "Best",
                    64);

                score = GestureRecognizer.Compare(_gestures[0], _gestures[1], 256);
                statusBar.text += string.Format(" Comparation score = {0}, points = {2}. {1} result.\n",
                    score,
                    score < .7f
                        ? "Bad"
                        : score < 0.9f
                            ? "Good"
                            : "Best",
                    256);

                score = GestureRecognizer.Compare(_gestures[0], _gestures[1], 1024);
                statusBar.text += string.Format(" Comparation score = {0}, points = {2}. {1} result.\n",
                    score,
                    score < .7f
                        ? "Bad"
                        : score < 0.9f
                            ? "Good"
                            : "Best",
                    1024);

                _gestures.Clear();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        state = (States) (((int) state + 1)%2);
    }
}