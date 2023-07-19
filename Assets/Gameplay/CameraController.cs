using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CULU;

namespace Gameplay {

	public class CameraController:MonoBehaviour {

		[SerializeField] float keyboardMoveSpeed = 5;
		[SerializeField] float keyboardRotationSpeed = 5;
		[SerializeField] float mouseRotationSensitivity = 100;

		[SerializeField] AnimationCurve minPitch;
		[SerializeField] AnimationCurve maxPitch;

		[SerializeField] Transform pitchHinge;
		[SerializeField] Transform cameraTransform;

		public Vector3 mouseOverPosition { get; private set; }
		Vector3 mouseOverPositionDelta;
		new Camera camera;
		private void Start() {
			camera=GetComponentInChildren<Camera>();
		}

		private void Update() {

			UpdateMouseOver();
			UpdateMovement();
			UpdateRotation();

		}

		void UpdateMouseOver() {
			Vector3 mouseOverPositionPrevious = mouseOverPosition;
			mouseOverPosition=Vector3.down*10;
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			int cnt = Physics.RaycastNonAlloc(ray,UtilityPhysics.rayactBuffer);
			for(int i = 0;i<cnt;i++) {
				RaycastHit hit = UtilityPhysics.rayactBuffer[i];
				if(hit.collider.GetComponent<Placement.GridObject>()) {
					mouseOverPosition=hit.point;
					break;
				}
			}
			mouseOverPositionDelta=mouseOverPosition-mouseOverPositionPrevious;
		}

		float distanceChangePending;
		void UpdateMovement() {
			float cameraDistance = -cameraTransform.localPosition.z;

			//local
			Vector4 localMovement = new Vector4();
			if(Input.GetKey(KeyCode.W)) localMovement.z+=keyboardMoveSpeed*cameraDistance*Time.deltaTime;
			if(Input.GetKey(KeyCode.S)) localMovement.z-=keyboardMoveSpeed*cameraDistance*Time.deltaTime;
			if(Input.GetKey(KeyCode.D)) localMovement.x+=keyboardMoveSpeed*cameraDistance*Time.deltaTime;
			if(Input.GetKey(KeyCode.A)) localMovement.x-=keyboardMoveSpeed*cameraDistance*Time.deltaTime;
			transform.position+=(Vector3)(transform.localToWorldMatrix*localMovement);

			//world
			if(Input.GetMouseButton(2)) {
				transform.position-=mouseOverPositionDelta;
				UpdateMouseOver();
			}

			//distance
			//if(Input.mouseScrollDelta.y>0) distanceChangePending-=1.5f;
			//if(Input.mouseScrollDelta.y<0) distanceChangePending+=1.5f;
			//float distanceDelta = Mathf.Sign(distanceChangePending)*distanceChangePending*distanceChangePending*5*Time.deltaTime;
			//cameraDistance+=distanceDelta;
			//distanceChangePending-=distanceDelta;

			if(Input.mouseScrollDelta.y>0) cameraDistance-=Mathf.Sqrt(cameraDistance);
			if(Input.mouseScrollDelta.y<0) cameraDistance+=Mathf.Sqrt(cameraDistance);

			cameraDistance=Mathf.Clamp(cameraDistance,15,500);
			cameraTransform.localPosition=Vector3.back*cameraDistance;

		}

		Vector3 mouseOverPositionPrevious;
		void UpdateRotation() {
			float pitchAngle = pitchHinge.localRotation.eulerAngles.x;
			float yawAngle = transform.localRotation.eulerAngles.y;

			if(Input.GetMouseButton(1)) {
				Vector3 mousePositionDelta = Input.mousePosition-mouseOverPositionPrevious;
				float mouseMovementScaling = 1f/camera.pixelHeight;
				Debug.Log(Input.mousePosition.y/camera.pixelHeight);
				yawAngle+=mousePositionDelta.x*mouseRotationSensitivity*mouseMovementScaling;
				pitchAngle-=mousePositionDelta.y*mouseRotationSensitivity*mouseMovementScaling;
			}

			float cameraDistance = -cameraTransform.localPosition.z;
			pitchAngle=Mathf.Clamp(pitchAngle,minPitch.Evaluate(cameraDistance),maxPitch.Evaluate(cameraDistance));

			mouseOverPositionPrevious=Input.mousePosition;
			pitchHinge.localRotation=Quaternion.Euler(pitchAngle,0,0);
			transform.localRotation=Quaternion.Euler(0,yawAngle,0);
		}

	}

}