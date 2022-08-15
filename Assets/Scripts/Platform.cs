using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    #region Variables
    public List<Material> colors = new List<Material>();
    public GameObject refBlock;
    private GameObject block;
    public int blockCount;
    public bool fail = false;

    float pitch = 0.5f;
    int i = 0;
    #endregion

    private void Start()
    {
        SetupPlatform();
    }

    private void SetupPlatform()
    {
        i = 0;
        refBlock = transform.GetChild(0).gameObject;
        SpawnBlock();
        pitch = 0.5f;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && UIManager.instance.gameStart && !fail)
        {
            block.GetComponent<Block>().move = false;
            
            float distance = block.transform.position.x - refBlock.transform.position.x;

            if (Mathf.Abs(distance) > refBlock.transform.localScale.x / 2)
            {
                fail = true;
                block.AddComponent<Rigidbody>().mass = 100f;
                return;
            }

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

            PlayerController.instance.ChangeXPos(refBlock.transform.position.x);

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
            block.GetComponent<Block>().move = true;

            i++;
        }
    }

    private void CutCube(int factor)
    {
        Vector3 cutPos = new Vector3(refBlock.transform.position.x + refBlock.transform.localScale.x / 2 * factor, refBlock.transform.position.y, refBlock.transform.position.z);
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

    public float CalculatePlatformSize()
    {
        float size = transform.GetChild(0).gameObject.transform.localScale.z * blockCount + transform.GetChild(1).gameObject.transform.localScale.z;

        return size;
    }
}
