using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class TutorialPanelHolder : MonoBehaviour
{
    [Serializable]
    struct PageData {
        public VideoClip clipToPlay;
        public string description;
    }

    [SerializeField]
    private TextMeshProUGUI heading, description, indexText;

    [SerializeField]
    private VideoPlayer mainPlayer;

    [SerializeField]
    private List<PageData> playlist = new List<PageData>();
    
    private int currentIndex;
    private int maxIndex;
    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
        maxIndex = playlist.Count;
        indexText.text = (currentIndex + 1) + " / " + maxIndex;
        
        mainPlayer.clip = playlist[currentIndex].clipToPlay;
        mainPlayer.Play();
        description.text = playlist[currentIndex].description;
    }

    public void PlayNextTutorial() {
        if (currentIndex + 1 < maxIndex) {
            currentIndex++;
            UpdatePage();
        }
    }

    public void PlayPreviousTutorial() {
        if (currentIndex > 0) {
            currentIndex--;
            UpdatePage();
        }
    }

    private void UpdatePage() {
        indexText.text = (currentIndex + 1) + " / " + maxIndex;

        mainPlayer.clip = playlist[currentIndex].clipToPlay;
        mainPlayer.Play();
        description.text = playlist[currentIndex].description;
    }
}
