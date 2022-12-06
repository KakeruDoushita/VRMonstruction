using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //�e�̃G�t�F�N�g
    [SerializeField]
    [Header("�e�̃G�t�F�N�g")]
    public GameObject Bulleteffect;

    //�v�[���}�l�[�W���[�N���X�̃v�[���}�l�[�W���[��ǉ�����
    ObjectPoolManager poolManager;

    //�e�̃X�s�[�h
    [SerializeField]
    [Header("�e�̃X�s�[�h")]
    private float Spead;

    //���W�b�g�{�f�B�[
    private Rigidbody myRb;

    // Start is called before the first frame update
    void Start()
    {
        //���W�b�g�{�f�B���擾
        myRb = GetComponent<Rigidbody>();
        //�I�u�W�F�N�g�v�[���}�l�[�W���[���Q�b�g���Ă���
        poolManager = transform.parent.GetComponent<ObjectPoolManager>();
        //������farce�ɂ��Ă���
        gameObject.SetActive(false);
    }


    //�e�̑ݏo����
    public void ShowInStage(Vector3 _pos, Vector3 _vec)
    {
        transform.position = _pos;

        myRb.velocity = _vec * Spead;
    }

    //�e��Ԃ�����
    public void HideFromStage()
    {
        poolManager.bulletCollect(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        //�^�O�Ō���i���̃I�u�W�F�N�g�ɏՓ˂����ꍇ�͌Ăяo���Ȃ�
        {
            Instantiate(Bulleteffect, transform.position, Quaternion.identity);//�Ԃ������ʒu��Bulleteffect�Ƃ���prefab��z�u����
            HideFromStage();
        }
    }
}
