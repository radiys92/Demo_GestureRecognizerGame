using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GCon;

public class GestureRecognizer
{
    // recognizer settings
    static int sizeOfScaleRect = 500;			// the size of the bounding box
    static int compareDetail = 15;				// Number of matching iterations (CPU consuming) 
    static int angleRange = 45;					// Angle detail level of when matching with templates 
    
    public static float Compare(Gesture a, Gesture b, int maxPoints)
    {
        var p1 = a.Frames.Select(i => i.position).ToList();
        var p2 = b.Frames.Select(i => i.position).ToList();

        p1 = NormalizePoints(p1, maxPoints);
        p2 = NormalizePoints(p2, maxPoints);

        return GestureMatch(p1,p2); 
    }

    private static List<Vector2> NormalizePoints(List<Vector2> points, int maxPoints)
    {
        points = OptimizeGesture(points, maxPoints);
        var center = CalcCenterOfGesture(points);
        var v = points[0];
        var radians = Mathf.Atan2(center.y - v.y, center.x - v.x);
        points = RotateGesture(points, -radians, center);
        points = ScaleGesture(points, sizeOfScaleRect);
        points = TranslateGestureToOrigin(points);
        return points;
    }

    private static List<Vector2> OptimizeGesture (List<Vector2> pointArray, int maxPoints)
    {
	    // take all the points in the gesture and finds the correct points compared with distance and the maximun value of points
    	
	    // calc the interval relative the length of the gesture drawn by the user
	    float interval = CalcTotalGestureLength(pointArray) / (maxPoints - 1);
    	
	    // use the same starting point in the new array from the old one. 
        var optimizedPoints = new List<Vector2> {pointArray.ElementAt(0)};

        float tempDistance = 0.0f;
    	
	    // run through the gesture array. Start at i = 1 because we compare point two with point one)
	    for (int i = 1; i < pointArray.Count(); ++i)
        {

            var v1 = pointArray[i - 1];
            var v = pointArray[i];
            float currentDistanceBetween2Points = Vector2.Distance(v1,v);
    		
		    if ((tempDistance + currentDistanceBetween2Points) >= interval)
            {

			    // the calc is: old pixel + the differens of old and new pixel multiply  
			    var newX = v1.x + ((interval - tempDistance) / currentDistanceBetween2Points) * (v.x - v1.x);
			    var newY = v1.y + ((interval - tempDistance) / currentDistanceBetween2Points) * (v.y - v1.y);
    			
			    // create new point
			    var newPoint = new Vector2(newX, newY);
    			
			    // set new point into array
			    optimizedPoints.Add(newPoint);

                var temp = pointArray.GetRange(i, pointArray.Count - i - 1);
                var last = pointArray[pointArray.Count - 1];
                for (var j = 0; j < temp.Count; j++)
                {
                    pointArray[i + 1 + j] = temp[j];
                }
                pointArray.Add(last);
                //pointArray.InsertRange(i + 1, temp);
			    pointArray.Insert(i, newPoint);
    			
			    tempDistance = 0.0f;
		    }
            else
            {
			    // the point was too close to the last point compared with the interval,. Therefore the distance will be stored for the next point to be compared.
			    tempDistance += currentDistanceBetween2Points;
		    }
	    }
    	
	    // Rounding-errors might happens. Just to check if all the points are in the new array
	    if (optimizedPoints.Count == maxPoints - 1) 
        {
            var v = pointArray[pointArray.Count - 1];
		    optimizedPoints.Add(new Vector2(v.x, v.y));
	    }

	    return optimizedPoints;
    }


    private static List<Vector2> RotateGesture(List<Vector2> pointArray, float radians, Vector3 center)  
    {
	    // loop through original array, rotate each point and return the new array
	    var cos = Mathf.Cos(radians);
	    var sin = Mathf.Sin(radians);

        return pointArray.Select(v =>
        {
            var newX = (v.x - center.x)*cos - (v.y - center.y)*sin + center.x;
            var newY = (v.x - center.x)*sin + (v.y - center.y)*cos + center.y;
            return new Vector2(newX, newY);
        }).ToList();
    }

    private static List<Vector2> ScaleGesture(List<Vector2> pointArray, int size)
    {
	    // equal min and max to the opposite infinity, such that every gesture size can fit the bounding box.
	    var minX = pointArray.Min(i=>i.x);
	    var maxX = pointArray.Max(i=>i.x); 
	    var minY = pointArray.Min(i=>i.y);
	    var maxY = pointArray.Max(i=>i.y);

        var w = maxX - minX;
        var h = maxY - minY;

        return pointArray.Select(i => new Vector2(i.x*(size/w), i.y*(size/h))).ToList();
    }


    private static List<Vector2> TranslateGestureToOrigin(List<Vector2> pointArray) 
    {
        var origin = new Vector2(0,0);
	    var center = CalcCenterOfGesture(pointArray);
	    return pointArray.Select(v=>new Vector2(v.x + origin.x - center.x,v.y + origin.y - center.y)).ToList();
    }


    // --------------------------------  		     GESTURE OPTIMIZING DONE   		----------------------------------------------------------------
    // -------------------------------- 		START OF THE MATCHING PROCESS	----------------------------------------------------------------

    private static float GestureMatch(List<Vector2> pointArray, List<Vector2> p2) 
    {
	    var tempDistance = Mathf.Infinity;

        var distance = CalcDistanceAtOptimalAngle(pointArray, p2, -angleRange, angleRange);

        if (distance < tempDistance)
        {
            tempDistance = distance;
        }

	    var halfDiagonal = 0.5f * Mathf.Sqrt(Mathf.Pow(sizeOfScaleRect, 2) + Mathf.Pow(sizeOfScaleRect, 2));
	    var score = 1.0f - (tempDistance / halfDiagonal);

        return score;
    }


    // --------------------------------  		   GESTURE RECOGNIZER DONE   		----------------------------------------------------------------
    // -------------------------------- 		START OF THE HELP FUNCTIONS		----------------------------------------------------------------


    private static Vector2 CalcCenterOfGesture(List<Vector2> pointArray)
    {
	    // finds the center of the drawn gesture
    	
	    var averageX = 0.0f;
	    var averageY = 0.0f;
    	
        foreach (var v in pointArray)
        {
		    averageX += v.x;
		    averageY += v.y;
	    }
    	
	    averageX = averageX / pointArray.Count;
	    averageY = averageY / pointArray.Count;
    	
	    return new Vector2(averageX, averageY);
    }

    private static float CalcTotalGestureLength(List<Vector2> pointArray)
    { 
	    // total length of gesture path
	    var length = 0.0f;
	    for (var i = 1; i < pointArray.Count(); ++i)
	    {
	        var a = pointArray[i - 1];
	        var b = pointArray[i];
            length += Vector2.Distance(a,b);
	    }

	    return length;
    }


    private static float CalcDistanceAtOptimalAngle(List<Vector2> pointArray, List<Vector2> template, float negativeAngle, float positiveAngle) {
	    // Create two temporary distances. Compare while running through the angles. 
	    // Each time a lower distace between points and template points are foound store it in one of the temporary variables. 
    	
	    var radian1 = Mathf.PI * negativeAngle + (1.0f - Mathf.PI ) * positiveAngle;
	    var tempDistance1 = CalcDistanceAtAngle(pointArray, template, radian1);
    	
	    var radian2 = (1.0f - Mathf.PI ) * negativeAngle + Mathf.PI  * positiveAngle;
	    var tempDistance2 = CalcDistanceAtAngle(pointArray, template, radian2);
    	
	    // the higher the number compareDetail is, the better recognition this system will perform. 
	    for (var i = 0; i < compareDetail; ++i)
        {
		    if (tempDistance1 < tempDistance2)
            {
			    positiveAngle = radian2;
			    radian2 = radian1;
			    tempDistance2 = tempDistance1;
			    radian1 = Mathf.PI * negativeAngle + (1.0f - Mathf.PI) * positiveAngle;
			    tempDistance1 = CalcDistanceAtAngle(pointArray, template, radian1);
		    } 
            else 
            {
			    negativeAngle = radian1;
			    radian1 = radian2;
			    tempDistance1 = tempDistance2;
			    radian2 = (1.0f - Mathf.PI) * negativeAngle + Mathf.PI * positiveAngle;
			    tempDistance2 = CalcDistanceAtAngle(pointArray, template, radian2);
		    }
	    }

	    return Mathf.Min(tempDistance1, tempDistance2);
    }

    private static float CalcDistanceAtAngle(List<Vector2> pointArray, List<Vector2> template, float radians) 
    {
	    // calc the distance of template and user gesture at 
	    var center = CalcCenterOfGesture(pointArray);
	    var newpoints = RotateGesture(pointArray, radians, center);

	    return CalcGestureTemplateDistance(newpoints, template);
    }

    private static float CalcGestureTemplateDistance(List<Vector2> newRotatedPoints, List<Vector2> templatePoints) 
    {
	    // calc the distance between gesture path from user and the template gesture
	    var distance = newRotatedPoints.Select((t, i) => Vector2.Distance(t, templatePoints[i])).Sum();

        return distance / newRotatedPoints.Count;
    }
}