using UnityEngine;

public class ChaosManager : MonoBehaviour
{
    [Header("Scoring")]
    public float chaosAmount = 0.0f;
    public float decayAmount = 0.3f;
    public float chaosPerRank = 20.0f;
    int rankNum = 1;
    public enum Rank { NORMAL = 1, PURRANORMAL = 2, DEMONIC = 3, HELLISH = 4, CHAOS = 5, CRAZYCHAOTICCATASTHROPHE = 6 };
    Rank currentRank = Rank.NORMAL;

    [Header("UI")]
    public float barPercentage = 0.0f;
    [SerializeField] UnityEngine.UI.Image chaosBar;
    [SerializeField] UnityEngine.UI.Image chaosIcon;
    [SerializeField] TMPro.TextMeshProUGUI PointMessage;
    [SerializeField] TMPro.TextMeshProUGUI RankMessage;
    [SerializeField] Sprite chaosLvl1Src;
    [SerializeField] Sprite chaosLvl2Src;
    [SerializeField] Sprite chaosLvl3Src;

    private void Start() {
        PointMessage.text = "";
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(chaosAmount > 0.0f) {
            decay();
        }
        UpdateRank();
        UpdateUI();
        Debug.Log(chaosAmount);
    }

    void decay() {
        chaosAmount -= Time.deltaTime * decayAmount;
        if(chaosAmount < 0.0f) {
            chaosAmount = 0.0f;
        }
    }

    public void AddChaos(float amount) {
        chaosAmount += amount;
    }

    void UpdateRank() {
        rankNum = (int)Mathf.Abs(chaosAmount / chaosPerRank) + 1;

        if (rankNum > 6) {
            currentRank = Rank.CRAZYCHAOTICCATASTHROPHE;
            return;
        }

        currentRank = (Rank)rankNum;


    }

    void UpdateUI() {

        barPercentage = (chaosAmount % chaosPerRank) / 20.0f;
        chaosBar.fillAmount = barPercentage;

        switch (currentRank) { 
            case Rank.NORMAL:
                chaosIcon.sprite = chaosLvl1Src;
                RankMessage.text = "NORMAL";
                break;

            case Rank.PURRANORMAL:
                chaosIcon.sprite = chaosLvl1Src;
                RankMessage.text = "PURRANORMAL";
                break;

            case Rank.DEMONIC:
                chaosIcon.sprite = chaosLvl2Src;
                RankMessage.text = "DEMONIC";
                break;

            case Rank.HELLISH:
                chaosIcon.sprite = chaosLvl3Src;
                RankMessage.text = "HELLISH";
                break;

            case Rank.CHAOS:
                chaosIcon.sprite = chaosLvl3Src;
                RankMessage.text = "CHAOS";
                break;

            case Rank.CRAZYCHAOTICCATASTHROPHE:
                chaosIcon.sprite = chaosLvl3Src;
                RankMessage.text = "CRAZY CHAOTIC CATASTROPHE";
                break;
        }
    }
}
