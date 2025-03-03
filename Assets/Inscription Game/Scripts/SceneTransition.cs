using UnityEngine;

namespace BiffeProd
{
    public class SceneTransition : MonoBehaviour
    {
        public string sceneName = "Scene Name Here...";
        public float transitionDuration = 1.0f;
        public Color color ;

        public void PerformTransition()
        {
            CustomTransitionManager.LoadLevelWithTransition(sceneName, transitionDuration, color);
        }
    }
}


