using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class missleScript : MonoBehaviour
{
    //���x
    [SerializeField]
    [Header("�~�T�C���̃X�s�[�h")]
    float MiSpeed;

    //�Ȃ���Ƃ��̑��x
    [SerializeField]
    [Header("�Ȃ���Ƃ��̑��x")]
    float LpSpeed;

    //�O�����ς�鋗��  y�̒l��150�オ��Ȃ�250�グ��
    [SerializeField]
    [Header("�O�����ς�鋗��  y�̒l��150�オ��Ȃ�250�グ��")]
    float Distance;

    //�Փˌ�̃G�t�F�N�g�����ꏊ
    [Header("�Փˌ�̃G�t�F�N�g�����ꏊ")]
    public GameObject explosion;//effect�ɂ�unity���prefab���֘A�t���܂�

    //Rigidbody������
    Rigidbody myRb;

    //�v�[���}�l�[�W���[�N���X�̃v�[���}�l�[�W���[��ǉ�����
    ObjectPoolManager poolManager;

    //��_�Ԃ̋���������
    private float distance_two;

    //�~�T�C�����X�|�[���n�_������ꏊ
    MissleReSpwon misslepoot;

    //�ڕW�̍��W������ꏊ
    private Transform PlayerHead;

    ////�v���C���[�̘r�̈З͂�����
    Vector3 playerArm;

    void Start()
    {
        //RIgidbody��ݒ�
        myRb = GetComponent<Rigidbody>();

        //�I�u�W�F�N�g�v�[���}�l�[�W���[���Q�b�g���Ă���
        poolManager = transform.parent.GetComponent<ObjectPoolManager>();

        //������farce�ɂ��Ă���
        gameObject.SetActive(false);
    }

    private void FixedUpdate()//�t�B�b�N�X�h�A�b�v�f�[�g�͈��Ԋu�ŌĂ΂�Ă���悤�Ɍ����鏈�������畨���v�Z��ړ��Ȃǂ̌v�Z�ɗp����
    {
        //�v���C���[�̓��̍��W������
        PlayerHead = GameObject.FindGameObjectWithTag("playerhead").transform;

        // �Ǐ]�ΏۃI�u�W�F�N�g��Transform����A�ړI�n���Z�o
        Vector3 targetPos = PlayerHead.TransformPoint(new Vector3(0f, 0f, 0f));

        //��_�Ԃ̋�������(�X�s�[�h�����Ɏg��)
        distance_two = Vector3.Distance(transform.position, targetPos);

        // ���݂̈ʒu(�~�T�C���̃X�s�[�h��ς��鏈��)
        float present_Location = (Time.deltaTime * LpSpeed) / distance_two;

        if (distance_two > Distance)//�̈�ɓ�������
        {
            Vector3 dir = new Vector3(PlayerHead.transform.position.x, transform.position.y, PlayerHead.transform.position.z);
            //����̃I�u�W�F�N�g�̕�������������
            transform.LookAt(dir);//�����̌������Ă���x�N�g������������悤�ɂ���

            myRb.velocity = MiSpeed * dir;
        }
        else//�̈�ɓ���O
        {
            //������AddForce�̗͂�؂�
            myRb.velocity = Vector3.zero;

            //����̃I�u�W�F�N�g�̕�������������
            transform.LookAt(targetPos);//�����̌������Ă���x�N�g������������悤�ɂ���
            transform.position = Vector3.Lerp(transform.position, targetPos, present_Location);
        }
    }

    public void Init(MissleReSpwon ReSpown)
    {
        misslepoot = ReSpown;
    }

    //�~�T�C���̑ݏo����
    public void ShowInStage(Vector3 _pos)
    {
        transform.position = _pos;

        //myRb.velocity = _vec * MiSpeed;
    }

    //�~�T�C����Ԃ�����
    public void HideFromStage()
    {
        poolManager.missleCollect(this);
    }


    void OnTriggerEnter(Collider other) //�Փˎ��̏���
    {
        if (other.gameObject.tag == "Player")
        //�^�O�Ō���i���̃I�u�W�F�N�g�ɏՓ˂����ꍇ�͌Ăяo���Ȃ�
        {
            //�����ŉ�ꂽ�Ƃ����w����ReSpwon�ɑ���
            misslepoot.MissleDesCount();

            HideFromStage();

            Instantiate(explosion, transform.position, Quaternion.identity);//�Ԃ������ʒu��explosion�Ƃ���prefab��z�u����
        }
        //�~�T�C�����͂����ꂽ������
        if (other.gameObject.tag == "PlayerArm")
        {
            //other���k���`�F�b�N����
            //������playerArm�̒��ɉ��������ĂȂ����`�F�b�N����
            var playerArmScript = other.GetComponent<PlayerArm>();

            if (playerArmScript != null)
            {
                playerArm = playerArmScript.CtrlVelocity;
            }
            myRb.velocity = playerArm;
        }
    }
}