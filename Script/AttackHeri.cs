using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHeri : MonoBehaviour
{
    //�퓬�w���̓���鏊
    [Header("�퓬�w���̓���鏊")]
    public GameObject Attackfab;

    //�퓬�w���̏o�Ă���Ԋu�̐��l
    [Header("�퓬�w���̏o�Ă���Ԋu�̐��l")]
    [SerializeField]
    float AttackHeriInterval = 1.0f;

    //�퓬�w���̍ő吔
    [Header("�퓬�w���̍ő吔")]
    [SerializeField]
    int ReSpwonMax = 3;

    //�퓬�w���̃��X�|�[���n�_�̐�
    [Header("�퓬�w���̃��X�|�[���n�_�̐�")]
    [SerializeField]
    int ReSpwonCnt = 3;

    //�퓬�w�����o�Ă���z��
    [Header("�퓬�w�����o�Ă���z��")]
    [SerializeField]
    private GameObject[] SpwonPoint;

    ////�I�u�W�F�N�g�v�[��
    //[Header("�I�u�W�F�N�g�v�[��")]
    //[SerializeField]
    //ObjectPoolManager objectPool;

    private void Start()
    {
        //���X�|�[���p�̃R���[�`���X�^�[�g
        StartCoroutine(Respawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    //�R���[�`��
    IEnumerator Respawn()
    {
        //�w�����[�v
        while (true)
        {
            for (; 0 < ReSpwonMax; ReSpwonMax--)
            {
                Debug.Log("deta");

                ////�I�u�W�F�N�g�v�[����Launch�֐��Ăяo��
                //objectPool.Launch(transform.position);

                //�����Ő�������C���^�[�o��������Ă���
                yield return new WaitForSeconds(AttackHeriInterval);
                //�v���n�u�̂��̂𐶐�
                var Heri = Instantiate(Attackfab);
                //���������G�̈ʒu�������_���ɐݒ肷��
                Heri.transform.position = GetRandomPosition();
                //�w���R�v�^�[�̏�������^����
                //Heri.GetComponent<AttackHelicopterScript>().Init(this);

                //objectPool.GetComponent<heriScript>().Init(this);
            }
            yield return null;
        }
    }

    public void AttckHeriDesCount()
    {
        ReSpwonMax++;
    }

    // �����_���Ȉʒu�𐶐�����֐�
    private Vector3 GetRandomPosition()
    {
        //Ram
        int RamCnt = Random.Range(0, ReSpwonCnt);//1�`Cnt�̊Ԃ������_���őI��
        Debug.Log("Range " + RamCnt);
        //Vector3�^��Position��Ԃ�
        return transform.position = SpwonPoint[RamCnt].transform.position;
    }
}
