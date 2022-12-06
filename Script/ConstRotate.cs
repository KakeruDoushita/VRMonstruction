using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstRotate : MonoBehaviour
{
    //�v���C���[�̓��̍��W
    [Header("�v���C���[�̓��̍��W")]
    private Transform PlayerHead;

    //�v���C���[�̓��Ƃ̋���
    [SerializeField]
    [Header("�v���C���[�̓��Ƃ̋���")]
    float Distance = 200;

    //�p�x
    [SerializeField]
    [Header("�p�x")]
    float Angle = 0;

    //��鑬�x�̑傫���̕��������ق�
    [SerializeField]
    [Header("��鑬�x�̑傫���̕��������ق�")]
    float AddMin = 60;

    //��鑬�x�̑傫���̕��傫���ق�
    [SerializeField]
    [Header("��鑬�x�̑傫���̕��傫���ق�")]
    float AddMax = 80;

    private float AddDeg = 80;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHead = GameObject.FindWithTag("playerhead").transform;
        AddDeg = Random.Range(AddMin, AddMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (Angle >= 360)
        {
            Angle = 0;
        }
        else
        {
            Angle += AddDeg * Time.deltaTime;
        }

        float rad = Angle * Mathf.Deg2Rad;

        transform.position = PlayerHead.position + new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * Distance;
    }

    //SetActive����Ƃ���
    public void SetAngle(float val)
    {
        Angle = val;
    }

    //GetDistance����Ƃ���
    public float GetDistance()
    {
        return Distance;
    }
}