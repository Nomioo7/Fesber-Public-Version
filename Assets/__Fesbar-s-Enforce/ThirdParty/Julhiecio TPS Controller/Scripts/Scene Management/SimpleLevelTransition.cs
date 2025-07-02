using UnityEngine;
using UnityEngine.Events;
namespace JUTPS.Utilities
{
    [AddComponentMenu("JU TPS/Scene Management/Trigger Load Level")]
    public class SimpleLevelTransition : MonoBehaviour
    {

        public UnityEvent OnExit;

        private void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Player")
            {
                OnExit?.Invoke();
                EnableCursor();

            }
        }

        public void EnableCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
