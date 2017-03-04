using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoCanvas : MonoBehaviour {
    [SerializeField]
    public Text m_Health;
    [SerializeField]
    public Text m_Armor;
    [SerializeField]
    public Text m_Score;
    [SerializeField]
    public Text m_Stamina;


    private void OnEnable()
    {
        GameController.HealthChanged += HealthChanged;
        GameController.ArmorChanged += ArmorChanged;
        GameController.ScoreChanged += ScoreChanged;
        GameController.StaminaChanged += StaminaChanged;
    }
    private void OnDisable()
    {
        GameController.HealthChanged -= HealthChanged;
        GameController.ArmorChanged -= ArmorChanged;
        GameController.ScoreChanged -= ScoreChanged;
        GameController.StaminaChanged -= StaminaChanged;
    }

    private void Start()
    {
        m_Health.text = GameController.instance.Health.ToString();
    }
    void HealthChanged(int val)
    {
        m_Health.text = val.ToString();
    }

    void ArmorChanged(int val)
    {
        m_Armor.text = val.ToString();
    }

    void ScoreChanged(int val)
    {
        m_Score.text = val.ToString();
    }

    void StaminaChanged(float val)
    {
        m_Stamina.text = val.ToString();
    }
}
