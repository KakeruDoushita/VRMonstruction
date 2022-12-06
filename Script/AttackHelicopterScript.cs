using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHelicopterScript : MonoBehaviour
{
    //オブジェクトプール
    ObjectPoolManager objectPool;

    //戦闘ヘリの出てくる間隔の数値
    [Header("戦闘ヘリの出てくる間隔の数値")]
    [SerializeField]
    float AttackHeriInterval = 1.0f;

    //戦闘ヘリの最大数
    [Header("戦闘ヘリの最大数")]
    [SerializeField]
    int ReSpwonMax = 3;

    [SerializeField]
    [Header("デバック用")]
    int ReSpwon = 0;

    //戦闘ヘリが出てくる配列
    [Header("戦闘ヘリが出てくる配列")]
    [SerializeField]
    public GameObject[] SpwonPoint = new GameObject[3];

    //目標の座標を入れる場所
    private Transform playerHead;

    private void Awake()
    {
        //オブジェクトプールマネージャーをゲットしていく
        objectPool = GameObject.Find("Doushita_ObjectPool").GetComponent<ObjectPoolManager>();
    }

    private void Start()
    {
        //プレイヤーの頭の座標を入れる
        playerHead = GameObject.FindGameObjectWithTag("playerhead").transform;
        

        //リスポーン用のコルーチンスタート
        StartCoroutine(Respawn());
    }

    //コルーチン
    IEnumerator Respawn()
    {
        while (true)
        {
            //ミサイルが壊れた時にこの処理が入る
            for (; 0 < ReSpwonMax; ReSpwonMax--)
            {
                //ここで生成するインターバルを取っている
                yield return new WaitForSeconds(AttackHeriInterval);
                //オブジェクトプールのLaunch関数呼び出し
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

    //ランダムな位置を生成する関数
    //生成場所一つ一つに生成する用に処理
    private Vector3 GetRandomPosition()
    {        //Vector3型のPositionを返す
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
