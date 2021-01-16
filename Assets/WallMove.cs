using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMove : MonoBehaviour
{
    public Material mat;

    public float speed;
    public int steps;

    private int curSteps;
    private bool direction;

    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<MeshRenderer>().material = mat;
        }

        StartCoroutine(Moving());
    }

    IEnumerator Moving()
    {
        while (true)
        {
            if (direction)
            {
                yield return new WaitForSeconds(0.01f);
                transform.position += new Vector3(speed, 0f, 0f);
                curSteps++;
                if (curSteps > steps)
                {
                    curSteps = 0;
                    direction = !direction;
                }
            }
            else
            {
                yield return new WaitForSeconds(0.01f);
                transform.position -= new Vector3(speed, 0f, 0f);
                curSteps++;
                if (curSteps > steps)
                {
                    curSteps = 0;
                    direction = !direction;
                }
            }
        }
    }

}
