![alt text](https://github.com/staledonuts/Deaddonut-se/blob/main/docs/images/DeadDonuts-Corp-banner.png "DeadDonuts Corp")

# BTweens v0.1 - Initial release.

Note: This is a early version package, currently being used in a personal project. You are welcome to contribute and add to the package if you want. the only rule is to keep it slim and simple.

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