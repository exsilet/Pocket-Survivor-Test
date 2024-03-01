using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.UI.Element
{
    public class Restart : MonoBehaviour
    {
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}