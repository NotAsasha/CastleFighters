using Assets.Build.Castle;
using Assets.Build.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CastleInstantiator : MonoBehaviour
{
    public static GameObject InstantiateCastle(CastleData Castle)
    {

        GameObject _castlePreFab = Resources.Load<GameObject>("Castle/Castle");
        GameObject _bar1 = Resources.Load<GameObject>("Castle/Bars/RoadBar");
        GameObject _bar2 = Resources.Load<GameObject>("Castle/Bars/GlassBar");
        GameObject _bar3 = Resources.Load<GameObject>("Castle/Bars/WoodenBar");
        GameObject _bar4 = Resources.Load<GameObject>("Castle/Bars/DoorBar");
        GameObject _point = Resources.Load<GameObject>("Castle/Point");

        MoneyCounter.ResetMoneyAmount();
        List<BarStorage> barsStorage = Castle.BarArray.ToList();
        List<PointStorage> pointsStorage = Castle.PointArray.ToList();

        GameObject currentCastle = Instantiate(_castlePreFab);
        currentCastle.name = Castle.CastleName;
        GameManager.AllPoints = new Dictionary<Vector2, Point>();

        int i = 0;
        foreach (var point in pointsStorage)
        {
            GameObject currentPoint = Instantiate(_point, currentCastle.GetComponent<Castle>().PointParent.transform);
            currentPoint.transform.position = new Vector3(point.PointId.x, point.PointId.y, currentCastle.transform.position.z);
            currentPoint.GetComponent<Point>().PointID = point.PointId;
            if (i < 2) currentPoint.GetComponent<Point>().Runtime = false;
            
            if (!GameManager.AllPoints.ContainsKey(point.PointId))
            {
                GameManager.AllPoints.Add(point.PointId, currentPoint.GetComponent<Point>());
            }
            i++;
        }

        foreach (var bar in barsStorage)
        {
            MoneyCounter.Money(bar);
            GameObject currentBar = null;
            switch (bar._barType)
            {
                case "Wood":
                    currentBar = Instantiate(_bar3, currentCastle.GetComponent<Castle>().BarParent.transform);
                    break;
                case "Glass":
                    currentBar = Instantiate(_bar2, currentCastle.GetComponent<Castle>().BarParent.transform);
                    break;
                case "Iron":
                    currentBar = Instantiate(_bar1, currentCastle.GetComponent<Castle>().BarParent.transform);
                    break;
                case "Door":
                    currentBar = Instantiate(_bar4, currentCastle.GetComponent<Castle>().BarParent.transform);
                    break;
            }
            if (currentBar == null) return currentCastle;

            currentBar.transform.SetPositionAndRotation(bar._barPosition, bar._barRotation);
            currentBar.transform.localScale = bar._barScale;
            Bar barComponent = currentBar.GetComponent<Bar>();
            if (GameManager.AllPoints.TryGetValue(bar._connectedPoint1, out var point1))
            {
                barComponent.StartJoint.connectedBody = point1.GetComponent<Rigidbody2D>();
                barComponent.StartJoint.anchor = currentBar.transform.InverseTransformPoint(point1.transform.position);
                point1.ConnectedBars.Add(barComponent);
            }
            if (GameManager.AllPoints.TryGetValue(bar._connectedPoint2, out var point2))
            {
                barComponent.EndJoint.connectedBody = point2.GetComponent<Rigidbody2D>();
                barComponent.EndJoint.anchor = currentBar.transform.InverseTransformPoint(point2.transform.position);
                point2.ConnectedBars.Add(barComponent);
            }

        }
        Debug.LogWarning("Money left: " + MoneyCounter.Money());
        return currentCastle;
    }
}