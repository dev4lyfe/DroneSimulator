using UnityEngine;
using System.Collections;

public class Propeller : MonoBehaviour {


	public float m_propellerThrottle = 0;
	const float c_propellerMultiplier = 100f;
	[SerializeField] float counterclockwise;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (transform.position, transform.up, Time.deltaTime * m_propellerThrottle * c_propellerMultiplier * counterclockwise);
	}
}
