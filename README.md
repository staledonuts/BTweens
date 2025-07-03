![alt text](https://github.com/staledonuts/Deaddonut-se/blob/main/DeadDonuts-Corp-banner.png "DeadDonuts Corp")

# BTweens v0.3.1 - Ignore Timescale Edition.

```
Latest feature:
- Added the possibility to ignore delta time.
- Re-worked backend to use string hashes for keys as part of a optimization pass.
```

Note: This is a early version package, currently being used in a personal project. You are welcome to contribute and add to the package if you want. the only rule is to keep it slim and simple if you want to add tons of extensions and features that are specific to other packages and or tools, we can add a Link to your repository as a recommendation.

Async Tween Engine for Unity
A lightweight, and easy-to-use asynchronous tweening engine for Unity, built on top of [UniTask](https://github.com/Cysharp/UniTask). This package provides a extension-method-based API to animate properties of GameObjects and UI elements with minimal setup.

Core Features
Async: Leverages Cysharp.Threading.Tasks (UniTask) for non-blocking, efficient animations.

Lifecycle Management: Tweens are automatically cancelled when the target GameObject is destroyed, preventing common errors and memory leaks.

Extension Methods: Animate components with a single line of code. Instead of writing complex coroutines, just call an extension method like .TweenLocalPosition(...) or .TweenAlpha(...).

Wide Component Support: Out-of-the-box support for animating common properties on:

Transform (position, rotation, scale)

RectTransform (anchoredPosition, sizeDelta)

CanvasGroup (alpha/fade)

Image (color)

AudioSource (volume)

Material (float properties)

...and more.

Specialized UI Helpers: Includes convenient methods like TweenToOffScreen which automatically calculates the target position to move a UI element just outside the canvas bounds.

## 🚀 How to Use
The easiest way to use BTween is through the provided extension methods on common Unity components like Transform, CanvasGroup, and RectTransform.

Basic Example: Fading and Scaling UI
This example shows how to fade in a UI panel and apply an elastic scaling effect to a title element.

```C#
using UnityEngine;
using Cysharp.Threading.Tasks;

public class SimpleUIAnimator : MonoBehaviour
{
    public CanvasGroup panelCanvasGroup;
    public Transform titleTransform;

    void Start()
    {
        // Set initial states
        panelCanvasGroup.alpha = 0f;
        titleTransform.localScale = Vector3.zero;

        // --- Fire-and-forget animation ---
        // Animate the alpha of the CanvasGroup to 1 over 0.5 seconds.
        panelCanvasGroup.TweenAlpha(1f, 0.5f);

        // Animate the local scale of the title using a built-in elastic ease.
        // The tween runs for 0.7 seconds with a 0.2 second delay before starting.
        titleTransform.TweenLocalScale(Vector3.one, 0.7f, easeFunction: BTween.Ease.OutElastic)
                      .Forget(); // Use .Forget() if not awaiting
    }
}
```
---
## Chaining Animations with async/await
Because BTween is built on UniTask, you can await any tween. This makes it trivial to create sequential animations.

In this example, an AnimateOut() method fades out a panel and then, once the fade is complete, moves it off-screen.
```C#
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class SequentialAnimator : MonoBehaviour
{
    public CanvasGroup panelCanvasGroup;
    public Button myButton;

    void Start()
    {
        // When the button is clicked, start the animation sequence.
        myButton.onClick.AddListener(() => AnimateOut().Forget());
    }

    public async UniTask AnimateOut()
    {
        Debug.Log("Starting animation sequence...");

        // 1. Await the alpha tween. The code will pause here until it's done.
        await panelCanvasGroup.TweenAlpha(0f, 0.3f, easeFunction: BTween.Ease.InQuad);

        // 2. This line will only execute AFTER the alpha tween is complete.
        // The RectTransform extension requires an OffScreenDirection enum.
        await panelCanvasGroup.transform.GetComponent<RectTransform>()
                              .TweenToOffScreen(OffScreenDirection.Bottom, 0.5f, easeFunction: BTween.Ease.InOutCubic);

        Debug.Log("Animations complete! Disabling object.");
        panelCanvasGroup.gameObject.SetActive(false);
    }
}
```
---
## Stopping a Tween
You can manually stop a running tween using BTween.StopTween(). The extension methods automatically assign a tweenIdentifierTag based on the property being animated (e.g., "Alpha", "LocalScale", "Position").

```C#
public void StopPanelFade()
{
    // Stops the alpha tween specifically on the panelCanvasGroup object.
    BTween.StopTween(panelCanvasGroup, "Alpha");
}

public void StopAllTweens()
{
    // Stops every tween managed by the system.
    BTween.StopAndClearAllManagedTweens();
}
```
---
## Using the Static API (Advanced)
For custom tweens or when you don't have an extension method, you can use the core static API directly. You must provide an owner, a unique tweenIdentifierTag, and a setter action.
```C#
using UnityEngine;
using UnityEngine.UI;

public class HealthBarAnimator : MonoBehaviour
{
    public Slider healthSlider;

    // Animate the slider value from its current value to a new target.
    public void UpdateHealth(float newHealthValue)
    {
        BTween.Float(
            owner: this, // The MonoBehaviour instance owns the tween.
            tweenIdentifierTag: "HealthSlider", // A unique name for this tween.
            startValue: healthSlider.value,
            endValue: newHealthValue,
            duration: 0.4f,
            setter: (value) => healthSlider.value = value, // The action that applies the value.
            easeFunction: BTween.Ease.OutQuad
        );
    }
}
```
