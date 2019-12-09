using UnityEngine;

public class CameraOrbit : MonoBehaviour {

	[Header("Camera Transforms")]
	[SerializeField]
	private Transform cameraTransform; // Transform of camera
	[SerializeField]
	private Transform pivotTransform; // Parent Pivot Object
    [SerializeField]
    private Transform lookAtPivotTransform;

	[Header("Read Only Variables")]
	[SerializeField]
	private Vector3 localRotation;
	[SerializeField]
	private float cameraDistance = 5f; // Zoom distance

	[Header("Camera Movement Properties")]
	public float cameraSensitivity = 5f;
	public float orbitDampening = 10f;
	public float yMax = 90f;
	public float yMin = 0f;
	public bool InvertYAxis = false;

	[Header("Camera Zoom Properties")]
	public float zoomSensitivity = 2f;
	public float zoomDampening = 6f;
	public float minZoom = 3.0f;
	public float maxZoom = 100f;

	public bool CameraDisabled = false;

	void Start () {
		cameraTransform = gameObject.GetComponent<Transform> ();
		pivotTransform = gameObject.transform.parent.GetComponent<Transform> ();
	}
	
    public void DisableCamera()
    {
        CameraDisabled = true;
    }

    public void EnableCamera()
    {
        CameraDisabled = false;
    }

	void LateUpdate() {
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
		
		}

		if (!CameraDisabled) {
			//rotation of camera based on input.
			if (Input.GetAxis ("CameraHorizontal") != 0 || Input.GetAxis ("CameraVertical") != 0) {
				localRotation.x += Input.GetAxis ("CameraHorizontal") * cameraSensitivity;
				if (InvertYAxis) {
					localRotation.y += Input.GetAxis ("CameraVertical") * cameraSensitivity;
				} else {
					localRotation.y -= Input.GetAxis ("CameraVertical") * cameraSensitivity; 
				}


				//Clamp the y to horizon and not flip over the top.
				localRotation.y = Mathf.Clamp(localRotation.y, yMin, yMax);
			}

			//Zooming, control from mouse scroll wheel. 
			if (Input.GetAxis ("ZoomControl") != 0f) {
				float ScrollAmount = Input.GetAxis ("Mouse ScrollWheel") * zoomSensitivity;

				//makes camera zoom faster further away, slower while it's closer.
				ScrollAmount *= cameraDistance * 0.3f;
				cameraDistance += ScrollAmount * -1f;// this is modification because of how unity handles scroll wheel axis
				cameraDistance = Mathf.Clamp(cameraDistance, minZoom, maxZoom); 
			}
		}

		//Actual Camera Transformations
		Quaternion quaternion = Quaternion.Euler(localRotation.y, localRotation.x, 0);
        Quaternion lookAtquaternion = Quaternion.Euler(0, localRotation.x, 0);
		pivotTransform.rotation = Quaternion.Lerp (pivotTransform.rotation, quaternion, orbitDampening * Time.deltaTime);
        lookAtPivotTransform.rotation = lookAtquaternion;

		//optimization, so that we can skip it if the zoom level remains the same
		if (cameraTransform.localPosition.z != cameraDistance * -1f) {
			cameraTransform.localPosition = new Vector3 (0f, 0f, Mathf.Lerp (cameraTransform.localPosition.z, cameraDistance * -1f, Time.deltaTime * zoomDampening));
		}


	}


}
