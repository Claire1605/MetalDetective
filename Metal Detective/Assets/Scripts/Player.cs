using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 1.0f;
    public float yFloat = 2.0f;
	public Transform rotatePoint;
	public Transform body;
	public Transform aerial;

    private float x = 0.0f;
    private float y = 0.0f;
    private float z = 0.0f;
    private Vector3 movement = Vector3.zero;

    private RaycastHit r;

    void Start () {
		
	}

	void Update () {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (Physics.Raycast(transform.position, Vector3.down, out r, 100, 1 << LayerMask.NameToLayer("Terrain"), QueryTriggerInteraction.Ignore))
        {
            float terrainHeight = r.point.y;
            y = terrainHeight + yFloat;
        }

        movement = new Vector3(x, 0.0f, z);

        transform.Translate(movement * speed * Time.deltaTime);

		if (movement.magnitude > 0.0f)
		{
			rotatePoint.transform.rotation = Quaternion.LookRotation(movement.normalized, Vector3.up);

			body.transform.localEulerAngles = Vector3.Lerp(body.transform.localEulerAngles, new Vector3(movement.magnitude * 20.0f, 0, 0.0f), Time.deltaTime * 2.0f);
			aerial.transform.localEulerAngles = Vector3.Lerp(aerial.transform.localEulerAngles, new Vector3(-movement.magnitude * 10.0f, 0, 0.0f), Time.deltaTime * 2.0f);
		}
		else
		{
			body.transform.localEulerAngles = Vector3.Lerp(body.transform.localEulerAngles, Vector3.zero, Time.deltaTime * 2.0f);
			aerial.transform.localEulerAngles = Vector3.Lerp(aerial.transform.localEulerAngles, Vector3.zero, Time.deltaTime * 2.0f);
		}

		Vector3 yAdjust = new Vector3(transform.position.x, y, transform.position.z);
        transform.position =  Vector3.Lerp(transform.position, yAdjust, Time.deltaTime * 10.0f);
	}
}
