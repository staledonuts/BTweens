![alt text](https://github.com/staledonuts/Deaddonut-se/blob/main/DeadDonuts-Corp-banner.png "DeadDonuts Corp")

# BTweens v0.3.1 - Ignore Timescale Edition.

```
Latest feature:
- Added the possibility to ignore delta time.
- Re-worked backend to use string hashes for keys as part of a optimization pass.
```

Note: This is an early version package, currently being used in a personal project. You are welcome to contribute and add to the package if you want. The only rule is to keep it slim and simple. If you want to add tons of extensions and features that are specific to other packages and or tools, we can add a link to your repository as a recommendation.

## Async Tween Engine for Unity
A lightweight, and easy-to-use asynchronous tweening engine for Unity, built on top of [UniTask](https://github.com/Cysharp/UniTask). This package provides an extension-method-based API to animate properties of GameObjects and UI elements with minimal setup.

## Core Features
* **Async**: Leverages Cysharp.Threading.Tasks (UniTask) for non-blocking, efficient animations.
* **Lifecycle Management**: Tweens are automatically cancelled when the target GameObject is destroyed, preventing common errors and memory leaks.
* **Optimized**: Uses `uint` hash keys internally for high-performance tween identification, avoiding string comparisons in hotspots.
* **Extension Methods**: Animate components with a single line of code. Instead of writing complex coroutines, just call an extension method like `.TweenLocalPosition(...)` or `.TweenAlpha(...)`.
* **Wide Component Support**: Out-of-the-box support for animating common properties on Transforms, RectTransforms, CanvasGroups, Images, and more.
* **Specialized UI Helpers**: Includes convenient methods like `TweenToOffScreen` which automatically calculates the target position to move a UI element just outside the canvas bounds.

## 🚀 How to Use
The easiest way to use BTween is through the provided extension methods on common Unity components.

### Basic Example: Fading and Scaling UI
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

        // Animate the alpha of the CanvasGroup to 1 over 0.5 seconds.
        // This automatically uses an optimized, pre-hashed key for "Alpha".
        panelCanvasGroup.TweenAlpha(1f, 0.5f);

        // Animate the local scale using a built-in elastic ease.
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
        myButton.onClick.AddListener(() => AnimateOut().Forget());
    }

    public async UniTask AnimateOut()
    {
        // 1. Await the alpha tween. The code will pause here until it's done.
        await panelCanvasGroup.TweenAlpha(0f, 0.3f, easeFunction: BTween.Ease.InQuad);

        // 2. This line will only execute AFTER the alpha tween is complete.
        await panelCanvasGroup.transform.GetComponent<RectTransform>()
                              .TweenToOffScreen(OffScreenDirection.Bottom, 0.5f, easeFunction: BTween.Ease.InOutCubic);

        panelCanvasGroup.gameObject.SetActive(false);
    }
}
```
---
## 🚀 Optimizing with Hash Keys
For performance, BTween uses uint hash keys instead of strings to identify tweens.

Automatic Optimization (Extension Methods)
When you use the built-in extension methods like .TweenAlpha() or .TweenLocalPosition(), the library automatically uses pre-calculated const hash keys. This is the most optimized approach, and you don't need to do anything extra.

Stopping Tweens
You can stop a running tween using BTween.StopTween(). For convenience, you can use a string, which will be hashed internally. For maximum performance, you can use the pre-hashed const values from the public TweenPropertyIDs class.
```C#
public void StopPanelFade()
{
    // Convenience method (hashes the string "Alpha" internally):
    BTween.StopTween(panelCanvasGroup, "Alpha");

    // Most performant method (uses the pre-hashed const value):
    BTween.StopTween(panelCanvasGroup, TweenPropertyIDs.Alpha);
}

public void StopAllTweens()
{
    // Stops every tween managed by the system.
    BTween.StopAndClearAllManagedTweens();
}
```
---
## Stopping a Tween
You can stop a running tween using BTween.StopTween(). For convenience, you can use a string, which will be hashed internally. For maximum performance, you can use the pre-hashed const values from the public TweenPropertyIDs class.

```C#
public void StopPanelFade()
{
    // Convenience method (hashes the string "Alpha" internally):
    BTween.StopTween(panelCanvasGroup, "Alpha");

    // Most performant method (uses the pre-hashed const value):
    BTween.StopTween(panelCanvasGroup, TweenPropertyIDs.Alpha);
}

public void StopAllTweens()
{
    // Stops every tween managed by the system.
    BTween.StopAndClearAllManagedTweens();
}
```
---
## Manual Optimization (Static API)
When using the static BTween.Float() API for custom tweens, you can improve performance by providing a uint hash instead of a string.

The BTween.StringHash(string) method is available to create these hashes. For best performance, you should calculate the hash once and cache it in a static readonly field if you use it frequently.
```C#
using UnityEngine;
using UnityEngine.UI;

public class HealthBarAnimator : MonoBehaviour
{
    public Slider healthSlider;

    // Cache the hash once to avoid calculating it repeatedly.
    private static readonly uint healthSliderHash = BTween.StringHash("HealthSlider");

    // Animate the slider value from its current value to a new target.
    public void UpdateHealth(float newHealthValue)
    {
        BTween.Float(
            owner: this,
            tweenIdentifierHash: healthSliderHash, // Use the cached hash.
            startValue: healthSlider.value,
            endValue: newHealthValue,
            duration: 0.4f,
            setter: (value) => healthSlider.value = value,
            easeFunction: BTween.Ease.OutQuad
        );
    }
}
```
