using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    const int cubNumber = 6;
    GameObject[] cubs = new GameObject[cubNumber * cubNumber];
    bool[] destroyMode = new bool[cubNumber * cubNumber];
    int cubCount = 0;
    GameObject[,] cubes = new GameObject[cubNumber, cubNumber]; 

    void Start()
    {
        for (int k = 0; k < cubNumber; k++)
        {
            for (int i = 0; i < cubNumber; i++)
            {
                cubs[i + k * cubNumber] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubs[i + k * cubNumber].transform.position = new Vector3(i * 2 - cubNumber,  k * 2 -cubNumber, 0);
            }
        }
        ColorChange(cubCount);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            cubCount++;
            if (cubCount >= cubNumber * cubNumber)
            {
                cubCount = 0;
            }
            ColorChange(cubCount);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            cubCount--;
            if (cubCount < 0)
            {
                cubCount = cubNumber * cubNumber - 1;
            }
            ColorChange(cubCount);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            cubCount += cubNumber;
            if (cubCount >= cubNumber * cubNumber)
            {
                cubCount -= cubNumber * cubNumber;
            }
            ColorChange(cubCount);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            cubCount -= cubNumber;
            if (cubCount < 0)
            {
                cubCount += (cubNumber - 1) * cubNumber;
            }
            ColorChange(cubCount);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyCub(cubCount);
        }
        //NewMethod();
    }
    /// <summary>
    /// Control&R&N
    /// </summary>
    private void NewMethod()
    {
        cubCount = cubCount < 0 ? 0 : cubCount >= cubNumber ? cubNumber - 1 : cubCount;
    }

    void NextCubCheckDown()
    {
        if (cubCount < 0)
        {
            cubCount = cubNumber - 1;
        }
        if (destroyMode[cubCount])
        {
            cubCount--;
            if (cubCount < 0 || destroyMode[cubCount])
            {
                for (int i = 0; i < cubNumber; i++)
                {
                    cubCount = cubNumber - 1 - i;
                    if (!destroyMode[cubCount])
                    {
                        return;
                    }
                }
            }
        }
    }
    void NextCubCheckUP()
    {
        if (cubCount >= cubNumber)
        {
            cubCount = 0;
        }
        if (destroyMode[cubCount])
        {
            cubCount++;
            if (cubCount >= cubNumber || destroyMode[cubCount])
            {
                for (int i = 0; i < cubNumber; i++)
                {
                    cubCount = 0 + i;
                    if (!destroyMode[cubCount])
                    {
                        return;
                    }
                }
            }
        }
    }
    void ColorChange(int number)
    {
        foreach (var item in cubs)
        {
            item.GetComponent<Renderer>().material.color = Color.white;
        }
        var r = cubs[number].GetComponent<Renderer>();
        r.material.color = Color.red;
    }
    void DestroyCub(int number)
    {
        cubs[number].SetActive(false);
        destroyMode[number] = true;
    }
}
