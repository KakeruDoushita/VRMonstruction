using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    [SerializeField] float t;//äÑçá

    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tmp = Vector3.Lerp(transform.position, target.position, t);

        transform.position = tmp;
    }

    public void SetFollowActive(bool val)
    {
        enabled = val;
    }
}
