using UnityEngine;
using UnityEngine.UI;


public class AchievementsPanel : MonoBehaviour {
    //---------- Fields ----------
    public Transform firstPage;
    public Transform secondPage;
    public Transform thirdPage;

    public Text D_Mission_Complete;
    public Text C_Mission_Complete;
    public Text B_Mission_Complete;
    public Text A_Mission_Complete;
    public Text S_Mission_Complete;
    public Text ABCD_Mission_Complete;
    public Text C_Bonus_Complete;
    public Text B_Bonus_Complete;
    public Text FourB_Artist_Complete;
    public Text GreedIsGood_Complete;
    public Text UnluckyLotto_Complete;
    public Text RareCollector_Complete;
    public Text TeemoBefore15_Complete;
    public Text FiveSameA_Complete;
    public Text XSABCD_Complete;
    public Text TripleC_Complete;
    public Text All_DA_Complete;
    public Text Rare_Seller_Complete;
    
    private int currentPage;

    //---------- Pages and Navigation ----------
    private void Awake() {
        this.currentPage = 1;
    }

    public void ScrollPageLeft() {
        if (currentPage == 1) {
            return;
        }
        currentPage -= 1;
        ShowHidePages();
    }

    public void ScrollPageRight() {
        if (currentPage == 3) {
            return;
        }
        currentPage += 1;
        ShowHidePages();
    }

    private void ShowHidePages() {
        if (currentPage == 1) {
            ToggleFirstPage(true);
            ToggleSecondPage(false);
            ToggleThirdPage(false);
        } else if (currentPage == 2) {
            ToggleFirstPage(false);
            ToggleSecondPage(true);
            ToggleThirdPage(false);
        } else if (currentPage == 3) {
            ToggleFirstPage(false);
            ToggleSecondPage(false);
            ToggleThirdPage(true);
        }
    }

    private void ToggleFirstPage(bool state) {
        firstPage.gameObject.SetActive(state);
    }

    private void ToggleSecondPage(bool state) {
        secondPage.gameObject.SetActive(state);
    }

    private void ToggleThirdPage(bool state) {
        thirdPage.gameObject.SetActive(state);
    }

    //---------- Achievement Management ----------

}
