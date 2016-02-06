using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DroneController : MonoBehaviour {


	[SerializeField] Transform[] m_propellers;
	[SerializeField] Slider[] m_throttleSliders;
	[SerializeField] Text[] m_throttleTexts;
	[SerializeField] Propeller[] m_propellerBlades;

	float[] m_throttles = {0f,0f,0f,0f};

	const float c_maxThrottle = 100f;
	const float c_throttleIncreaseRate = 100f;
	const float c_torqueScalar = 500f;

	Rigidbody m_body;

	void Start () {
		m_body = GetComponent<Rigidbody> ();
		foreach (Slider x in m_throttleSliders) {
			x.maxValue = c_maxThrottle;
		}
	}

	void Update () {
	#region Input
		if (Input.GetKey(KeyCode.A)) {
			AllThrottleUp();
		}
		if (Input.GetKey(KeyCode.Z)) {
			AllThrottleDown();
		}
		if (Input.GetKey(KeyCode.I)) {
			TiltForward();
		}
		if (Input.GetKey(KeyCode.K)) {
			TiltBack();
		}
		if (Input.GetKey(KeyCode.J)) {
			TiltLeft();
		}
		if (Input.GetKey(KeyCode.L)) {
			TiltRight();
		}
		if (Input.GetKey(KeyCode.S)) {
			YawRight();
		}
		if (Input.GetKey(KeyCode.D)) {
			YawLeft();
		}
	#endregion

	#region GUIDisplayAndPropellerAnimation
	for (int i = 0; i < m_propellers.Length; i++) {

			m_throttleSliders[i].value = m_throttles[i];
			m_throttleTexts[i].text = m_throttles[i].ToString("F2");
			m_propellerBlades[i].m_propellerThrottle = m_throttles[i];

	}

	#endregion
	}

	#region Physics

	void FixedUpdate () {

		for (int i = 0; i < m_propellers.Length; i++) {
			m_body.AddForceAtPosition (transform.up * m_throttles [i], m_propellers [i].position);
		}


	}
	#endregion


	/* Drone
	 *[0---1] 
	 *  [ ]
	 *[2---3]
	 */

	void AllThrottleUp () {
		for (int i = 0; i < m_propellers.Length; i++) {
			m_throttles[i] = ShiftThrottle (m_throttles[i], 1f);
		}
	}

	void AllThrottleDown() {
		for (int i = 0; i < m_propellers.Length; i++) {
			m_throttles[i] = ShiftThrottle (m_throttles[i], -1f);
		}
	}

	void TiltForward () {
		//propellers 0 and 1
		m_throttles[0] = ShiftThrottle (m_throttles[0], 1f);
		m_throttles[1] = ShiftThrottle (m_throttles[1], 1f);
		m_throttles[2] = ShiftThrottle (m_throttles[2], -1f);
		m_throttles[3] = ShiftThrottle (m_throttles[3], -1f);

	}

	void TiltBack () {
		//propellers 2 and 3
		m_throttles[0] = ShiftThrottle (m_throttles[0], -1f);
		m_throttles[1] = ShiftThrottle (m_throttles[1], -1f);
		m_throttles[2] = ShiftThrottle (m_throttles[2], 1f);
		m_throttles[3] = ShiftThrottle (m_throttles[3], 1f);
	
	}

	void TiltRight () {
		//propellers 1 and 3
		m_throttles[0] = ShiftThrottle (m_throttles[0], 1f);
		m_throttles[1] = ShiftThrottle (m_throttles[1], -1f);
		m_throttles[2] = ShiftThrottle (m_throttles[2], 1f);
		m_throttles[3] = ShiftThrottle (m_throttles[3], -1f);
	}

	void TiltLeft () {
		//propellers 0 and 2
		m_throttles[0] = ShiftThrottle (m_throttles[0], -1f);
		m_throttles[1] = ShiftThrottle (m_throttles[1], 1f);
		m_throttles[2] = ShiftThrottle (m_throttles[2], -1f);
		m_throttles[3] = ShiftThrottle (m_throttles[3], 1f);

	}

	void YawRight () {
		m_body.AddTorque (Vector3.up * -c_torqueScalar);
	}

	void YawLeft () {
		m_body.AddTorque (Vector3.up * c_torqueScalar);

	}


	float ShiftThrottle (float referenceThrottle, float direction) {
		float returnVal = (Time.deltaTime * direction * c_throttleIncreaseRate + referenceThrottle);
		if (returnVal > c_maxThrottle) {
			returnVal = c_maxThrottle;
		} else if (returnVal < 0) {
			returnVal = 0;
		}
		return returnVal;
	}
}
