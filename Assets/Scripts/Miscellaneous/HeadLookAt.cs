using UnityEngine;
using System.Collections;

public class HeadLookAt : MonoBehaviour
{
	public float rotationSpeed = 10;
	public Transform target;

	private void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			target = other.transform;
		}
	}

	void LookAtTarget ()
	{
		if (target) {
			Vector3 relativePos = target.position - transform.position;
			Quaternion rotation = Quaternion.LookRotation (relativePos);
			rotation.y = rotation.y * rotationSpeed;
			transform.rotation = rotation;
		}
	}

	void LateUpdate(){
		LookAtTarget ();
	}
}
