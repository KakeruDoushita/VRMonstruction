using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleBullet : MonoBehaviour
{
    // �ǔ��Ώ�
    [Tooltip("�ǔ��Ώ�")]
    public GameObject target;

    // �{�̏��
    Transform bodyTF;
    Rigidbody bodyRB;
    Collider bodyCol;

    // �i�s����(���[���h���)
    [SerializeField]
    [Tooltip("�i�s����(���[���h���)")]
    Vector3 movevec;

    // �����x�̑傫��
    [Tooltip("�����x�̑傫��")]
    public float acceleration;

    // �ō����x
    [Tooltip("�ō����x")]
    public float maxSpeed;

    //�Փˌ�̃G�t�F�N�g�����ꏊ
    [Tooltip("�Փˌ�̃G�t�F�N�g�����ꏊ")]
    public GameObject explosion;//effect�ɂ�unity���prefab���֘A�t���܂�

    [SerializeField]
    [Tooltip("��]����X�s�[�h")]
    float Rotationspeed;


    // Start is called before the first frame update
    void Start()
    {
        bodyTF = transform.parent;//�e�̃I�u�W�F�N�g
        bodyRB = transform.parent.GetComponent<Rigidbody>();
        bodyCol = transform.parent.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // �ǔ��Ώۂ�������ǔ�����
        if (target)
        {
            // �i�s�������擾����
            Vector3 distance = target.transform.position - bodyTF.position;
            movevec = Vector3.Normalize(distance);

            // �W���W���ɉ�]�����܂��傤
            //transform.rotation = Quaternion.RotateTowards(transform.rotation,//�����̉�]�ʒu
                                             //Quaternion.Euler(movevec),//��]����p�x
                                             //Rotationspeed);// ��]����X�s�[�h
            bodyTF.LookAt(movevec);

        }
        // �ǔ��Ώۂ����Ȃ������炻�̂܂ܐi��
        else
        {
            // �i�s�������擾����
            movevec = bodyTF.forward;
        }
        // ����������
        bodyRB.AddForce(movevec * acceleration);

        // ���������瑬�x��␳����
        float mag = bodyRB.velocity.magnitude;
        if (mag > maxSpeed)
        {
            bodyRB.velocity *= maxSpeed / mag;
        }
    }

    // ���G�͈͂Ƀv���C���[����������ǐՂ���
    void OnTriggerEnter(Collider other)
    {
        if (other == bodyCol || other.tag != "playerhead") return;
        target = other.gameObject;
    }

    // �ǐՑΏۂ����G�͈͂���O�ꂽ��ǐՂ��Ȃ�
    void OnTriggerExit(Collider other)
    {
        target = null;
    }

    void OnCollisionEnter(Collision collision) //�Փˎ��̏���
    {

        //Destroy(this.gameObject);
        if (collision.gameObject.tag == "playerhead")
        //�^�O�Ō���i���̃I�u�W�F�N�g�ɏՓ˂����ꍇ�͌Ăяo���Ȃ�
        {
            Debug.Log("atata");
            Destroy(gameObject);//�~�T�C��

            Instantiate(explosion, transform.position, Quaternion.identity);//�Ԃ������ʒu��explosion�Ƃ���prefab��z�u����

        }
    }

}
