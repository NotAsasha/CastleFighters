using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Build.Scripts
{
    public class BarCreator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public GameObject RoadBar;
        public GameObject WoodenBar;
        public GameObject GlassBar;
        public GameObject DoorBar;
        public bool BarCreationStarted = false;
        public Bar CurrentBar;
        public GameObject BarToInstantiate;
        public Transform BarParent;
        public Point CurrentStartPoint;
        public Point CurrerEndPoint;
        public GameObject PointToInstantiate;
        public Transform PointParent;



        private List<Bar> _barList = new();


        public void OnPointerDown(PointerEventData eventData)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                return;
            }
            if (BarCreationStarted == false)
            {
                BarCreationStarted = true;
                StartBarCreation(Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(eventData.position)));
            }
            else
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    FinishBarCreation();
                }
                else if (eventData.button == PointerEventData.InputButton.Right)
                {
                    BarCreationStarted = false;
                    DeleteCurrentBar();
                }
            }
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            if (BarCreationStarted == true) FinishBarCreation();
        }



        void StartBarCreation(Vector2 StartPosition)
        {

            CurrentBar = Instantiate(BarToInstantiate, BarParent).GetComponent<Bar>();
            CurrentBar.StartPosition = StartPosition;
            CurrentBar.ID = BarParent.childCount;

            if (GameManager.AllPoints.TryGetValue(StartPosition, out var point))
            {
                CurrentStartPoint = point;
            }
            else
            {
                CurrentStartPoint = Instantiate(PointToInstantiate, StartPosition, Quaternion.identity, PointParent)
                    .GetComponent<Point>();
                GameManager.AllPoints.Add(StartPosition, CurrentStartPoint);
            }

            CurrerEndPoint = Instantiate(PointToInstantiate, StartPosition, Quaternion.identity, PointParent)
                .GetComponent<Point>();
        }




        void FinishBarCreation()
        {
            if (GameManager.AllPoints.TryGetValue(CurrerEndPoint.transform.position, out var point))
            {
                Destroy(CurrerEndPoint.gameObject);
                CurrerEndPoint = point;
            }
            else
            {
                GameManager.AllPoints.Add(CurrerEndPoint.transform.position, CurrerEndPoint);
            }

            CurrentStartPoint.ConnectedBars.Add(CurrentBar);
            CurrerEndPoint.ConnectedBars.Add(CurrentBar);

            CurrentBar.StartJoint.connectedBody = CurrentStartPoint.rbd;
            CurrentBar.StartJoint.anchor = CurrentBar.transform.InverseTransformPoint(CurrentBar.StartPosition);
            CurrentBar.EndJoint.connectedBody = CurrerEndPoint.rbd;
            CurrentBar.EndJoint.anchor = CurrentBar.transform.InverseTransformPoint(CurrerEndPoint.transform.position);
            _barList.Add(CurrentBar);


            if (CurrentBar.SaveBar()._barScale.x < 0.1f)
            {
                Debug.LogWarning("Too short Bar");
                _barList.Remove(CurrentBar);
                DeleteBar(CurrentBar, false);
            }


            try { MoneyCounter.Money(CurrentBar.SaveBar()); }
            catch (Exception ex)
            {
                Debug.LogWarning(ex.Message);
                _barList.Remove(CurrentBar);
                DeleteBar(CurrentBar, false);
            }
            BarCreationStarted = false;
        }


        public void DeleteCurrentBar()
        {
            Debug.Log("Called Current Destroy");
            Destroy(CurrentBar.gameObject);

            if (CurrentStartPoint.ConnectedBars.Count == 0 && CurrentStartPoint.Runtime == true)
            {
                GameManager.AllPoints.Remove(CurrentStartPoint.transform.position);
                Destroy(CurrentStartPoint.gameObject);
            }
            if (CurrerEndPoint.ConnectedBars.Count == 0 && CurrerEndPoint.Runtime == true)
                Destroy(CurrerEndPoint.gameObject);
        }


        public void DeleteBar(Bar bar, bool doReduseMoney)
        {
            Debug.Log("Called Bar Destruct");

            Point StartPoint = bar.StartJoint.connectedBody.GetComponent<Point>();
            Point EndPoint = bar.EndJoint.connectedBody.GetComponent<Point>();

            StartPoint.ConnectedBars.Remove(bar);
            EndPoint.ConnectedBars.Remove(bar);
            if (StartPoint.ConnectedBars.Count == 0 && StartPoint.Runtime == true)
            {
                GameManager.AllPoints.Remove(StartPoint.transform.position);
                Destroy(StartPoint.gameObject);
            }
            if (EndPoint.ConnectedBars.Count == 0 && EndPoint.Runtime == true)
            {
                Destroy(EndPoint.gameObject);
                GameManager.AllPoints.Remove(EndPoint.transform.position);
            }
            Destroy(bar.gameObject);

            if (doReduseMoney) MoneyCounter.ReverseMoney();
        }

        public void DeletePrevious()
        {
            if (_barList.Count < 1) return;

            if (BarCreationStarted)
            {
                BarCreationStarted = false;
                DeleteCurrentBar();
            }
            Bar bar = _barList.Last();
            Debug.Log("Called Manual Destroy");
            _barList.Remove(bar);
            DeleteBar(bar, true);
        }

        private void Update()
        {
            BarParent = GameObject.FindGameObjectWithTag("Castle").GetComponent<Castle.Castle>().BarParent.transform;
            PointParent = GameObject.FindGameObjectWithTag("Castle").GetComponent<Castle.Castle>().PointParent.transform;
            if (BarCreationStarted == true)
            {
                Vector2 EndPosition =
                    (Vector2)Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                Vector2 Dir = EndPosition - CurrentBar.StartPosition;
                Vector2 ClampedPosition = CurrentBar.StartPosition + Vector2.ClampMagnitude(Dir, CurrentBar.MaxLength);


                CurrerEndPoint.transform.position = (Vector2)Vector2Int.FloorToInt(ClampedPosition);
                CurrerEndPoint.PointID = CurrerEndPoint.transform.position;
                CurrentBar.UpdateCreatingBar(CurrerEndPoint.transform.position);
            }
        }
    }
}
