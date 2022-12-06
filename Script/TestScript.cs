using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript: MonoBehaviour
{

    //�X�^�[�g�ƏI���̖ڈ�
    public Transform startMarker;
    public Transform endMarker;

    // �X�s�[�h
    [SerializeField]
    public float speed = 1.0F;

    //��_�Ԃ̋���������
    private float distance_two;

    void Start()
    {
        //��_�Ԃ̋�������(�X�s�[�h�����Ɏg��)
        distance_two = Vector3.Distance(startMarker.position, endMarker.position);
    }

    void Update()
    {

        // ���݂̈ʒu
        float present_Location = (Time.time * speed) / distance_two;

        // �I�u�W�F�N�g�̈ړ�
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, present_Location);
    }
}