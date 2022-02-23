
using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
	float yaw, pitch;
	public float xSensitivity = 5f;
	public float ySensitivity = 3f;
	public float minPitch = 5f;
	public float maxPitch = 60f;
	public float minZDist = -10f;
	public float maxZDist = -3f;
	public float zoomSpeed = 0.5f;

	public Vector3 cameraOffset;
	public Transform target;

	private void Start()
	{
		yaw = transform.rotation.eulerAngles.y;
		pitch = transform.rotation.eulerAngles.x;
	}
	private void Update()
	{
		if (Input.GetMouseButton(1))
		{ 
			pitch -= Input.GetAxis("Mouse Y") * ySensitivity;
			yaw += Input.GetAxis("Mouse X") * xSensitivity;
		}

		pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

		cameraOffset.z += Input.mouseScrollDelta.x * zoomSpeed;
		
		cameraOffset.z = Mathf.Clamp(cameraOffset.z, minZDist, maxZDist);

		transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
		transform.position = target.position + transform.TransformDirection(cameraOffset);
	
			
	
	}


}