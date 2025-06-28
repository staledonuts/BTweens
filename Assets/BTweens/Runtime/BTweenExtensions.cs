using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class TweenExtensions
{
    /// <summary>
    /// Tweens the volume of an AudioSource to a target value over a specified duration.
    /// </summary>
    /// <param name="audioSource">The AudioSource to modify.</param>
    /// <param name="targetVolume">The target volume to tween to (0-1).</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween.</param>
    /// <param name="onCompleteDelay">A delay in seconds before the onComplete action is invoked.</param>
    public static UniTask TweenVolume(this AudioSource audioSource, float targetVolume, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f)
    {
        if (audioSource == null) return UniTask.CompletedTask;
        return BTween.Float(audioSource, "Volume", vol => audioSource.volume = vol, audioSource.volume, targetVolume, duration, onComplete, easeFunction, onCompleteDelay);
    }
#if BroAudio
    /// <summary>
    /// Tweens the volume of an IAudioPlayer (BroAudio) to a target value over a specified duration.
    /// </summary>
    /// <param name="audioPlayer">The IAudioPlayer instance to modify.</param>
    /// <param name="targetVolume">The target volume to tween to.</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween.</param>
    /// <param name="onCompleteDelay">A delay in seconds before the onComplete action is invoked.</param>
    public static UniTask TweenVolume(this IAudioPlayer audioPlayer, float targetVolume, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f)
    {
        if (audioPlayer == null) return UniTask.CompletedTask;
        return Tween.Float(audioPlayer, "Volume", vol => audioPlayer.SetVolume(vol), audioPlayer.CurrentPlayingClip.Volume, targetVolume, duration, onComplete, easeFunction, onCompleteDelay);
    }
#endif

    /// <summary>
    /// Tweens the alpha of a CanvasGroup to a target value over a specified duration.
    /// </summary>
    /// <param name="canvasGroup">The CanvasGroup to modify.</param>
    /// <param name="targetAlpha">The target alpha value to tween to (0-1).</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween.</param>
    /// <param name="onCompleteDelay">A delay in seconds before the onComplete action is invoked.</param>
    public static UniTask TweenAlpha(this CanvasGroup canvasGroup, float targetAlpha, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f)
    {
        if (canvasGroup == null) return UniTask.CompletedTask;
        return BTween.Float(canvasGroup, "Alpha", alpha => canvasGroup.alpha = alpha, canvasGroup.alpha, targetAlpha, duration, onComplete, easeFunction, onCompleteDelay);
    }

    /// <summary>
    /// Tweens the local position of a Transform to a target position over a specified duration.
    /// </summary>
    /// <param name="transform">The Transform to modify.</param>
    /// <param name="targetPosition">The target local position to tween to.</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween.</param>
    /// <param name="onCompleteDelay">A delay in seconds before the onComplete action is invoked.</param>
    public static UniTask TweenLocalPosition(this Transform transform, Vector3 targetPosition, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f)
    {
        if (transform == null) return UniTask.CompletedTask;
        return BTween.Vector3(transform, "LocalPosition", pos => transform.localPosition = pos, transform.localPosition, targetPosition, duration, onComplete, easeFunction, onCompleteDelay);
    }

    /// <summary>
    /// Tweens the world position of a Transform to a target position over a specified duration.
    /// </summary>
    /// <param name="transform">The Transform to modify.</param>
    /// <param name="targetPosition">The target world position to tween to.</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween.</param>
    /// <param name="onCompleteDelay">A delay in seconds before the onComplete action is invoked.</param>
    public static UniTask TweenPosition(this Transform transform, Vector3 targetPosition, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f)
    {
        if (transform == null) return UniTask.CompletedTask;
        return BTween.Vector3(transform, "Position", pos => transform.position = pos, transform.position, targetPosition, duration, onComplete, easeFunction, onCompleteDelay);
    }

    /// <summary>
    /// Tweens the local scale of a Transform to a target scale over a specified duration.
    /// </summary>
    /// <param name="transform">The Transform to modify.</param>
    /// <param name="targetScale">The target local scale to tween to.</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween.</param>
    /// <param name="onCompleteDelay">A delay in seconds before the onComplete action is invoked.</param>
    public static UniTask TweenLocalScale(this Transform transform, Vector3 targetScale, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f)
    {
        if (transform == null) return UniTask.CompletedTask;
        return BTween.Vector3(transform, "LocalScale", scale => transform.localScale = scale, transform.localScale, targetScale, duration, onComplete, easeFunction, onCompleteDelay);
    }

    /// <summary>
    /// Tweens the local rotation of a Transform to a target rotation over a specified duration.
    /// </summary>
    /// <param name="transform">The Transform to modify.</param>
    /// <param name="targetRotation">The target local rotation (as a Quaternion) to tween to.</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween.</param>
    /// <param name="onCompleteDelay">A delay in seconds before the onComplete action is invoked.</param>
    public static UniTask TweenLocalRotation(this Transform transform, Quaternion targetRotation, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f)
    {
        if (transform == null) return UniTask.CompletedTask;
        return BTween.Quaternion(transform, "LocalRotation", rot => transform.localRotation = rot, transform.localRotation, targetRotation, duration, onComplete, easeFunction, onCompleteDelay);
    }

    /// <summary>
    /// Tweens the local euler angles of a Transform to a target set of angles over a specified duration.
    /// </summary>
    /// <param name="transform">The Transform to modify.</param>
    /// <param name="targetEulerAngles">The target local euler angles to tween to.</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween.</param>
    /// <param name="onCompleteDelay">A delay in seconds before the onComplete action is invoked.</param>
    public static UniTask TweenLocalEulerAngles(this Transform transform, Vector3 targetEulerAngles, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f)
    {
        if (transform == null) return UniTask.CompletedTask;
        return BTween.Vector3(transform, "LocalEulerAngles", euler => transform.localEulerAngles = euler, transform.localEulerAngles, targetEulerAngles, duration, onComplete, easeFunction, onCompleteDelay);
    }

    /// <summary>
    /// Tweens the anchoredPosition of a RectTransform to a target position over a specified duration.
    /// </summary>
    /// <param name="rectTransform">The RectTransform to modify.</param>
    /// <param name="targetPosition">The target anchored position to tween to.</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween.</param>
    /// <param name="onCompleteDelay">A delay in seconds before the onComplete action is invoked.</param>
    public static UniTask TweenAnchoredPosition(this RectTransform rectTransform, Vector2 targetPosition, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f)
    {
        if (rectTransform == null) return UniTask.CompletedTask;
        return BTween.Vector2(rectTransform, "AnchoredPosition", pos => rectTransform.anchoredPosition = pos, rectTransform.anchoredPosition, targetPosition, duration, onComplete, easeFunction, onCompleteDelay);
    }

    /// <summary>
    /// Tweens a RectTransform from its current position to a calculated position just outside the parent Canvas border.
    /// </summary>
    /// <param name="rectTransform">The RectTransform to move.</param>
    /// <param name="direction">The direction to move the element off-screen.</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween.</param>
    /// <param name="onCompleteDelay">A delay in seconds before the onComplete action is invoked.</param>
    public static UniTask TweenToOffScreen(this RectTransform rectTransform, OffScreenDirection direction, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f)
    {
        if (rectTransform == null)
        {
            Debug.LogWarning("Cannot tween a null RectTransform.");
            return UniTask.CompletedTask;
        }
        
        // Find the root Canvas that this RectTransform belongs to.
        Canvas canvas = rectTransform.GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("TweenToOffScreen requires the RectTransform to be within a Canvas.", rectTransform);
            return UniTask.CompletedTask;
        }

        // Calculate the target position based on the canvas and element's properties.
        Vector2 targetPosition = GetOffScreenPosition(rectTransform, canvas, direction);

        // Use the existing tweening method to perform the move.
        return rectTransform.TweenAnchoredPosition(targetPosition, duration, onComplete, easeFunction, onCompleteDelay);
    }

    /// <summary>
    /// Tweens the sizeDelta of a RectTransform to a target size over a specified duration.
    /// </summary>
    /// <param name="rectTransform">The RectTransform to modify.</param>
    /// <param name="targetSize">The target sizeDelta to tween to.</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween.</param>
    /// <param name="onCompleteDelay">A delay in seconds before the onComplete action is invoked.</param>
    public static UniTask TweenSizeDelta(this RectTransform rectTransform, Vector2 targetSize, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f)
    {
        if (rectTransform == null) return UniTask.CompletedTask;
        return BTween.Vector2(rectTransform, "SizeDelta", size => rectTransform.sizeDelta = size, rectTransform.sizeDelta, targetSize, duration, onComplete, easeFunction, onCompleteDelay);
    }

    /// <summary>
    /// Tweens a float property of a Material to a target value over a specified duration, identified by its name.
    /// </summary>
    /// <param name="material">The Material to modify.</param>
    /// <param name="propertyName">The string name of the float property in the shader.</param>
    /// <param name="targetValue">The target float value to tween to.</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween.</param>
    /// <param name="onCompleteDelay">A delay in seconds before the onComplete action is invoked.</param>
    public static UniTask TweenMaterialFloat(this Material material, string propertyName, float targetValue, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f)
    {
        if (material == null || string.IsNullOrEmpty(propertyName) || !material.HasProperty(propertyName)) return UniTask.CompletedTask;
        return BTween.Float(material, $"MaterialFloat_{propertyName}", value => material.SetFloat(propertyName, value), material.GetFloat(propertyName), targetValue, duration, onComplete, easeFunction, onCompleteDelay);
    }

    /// <summary>
    /// Tweens a float property of a Material to a target value over a specified duration, identified by its property hash.
    /// </summary>
    /// <param name="material">The Material to modify.</param>
    /// <param name="propertyHash">The integer hash of the property name (use Shader.PropertyToID for better performance).</param>
    /// <param name="targetValue">The target float value to tween to.</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween.</param>
    /// <param name="onCompleteDelay">A delay in seconds before the onComplete action is invoked.</param>
    public static UniTask TweenMaterialFloat(this Material material, int propertyHash, float targetValue, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f)
    {
        if (material == null || !material.HasProperty(propertyHash)) return UniTask.CompletedTask;
        return BTween.Float(material, $"MaterialFloat_{propertyHash}", value => material.SetFloat(propertyHash, value), material.GetFloat(propertyHash), targetValue, duration, onComplete, easeFunction, onCompleteDelay);
    }

    /// <summary>
    /// Tweens the value of a Slider to a target value over a specified duration.
    /// </summary>
    /// <param name="slider">The Slider to modify.</param>
    /// <param name="targetValue">The target value to tween to.</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween.</param>
    /// <param name="onCompleteDelay">A delay in seconds before the onComplete action is invoked.</param>
    public static UniTask TweenSliderValue(this Slider slider, float targetValue, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f)
    {
        if (slider == null) return UniTask.CompletedTask;
        return BTween.Float(slider, slider.name, value => slider.value = value, slider.value, targetValue, duration, onComplete, easeFunction, onCompleteDelay);
    }

    /// <summary>
    /// Tweens the color of an Image to a target color over a specified duration.
    /// </summary>
    /// <param name="image">The Image to modify.</param>
    /// <param name="targetColor">The target color to tween to.</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween.</param>
    /// <param name="onCompleteDelay">A delay in seconds before the onComplete action is invoked.</param>
    public static UniTask TweenImageColor(this Image image, Color targetColor, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f)
    {
        if (image == null) return UniTask.CompletedTask;
        return BTween.Color(image, "Color", color => image.color = color, image.color, targetColor, duration, onComplete, easeFunction, onCompleteDelay);
    }

    /// <summary>
    /// Tweens the color of all vertices in the mesh to a target color.
    /// Note: This replaces the entire vertex color array on each frame.
    /// </summary>
    /// <param name="mesh">The mesh to modify.</param>
    /// <param name="targetColor">The target color for all vertices.</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween.</param>
    /// <param name="onCompleteDelay">A delay before the onComplete action is invoked.</param>
    public static UniTask TweenMeshColor(this Mesh mesh, Color targetColor, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f)
    {
        if (mesh == null) return UniTask.CompletedTask;

        // Use the color of the first vertex as the start color, or white if no colors exist.
        Color startColor = (mesh.colors != null && mesh.colors.Length > 0) ? mesh.colors[0] : Color.white;

        // Pre-allocate the array to avoid allocations in the setter.
        Color[] colors = new Color[mesh.vertexCount];

        Action<Color> setter = (c) =>
        {
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = c;
            }
            mesh.colors = colors;
        };

        return BTween.Color(mesh, "MeshColor", setter, startColor, targetColor, duration, onComplete, easeFunction, onCompleteDelay);
    }

    /// <summary>
    /// Tweens the alpha channel of the mesh's vertex colors. Preserves the existing RGB values.
    /// If the mesh has no vertex colors, it initializes them to white.
    /// </summary>
    /// <param name="mesh">The mesh to modify.</param>
    /// <param name="targetAlpha">The target alpha value (0-1).</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween.</param>
    /// <param name="onCompleteDelay">A delay before the onComplete action is invoked.</param>
    public static UniTask TweenMeshColorAlpha(this Mesh mesh, float targetAlpha, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f)
    {
        if (mesh == null) return UniTask.CompletedTask;

        // Get a copy of the original colors to preserve their RGB values.
        Color[] newColors = mesh.colors;

        // If the mesh has no colors, create a new array of white colors.
        if (newColors == null || newColors.Length != mesh.vertexCount)
        {
            newColors = new Color[mesh.vertexCount];
            for (int i = 0; i < newColors.Length; i++)
            {
                newColors[i] = Color.white;
            }
        }

        // Use the alpha of the first vertex as the start alpha, or 1 if no colors exist.
        float startAlpha = newColors.Length > 0 ? newColors[0].a : 1f;

        Action<float> setter = (alpha) =>
        {
            for (int i = 0; i < newColors.Length; i++)
            {
                newColors[i].a = alpha;
            }
            mesh.colors = newColors;
        };

        return BTween.Float(mesh, "MeshColorAlpha", setter, startAlpha, targetAlpha, duration, onComplete, easeFunction, onCompleteDelay);
    }

    #region Helpers
    /// <summary>
    /// Calculates the anchored position required to place an element just outside the canvas border.
    /// </summary>
    private static Vector2 GetOffScreenPosition(RectTransform elementRect, Canvas canvas, OffScreenDirection direction)
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        
        // Start with the current position so we only have to modify one axis.
        Vector2 targetPosition = elementRect.anchoredPosition;

        float canvasWidth = canvasRect.rect.width;
        float canvasHeight = canvasRect.rect.height;
        float elementWidth = elementRect.rect.width;
        float elementHeight = elementRect.rect.height;

        // Calculate the element's pivot offset in pixels. This is crucial for accurate placement.
        // For a left pivot (0), pivotOffsetX is 0.
        // For a center pivot (0.5), pivotOffsetX is half the element's width.
        float pivotOffsetX = elementWidth * elementRect.pivot.x;
        float pivotOffsetY = elementHeight * elementRect.pivot.y;

        switch (direction)
        {
            case OffScreenDirection.Right:
                // Target the element's pivot point to be just off the right edge of the canvas.
                targetPosition.x = (canvasWidth / 2f) + pivotOffsetX;
                break;
            case OffScreenDirection.Left:
                // Target the element's pivot point to be just off the left edge of the canvas.
                targetPosition.x = -(canvasWidth / 2f) - (elementWidth - pivotOffsetX);
                break;
            case OffScreenDirection.Top:
                // Target the element's pivot point to be just off the top edge of the canvas.
                targetPosition.y = (canvasHeight / 2f) + pivotOffsetY;
                break;
            case OffScreenDirection.Bottom:
                // Target the element's pivot point to be just off the bottom edge of the canvas.
                targetPosition.y = -(canvasHeight / 2f) - (elementHeight - pivotOffsetY);
                break;
        }
        return targetPosition;
    }
    #endregion
    
}