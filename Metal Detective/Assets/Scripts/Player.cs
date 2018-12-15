using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 1.0f;
    public float yFloat = 2.0f;

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

        Vector3 yAdjust = new Vector3(transform.position.x, y, transform.position.z);
        transform.position =  Vector3.Lerp(transform.position, yAdjust, Time.deltaTime * 10.0f);
	}
}
