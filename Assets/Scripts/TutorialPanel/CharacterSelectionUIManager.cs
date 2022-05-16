using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TutorialPanel;

public class CharacterSelectionUIManager : MonoBehaviour
{   
    public TutorialPanelHolder _tutorialPanel;
    public Button backButton;
    public Button playButton;

    // Start is called before the first frame update
    void Start()
    {
        _tutorialPanel.gameObject.SetActive(true);
        _tutorialPanel.OnClose.AddListener(ClostTutorial);
        backButton.interactable = false;
        playButton.interactable = false;
    }

    public void ClostTutorial() {
        
        backButton.interactable = true;
        playButton.interactable = true;
    }
}
