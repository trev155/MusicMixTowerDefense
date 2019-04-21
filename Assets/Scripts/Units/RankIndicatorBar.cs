using UnityEngine;

public class RankIndicatorBar : MonoBehaviour {
    public Transform Bar1;
    public Transform Bar2;
    public Transform Bar3;
    public Transform Bar4;
    public Transform Bar5;
    public Transform Bar6;

    public Transform RankText_D;
    public Transform RankText_C;
    public Transform RankText_B;
    public Transform RankText_A;
    public Transform RankText_S;
    public Transform RankText_X;

    public void Initialize(PlayerUnitRank rank) {
        if (rank == PlayerUnitRank.D) {
            Bar1.gameObject.SetActive(true);
        }
        if (rank == PlayerUnitRank.C) {
            Bar1.gameObject.SetActive(true);
            Bar2.gameObject.SetActive(true);
        }
        if (rank == PlayerUnitRank.B) {
            Bar1.gameObject.SetActive(true);
            Bar2.gameObject.SetActive(true);
            Bar3.gameObject.SetActive(true);
        }
        if (rank == PlayerUnitRank.A) {
            Bar1.gameObject.SetActive(true);
            Bar2.gameObject.SetActive(true);
            Bar3.gameObject.SetActive(true);
            Bar4.gameObject.SetActive(true);
        }
        if (rank == PlayerUnitRank.S) {
            Bar1.gameObject.SetActive(true);
            Bar2.gameObject.SetActive(true);
            Bar3.gameObject.SetActive(true);
            Bar4.gameObject.SetActive(true);
            Bar5.gameObject.SetActive(true);
        }
        if (rank == PlayerUnitRank.X) {
            Bar1.gameObject.SetActive(true);
            Bar2.gameObject.SetActive(true);
            Bar3.gameObject.SetActive(true);
            Bar4.gameObject.SetActive(true);
            Bar5.gameObject.SetActive(true);
            Bar6.gameObject.SetActive(true);
        }

        if (rank == PlayerUnitRank.D) {
            RankText_D.gameObject.SetActive(true);
        } else if (rank == PlayerUnitRank.C) {
            RankText_C.gameObject.SetActive(true);
        } else if (rank == PlayerUnitRank.B) {
            RankText_B.gameObject.SetActive(true);
        } else if (rank == PlayerUnitRank.A) {
            RankText_A.gameObject.SetActive(true);
        } else if (rank == PlayerUnitRank.S) {
            RankText_S.gameObject.SetActive(true);
        } else if (rank == PlayerUnitRank.X) {
            RankText_X.gameObject.SetActive(true);
        }
    }
}