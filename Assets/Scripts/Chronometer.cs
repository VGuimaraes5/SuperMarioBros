using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chronometer : MonoBehaviour
{
    public float m_MaxTime = 300.0f;
    public float m_ElapsedTime = 0.0f;
    public bool m_FinishLevel = false;
    public string m_Mask = "000";
    private Text m_Text;

    private void Start()
    {
        m_Text = GetComponent<Text>();
        UpdateUI();
    }

    private void Update()
    {
        if (m_FinishLevel)
            return;

        m_ElapsedTime += Time.deltaTime;
        UpdateUI();
    }

    private void UpdateUI()
    {
        m_Text.text = Mathf.Ceil(m_MaxTime - m_ElapsedTime).ToString(m_Mask);
    }
}
