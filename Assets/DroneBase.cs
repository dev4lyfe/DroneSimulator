using UnityEngine;
using System.Collections;

public class DroneBase : MonoBehaviour {

	[SerializeField] Transform drone;
	
	// Update is called once per frame
	void Update () {
		transform.position = drone.position;
		transform.rotation = Quaternion.Euler (0, drone.rotation.eulerAngles.y, 0);
	}
}
