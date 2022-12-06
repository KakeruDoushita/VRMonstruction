using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHelicopterScript : MonoBehaviour
{
    //�I�u�W�F�N�g�v�[��
    ObjectPoolManager objectPool;

    //�퓬�w���̏o�Ă���Ԋu�̐��l
    [Header("�퓬�w���̏o�Ă���Ԋu�̐��l")]
    [SerializeField]
    float AttackHeriInterval = 1.0f;

    //�퓬�w���̍ő吔
    [Header("�퓬�w���̍ő吔")]
    [SerializeField]
    int ReSpwonMax = 3;

    [SerializeField]
    [Header("�f�o�b�N�p")]
    int ReSpwon = 0;

    //�퓬�w�����o�Ă���z��
    [Header("�퓬�w�����o�Ă���z��")]
    [SerializeField]
    public GameObject[] SpwonPoint = new GameObject[3];

    //�ڕW�̍��W������ꏊ
    private Transform playerHead;

    private void Awake()
    {
        //�I�u�W�F�N�g�v�[���}�l�[�W���[���Q�b�g���Ă���
        objectPool = GameObject.Find("Doushita_ObjectPool").GetComponent<ObjectPoolManager>();
    }

    private void Start()
    {
        //�v���C���[�̓��̍��W������
        playerHead = GameObject.FindGameObjectWithTag("playerhead").transform;
        

        //���X�|�[���p�̃R���[�`���X�^�[�g
        StartCoroutine(Respawn());
    }

    //�R���[�`��
    IEnumerator Respawn()
    {
        while (true)
        {
            //�~�T�C������ꂽ���ɂ��̏���������
            for (; 0 < ReSpwonMax; ReSpwonMax--)
            {
                //�����Ő�������C���^�[�o��������Ă���
                yield return new WaitForSeconds(AttackHeriInterval);
                //�I�u�W�F�N�g�v�[����Launch�֐��Ăяo��
                //Debug.Log(ReSpwon);
                objectPool.heriLaunch(GetRandomPosition());
            }
            yield return null;
        }
    }

    public void AttackDesCount()
    {
        ReSpwonMax++;
    }

    //�����_���Ȉʒu�𐶐�����֐�
    //�����ꏊ���ɐ�������p�ɏ���
    private Vector3 GetRandomPosition()
    {        //Vector3�^��Position��Ԃ�
        transform.position = SpwonPoint[ReSpwon].transform.position;

        if(ReSpwon == 2)
        {
            ReSpwon = 0;
        }
        else
        {
            ReSpwon += 1;
        }
        return transform.position;
    }
}
