using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public RuntimeDialogueGraph RuntimeGraph;

    [Header("UI Components")]

    public GameObject DialoguePanel;
    public TextMeshProUGUI SpeakerNameText;
    public TextMeshProUGUI DialogueText;

    [Header("Choice Button UI")]

    public Button ChoiceButtonPrefab;
    public Transform ChoiceButtonContainer;

    private Dictionary<string, RuntimeDialogueNode> _nodeLookup = new Dictionary<string, RuntimeDialogueNode>();
    private RuntimeDialogueNode _currentNode;

    [Header("Visuals")]

    public Image BackgroundImage;
    public GameObject CharacterPanel;
    public Image CharacterImage;
    private string _currentCharacterName;
    private string _currentCharacterPose;

    public Dictionary<string, Sprite> Backgrounds;
    public Dictionary<string, Sprite> Characters;

    private void Start()
    {
        Backgrounds = new Dictionary<string, Sprite>
        {
                //Cabin BG
            { "MainBG", Resources.Load<Sprite>("Backgrounds/MainBG") },
            { "MainCabinBG_Night", Resources.Load<Sprite>("Backgrounds/MainCabinBG_Night") },

                //Evil BG
            { "MainCabinBG_Evil1", Resources.Load<Sprite>("Backgrounds/MainCabinBG_Evil1") },
            { "MainCabinBG_Evil2", Resources.Load<Sprite>("Backgrounds/MainCabinBG_Evil2") },
            { "MainCabinBG_Evil3", Resources.Load<Sprite>("Backgrounds/MainCabinBG_Evil3") },
            { "MainCabinBG_Evil4", Resources.Load<Sprite>("Backgrounds/MainCabinBG_Evil4") },
            { "MainCabinBG_Evil5", Resources.Load<Sprite>("Backgrounds/MainCabinBG_Evil5") },
            { "MainCabinBG_Evil6", Resources.Load<Sprite>("Backgrounds/MainCabinBG_Evil6") },

                //Good BG
            { "MainCabinBG_Good1", Resources.Load<Sprite>("Backgrounds/MainCabinBG_Good1") },
            { "MainCabinBG_Good2", Resources.Load<Sprite>("Backgrounds/MainCabinBG_Good2") },
            { "MainCabinBG_Good3", Resources.Load<Sprite>("Backgrounds/MainCabinBG_Good3") },
            { "MainCabinBG_Good4", Resources.Load<Sprite>("Backgrounds/MainCabinBG_Good4") },
            { "MainCabinBG_Good5", Resources.Load<Sprite>("Backgrounds/MainCabinBG_Good5") },

                //Foresst BG
            { "MainForesstBG", Resources.Load<Sprite>("Backgrounds/MainForesstBG") },
            { "MainForesstBG_Bloody", Resources.Load<Sprite>("Backgrounds/MainForesstBG_Bloody") },
            { "ForesstBG_PurpleTone", Resources.Load<Sprite>("Backgrounds/ForesstBG_PurpleTone") },

        };

        Characters = new Dictionary<string, Sprite>
        {
                //Character BG
            { "Needle_Angry", Resources.Load<Sprite>("Characters/Needle_Angry") },
            { "Needle_Base", Resources.Load<Sprite>("Characters/Needle_Base") },
            { "Needle_Happy", Resources.Load<Sprite>("Characters/Needle_Happy") },
            { "Needle_Injured", Resources.Load<Sprite>("Characters/Needle_Injured") },

        };


        foreach (var node in RuntimeGraph.AllNodes)
        {
            _nodeLookup[node.NodeID] = node;
        }

        if (!string.IsNullOrEmpty(RuntimeGraph.EntryNodeID))
        {
            ShowNode(RuntimeGraph.EntryNodeID);
        }
        else
        {
            EndDialogue();
        }
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && _currentNode != null && _currentNode.Choices.Count == 0)
        {
            if (!string.IsNullOrEmpty(_currentNode.NextNodeID))
            {
                ShowNode(_currentNode.NextNodeID);
            }
            else
            {
                EndDialogue();
            }
        }
    }

    private void ShowNode(string nodeID)
    {
        if (!_nodeLookup.ContainsKey(nodeID))
        {
            EndDialogue();
            return;
        }

        _currentNode = _nodeLookup[nodeID];

        DialoguePanel.SetActive(true);
        SpeakerNameText.SetText(_currentNode.SpeakerName);
        DialogueText.SetText(_currentNode.DialogueText);

        foreach (Transform child in ChoiceButtonContainer)
        {
            Destroy(child.gameObject);
        }

        if (_currentNode.Choices.Count > 0)
        {
            foreach (var choice in _currentNode.Choices)
            {
                Button button = Instantiate(ChoiceButtonPrefab, ChoiceButtonContainer);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = choice.ChoiceText;
                }

                if (button != null)
                {
                    button.onClick.AddListener(() =>
                    {
                        if (!string.IsNullOrEmpty(choice.DestinationNodeID))
                        {
                            ShowNode(choice.DestinationNodeID);
                        }
                        else
                        {
                            EndDialogue();
                        }
                    });
                }
            }
        }

        if (!string.IsNullOrEmpty(_currentNode.BackgroundName))
        {
            if (Backgrounds.TryGetValue(_currentNode.BackgroundName, out var bgSprite))
            {
                BackgroundImage.sprite = bgSprite;
                BackgroundImage.enabled = true;
            }
        }

        if (!string.IsNullOrEmpty(_currentNode.CharacterName))
        {
            
            _currentCharacterName = _currentNode.CharacterName;
            _currentCharacterPose = _currentNode.CharacterExpression;
        }

        
        if (!string.IsNullOrEmpty(_currentCharacterName))
        {
            CharacterPanel.SetActive(true);

            string key = _currentCharacterName;
            if (!string.IsNullOrEmpty(_currentCharacterPose))
                key += $"_{_currentCharacterPose}";

            if (Characters.TryGetValue(key, out var charSprite))
            {
                CharacterImage.sprite = charSprite;
                CharacterImage.enabled = true;

               
            }
            else
            {   
                CharacterImage.enabled = false;
            }
        }
        else
        {
            CharacterPanel.SetActive(false);
        }

        if (_currentNode.CharacterName == "HIDE")
        {
            _currentCharacterName = null;
            _currentCharacterPose = null;
            CharacterPanel.SetActive(false);
            return;
        }

    }

    private void EndDialogue()
    {
        DialoguePanel.SetActive(true);
        _currentNode = null;

        foreach (Transform child in ChoiceButtonContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
