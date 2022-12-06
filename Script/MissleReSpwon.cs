using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleReSpwon : MonoBehaviour
{
    //�I�u�W�F�N�g�v�[���̎擾�ꏊ�ϐ�
    ObjectPoolManager objectPool;

    //�~�T�C���̏o�Ă���Ԋu
    [Header("�~�T�C���̏o�Ă���Ԋu")]
    [SerializeField]
    float MissleInterval = 1.0f;

    //�~�T�C���̍ő吔
    [Header("�~�T�C���̍ő吔")]
    [SerializeField]
    int ReSpwonMax = 3;

    //�~�T�C�����o�Ă���z��
    [Header("�~�T�C�����o�Ă���z��")]
    [SerializeField]
    private GameObject[] SpwonPoint;

    //�ڕW�̍��W������ꏊ
    private Transform playerHead;

    //SE���擾����ϐ�
    private YoshigaSoundManager pySE;

    //�q�G�����L�[�̃T�E���h�}�l�[�W���[
    private GameObject soundManager;

    private void Awake()
    {
        //�I�u�W�F�N�g�v�[���}�l�[�W���[���Q�b�g���Ă���
        objectPool = GameObject.Find("Doushita_ObjectPool").GetComponent<ObjectPoolManager>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        //YoshigaSoundManager���擾����
        soundManager = GameObject.Find("SoundManager");
        pySE = soundManager.transform.GetComponent<YoshigaSoundManager>();

        //�v���C���[�̓��̍��W������
        playerHead = GameObject.FindGameObjectWithTag("playerhead").transform;

        //���X�|�[���p�̃R���[�`���X�^�[�g
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        while (true)
        {
            //�~�T�C������ꂽ���ɂ��̏���������
            for (; 0 < ReSpwonMax; ReSpwonMax--)
            {
                //���˂��ꂽ���̃C���^�[�o����݂���
                yield return new WaitForSeconds(MissleInterval);
                objectPool.missleLaunch(GetRandomPosition());
                //�����ŉ���炷
                pySE.PlaySE("missle");
            }
            yield return null;
        }
    }

    public void MissleDesCount()
    {
        ReSpwonMax++;
    }

    // �����_���Ȉʒu�𐶐�����֐�
    private Vector3 GetRandomPosition()
    {
        //Ram
        int RamCnt = Random.Range(0, ReSpwonMax);//1�`Cnt�̊Ԃ������_���őI��
        //Vector3�^��Position��Ԃ�
        return transform.position = SpwonPoint[RamCnt].transform.position;
    }
}
