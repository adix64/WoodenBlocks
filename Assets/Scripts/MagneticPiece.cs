using System.Collections;
using UnityEngine;
using AppEvent;
[RequireComponent(typeof(Collider))]
public class MagneticPiece : MonoBehaviour
{
    public float minSnapDistance = 0.35f;
    private Camera mainCamera;
    private float cameraDepth;
    Transform[] anchorPoints;
    Transform[] otherObjects;
    Transform[] otherAnchorPoints;
	Vector3 grabOffset;
	bool canSnap = true;
    void Start()
	{
		mainCamera = Camera.main;
		InitAnchorPoints();
		InitOtherObjects();
		EventSystem<GameEvent>.Subscribe(GameEvent.newPieceAdded, InitOtherObjects);
	}
	private void OnDestroy()
	{
		EventSystem<GameEvent>.Unsubscribe(GameEvent.newPieceAdded, InitOtherObjects);
	}
	private void InitOtherObjects()
	{
		otherObjects = new Transform[transform.parent.childCount - 1];
		int otherAnchorPointsCount = 0;
		int otherObjectsCount = 0;
		for (int i = 0; i < transform.parent.childCount; i++)
		{ 
			if (transform.parent.GetChild(i) == transform)
				continue;
			otherObjects[otherObjectsCount] = transform.parent.GetChild(i);
			otherAnchorPointsCount += otherObjects[otherObjectsCount++].childCount;
		}
		otherAnchorPoints = new Transform[otherAnchorPointsCount];
		int count = 0;
		for (int i = 0; i < otherObjects.Length; i++)
			for (int j = 0; j < otherObjects[i].childCount; j++)
				otherAnchorPoints[count++] = otherObjects[i].GetChild(j);
	}
	private void InitAnchorPoints()
	{
		anchorPoints = new Transform[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
			anchorPoints[i] = transform.GetChild(i);
	}
	private void OnMouseDown()
	{
		grabOffset = transform.position - GetObjectWorldPointFromMouse();
		canSnap = true;
	}
	void OnMouseDrag()
	{
		DragMoveObject();
		Transform closestAnchor = null, otherClosestAnchor = null;
		FindClosestAnchors(ref closestAnchor, ref otherClosestAnchor);
		if (closestAnchor != null)
		{
			Quaternion fromToRot = Quaternion.FromToRotation(closestAnchor.up, -otherClosestAnchor.up);
			fromToRot.ToAngleAxis(out float angle, out Vector3 axis);
			transform.RotateAround(closestAnchor.position, axis, angle);
			transform.position += otherClosestAnchor.position - closestAnchor.position;
			if (canSnap)
			{
				grabOffset = transform.position - GetObjectWorldPointFromMouse();
				canSnap = false;
				StartCoroutine(ResetCanSnap(1f));
			}
		}
	}
	private IEnumerator ResetCanSnap(float t) 
	{ 
		yield return new WaitForSeconds(t);
		canSnap = true;
	}
	private void DragMoveObject()
	{
		Vector3 NewWorldPosition = GetObjectWorldPointFromMouse();

		transform.position = NewWorldPosition + grabOffset;
	}
	private Vector3 GetObjectWorldPointFromMouse()
	{
		cameraDepth = mainCamera.WorldToScreenPoint(transform.position).z;
		Vector3 ScreenPosition =
			new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDepth);
		Vector3 NewWorldPosition =
			mainCamera.ScreenToWorldPoint(ScreenPosition);
		return NewWorldPosition;
	}
	private void FindClosestAnchors(ref Transform closestAnchor, ref Transform otherClosestAnchor)
	{
		float minAnchorDist = float.MaxValue;
		for (int i = 0; i < otherAnchorPoints.Length; i++)
		{
			for (int k = 0; k < anchorPoints.Length; k++)
			{
				float dist = Vector3.Distance(anchorPoints[k].position, otherAnchorPoints[i].position);
				if (dist < minSnapDistance && dist < minAnchorDist)
				{
					closestAnchor = anchorPoints[k];
					otherClosestAnchor = otherAnchorPoints[i];
					minAnchorDist = dist;
				}
			}
		}
	}
}