using System;
using UnityEngine;

public class DragTouchInput
{
    public event Action<Vector3> Drag;
    public event Action EndDrag;

    public bool IsDragging { get; private set; }

    private readonly Camera _camera;
    private readonly Transform _target;

    public DragTouchInput(Transform target)
    {
        _camera = Camera.main;
        _target = target;
    }

    public void Update()
    {
        if (IsDragging)
        {
            OnDrag();
        }
        else if (IsBeginDrag())
        {
            OnBeginDrag();
        }
    }

    private void OnDrag()
    {
        if (IsEndDrag())
            OnEndDrag();

        var position = GetTouchWorldPosition();
        Drag?.Invoke(position);
    }

    private void OnBeginDrag()
    {
        IsDragging = true;
    }

    private void OnEndDrag()
    {
        IsDragging = false;
        EndDrag?.Invoke();
    }

    private bool HasTouch()
    {
        return Input.GetMouseButton(0);
    }

    private bool IsBeginDrag()
    {
        if (HasTouch() == false)
            return false;

        return RaycastTouch();
    }

    private bool IsEndDrag()
    {
        return Input.GetMouseButtonUp(0);
    }

    private Vector3 GetTouchPosition()
    {
        return Input.mousePosition;
    }

    private Vector3 GetTouchWorldPosition()
    {
        var touchPosition = GetTouchPosition();
        var touchInTargetSpace = new Vector3(touchPosition.x, touchPosition.y, _target.position.z);
        var worldPosition = -_camera.ScreenToWorldPoint(touchInTargetSpace);

        return worldPosition * 2;
    }

    private bool RaycastTouch()
    {
        var touchPosition = GetTouchPosition();
        var ray = _camera.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out var hit) == false)
            return false;

        if (hit.transform != _target)
            return false;

        Drag?.Invoke(hit.point);
        return true;
    }
}