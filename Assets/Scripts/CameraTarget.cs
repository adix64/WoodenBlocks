using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppEvent;

public class CameraTarget : MonoBehaviour
{
    public Transform allPiecesContainer;
    public Transform[] allPieces;
    public float moveSpeed = 2f;
    void Start()
	{
		CountAllPieces();
		EventSystem<GameEvent>.Subscribe(GameEvent.newPieceAdded, CountAllPieces);
	}
	private void OnDestroy()
	{
		EventSystem<GameEvent>.Unsubscribe(GameEvent.newPieceAdded, CountAllPieces);
	}
	private void CountAllPieces()
	{
		allPieces = new Transform[allPiecesContainer.childCount];
		for (int i = 0; i < allPieces.Length; i++)
			allPieces[i] = allPiecesContainer.GetChild(i);
	}
	void Update()
    {
        Vector3 weightedSum = Vector3.zero;
        for (int i = 0; i < allPieces.Length; i++)
            weightedSum += allPieces[i].position;

        weightedSum /= (float)allPieces.Length;
        transform.position = Vector3.Lerp(transform.position, weightedSum, Time.deltaTime * moveSpeed);
    }
}
