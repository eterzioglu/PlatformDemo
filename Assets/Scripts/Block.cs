using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Vector3 pos1;
    private Vector3 pos2;
    public float speed = 1.0f;
    public bool move = false;

    private void Start()
    {
        pos1 = new Vector3(-5, transform.position.y, transform.position.z);
        pos2 = new Vector3(5, transform.position.y, transform.position.z);

	}

    void Update()
    {
        if(move)
        {
            transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
        }
    }

	public GameObject BlockCut(Transform victim, Vector3 _pos, Transform parent)
	{
		GameObject returnBlock = gameObject;
		GameObject fallingBlock = null;

		Vector3 pos = new Vector3(_pos.x, victim.position.y, victim.position.z);
		Vector3 victimScale = victim.localScale;
		float distance = victim.transform.position.x - _pos.x;

		Vector3 leftPoint = victim.position - Vector3.right * victimScale.x / 2;
		Vector3 rightPoint = victim.position + Vector3.right * victimScale.x / 2;
		Material mat = victim.GetComponent<MeshRenderer>().material;
		Destroy(victim.gameObject);

		GameObject rightSideObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		rightSideObj.transform.position = (rightPoint + pos) / 2;
		float rightWidth = Vector3.Distance(pos, rightPoint);
		rightSideObj.transform.localScale = new Vector3(rightWidth, victimScale.y, victimScale.z);
		rightSideObj.GetComponent<MeshRenderer>().material = mat;
		rightSideObj.AddComponent<Block>();

		GameObject leftSideObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		leftSideObj.transform.position = (leftPoint + pos) / 2;
		float leftWidth = Vector3.Distance(pos, leftPoint);
		leftSideObj.transform.localScale = new Vector3(leftWidth, victimScale.y, victimScale.z);
		leftSideObj.GetComponent<MeshRenderer>().material = mat;
		leftSideObj.AddComponent<Block>();

		if (distance >= 0)
        {
			returnBlock = rightSideObj;
			fallingBlock = leftSideObj;
        }
		else
        {
			returnBlock = leftSideObj;
			fallingBlock = rightSideObj;
        }


		returnBlock.transform.parent = parent;
		fallingBlock.AddComponent<Rigidbody>().mass = 100f;

		return returnBlock;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "destroyer")
        {
			Destroy(gameObject);
        }
    }
}
