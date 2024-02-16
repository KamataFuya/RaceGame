using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    //チェックポイントの通し番号
    [SerializeField]
    int index;

    public RaceSystem raceSystem;
    public CheckPoint nextCheckPoint;
    public Vector3 forward;

    //リスト
    static List<CheckPoint> list = new List<CheckPoint>();
    static public CheckPoint StartPoint => list[0];

    private void Awake()
    {
        //チェックポイントを一つずつリストに追加
        list.Add(this);
    }

    static public void SetForword()
    {
        CheckPoint prev = null;
        //リストの中身をソート
        list.Sort((a, b) => a.index - b.index);

        //リストの中身を一つ一つ変数に格納し、要素の数だけループさせる
        foreach(var cp in list)
        {
            if (prev != null)
            {
                // コースの進行方向について
                // 一個前のチェックポイントにこのチェックポイントへの向きを入れる
                prev.nextCheckPoint = cp;
                prev.forward = (cp.transform.position - prev.transform.position).normalized;
            }
            prev = cp;
        }

        // 最後の一個をスタート地点に向かせる
        list[list.Count - 1].forward = (list[0].transform.position - list[list.Count - 1].transform.position).normalized;

        // スタート地点につなぐ
        list[list.Count - 1].nextCheckPoint = list[0];
    }

    //プレイヤーと当たった時の処理 -> RaceSystemのゲームオブジェクトを割り当てないと判定が行われない
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")//playerに衝突されたら消える
        {
            raceSystem.PassTheCheckpoint();
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 進行具合の取得
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public float GetProgress(Vector3 p)
    {
        var v = p - transform.position;
        return Vector3.Dot(forward, v);
    }

    /// <summary>
    /// 通過判定
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <returns> 0 : 通過していない, 1 : 通過している </returns>
    public bool CheckPassed(Vector3 p0, Vector3 p1)
    {
        return Vector3.Dot(nextCheckPoint.forward,
            p0 - nextCheckPoint.transform.position) * Vector3.Dot(nextCheckPoint.forward,
            p1 - nextCheckPoint.transform.position) < 0 ? true : false;
    }

    /// <summary>
    /// 逆走チェック(正しい進行方向に対して90度以上の角度があれば逆走と判定)
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <returns> 0 : 逆走していない, 1 : 逆走している </returns>
    public bool CheckReverseRunning(Vector3 p0, Vector3 p1)
    {
        return Vector3.Dot(forward, p1 - p0) < 0;
    }

}
