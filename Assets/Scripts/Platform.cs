using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public List<Material> colors = new List<Material>();
    public GameObject refBlock;
    public int maxBlockCount;
    GameObject block;
    int i = 0;

    private void Start()
    {
        SpawnBlock();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && UIManager.instance.gameStart)
        {
            block.GetComponent<Block>().move = false;
            SpawnBlock();
        }
    }

    public void SpawnBlock()
    {
        if(gameObject.transform.childCount < 9)
        {
            float zPos = refBlock.transform.position.z + refBlock.transform.localScale.z * (i + 1);

            Vector3 spawnPos = new Vector3(5, refBlock.transform.position.y, zPos);

            block = Instantiate(refBlock, spawnPos, Quaternion.identity, transform);
            block.GetComponent<Renderer>().material = colors[i];
            block.AddComponent<Block>();

            i++;
        }
    }
}
