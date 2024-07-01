using System;
using System.Collections.Generic;
using UnityEngine;

public class ChaosManager : MonoBehaviour
{
    [Header("Scoring")]
    public float chaosAmount = 0.0f;
    public float totalChaosAmount = 0.0f;
    public float decayAmount = 0.3f;
    public float chaosPerRank = 20.0f;
    int rankNum = 1;
    public enum Rank { NORMAL = 1, PURRANORMAL = 2, DEMONIC = 3, HELLISH = 4, CHAOS = 5, CRAZYCHAOTICCATASTHROPHE = 6 };
    Rank currentRank = Rank.NORMAL;
    public float gameDuration = 180.0f;
    float gameDurationTimer = 0.0f;
    public InGamePauseMenu menus;


    [Header("UI")]
    public float barPercentage = 0.0f;
    public UnityEngine.UI.Image chaosBar;
    public UnityEngine.UI.Image chaosIcon;
    public TMPro.TextMeshProUGUI PointMessage;
    public TMPro.TextMeshProUGUI Timer;
    [SerializeField] float chaosMessageDecayTime = 3.0f;
    float chaosMessageDecayTimer = 0.0f;
    public TMPro.TextMeshProUGUI RankMessage;
    public Sprite chaosLvl1Src;
    public Sprite chaosLvl2Src;
    public Sprite chaosLvl3Src;

    List<string> pointLogs = new List<string>();

    private void Start() {
        PointMessage.text = "";
        gameDurationTimer = gameDuration;
        UpdateUI();
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(chaosAmount > 0.0f) {
            decay();
        }
        UpdateRank();
        UpdateUI();

        gameDurationTimer -= Time.deltaTime;
        if(gameDurationTimer <= 0.0f) {
            gameDurationTimer = gameDuration;
            menus.ResultScreen(totalChaosAmount);
        }
        Timer.text = TimeSpan.FromSeconds(gameDurationTimer).ToString(@"mm\:ss");
    }

    void decay() {
        chaosAmount -= Time.deltaTime * decayAmount;
        if(chaosAmount < 0.0f) {
            chaosAmount = 0.0f;
        }
    }

    public void AddChaos(float amount) {
        chaosAmount += amount;
        if (chaosAmount > 6 * chaosPerRank) {
            chaosAmount = 6 * chaosPerRank;
        }
        totalChaosAmount += amount;
    }

    void UpdateRank() {
        rankNum = (int)Mathf.Abs(chaosAmount / chaosPerRank) + 1;
        if (rankNum > 6) {
            rankNum = 6;
        }

        currentRank = (Rank)rankNum;
        decayAmount = 2 + (rankNum / 2);
    }

    void UpdateUI() {

        barPercentage = (chaosAmount % chaosPerRank) / chaosPerRank;
        chaosBar.fillAmount = barPercentage;

        switch (currentRank) { 
            case Rank.NORMAL:
                chaosIcon.sprite = chaosLvl1Src;
                RankMessage.text = "NORMAL";
                chaosBar.color = new Color(0.49f, 0.467f, 0.824f);
                break;
                
            case Rank.PURRANORMAL:
                chaosIcon.sprite = chaosLvl1Src;
                RankMessage.text = "PURRANORMAL";
                chaosBar.color = new Color(0.741f, 0.169f, 0.502f);
                break;

            case Rank.DEMONIC:
                chaosIcon.sprite = chaosLvl2Src;
                RankMessage.text = "DEMONIC";
                chaosBar.color = new Color(0.98f, 0.263f, 0.263f);
                break;

            case Rank.HELLISH:
                chaosIcon.sprite = chaosLvl2Src;
                RankMessage.text = "HELLISH";
                chaosBar.color = new Color(1, 0.455f, 0.361f);
                break;

            case Rank.CHAOS:
                chaosIcon.sprite = chaosLvl3Src;
                RankMessage.text = "CHAOS";
                chaosBar.color = new Color(1, 0.624f, 0.357f);
                break;

            case Rank.CRAZYCHAOTICCATASTHROPHE:
                chaosIcon.sprite = chaosLvl3Src;
                RankMessage.text = "CRAZY CHAOTIC CATASTROPHE";
                chaosBar.color = new Color(0.96f, 0.749f, 0.435f);
                break;
        }

        writePointLogs();

        if(pointLogs.Count > 0) {
            chaosMessageDecayTimer += Time.deltaTime;
            if (chaosMessageDecayTimer > chaosMessageDecayTime) {
                chaosMessageDecayTimer = 0.0f;
                pointLogs.RemoveAt(0);
            }
        }
        
    }

    public void addPointMessages(string msg) {
        if(pointLogs.Count >= 3) {
            pointLogs.RemoveAt(0);
        }
        pointLogs.Add(msg);
    }

    void writePointLogs() {
        PointMessage.text = "";

        if (pointLogs.Count <= 0) { return; }

        for(int i = 0; i < pointLogs.Count; i++) {
            PointMessage.text += pointLogs[i] + "\n";
        }
    }
}
