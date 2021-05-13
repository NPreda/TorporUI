using UnityEngine;
using TMPro;

public class NotesController : MonoBehaviour
{
    [SerializeField]private GameObject actBar;
    [SerializeField]private GameObject fieldPanel;
    [SerializeField]private TMP_InputField inputField;
    [SerializeField]private UIButton inputButton;
    [SerializeField]private TextLogControl logControl;

    private UIButtonGroup actButtons = new UIButtonGroup();
    private ButtonFactory buttonFactory = new ButtonFactory();
    private SettingsDB settings;

    public void Start()
    {
        settings = Registry.Instance.settings;
        actButtons.OnButtonPressed += ButtonSelected;

        string languagePrefix = settings.language.ToString();
        TextAsset localizationJson = Resources.Load<TextAsset>("Data/Localization/" + languagePrefix + "/UI/actbuttons");
        ActButtonStruct buttonStruct = JsonUtility.FromJson<ActButtonStruct>(localizationJson.text);

        //create the buttons
        CreateButtons(buttonStruct);
    }

    private void CreateButtons(ActButtonStruct buttonStruct)    //create all the buttons for the act selection
    {
        UIButton newButton = buttonFactory.GetNewInstance(actBar, buttonStruct.act_1, "act_1", "Prefabs/UI/SelectorButton");
        actButtons.Add(newButton);  

        newButton = buttonFactory.GetNewInstance(actBar, buttonStruct.act_2, "act_2", "Prefabs/UI/SelectorButton");
        actButtons.Add(newButton);  
        
        newButton = buttonFactory.GetNewInstance(actBar, buttonStruct.act_3, "act_3", "Prefabs/UI/SelectorButton");
        actButtons.Add(newButton);  

        newButton = buttonFactory.GetNewInstance(actBar, buttonStruct.act_4, "act_4", "Prefabs/UI/SelectorButton");
        actButtons.Add(newButton);  

        inputButton= buttonFactory.GetNewInstance(fieldPanel, buttonStruct.add_note, "add_note", "Prefabs/UI/IndexButton");
        inputButton.OnLeftClickEvent += inputText;
    }

    private void ButtonSelected(UIButton button)
    {
        logControl.SetAct(button.id);
    }

    private void inputText(UIButton button)
    {
        logControl.LogText(inputField.text);
        inputField.text = "";
    }
}
