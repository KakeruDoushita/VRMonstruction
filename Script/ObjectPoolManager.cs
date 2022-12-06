using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    //�e�̃v���n�u
    [Header("�e�̃v���n�u")]
    [SerializeField]
    BulletScript Bullet;

    //�퓬�w���̃v���n�u
    [Header("�퓬�w���̃v���n�u")]
    [SerializeField]
    heriScript AttackHeri;

    //�~�T�C���̃v���n�u
    [Header("�~�T�C���̃v���n�u")]
    [SerializeField]
    missleScript missle;

    //�퓬�w���̃��X�|�[���ꏊ�̃v���n�u
    [Header("�퓬�w���̃��X�|�[���ꏊ�̃v���n�u")]
    [SerializeField]
    AttackHelicopterScript AttackPoot;

    //�~�T�C���̃��X�|�[���ꏊ�̃v���n�u
    [Header("�~�T�C���̃��X�|�[���ꏊ�̃v���n�u")]
    [SerializeField]
    MissleReSpwon misslepoot;

    //�������鐔
    [Header("�������鐔")]
    [SerializeField]
    int bulletReSpwonMax = 10;

    //�퓬�w���̍ő吔
    [Header("�퓬�w���̍ő吔")]
    [SerializeField]
    int heriReSpwonMax = 3;

    //�~�T�C���̍ő吔
    [Header("�~�T�C���̍ő吔")]
    [SerializeField]
    int missleSpwonMax = 3;

    //���������e���i�[����Queue
    Queue<BulletScript> BulletQueue;

    //���������퓬�w�����i�[����Queue
    Queue<heriScript> AttackQueue;

    //List<heriScript> AttackHeriList;

    //���������~�T�C�����i�[����Queue
    Queue<missleScript> missleQueue;

    //�v���C���[�̉�����]����I�u�W�F�N�g�̃v���n�u
    [SerializeField] GameObject constRotate;
    //�v���C���[�̎�����܂��}�l�[�W���[��z�񉻂���
    GameObject[] constRotates = new GameObject[3];

    //���񐶐����̃|�W�V����
    Vector3 setPos = new Vector3(100, 100, 0);

    //�N�����̏���
    private void Awake()
    {
        //Queue�̏�����
        BulletQueue = new Queue<BulletScript>();

        AttackQueue = new Queue<heriScript>();

        //AttackHeriList = new List<heriScript>();

        missleQueue = new Queue<missleScript>();

        //�e�𐶐����郋�[�v
        for (int i = 0; i < bulletReSpwonMax; i++)
        {
            //����
            BulletScript tmpBullet = Instantiate(Bullet, setPos, Quaternion.identity, transform);

            //Queue�ɒǉ�
            BulletQueue.Enqueue(tmpBullet);

        }

        //�w�����쐬���郋�[�v
        //for (int i = 0; i < 3; i++)
        //{
        //    //����
        //    heriScript tmpheri = Instantiate(AttackHeri, setPos, Quaternion.identity, transform);

        //    //
        //    tmpheri.Init(AttackPoot);

        //    tmpheri.SetIdx(i);
        //    Debug.Log("�N���G�C�g");
        //    //List�ɒǉ�
        //    AttackHeriList.Add(tmpheri);
        //}

        //�w�����쐬���郋�[�v
        for (int i = 0; i < heriReSpwonMax; i++)
        {
            //����
            heriScript tmpheri = Instantiate(AttackHeri, setPos, Quaternion.identity, transform);

            tmpheri.Init(AttackPoot);

            //Queue�ɒǉ�
            AttackQueue.Enqueue(tmpheri);
        }


        //�~�T�C�����쐬���郋�[�v
        for (int i = 0; i < missleSpwonMax; i++)
        {
            //����
            missleScript tmpmissle = Instantiate(missle, setPos, Quaternion.identity, transform);

            tmpmissle.Init(misslepoot);

            //Queue�ɒǉ�
            missleQueue.Enqueue(tmpmissle);
        }

        // �v���C���[����𓙋����ŉ�]����I�u�W�F�N�g�𐶐�
        CreateConstRotate();
    }

    private void Start()
    {
        int cnt = 0;
        ConstRotate tmpScript = constRotate.GetComponent<ConstRotate>();
        foreach (var heri in AttackQueue)
        {
            heri.SetDistance(tmpScript.GetDistance());
            heri.SetTarget(constRotates[cnt].transform);
            heri.gameObject.GetComponent<Follow>().target = constRotates[cnt].transform;
            cnt++;
        }
    }

    //�w���̗U���p�̒e�𐶐�����
    private void CreateConstRotate()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject tmp = Instantiate(constRotate, transform);
            tmp.GetComponent<ConstRotate>().SetAngle(120 * i);
            constRotates[i] = tmp;
        }
    }

    //�e��݂��o������
    public BulletScript bulletLaunch(Vector3 _pos, Vector3 _vec)
    {
        //Queue����Ȃ�null
        if (BulletQueue.Count <= 0) return null;
        //Queue����w��������o��
        BulletScript tmpBullet = BulletQueue.Dequeue();
        //�e��\������
        tmpBullet.gameObject.SetActive(true);
        //�n���ꂽ���W�ɒe���ړ�����
        tmpBullet.ShowInStage(_pos, _vec);
        //�Ăяo�����ɓn��
        return tmpBullet;
    }

    //�e�̉������
    public void bulletCollect(BulletScript _bullet)
    {
        //�w���̃Q�[���I�u�W�F�N�g���\��
        _bullet.gameObject.SetActive(false);

        //Queue�Ɋi�[
        BulletQueue.Enqueue(_bullet);
    }

    ////�w����݂��o������
    //public void heriLaunch(int idx, Vector3 _pos)
    //{

    //    if (AttackHeriList[idx].gameObject.activeSelf) return;
    //    //Queue����w��������o��
    //    heriScript tmpHeri = AttackHeriList[idx];
    //    //�n���ꂽ���W�ɒe���ړ�����
    //    tmpHeri.ShowInStage(_pos);

    //    //�w����\������
    //    tmpHeri.gameObject.SetActive(true);
    //}

    //�w����݂��o������
    public heriScript heriLaunch(Vector3 _pos)
    {
        //Queue����Ȃ�null
        if (AttackQueue.Count <= 0) return null;
        //Queue����w��������o��
        heriScript tmpheri = AttackQueue.Dequeue();
        //�e��\������
        tmpheri.gameObject.SetActive(true);
        //�n���ꂽ���W�ɒe���ړ�����
        tmpheri.ShowInStage(_pos);
        //�Ăяo�����ɓn��
        return tmpheri;
    }

    ////�w���̉������
    //public void heriCollect(heriScript _heri)
    //{
    //    //�w���̃Q�[���I�u�W�F�N�g���\��
    //    _heri.gameObject.SetActive(false);

    //    ////Queue�Ɋi�[
    //    //AttackHeriQueue.Enqueue(_heri);
    //}

    //�w���̉������
    public void heriCollect(heriScript _heri)
    {
        //�w���̃Q�[���I�u�W�F�N�g���\��
        _heri.gameObject.SetActive(false);

        //Queue�Ɋi�[
        AttackQueue.Enqueue(_heri);
    }

    //�~�T�C����݂��o������
    public missleScript missleLaunch(Vector3 _pos)
    {
        //Queue����Ȃ�null
        if (missleQueue.Count <= 0) return null;
        //Queue����w��������o��
        missleScript tmpmissle = missleQueue.Dequeue();
        //�e��\������
        tmpmissle.gameObject.SetActive(true);
        //�n���ꂽ���W�ɒe���ړ�����
        tmpmissle.ShowInStage(_pos);
        //�Ăяo�����ɓn��
        return tmpmissle;
    }

    //�~�T�C���̉������
    public void missleCollect(missleScript _missle)
    {
        //�w���̃Q�[���I�u�W�F�N�g���\��
        _missle.gameObject.SetActive(false);

        //Queue�Ɋi�[
        missleQueue.Enqueue(_missle);
    }

    //�{�X��ŃI�u�W�F�N�g�v�[����false�ɂ���֐�
    public void ObjectPoolfalse()
    {
        this.gameObject.SetActive(false);
    }
}
