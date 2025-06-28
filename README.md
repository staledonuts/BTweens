#BTweens

Async Tween Engine for Unity
A lightweight, and easy-to-use asynchronous tweening engine for Unity, built on top of UniTask. This package provides a extension-method-based API to animate properties of GameObjects and UI elements with minimal setup.

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
