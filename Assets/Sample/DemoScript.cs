using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A demonstration component that controls a simple dialogue UI panel.
/// It uses the custom Tween system to slide the panel in and out from the left side of the screen.
/// </summary>
public class DemoScript : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("The main UI panel for the dialogue, which will be moved.")]
    [SerializeField] private RectTransform _dialoguePanel;

    [Tooltip("The button that triggers the show/hide animation.")]
    [SerializeField] private Button _toggleButton;

    [SerializeField] private Canvas _mainCanvas;

    [Header("Animation Settings")]
    [Tooltip("The duration of the slide animation in seconds.")]
    [SerializeField] private float _animationDuration = 0.4f;

    // A variable to store the initial on-screen position of the dialogue panel.
    private Vector2 _initialAnchoredPosition;

    // A flag to track the current state of the panel (on-screen or off-screen).
    private bool _isPanelVisible = true;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// We use Awake to store the initial state and set up the button listener.
    /// </summary>
    private void Awake()
    {
        if (_dialoguePanel == null || _toggleButton == null)
        {
            Debug.LogError("DialogueUIView: Please assign the Dialogue Panel and Toggle Button in the Inspector.", this);
            return;
        }

        // Store the starting on-screen position of the panel.
        _initialAnchoredPosition = _dialoguePanel.anchoredPosition;

        // Add a listener to the button's onClick event, so our TogglePanel method is called when clicked.
        _toggleButton.onClick.AddListener(TogglePanel);
    }

    /// <summary>
    /// Toggles the visibility of the dialogue panel, animating it on or off the screen.
    /// This method is called by the toggleButton's onClick event.
    /// </summary>
    public void TogglePanel()
    {
        // Toggle the visibility state.
        _isPanelVisible = !_isPanelVisible;

        if (_isPanelVisible)
        {
            // If the panel should be visible, tween it back to its original on-screen position.
            // We use the TweenAnchoredPosition extension method from your package.
            // We also use the OutCubic ease for a smooth deceleration effect.
            _dialoguePanel.TweenAnchoredPosition(
                _initialAnchoredPosition, 
                _animationDuration, 
                easeFunction: BTween.Ease.OutCubic
            );
        }
        else
        {
            // If the panel should be hidden, tween it to the left, just off-screen.
            // We use the TweenToOffScreen extension method, which is perfect for this.
            // We use the InCubic ease for a smooth acceleration effect as it leaves the screen.
            _dialoguePanel.TweenToOffScreen(
                OffScreenDirection.Left, 
                _animationDuration, 
                easeFunction: BTween.Ease.InCubic
            );
        }
    }

    /// <summary>
    /// A good practice to remove the listener when the object is destroyed to prevent memory leaks.
    /// </summary>
    private void OnDestroy()
    {
        if (_toggleButton != null)
        {
            _toggleButton.onClick.RemoveListener(TogglePanel);
        }
    }
}
