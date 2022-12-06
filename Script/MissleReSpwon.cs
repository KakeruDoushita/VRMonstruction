using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleReSpwon : MonoBehaviour
{
    //オブジェクトプールの取得場所変数
    ObjectPoolManager objectPool;

    //ミサイルの出てくる間隔
    [Header("ミサイルの出てくる間隔")]
    [SerializeField]
    float MissleInterval = 1.0f;

    //ミサイルの最大数
    [Header("ミサイルの最大数")]
    [SerializeField]
    int ReSpwonMax = 3;

    //ミサイルが出てくる配列
    [Header("ミサイルが出てくる配列")]
    [SerializeField]
    private GameObject[] SpwonPoint;

    //目標の座標を入れる場所
    private Transform playerHead;

    //SEを取得する変数
    private YoshigaSoundManager pySE;

    //ヒエラルキーのサウンドマネージャー
    private GameObject soundManager;

    private void Awake()
    {
        //オブジェクトプールマネージャーをゲットしていく
        objectPool = GameObject.Find("Doushita_ObjectPool").GetComponent<ObjectPoolManager>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        //YoshigaSoundManagerを取得する
        soundManager = GameObject.Find("SoundManager");
        pySE = soundManager.transform.GetComponent<YoshigaSoundManager>();

        //プレイヤーの頭の座標を入れる
        playerHead = GameObject.FindGameObjectWithTag("playerhead").transform;

        //リスポーン用のコルーチンスタート
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        while (true)
        {
            //ミサイルが壊れた時にこの処理が入る
            for (; 0 < ReSpwonMax; ReSpwonMax--)
            {
                //発射された時のインターバルを設ける
                yield return new WaitForSeconds(MissleInterval);
                objectPool.missleLaunch(GetRandomPosition());
                //ここで音を鳴らす
                pySE.PlaySE("missle");
            }
            yield return null;
        }
    }

    public void MissleDesCount()
    {
        ReSpwonMax++;
    }

    // ランダムな位置を生成する関数
    private Vector3 GetRandomPosition()
    {
        //Ram
        int RamCnt = Random.Range(0, ReSpwonMax);//1～Cntの間をランダムで選ぶ
        //Vector3型のPositionを返す
        return transform.position = SpwonPoint[RamCnt].transform.position;
    }
}
