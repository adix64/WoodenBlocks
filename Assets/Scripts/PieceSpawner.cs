using UnityEngine;
using AppEvent;

public class PieceSpawner : MonoBehaviour
{
    public GameObject cubePiecePrefab;
    public GameObject cylinderPiecePrefab;
    public Vector3 spawnPosition;
    public Transform allObjectsContainer;
    float maxOffset = 5f;
    public void AddCube()
    {
        InstantiatePiece(cubePiecePrefab);
    }
    public void AddCylinder()
    {
        InstantiatePiece(cylinderPiecePrefab);
    }
    private void InstantiatePiece(GameObject prefab)
    {
        Vector3 spawnPositionOffset = new Vector3(Random.Range(-maxOffset, maxOffset),
                                                   0f,
                                                  Random.Range(-maxOffset, maxOffset));
        Quaternion randomRot = Quaternion.Euler(Random.Range(0, 360f),
                                                Random.Range(0, 360f),
                                                Random.Range(0, 360f));
        Instantiate(prefab,
                    spawnPosition + spawnPositionOffset,
                    randomRot,
                    allObjectsContainer);
        EventSystem<GameEvent>.TriggerEvent(GameEvent.newPieceAdded);
    }
}
