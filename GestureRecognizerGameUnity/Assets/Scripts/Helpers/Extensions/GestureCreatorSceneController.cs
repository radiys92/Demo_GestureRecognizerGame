using System.Collections.Generic;
using System.Linq;
using GCon;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Helpers.Extensions
{
    public class GestureCreatorSceneController : MonoBehaviour
    {
        private const string TemplatesAssetPath = "GestureTemplates";

        public Transform ListItem0;
        public Button AddButton;
        public Button DeleteButton;
        public Button RewriteButton;

        private GestureTemplates _templates;
        private Transform _listRoot;
        private int _selectedId = -1;
        private LineRenderer _lr;
        private Vector2[] _lastGesture;
        private int _gestureDrawId;

        private void Awake()
        {
            _listRoot = ListItem0.parent;
            ListItem0.gameObject.SetActive(false);
            ListItem0.GetComponent<Button>().onClick.AddListener(CreateClickHandler(ListItem0));

            _templates = Resources.Load<GestureTemplates>(TemplatesAssetPath);
            _lr = CreateLine();

            UpdateListView();

            GestureController.OnGestureStart += OnGestureStart;
            GestureController.OnGestureEnd += OnGestureEnd;

            AddButton.interactable = false;
            DeleteButton.interactable = false;
            RewriteButton.interactable = false;

            AddButton.onClick.AddListener(() =>
            {
                if (_lastGesture == null)
                    return;


                var l = _templates.Templates.ToList();
                l.Add(new GestureTemplate() {points = _lastGesture});
                _templates.Templates = l.ToArray();
                UpdateListView();
            });

            DeleteButton.onClick.AddListener(() =>
            {
                if (_selectedId < 0 || _selectedId > _templates.Templates.Length - 1)
                    return;

                var l = _templates.Templates.ToList();
                l.RemoveAt(_selectedId);
                _templates.Templates = l.ToArray();
                UpdateListView();
            });

            RewriteButton.onClick.AddListener(() =>
            {
                if (_selectedId < 0 || _selectedId > _templates.Templates.Length - 1)
                    return;
                if (_lastGesture == null)
                    return;

                _templates.Templates[_selectedId].points = _lastGesture;
                UpdateListView();
            
            });
        }

        private void UpdateListView()
        {
            UpdateItemsCount(_templates.Templates.Length);
            if (_lr != null)
                _lr.SetVertexCount(0);
        }

        private void OnGestureStart(Gesture gesture)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform) GameObject.Find("GesturesList").transform, gesture.StartPoint))
                return;

            _gestureDrawId = gesture.ID;
            _lr.SetVertexCount(0);
            AddButton.interactable = true;
            gesture.OnGestureStay += OnGestureStay;
        }

        private void OnGestureEnd(Gesture gesture)
        {
            if (gesture.ID == _gestureDrawId)
            {
                _lastGesture = gesture.Frames.Select(i => i.position).ToArray();
                _gestureDrawId = -1;
            }
        }

        private void OnGestureStay(Gesture gesture)
        {
            DrawPoints(gesture.Frames.Select(i => i.position).ToArray());
        }

        private LineRenderer CreateLine()
        {
            var go = new GameObject("~Line");
            var lr = go.AddComponent<LineRenderer>();
            var mat = new Material(new Material(Shader.Find("MonoColor/OpaqueNLitColor")))
            {
                color = Color.green
            };
            lr.material = mat;
            lr.SetWidth(.1f, .1f);
            lr.SetVertexCount(0);
            return lr;
        }

        private void UpdateItemsCount(int count)
        {
            var needAdd = count - _listRoot.childCount;
            for (var i = 0; i < needAdd; i++)
            {
                var item = Instantiate(ListItem0);
                item.gameObject.SetActive(false);
                item.GetComponent<Button>().onClick.AddListener(CreateClickHandler(item));
                item.SetParent(_listRoot,false);
            }
            for (var i = 0; i < Mathf.Max(count,_listRoot.childCount); i++)
            {
                var item = _listRoot.GetChild(i);
                item.gameObject.name = i + "";
                item.GetComponentInChildren<Text>().text = (i+1) + "";
                item.gameObject.SetActive(i < count);
            }
        }

        private UnityAction CreateClickHandler(Transform item)
        {
            return () => { OnItemClicked(item); };
        }

        private void OnItemClicked(Transform item)
        {
            if (int.TryParse(item.gameObject.name, out _selectedId))
            {
                if (_selectedId < 0 || _selectedId > _templates.Templates.Length - 1)
                    return;

                DeleteButton.interactable = true;
                RewriteButton.interactable = true;

                var points = _templates.Templates[_selectedId].points;
                DrawPoints(points);

                SetSelected(_selectedId);
            }
        }

        private void SetSelected(int selectedId)
        { 
            for (int i = 0; i < _listRoot.childCount; i++)
            {
                _listRoot.GetChild(i).GetComponent<Image>().color = i == selectedId
                    ? Color.green
                    : Color.white;
            }
        }

        private void DrawPoints(IList<Vector2> points)
        {
            _lr.SetVertexCount(points.Count);
            for (var i = 0; i < points.Count; i++)
            {
                var pos = Camera.main.ScreenToWorldPoint(points[i]);
                pos.z = 0;
                _lr.SetPosition(i, pos);
            }
        }
    }
}