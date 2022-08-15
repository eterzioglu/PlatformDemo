using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public List<Material> colors = new List<Material>();
    public GameObject refBlock;
    GameObject block;
    Vector3 cutPos;
    float pitch = 0.5f;
    int i = 0;

    private void Start()
    {
        refBlock = transform.GetChild(0).gameObject;
        SpawnBlock();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && UIManager.instance.gameStart)
        {
            block.GetComponent<Block>().move = false;

            
            float distance = block.transform.position.x - refBlock.transform.position.x;

            if(distance >= 0.1)
            {
                CutCube(1);
            }
            else if(distance <= -0.1)
            {
                CutCube(-1);
            }
            else
            {
                PerfectTiming();
            }

            SpawnBlock();
        }
    }

    public void SpawnBlock()
    {
        if(i < 8)
        {
            float zPos = refBlock.transform.position.z + refBlock.transform.localScale.z;

            Vector3 spawnPos = new Vector3(5, refBlock.transform.position.y, zPos);

            block = GameObject.CreatePrimitive(PrimitiveType.Cube);
            block.transform.localScale = refBlock.transform.localScale;
            block.transform.position = spawnPos;
            block.transform.parent = transform;
            block.gameObject.name = "Block";
            block.GetComponent<Renderer>().material = colors[i];
            block.AddComponent<Block>();

            i++;
        }
    }

    private void CutCube(int factor)
    {
        cutPos = new Vector3(refBlock.transform.position.x + refBlock.transform.localScale.x / 2 * factor, refBlock.transform.position.y, refBlock.transform.position.z);
        refBlock = block.GetComponent<Block>().BlockCut(block.transform, cutPos, transform);
        pitch = 0.5f;
    }

    private void PerfectTiming()
    {
        block.transform.position = new Vector3(refBlock.transform.position.x, block.transform.position.y, block.transform.position.z);
        refBlock = block;
        AudioManager.instance.Success(pitch);
        pitch += 0.5f;
    }
}
