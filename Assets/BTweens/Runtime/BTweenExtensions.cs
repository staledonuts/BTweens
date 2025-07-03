using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class TweenExtensions
{
    /// <summary>
    /// Common PropertyIDs Pre-hashed.
    /// </summary>
    
    public static class TweenPropertyIDs
    {
        /// <summary>
        /// Pre-hashed "Volume" string.
        /// </summary>
        public const uint Volume = 2568387391;
        /// <summary>
        /// Pre-hashed "Alpha" string.
        /// </summary>
        public const uint Alpha = 3989429483;
        /// <summary>
        /// Pre-hashed "LocalPosition" string.
        /// </summary>
        public const uint LocalPosition = 238328229;
        /// <summary>
        /// Pre-hashed "Position" string.
        /// </summary>
        public const uint Position = 2950346363;
        /// <summary>
        /// Pre-hashed "LocalScale" string.
        /// </summary>
        public const uint LocalScale = 3698485907;
        /// <summary>
        /// Pre-hashed "LocalRotation" string.
        /// </summary>
        public const uint LocalRotation = 1358043422;
        /// <summary>
        /// Pre-hashed "LocalEulerAngles" string.
        /// </summary>
        public const uint LocalEulerAngles = 4229983280;
        /// <summary>
        /// Pre-hashed "AnchoredPosition" string.
        /// </summary>
        public const uint AnchoredPosition = 753138243;
        /// <summary>
        /// Pre-hashed "SizeDelta" string.
        /// </summary>
        public const uint SizeDelta = 2950488622;
        /// <summary>
        /// Pre-hashed "Color" string.
        /// </summary>
        public const uint Color = 3715655789;
        /// <summary>
        /// Pre-hashed "MeshColor" string.
        /// </summary>
        public const uint MeshColor = 1010995168;
        /// <summary>
        /// Pre-hashed "MeshColorAlpha" string.
        /// </summary>
        public const uint MeshColorAlpha = 2280267467;
    }
    
    /// <summary>
    /// Tweens the volume of an AudioSource to a target value over a specified duration.
    /// </summary>
    /// <param name="audioSource">The AudioSource to modify.</param>
    /// <param name="targetVolume">The target volume to tween to (0-1).</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween.</param>
    /// <param name="onCompleteDelay">A delay in seconds before the onComplete action is invoked.</param>
    /// <param name="ignoreTimeScale">If true, the tween will ignore Time.timeScale and use unscaled time.</param>
    public static UniTask TweenVolume(this AudioSource audioSource, float targetVolume, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        if (audioSource == null) return UniTask.CompletedTask;
        return BTween.Float(audioSource, TweenPropertyIDs.Volume, vol => audioSource.volume = vol, audioSource.volume, targetVolume, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }

    /// <summary>
    /// Tweens the alpha of a CanvasGroup to a target value over a specified duration.
    /// </summary>
    /// <param name="canvasGroup">The CanvasGroup to modify.</param>
    /// <param name="targetAlpha">The target alpha value to tween to (0-1).</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween.</param>
    /// <param name="onCompleteDelay">A delay in seconds before the onComplete action is invoked.</param>
    /// <param name="ignoreTimeScale">If true, the tween will ignore Time.timeScale and use unscaled time.</param>
    public static UniTask TweenAlpha(this CanvasGroup canvasGroup, float targetAlpha, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        if (canvasGroup == null) return UniTask.CompletedTask;
        return BTween.Float(canvasGroup, TweenPropertyIDs.Alpha, alpha => canvasGroup.alpha = alpha, canvasGroup.alpha, targetAlpha, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
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
    /// <param name="ignoreTimeScale">If true, the tween will ignore Time.timeScale and use unscaled time.</param>
    public static UniTask TweenLocalPosition(this Transform transform, Vector3 targetPosition, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        if (transform == null) return UniTask.CompletedTask;
        return BTween.Vector3(transform, TweenPropertyIDs.LocalPosition, pos => transform.localPosition = pos, transform.localPosition, targetPosition, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }

    /// <summary>
    /// Tweens the world position of a Transform to a target position over a specified duration.
    /// </summary>
    /// <param name="transform">The Transform to modify.</param>
    /// <param name="targetPosition">The target world position to tween to.</param>
    /// <inheritdoc cref="TweenLocalPosition(Transform, Vector3, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask TweenPosition(this Transform transform, Vector3 targetPosition, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        if (transform == null) return UniTask.CompletedTask;
        return BTween.Vector3(transform, TweenPropertyIDs.Position, pos => transform.position = pos, transform.position, targetPosition, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }

    /// <summary>
    /// Tweens the local scale of a Transform to a target scale over a specified duration.
    /// </summary>
    /// <param name="transform">The Transform to modify.</param>
    /// <param name="targetScale">The target local scale to tween to.</param>
    /// <inheritdoc cref="TweenLocalPosition(Transform, Vector3, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask TweenLocalScale(this Transform transform, Vector3 targetScale, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        if (transform == null) return UniTask.CompletedTask;
        return BTween.Vector3(transform, TweenPropertyIDs.LocalScale, scale => transform.localScale = scale, transform.localScale, targetScale, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }

    /// <summary>
    /// Tweens the local rotation of a Transform to a target rotation over a specified duration.
    /// </summary>
    /// <param name="transform">The Transform to modify.</param>
    /// <param name="targetRotation">The target local rotation (as a Quaternion) to tween to.</param>
    /// <inheritdoc cref="TweenLocalPosition(Transform, Vector3, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask TweenLocalRotation(this Transform transform, Quaternion targetRotation, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        if (transform == null) return UniTask.CompletedTask;
        return BTween.Quaternion(transform, TweenPropertyIDs.LocalRotation, rot => transform.localRotation = rot, transform.localRotation, targetRotation, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }

    /// <summary>
    /// Tweens the local euler angles of a Transform to a target set of angles over a specified duration.
    /// </summary>
    /// <param name="transform">The Transform to modify.</param>
    /// <param name="targetEulerAngles">The target local euler angles to tween to.</param>
    /// <inheritdoc cref="TweenLocalPosition(Transform, Vector3, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask TweenLocalEulerAngles(this Transform transform, Vector3 targetEulerAngles, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        if (transform == null) return UniTask.CompletedTask;
        return BTween.Vector3(transform, TweenPropertyIDs.LocalEulerAngles, euler => transform.localEulerAngles = euler, transform.localEulerAngles, targetEulerAngles, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }

    /// <summary>
    /// Tweens the anchoredPosition of a RectTransform to a target position over a specified duration.
    /// </summary>
    /// <param name="rectTransform">The RectTransform to modify.</param>
    /// <param name="targetPosition">The target anchored position to tween to.</param>
    /// <inheritdoc cref="TweenLocalPosition(Transform, Vector3, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask TweenAnchoredPosition(this RectTransform rectTransform, Vector2 targetPosition, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        if (rectTransform == null) return UniTask.CompletedTask;
        return BTween.Vector2(rectTransform, TweenPropertyIDs.AnchoredPosition, pos => rectTransform.anchoredPosition = pos, rectTransform.anchoredPosition, targetPosition, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }

    /// <summary>
    /// Tweens a RectTransform from its current position to a calculated position just outside the parent Canvas border.
    /// </summary>
    /// <param name="rectTransform">The RectTransform to move.</param>
    /// <param name="direction">The direction to move the element off-screen.</param>
    /// <inheritdoc cref="TweenLocalPosition(Transform, Vector3, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask TweenToOffScreen(this RectTransform rectTransform, OffScreenDirection direction, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        if (rectTransform == null) return UniTask.CompletedTask;
        Canvas canvas = rectTransform.GetComponentInParent<Canvas>();
        if (canvas == null) return UniTask.CompletedTask;
        
        Vector2 targetPosition = GetOffScreenPosition(rectTransform, canvas, direction);
        return rectTransform.TweenAnchoredPosition(targetPosition, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }

    /// <summary>
    /// Tweens the sizeDelta of a RectTransform to a target size over a specified duration.
    /// </summary>
    /// <param name="rectTransform">The RectTransform to modify.</param>
    /// <param name="targetSize">The target sizeDelta to tween to.</param>
    /// <inheritdoc cref="TweenLocalPosition(Transform, Vector3, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask TweenSizeDelta(this RectTransform rectTransform, Vector2 targetSize, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        if (rectTransform == null) return UniTask.CompletedTask;
        return BTween.Vector2(rectTransform, TweenPropertyIDs.SizeDelta, size => rectTransform.sizeDelta = size, rectTransform.sizeDelta, targetSize, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }
    
    /// <summary>
    /// Tweens a float property of a Material to a target value over a specified duration, identified by its name.
    /// </summary>
    /// <param name="material">The Material to modify.</param>
    /// <param name="propertyName">The string name of the float property in the shader.</param>
    /// <param name="targetValue">The target float value to tween to.</param>
    /// <inheritdoc cref="TweenLocalPosition(Transform, Vector3, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask TweenMaterialFloat(this Material material, string propertyName, float targetValue, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        if (material == null) return UniTask.CompletedTask;
        return BTween.Float(material, $"MaterialFloat_{propertyName}", value => material.SetFloat(propertyName, value), material.GetFloat(propertyName), targetValue, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }

    /// <summary>
    /// Tweens a float property of a Material to a target value over a specified duration, identified by its property hash.
    /// </summary>
    /// <param name="material">The Material to modify.</param>
    /// <param name="propertyHash">The integer hash of the property name (use Shader.PropertyToID for better performance).</param>
    /// <param name="targetValue">The target float value to tween to.</param>
    /// <inheritdoc cref="TweenLocalPosition(Transform, Vector3, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask TweenMaterialFloat(this Material material, int propertyHash, float targetValue, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        if (material == null) return UniTask.CompletedTask;
        return BTween.Float(material, (uint)propertyHash, value => material.SetFloat(propertyHash, value), material.GetFloat(propertyHash), targetValue, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }

    /// <summary>
    /// Tweens the value of a Slider to a target value over a specified duration.
    /// </summary>
    /// <param name="slider">The Slider to modify.</param>
    /// <param name="targetValue">The target value to tween to.</param>
    /// <inheritdoc cref="TweenLocalPosition(Transform, Vector3, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask TweenSliderValue(this Slider slider, float targetValue, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        if (slider == null) return UniTask.CompletedTask;
        // Use the slider's name as the tween identifier tag.
        return BTween.Float(slider, slider.name, value => slider.value = value, slider.value, targetValue, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }

    /// <summary>
    /// Tweens the color of an Image to a target color over a specified duration.
    /// </summary>
    /// <param name="image">The Image to modify.</param>
    /// <param name="targetColor">The target color to tween to.</param>
    /// <inheritdoc cref="TweenLocalPosition(Transform, Vector3, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask TweenImageColor(this Image image, Color targetColor, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        if (image == null) return UniTask.CompletedTask;
        return BTween.Color(image, TweenPropertyIDs.Color, color => image.color = color, image.color, targetColor, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }

    /// <summary>
    /// Tweens the color of all vertices in the mesh to a target color.
    /// Note: This replaces the entire vertex color array on each frame.
    /// </summary>
    /// <param name="mesh">The mesh to modify.</param>
    /// <param name="targetColor">The target color for all vertices.</param>
    /// <inheritdoc cref="TweenLocalPosition(Transform, Vector3, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask TweenMeshColor(this Mesh mesh, Color targetColor, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        if (mesh == null) return UniTask.CompletedTask;

        Color startColor = (mesh.colors != null && mesh.colors.Length > 0) ? mesh.colors[0] : Color.white;
        Color[] colors = new Color[mesh.vertexCount];

        Action<Color> setter = (c) =>
        {
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = c;
            }
            mesh.colors = colors;
        };

        return BTween.Color(mesh, TweenPropertyIDs.MeshColor, setter, startColor, targetColor, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }

    /// <summary>
    /// Tweens the alpha channel of the mesh's vertex colors. Preserves the existing RGB values.
    /// If the mesh has no vertex colors, it initializes them to white.
    /// </summary>
    /// <param name="mesh">The mesh to modify.</param>
    /// <param name="targetAlpha">The target alpha value (0-1).</param>
    /// <inheritdoc cref="TweenLocalPosition(Transform, Vector3, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask TweenMeshColorAlpha(this Mesh mesh, float targetAlpha, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        if (mesh == null) return UniTask.CompletedTask;

        Color[] newColors = mesh.colors;

        if (newColors == null || newColors.Length != mesh.vertexCount)
        {
            newColors = new Color[mesh.vertexCount];
            for (int i = 0; i < newColors.Length; i++)
            {
                newColors[i] = Color.white;
            }
        }

        float startAlpha = newColors.Length > 0 ? newColors[0].a : 1f;

        Action<float> setter = (alpha) =>
        {
            for (int i = 0; i < newColors.Length; i++)
            {
                newColors[i].a = alpha;
            }
            mesh.colors = newColors;
        };

        return BTween.Float(mesh, TweenPropertyIDs.MeshColorAlpha, setter, startAlpha, targetAlpha, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }

    #region Helpers
    /// <summary>
    /// Calculates the anchored position required to place an element just outside the canvas border.
    /// </summary>
    private static Vector2 GetOffScreenPosition(RectTransform elementRect, Canvas canvas, OffScreenDirection direction)
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 targetPosition = elementRect.anchoredPosition;

        float canvasWidth = canvasRect.rect.width;
        float canvasHeight = canvasRect.rect.height;
        float elementWidth = elementRect.rect.width;
        float elementHeight = elementRect.rect.height;
        
        float pivotOffsetX = elementWidth * elementRect.pivot.x;
        float pivotOffsetY = elementHeight * elementRect.pivot.y;

        switch (direction)
        {
            case OffScreenDirection.Right:
                targetPosition.x = (canvasWidth / 2f) + pivotOffsetX;
                break;
            case OffScreenDirection.Left:
                targetPosition.x = -(canvasWidth / 2f) - (elementWidth - pivotOffsetX);
                break;
            case OffScreenDirection.Top:
                targetPosition.y = (canvasHeight / 2f) + pivotOffsetY;
                break;
            case OffScreenDirection.Bottom:
                targetPosition.y = -(canvasHeight / 2f) - (elementHeight - pivotOffsetY);
                break;
        }
        return targetPosition;
    }
    #endregion
}