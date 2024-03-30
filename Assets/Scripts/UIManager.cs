using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // REFERENCE TO MAIN PANELS
    // REFERENCE TO SUB-PANELS
    // REFERENCE TO BUTTONS
    // REFERENCE TO TEXT FIELDS
    // ADD ALL LISTENERS 
    // METHOD CALLS TO CHANGE VISIBILITY

    #region PANELS

    [Header("MAIN PANEL REFERENCES")]
    [SerializeField] private GameObject EntryMain_Panel;
    [SerializeField] private GameObject Prompt_Panel;
    [SerializeField] private GameObject Scan_Panel;
    [SerializeField] private GameObject Completion_Panel;

    [Header("SUB PANELS ENTRY PANEL")]
    [SerializeField] private GameObject Options_Panel;
    [SerializeField][Tooltip("This Panels loads for 2 seconds with an animated text")] private GameObject Entry_Panel;
    [SerializeField] private GameObject Main_Panel;
    [SerializeField] private GameObject About_Panel;
    [SerializeField] private GameObject HuntTypePanel;

    [Header("SUB PANEL HUNT ERROR")] 
    [SerializeField] private GameObject HuntErrorText_Panel;

    [Header("SUB PANEL SCAN PANEL")] 
    [SerializeField] private GameObject WrongScan_Panel;

    #endregion
    #region BUTTONS

    [Header("BUTTONS MAIN")]
    [SerializeField] private Button Start_Button;
    [SerializeField] private Button Options_Button;
    [SerializeField] private Button About_Button;
    [SerializeField] private Button MainExit_Button;

    [Header("BUTTONS ABOUT")] 
    [SerializeField] private Button AboutExit_Button;

    [Header("BUTTONS OPTIONS")] 
    [SerializeField] private Button OptionsExit_Button;

    [Header("BUTTONS HUNT TYPE")]
    [SerializeField] private Button HuntOptionOne_Button;
    [SerializeField] private Button HuntOptionTwo_Button;
    [SerializeField] private Button HuntTypeBack_Button;

    [Header("BUTTONS PROMPT PANEL")] 
    [SerializeField] private Button ScanAround_Button;
    [SerializeField] private Button GiveUpPromp_Button;

    [Header("BUTTONS SCAN PANEL")] 
    [SerializeField] private Button GiveUpScanPanel_Button;
    [SerializeField] private Button AnswerOne_Button;
    [SerializeField] private Button AnswerTwo_Button;

    [Header("BUTTON COMPLETION PANEL")] 
    [SerializeField] private Button MainMenu_Button;

    #endregion
    #region TEXTREGION

    [Header("SCAN PANEL TEXT")] 
    [SerializeField] private TextMeshProUGUI Question_Text;
    [SerializeField] private TextMeshProUGUI AnswerOne_Text;
    [SerializeField] private TextMeshProUGUI AnswerTwo_Text;
    [SerializeField] private TextMeshProUGUI OldPrompt_Text;

    [Header("PROMPT PANEL TEXT ")] 
    [SerializeField] private TextMeshProUGUI PointsText_Prompt;
    [SerializeField] private TextMeshProUGUI PromptText_Prompt;
    [SerializeField] private TextMeshProUGUI TimeText_ScanPanel;

    [Header("COMPLETION PANEL TEXT")] 
    [SerializeField] private TextMeshProUGUI Points_Text;
    [SerializeField] private TextMeshProUGUI CompletionTime_Text;
    
    
    

    #endregion

    private string CurrentAnswer = "";


    private void Start()
    {
        NullCheck();
        BindButtonListeners();
        // disable entry main panel
        // enable options panel
        StartCoroutine(PanelControl(EntryMain_Panel, true, 0.0f));
        StartCoroutine(PanelControl(Entry_Panel, false, 2.0f));
        StartCoroutine(PanelControl(Main_Panel, true, 2.0f));
    }

    IEnumerator PanelControl(GameObject Panel,bool state,  float timeWait)
    {
        yield return new WaitForSeconds(timeWait);
        Panel.SetActive(state);
    }

    private void BindButtonListeners()
    {
        Start_Button.onClick.AddListener(StartSelected);
        About_Button.onClick.AddListener(AboutSelected);
        Options_Button.onClick.AddListener(OptionSelected);
        MainExit_Button.onClick.AddListener(MainExitSelected);
        AboutExit_Button.onClick.AddListener(AboutExitSelected);
        OptionsExit_Button.onClick.AddListener(OptionExitSelected);
        HuntTypeBack_Button.onClick.AddListener(HuntExitSelected);
        HuntOptionOne_Button.onClick.AddListener(FirstHuntOption);
        HuntOptionTwo_Button.onClick.AddListener(SecondHuntOption);
        GiveUpPromp_Button.onClick.AddListener(GiveUpPromptSelected);
        ScanAround_Button.onClick.AddListener(ScanSelected);
        GiveUpScanPanel_Button.onClick.AddListener(GiveUpScanSelected);
        MainMenu_Button.onClick.AddListener(MainMenuSelected);
        AnswerOne_Button.onClick.AddListener(AnswerASelected);
        AnswerTwo_Button.onClick.AddListener(AnswerBSelected);
        GiveUpPromp_Button.onClick.AddListener(GiveUpScanningPanel);
        MainMenu_Button.onClick.AddListener(GiveUpScanningPanel);
        GiveUpScanPanel_Button.onClick.AddListener(GiveUpScanningPanel);
 
    }
    
    // updating time 
    // updating points
    public void UpdateTimerText(float time)
    {
        TimeText_ScanPanel.text = time.ToString("00:00");
    }

    public void UpdateOldText(float score)
    {
        OldPrompt_Text.text = "POINTS: " + score.ToString();
    }

    public void UpdatePromptPoint(int score)
    {
        PointsText_Prompt.text = "POINTS: " + score.ToString();
    }

    public void UpdateFinalTimeText(float time)
    {
        CompletionTime_Text.text =   "COMPLETION \nTIME \n" +  "<color=green>" + time.ToString("00:00") + "</color>";
    }

    public void UpdateFinalScoreText(int score)
    {
        Points_Text.text = "TOTAL POINTS \n" + "<color=green>" + score.ToString() + "</color>";
    }

    public void UpdatePoints(int score)
    {
        Points_Text.text = score.ToString();
    }
    
    public String GetCurrentAnswer()
    {
        return CurrentAnswer ?? "";
    }
    
    // enables previous menus
    public void RestartScanningPanel()
    {
        Prompt_Panel.SetActive(true);
        Scan_Panel.SetActive(false);
        Completion_Panel.SetActive(false);
    }

    public void GiveUpScanningPanel()
    {
        EntryMain_Panel.SetActive(true);
        Prompt_Panel.SetActive(false);
        Scan_Panel.SetActive(false);
        Completion_Panel.SetActive(false);
    }
    
    // setting this up for testing purpose only
    public void  CongratPageSelected()
    {
        Completion_Panel.SetActive(true);
        Scan_Panel.SetActive(false);
    }

    public void AddListerAnswerOne(UnityAction method)
    {
        AnswerOne_Button.onClick.AddListener(method);
    }

    public void AddListerAnswerTwo(UnityAction method)
    {
        AnswerTwo_Button.onClick.AddListener(method);
    }
    
    /// ends here 
    /// <summary>
    /// Shall update the prompt text 
    /// </summary>
    /// <param name="prompt"></param>
    public void UpdatePrompt(String prompt)
    {
        PromptText_Prompt.text = prompt;
    }
    
    /// <summary>
    /// shall update the questions
    /// </summary>
    /// <param name="question"></param>
    public void UpdateQuestion(string question)
    {
        Question_Text.text = question;
    }

    
    /// <summary>
    /// Updates the answers being displayed
    /// </summary>
    /// <param name="answerOne"></param>
    /// <param name="answerTwo"></param>
    public void UpdateAnswers(string answerOne, string answerTwo)
    {
        AnswerOne_Text.text = answerOne;
        AnswerTwo_Text.text = answerTwo;
    }

    public void AddListerHuntType(UnityAction method)
    {
        HuntOptionOne_Button.onClick.AddListener(method);
    }

    
    /// <summary>
    /// allows manager to add other method calls to Scan Button
    /// </summary>
    /// <param name="method"></param>
    public void AddListerScanButton(UnityAction method)
    {
        ScanAround_Button.onClick.AddListener(method);
    }


    public void AddListerResetButton(UnityAction method)
    {
        MainMenu_Button.onClick.AddListener(method);
        GiveUpPromp_Button.onClick.AddListener(method);
        GiveUpScanPanel_Button.onClick.AddListener(method);
    }
 
    private void AnswerASelected()
    {
        CurrentAnswer = AnswerOne_Text.text;
    }

    private void AnswerBSelected()
    {
        CurrentAnswer = AnswerTwo_Text.text;
    }
    
    private void MainMenuSelected()
    {
        Prompt_Panel.SetActive(false);
        Scan_Panel.SetActive(false);
        Completion_Panel.SetActive(false);
        EntryMain_Panel.SetActive(true);
        Main_Panel.SetActive(true);
    }

    private void ScanSelected()
    {
        Scan_Panel.SetActive(true);
        Prompt_Panel.SetActive(false);
        EntryMain_Panel.SetActive(false);
    }

    private void GiveUpPromptSelected()
    {
        Prompt_Panel.SetActive(false);
        Main_Panel.SetActive(true);
    }
    private void GiveUpScanSelected()
    {
        Scan_Panel.SetActive(false);
        EntryMain_Panel.SetActive(true);
        Main_Panel.SetActive(true);
        
    }
    private void FirstHuntOption()
    {
        HuntTypePanel.SetActive(false);
        Prompt_Panel.SetActive(true);
    }
    private void SecondHuntOption()
    {
        HuntErrorText_Panel.SetActive(true);
        StartCoroutine(PanelControl(HuntErrorText_Panel, false, 3.0f));
    }
    private void HuntExitSelected()
    {
        Main_Panel.SetActive(true);
        HuntTypePanel.SetActive(false);
    }
    private void StartSelected()
    {
        Main_Panel.SetActive(false);
        HuntTypePanel.SetActive(true);
    }
    private void AboutSelected()
    {
        Main_Panel.SetActive(false);
        About_Panel.SetActive(true);
    }
    private void OptionSelected()
    {
        Main_Panel.SetActive(false);
        Options_Panel.SetActive(true);
    }
    private void OptionExitSelected()
    {
        Options_Panel.SetActive(false);
        Main_Panel.SetActive(true);
    }
    private void AboutExitSelected()
    {
        About_Panel.SetActive(false);
        Main_Panel.SetActive(true);
    }
    private void MainExitSelected()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();

    }
    private void NullCheck()
    {
        // panels
        Assert.IsNotNull(EntryMain_Panel);
        Assert.IsNotNull(Prompt_Panel);
        Assert.IsNotNull(Scan_Panel);
        Assert.IsNotNull(Completion_Panel);
        Assert.IsNotNull(Options_Panel);
        Assert.IsNotNull(Entry_Panel);
        Assert.IsNotNull(Main_Panel);
        Assert.IsNotNull(About_Panel);
        Assert.IsNotNull(HuntTypePanel);
        Assert.IsNotNull(HuntErrorText_Panel);
        Assert.IsNotNull(WrongScan_Panel);
        
        // buttons
        Assert.IsNotNull(Start_Button);
        Assert.IsNotNull(Options_Button);
        Assert.IsNotNull(About_Button);
        Assert.IsNotNull(MainExit_Button);
        Assert.IsNotNull(AboutExit_Button);
        Assert.IsNotNull(OptionsExit_Button);
        Assert.IsNotNull(HuntOptionOne_Button);
        Assert.IsNotNull(HuntOptionTwo_Button);
        Assert.IsNotNull(HuntTypeBack_Button);
        Assert.IsNotNull(ScanAround_Button);
        Assert.IsNotNull(GiveUpPromp_Button);
        Assert.IsNotNull(GiveUpScanPanel_Button);
        Assert.IsNotNull(AnswerOne_Button);
        Assert.IsNotNull(AnswerTwo_Button);
        Assert.IsNotNull(MainMenu_Button);
        
        // text
        Assert.IsNotNull(Question_Text);
        Assert.IsNotNull(AnswerOne_Text);
        Assert.IsNotNull(AnswerTwo_Text);
        Assert.IsNotNull(PointsText_Prompt);
        Assert.IsNotNull(PromptText_Prompt);
        Assert.IsNotNull(Points_Text);
        Assert.IsNotNull(CompletionTime_Text);
        Assert.IsNotNull(TimeText_ScanPanel);
    }
}
