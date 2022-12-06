using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHeri : MonoBehaviour
{
    //戦闘ヘリの入れる所
    [Header("戦闘ヘリの入れる所")]
    public GameObject Attackfab;

    //戦闘ヘリの出てくる間隔の数値
    [Header("戦闘ヘリの出てくる間隔の数値")]
    [SerializeField]
    float AttackHeriInterval = 1.0f;

    //戦闘ヘリの最大数
    [Header("戦闘ヘリの最大数")]
    [SerializeField]
    int ReSpwonMax = 3;

    //戦闘ヘリのリスポーン地点の数
    [Header("戦闘ヘリのリスポーン地点の数")]
    [SerializeField]
    int ReSpwonCnt = 3;

    //戦闘ヘリが出てくる配列
    [Header("戦闘ヘリが出てくる配列")]
    [SerializeField]
    private GameObject[] SpwonPoint;

    ////オブジェクトプール
    //[Header("オブジェクトプール")]
    //[SerializeField]
    //ObjectPoolManager objectPool;

    private void Start()
    {
        //リスポーン用のコルーチンスタート
        StartCoroutine(Respawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    //コルーチン
    IEnumerator Respawn()
    {
        //ヘリループ
        while (true)
        {
            for (; 0 < ReSpwonMax; ReSpwonMax--)
            {
                Debug.Log("deta");

                ////オブジェクトプールのLaunch関数呼び出し
                //objectPool.Launch(transform.position);

                //ここで生成するインターバルを取っている
                yield return new WaitForSeconds(AttackHeriInterval);
                //プレハブのものを生成
                var Heri = Instantiate(Attackfab);
                //生成した敵の位置をランダムに設定する
                Heri.transform.position = GetRandomPosition();
                //ヘリコプターの初期情報を与える
                //Heri.GetComponent<AttackHelicopterScript>().Init(this);

                //objectPool.GetComponent<heriScript>().Init(this);
            }
            yield return null;
        }
    }

    public void AttckHeriDesCount()
    {
        ReSpwonMax++;
    }

    // ランダムな位置を生成する関数
    private Vector3 GetRandomPosition()
    {
        //Ram
        int RamCnt = Random.Range(0, ReSpwonCnt);//1～Cntの間をランダムで選ぶ
        Debug.Log("Range " + RamCnt);
        //Vector3型のPositionを返す
        return transform.position = SpwonPoint[RamCnt].transform.position;
    }
}
