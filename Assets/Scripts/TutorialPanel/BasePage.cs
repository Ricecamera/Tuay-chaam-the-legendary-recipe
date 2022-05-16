using UnityEngine;
using UnityEngine.Video;

namespace TutorialPanel {
    class BasePage : ScriptableObject {
        public string heading;
        [TextArea]
        public string description;
    }
}
