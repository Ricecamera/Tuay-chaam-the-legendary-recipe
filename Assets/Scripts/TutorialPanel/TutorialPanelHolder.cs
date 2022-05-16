using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Video;
using TMPro;

namespace TutorialPanel {
    public class TutorialPanelHolder : MonoBehaviour
    {
        public UnityEvent OnClose = new UnityEvent();
        [SerializeField]
        private TextMeshProUGUI heading, description, indexText;

        [SerializeField]
        private VideoPlayer mainPlayer;
        [SerializeField]
        private RawImage showedImage;

        [SerializeField]
        private List<BasePage> playlist;
    
        private int currentIndex;
        private int maxIndex;
        // Start is called before the first frame update
        void Start()
        {
            currentIndex = 0;
            maxIndex = playlist.Count;
            UpdatePage(currentIndex);
        }

        public void PlayNextTutorial() {
            if (currentIndex + 1 < maxIndex) {
                ClearPageData();
                currentIndex++;
                UpdatePage(currentIndex);
            }
        }

        public void PlayPreviousTutorial() {
            if (currentIndex > 0) {
                ClearPageData();
                currentIndex--;
                UpdatePage(currentIndex);
            }
        }

        public void CloseTutorial() {
            OnClose.Invoke();
            gameObject.SetActive(false);
        }

        private void UpdatePage(int Index) {
            indexText.text = (Index + 1) + " / " + maxIndex;

            if (playlist[Index] is ImagePage) {

                // show the image and hide the video player
                
                showedImage.gameObject.SetActive(true);

                ImagePage imagePage = (ImagePage)playlist[Index];

                if (imagePage.image != null) {
                    showedImage.texture = imagePage.image;
                }
                else {
                    Debug.LogError("The image is not available");
                }

                // set new heading and text
                description.text = imagePage.description;
                heading.text = imagePage.heading;

            }
            else if (playlist[Index] is VideoPage) {
                // show the video player and hide the image
                mainPlayer.gameObject.SetActive(true);
                

                VideoPage videoPage = (VideoPage) playlist[Index];

                if (videoPage.clip != null) {
                    // set new video and play it
                    mainPlayer.clip = videoPage.clip;
                    mainPlayer.Play();
                }
                else {
                    Debug.LogError("The video clip is not available");
                }

                // set new heading and text
                description.text = videoPage.description;
                heading.text = videoPage.heading;
                

               
            }
            else {
                // hide both the video player and the image
                mainPlayer.gameObject.SetActive(false);
                showedImage.gameObject.SetActive(false);

                description.text = playlist[currentIndex].description;
                heading.text = playlist[currentIndex].heading;
            }
        }

        private void ClearPageData() {
            if (playlist[currentIndex] is ImagePage) {
                // clear old image sprite
                showedImage.texture = null;
                showedImage.gameObject.SetActive(false);
            }
            else if (playlist[currentIndex] is VideoPage) {
                // clear old video
                mainPlayer.Stop();
                mainPlayer.gameObject.SetActive(false);
            }
        }

    }

}