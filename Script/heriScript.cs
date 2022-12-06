using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heriScript : MonoBehaviour
{
    //�w���̔����G�t�F�N�g
    [Header("�w���̔����G�t�F�N�g")]
    public GameObject herieffect;

    [SerializeField]
    [Header("�E�̒e�̔��ˏꏊ")]
    private GameObject RightFiringPoint;

    [SerializeField]
    [Header("���̒e�̔��ˏꏊ")]
    private GameObject LeftFiringPoint;

    //�w���̃X�s�[�h
    [SerializeField]
    [Header("�w���̃X�s�[�h")]
    float Speed = 50;

    [SerializeField]
    [Header("�e�̔��ˊԊu")]
    float Bulletfiringinterval = 100;

    //�ڕW�̍��W������ꏊ
    private Transform playerHead;

    //�e�̃C���^�[�o��
    private int count;

    //���X�|�[���p�̃X�N���v�g
    AttackHelicopterScript AttackPoot;

    //Rigidbody�̓����ꏊ
    private Rigidbody myRb;

    //�I�u�W�F�N�g�v�[���̎擾�ꏊ�ϐ�
    ObjectPoolManager objectPool;

    //�ȗ���
    [SerializeField]
    private GameObject pool;

    //SE���擾����ϐ�
    YoshigaSoundManager pySE;

    //�q�G�����L�[�̃T�E���h�}�l�[�W���[
    private GameObject soundManager;

    //�������������_���œ����ꏊ
    float Distance = 0;

    bool isFollowStarted = false;

    //�v���C���[�̓�
    Transform target;   // �������ꏊ

    private void Awake()
    {
        //RIgidbody�̓����Ƃ���
        myRb = GetComponent<Rigidbody>();

        //ObjectPool�̖��O�����A�N�e�B�u�ȃI�u�W�F�N�g�̒�����T���Ď擾����
        //�w�肵�����O�̃I�u�W�F�N�g�𑶍݂��邷�ׂẴA�N�e�B�u�ȃI�u�W�F�N�g�̒�����T���o���Ď擾����
        pool = GameObject.Find("Doushita_ObjectPool");

        //�I�u�W�F�N�g�v�[�����擾
        objectPool = pool.GetComponent<ObjectPoolManager>();
    }

    void Start()
    {
        //YoshigaSoundManager���擾����
        soundManager = GameObject.Find("SoundManager");
        pySE = soundManager.GetComponent<YoshigaSoundManager>();

        //�v���C���[�̓��̍��W������
        playerHead = GameObject.FindGameObjectWithTag("playerhead").transform;

        //������farce�ɂ��Ă���
        gameObject.SetActive(false);
    }

    //�퓬�w����i�܂��鏈��
    private void FixedUpdate()
    {
        // �v���C���[�̕���
        Vector3 pDir = new Vector3(playerHead.position.x, transform.position.y, playerHead.position.z);
        //��_�Ԃ̋��������߂�
        float distance_two = Vector3.Distance(transform.position, playerHead.transform.position);
        
        //�v���C���[�̕��������Ă���
        transform.LookAt(pDir);
        
        // �v���C���[�̓��Ƃ̋���������Ă���ꍇ�ɂ́A�v���C���[�̓��ɋ߂��B
        if (distance_two >= Distance)
        {
            //����̃I�u�W�F�N�g�̕�������������
            Vector3 DIS = Vector3.Normalize(playerHead.position - transform.position);//�w�����猩���v���C���[�Ƃ̋���
            //transform.LookAt(rDir);//�����̌������Ă���x�N�g������������悤�ɂ���
            myRb.velocity = DIS * Speed;
        }
        else //if (distance_two < Distance)//���̋����ԂɂȂ�������Ȃ���A�e���o��
        {
            myRb.velocity = Vector3.zero;//Velocty���[���ɂ���

            //������follow�������Ă�
            if (!isFollowStarted)
            {
                gameObject.GetComponent<Follow>().SetFollowActive(true);
                isFollowStarted = true;
            }

            // �U���J�n
            count += 1;
            if (count % Bulletfiringinterval == 0)
            {
                // �e�𔭎˂���
                LauncherShot();
            }
        }
    }

    //ReSpown��Init�̂��
    public void Init(AttackHelicopterScript ReSpown)
    {
        AttackPoot = ReSpown;
    }

    //�w���̑ݏo����
    public void ShowInStage(Vector3 _pos)
    {
        transform.position = _pos;
    }

    //�w����Ԃ�����
    public void HideFromStage()
    {
        //gameObject.SetActive(false);
        objectPool.heriCollect(this);
    }

    //�e��ł��o������
    public void LauncherShot()
    {
        // �e�𔭎˂���ꏊ���擾
        Vector3 bulletPosition = RightFiringPoint.transform.position;

        //�I�u�W�F�N�g�v�[���ŌĂяo�����ʂ�ł�
        objectPool.bulletLaunch(bulletPosition, Vector3.Normalize(playerHead.position - bulletPosition));

        // �e�𔭎˂���ꏊ���擾
        Vector3 bulletPosition1 = LeftFiringPoint.transform.position;

        //�I�u�W�F�N�g�v�[���ŌĂяo�����ʂ�ł�
        objectPool.bulletLaunch(bulletPosition1, Vector3.Normalize(playerHead.position - bulletPosition1));

    }

    //�w���͔j��p�X�N���v�g
    public void heriDestoroy()
    {
        //�w���̉������
        HideFromStage();
        Instantiate(herieffect, this.transform.position, Quaternion.identity);//�Ԃ������ʒu�𔚔�������
        TelopManager.Instance.BreakNormalObject(TelopManager.BREAKABLE_OBJECTS.HELICOPTER);//���[�����̏����̓z
    }

    ////Idx��Set����
    //public void SetIdx(int idx)
    //{
    //    myIdx = idx;
    //}

    //target
    public void SetTarget(Transform _target)
    {
        target = _target;
    }
    //setDisance���������ێ�����
    public void SetDistance(float val)
    {
        Distance = val;
    }

    void OnTriggerEnter(Collider other)//�󂳂ꂽ���̏���
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "HeatBeam")
        {
            //�w���̉������
            AttackPoot.AttackDesCount();

            gameObject.GetComponent<Follow>().SetFollowActive(false);
            isFollowStarted = false;
            Instantiate(herieffect, this.transform.position, Quaternion.identity);//�Ԃ������ʒu�𔚔�������
            TelopManager.Instance.BreakNormalObject(TelopManager.BREAKABLE_OBJECTS.HELICOPTER);//���[�����̏����̓z
            HideFromStage();
        }
    }
}