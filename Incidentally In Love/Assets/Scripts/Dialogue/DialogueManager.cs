using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    // smaller = faster
    [SerializeField] private float wpm;


    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI dialogueNameText;
    [SerializeField] private Animator imageAnimator;
    [SerializeField] private Animator leftorrightAnimator;

    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private bool readingTextIsFinished = false;

    private Coroutine displayLineCoroutine;

    private static DialogueManager instance;

    // ink tags for dialoguebox
    private const string CHARACTER_TAG = "character";
    private const string IMAGE_TAG = "image";
    private const string LEFTORRIGHT = "leftorright";

    private void Awake() 
    {
        instance = this;
    }

    public static DialogueManager GetInstance() 
    {
        return instance;
    }

    private void Start() 
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        leftorrightAnimator = dialoguePanel.GetComponent<Animator>();

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices) 
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update() 
    {
        if (dialogueIsPlaying == false) 
        {
            return;
        }
        
        if (readingTextIsFinished && currentStory.currentChoices.Count == 0 && InputManager.GetInstance().GetSubmitPressed())
        {
            ContinueStory();
        }
    }

    public void LoadMenuScene() 
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -2);
    }
    public void EnterDialogueMode(TextAsset inkJSON) 
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode() 
    {
        yield return new WaitForSeconds(0.2f);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = null;
        LoadMenuScene();
    }

    private void ContinueStory() 
    {
        if (currentStory.canContinue) 
        {
            if (displayLineCoroutine != null) 
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            EditDialogueBox(currentStory.currentTags);
        }
        else 
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    public bool DialogueManagerStared() 
    {
        return true;
    }

    private IEnumerator DisplayLine(string line) 
    {
        
        continueIcon.SetActive(false);
        RemoveChoices();
        readingTextIsFinished = false;

        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.maxVisibleCharacters++;
            yield return new WaitForSeconds(wpm);
        }

        continueIcon.SetActive(true);
        DisplayChoices();
        readingTextIsFinished = true;
    }

    
    private void RemoveChoices() 
    {
        foreach (GameObject choiceButton in choices) 
        {
            choiceButton.SetActive(false);
        }
    }

    private void EditDialogueBox(List<string> currentTags)
    {
        foreach (string tag in currentTags) 
        {

            string[] cutTag = tag.Split(':');
            string tagKey = cutTag[0].Trim();
            string tagValue = cutTag[1].Trim();
            
            switch (tagKey) 
            {
                case CHARACTER_TAG:
                    dialogueNameText.text = tagValue;
                    break;
                case IMAGE_TAG:
                    imageAnimator.Play(tagValue);
                    break;
                case LEFTORRIGHT:
                    leftorrightAnimator.Play(tagValue);
                    break;
                default:
                    break;
            }
        }
    }

    private void DisplayChoices() 
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        int index = 0;
        foreach(Choice choice in currentChoices) 
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        StartCoroutine(SelectChoice());
    }

    private IEnumerator SelectChoice() 
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
            currentStory.ChooseChoiceIndex(choiceIndex);
            InputManager.GetInstance().RegisterSubmitPressed();
            ContinueStory();
    }

}
