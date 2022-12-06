using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    //弾のプレハブ
    [Header("弾のプレハブ")]
    [SerializeField]
    BulletScript Bullet;

    //戦闘ヘリのプレハブ
    [Header("戦闘ヘリのプレハブ")]
    [SerializeField]
    heriScript AttackHeri;

    //ミサイルのプレハブ
    [Header("ミサイルのプレハブ")]
    [SerializeField]
    missleScript missle;

    //戦闘ヘリのリスポーン場所のプレハブ
    [Header("戦闘ヘリのリスポーン場所のプレハブ")]
    [SerializeField]
    AttackHelicopterScript AttackPoot;

    //ミサイルのリスポーン場所のプレハブ
    [Header("ミサイルのリスポーン場所のプレハブ")]
    [SerializeField]
    MissleReSpwon misslepoot;

    //生成する数
    [Header("生成する数")]
    [SerializeField]
    int bulletReSpwonMax = 10;

    //戦闘ヘリの最大数
    [Header("戦闘ヘリの最大数")]
    [SerializeField]
    int heriReSpwonMax = 3;

    //ミサイルの最大数
    [Header("ミサイルの最大数")]
    [SerializeField]
    int missleSpwonMax = 3;

    //生成した弾を格納するQueue
    Queue<BulletScript> BulletQueue;

    //生成した戦闘ヘリを格納するQueue
    Queue<heriScript> AttackQueue;

    //List<heriScript> AttackHeriList;

    //生成したミサイルを格納するQueue
    Queue<missleScript> missleQueue;

    //プレイヤーの回りを回転するオブジェクトのプレハブ
    [SerializeField] GameObject constRotate;
    //プレイヤーの周りをまわるマネージャーを配列化する
    GameObject[] constRotates = new GameObject[3];

    //初回生成時のポジション
    Vector3 setPos = new Vector3(100, 100, 0);

    //起動時の処理
    private void Awake()
    {
        //Queueの初期化
        BulletQueue = new Queue<BulletScript>();

        AttackQueue = new Queue<heriScript>();

        //AttackHeriList = new List<heriScript>();

        missleQueue = new Queue<missleScript>();

        //弾を生成するループ
        for (int i = 0; i < bulletReSpwonMax; i++)
        {
            //生成
            BulletScript tmpBullet = Instantiate(Bullet, setPos, Quaternion.identity, transform);

            //Queueに追加
            BulletQueue.Enqueue(tmpBullet);

        }

        //ヘリを作成するループ
        //for (int i = 0; i < 3; i++)
        //{
        //    //生成
        //    heriScript tmpheri = Instantiate(AttackHeri, setPos, Quaternion.identity, transform);

        //    //
        //    tmpheri.Init(AttackPoot);

        //    tmpheri.SetIdx(i);
        //    Debug.Log("クリエイト");
        //    //Listに追加
        //    AttackHeriList.Add(tmpheri);
        //}

        //ヘリを作成するループ
        for (int i = 0; i < heriReSpwonMax; i++)
        {
            //生成
            heriScript tmpheri = Instantiate(AttackHeri, setPos, Quaternion.identity, transform);

            tmpheri.Init(AttackPoot);

            //Queueに追加
            AttackQueue.Enqueue(tmpheri);
        }


        //ミサイルを作成するループ
        for (int i = 0; i < missleSpwonMax; i++)
        {
            //生成
            missleScript tmpmissle = Instantiate(missle, setPos, Quaternion.identity, transform);

            tmpmissle.Init(misslepoot);

            //Queueに追加
            missleQueue.Enqueue(tmpmissle);
        }

        // プレイヤー周りを等距離で回転するオブジェクトを生成
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

    //ヘリの誘導用の弾を生成する
    private void CreateConstRotate()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject tmp = Instantiate(constRotate, transform);
            tmp.GetComponent<ConstRotate>().SetAngle(120 * i);
            constRotates[i] = tmp;
        }
    }

    //弾を貸し出す処理
    public BulletScript bulletLaunch(Vector3 _pos, Vector3 _vec)
    {
        //Queueが空ならnull
        if (BulletQueue.Count <= 0) return null;
        //Queueからヘリを一つ取り出す
        BulletScript tmpBullet = BulletQueue.Dequeue();
        //弾を表示する
        tmpBullet.gameObject.SetActive(true);
        //渡された座標に弾を移動する
        tmpBullet.ShowInStage(_pos, _vec);
        //呼び出し元に渡す
        return tmpBullet;
    }

    //弾の回収処理
    public void bulletCollect(BulletScript _bullet)
    {
        //ヘリのゲームオブジェクトを非表示
        _bullet.gameObject.SetActive(false);

        //Queueに格納
        BulletQueue.Enqueue(_bullet);
    }

    ////ヘリを貸し出す処理
    //public void heriLaunch(int idx, Vector3 _pos)
    //{

    //    if (AttackHeriList[idx].gameObject.activeSelf) return;
    //    //Queueからヘリを一つ取り出す
    //    heriScript tmpHeri = AttackHeriList[idx];
    //    //渡された座標に弾を移動する
    //    tmpHeri.ShowInStage(_pos);

    //    //ヘリを表示する
    //    tmpHeri.gameObject.SetActive(true);
    //}

    //ヘリを貸し出す処理
    public heriScript heriLaunch(Vector3 _pos)
    {
        //Queueが空ならnull
        if (AttackQueue.Count <= 0) return null;
        //Queueからヘリを一つ取り出す
        heriScript tmpheri = AttackQueue.Dequeue();
        //弾を表示する
        tmpheri.gameObject.SetActive(true);
        //渡された座標に弾を移動する
        tmpheri.ShowInStage(_pos);
        //呼び出し元に渡す
        return tmpheri;
    }

    ////ヘリの回収処理
    //public void heriCollect(heriScript _heri)
    //{
    //    //ヘリのゲームオブジェクトを非表示
    //    _heri.gameObject.SetActive(false);

    //    ////Queueに格納
    //    //AttackHeriQueue.Enqueue(_heri);
    //}

    //ヘリの回収処理
    public void heriCollect(heriScript _heri)
    {
        //ヘリのゲームオブジェクトを非表示
        _heri.gameObject.SetActive(false);

        //Queueに格納
        AttackQueue.Enqueue(_heri);
    }

    //ミサイルを貸し出す処理
    public missleScript missleLaunch(Vector3 _pos)
    {
        //Queueが空ならnull
        if (missleQueue.Count <= 0) return null;
        //Queueからヘリを一つ取り出す
        missleScript tmpmissle = missleQueue.Dequeue();
        //弾を表示する
        tmpmissle.gameObject.SetActive(true);
        //渡された座標に弾を移動する
        tmpmissle.ShowInStage(_pos);
        //呼び出し元に渡す
        return tmpmissle;
    }

    //ミサイルの回収処理
    public void missleCollect(missleScript _missle)
    {
        //ヘリのゲームオブジェクトを非表示
        _missle.gameObject.SetActive(false);

        //Queueに格納
        missleQueue.Enqueue(_missle);
    }

    //ボス戦でオブジェクトプールをfalseにする関数
    public void ObjectPoolfalse()
    {
        this.gameObject.SetActive(false);
    }
}
