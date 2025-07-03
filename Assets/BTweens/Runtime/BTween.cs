using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

/// <summary>
/// A static core engine for creating, managing, and executing asynchronous tweens using UniTask.
/// This class handles tween lifecycle, automatic cancellation on object destruction, and provides a suite of easing functions.
/// </summary>
public static class BTween
{
    // The dictionary now uses a uint hash for the tween identifier.
    private static readonly Dictionary<Tuple<object, uint>, Tuple<CancellationTokenSource, IDisposable>> _activeTweens = new Dictionary<Tuple<object, uint>, Tuple<CancellationTokenSource, IDisposable>>();

    // CreateKey now returns a key with a uint hash.
    private static Tuple<object, uint> CreateKey(object owner, uint tweenIdentifierHash)
    {
        return Tuple.Create(owner, tweenIdentifierHash);
    }

    #region Public API Overloads (uint hash)
    /// <summary>
    /// Starts a tween for a float value using a uint hash identifier.
    /// </summary>
    /// <param name="owner">The object that owns this tween. Used for identification and automatic cancellation on destruction.</param>
    /// <param name="tweenIdentifierHash">A uint hash to uniquely identify this tween on the owner object.</param>
    /// <param name="setter">The action that applies the tweened value each frame.</param>
    /// <param name="startValue">The starting value of the tween.</param>
    /// <param name="endValue">The target value of the tween.</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An optional action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween's progression.</param>
    /// <param name="onCompleteDelay">An optional delay in seconds before invoking the onComplete action.</param>
    /// <param name="ignoreTimeScale">If true, the tween will ignore Time.timeScale and use unscaled time.</param>
    /// <returns>A UniTask that completes when the tween is finished or cancelled.</returns>
    public static UniTask Float(object owner, uint tweenIdentifierHash, Action<float> setter, float startValue, float endValue, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        return StartTween(owner, tweenIdentifierHash, startValue, endValue, duration, setter, Mathf.LerpUnclamped, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }

    /// <summary>
    /// Starts a tween for a Vector2 value using a uint hash identifier.
    /// </summary>
    /// <inheritdoc cref="Float(object, uint, Action{float}, float, float, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask Vector2(object owner, uint tweenIdentifierHash, Action<Vector2> setter, Vector2 startValue, Vector2 endValue, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        return StartTween(owner, tweenIdentifierHash, startValue, endValue, duration, setter, UnityEngine.Vector2.LerpUnclamped, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }

    /// <summary>
    /// Starts a tween for a Vector3 value using a uint hash identifier.
    /// </summary>
    /// <inheritdoc cref="Float(object, uint, Action{float}, float, float, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask Vector3(object owner, uint tweenIdentifierHash, Action<Vector3> setter, Vector3 startValue, Vector3 endValue, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        return StartTween(owner, tweenIdentifierHash, startValue, endValue, duration, setter, UnityEngine.Vector3.LerpUnclamped, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }

    /// <summary>
    /// Starts a tween for a Quaternion value using a uint hash identifier. Uses Slerp for correct rotational interpolation.
    /// </summary>
    /// <inheritdoc cref="Float(object, uint, Action{float}, float, float, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask Quaternion(object owner, uint tweenIdentifierHash, Action<Quaternion> setter, Quaternion startValue, Quaternion endValue, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        return StartTween(owner, tweenIdentifierHash, startValue, endValue, duration, setter, UnityEngine.Quaternion.SlerpUnclamped, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }

    /// <summary>
    /// Starts a tween for a Color value using a uint hash identifier.
    /// </summary>
    /// <inheritdoc cref="Float(object, uint, Action{float}, float, float, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask Color(object owner, uint tweenIdentifierHash, Action<Color> setter, Color startValue, Color endValue, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
    {
        return StartTween(owner, tweenIdentifierHash, startValue, endValue, duration, setter, UnityEngine.Color.LerpUnclamped, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    }
    #endregion

    #region Public API Overloads (string tag - for convenience)
    /// <summary>
    /// Starts a tween for a float value. Hashes the string tag to a uint for internal use.
    /// </summary>
    /// <param name="owner">The object that owns this tween. Used for identification and automatic cancellation on destruction.</param>
    /// <param name="tweenIdentifierTag">A string tag to uniquely identify this tween on the owner object.</param>
    /// <param name="setter">The action that applies the tweened value each frame.</param>
    /// <param name="startValue">The starting value of the tween.</param>
    /// <param name="endValue">The target value of the tween.</param>
    /// <param name="duration">The duration of the tween in seconds.</param>
    /// <param name="onComplete">An optional action to invoke when the tween completes.</param>
    /// <param name="easeFunction">The easing function to use for the tween's progression.</param>
    /// <param name="onCompleteDelay">An optional delay in seconds before invoking the onComplete action.</param>
    /// <param name="ignoreTimeScale">If true, the tween will ignore Time.timeScale and use unscaled time.</param>
    /// <returns>A UniTask that completes when the tween is finished or cancelled.</returns>
    public static UniTask Float(object owner, string tweenIdentifierTag, Action<float> setter, float startValue, float endValue, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
        => Float(owner, StringHash(tweenIdentifierTag), setter, startValue, endValue, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    
    /// <summary>
    /// Starts a tween for a Vector2 value. Hashes the string tag to a uint for internal use.
    /// </summary>
    /// <inheritdoc cref="Float(object, string, Action{float}, float, float, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask Vector2(object owner, string tweenIdentifierTag, Action<Vector2> setter, Vector2 startValue, Vector2 endValue, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
        => Vector2(owner, StringHash(tweenIdentifierTag), setter, startValue, endValue, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);

    /// <summary>
    /// Starts a tween for a Vector3 value. Hashes the string tag to a uint for internal use.
    /// </summary>
    /// <inheritdoc cref="Float(object, string, Action{float}, float, float, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask Vector3(object owner, string tweenIdentifierTag, Action<Vector3> setter, Vector3 startValue, Vector3 endValue, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
        => Vector3(owner, StringHash(tweenIdentifierTag), setter, startValue, endValue, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);

    /// <summary>
    /// Starts a tween for a Quaternion value. Hashes the string tag to a uint for internal use. Uses Slerp for correct rotational interpolation.
    /// </summary>
    /// <inheritdoc cref="Float(object, string, Action{float}, float, float, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask Quaternion(object owner, string tweenIdentifierTag, Action<Quaternion> setter, Quaternion startValue, Quaternion endValue, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
        => Quaternion(owner, StringHash(tweenIdentifierTag), setter, startValue, endValue, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);

    /// <summary>
    /// Starts a tween for a Color value. Hashes the string tag to a uint for internal use.
    /// </summary>
    /// <inheritdoc cref="Float(object, string, Action{float}, float, float, float, Action, Func{float, float}, float, bool)"/>
    public static UniTask Color(object owner, string tweenIdentifierTag, Action<Color> setter, Color startValue, Color endValue, float duration, Action onComplete = null, Func<float, float> easeFunction = null, float onCompleteDelay = 0f, bool ignoreTimeScale = false)
        => Color(owner, StringHash(tweenIdentifierTag), setter, startValue, endValue, duration, onComplete, easeFunction, onCompleteDelay, ignoreTimeScale);
    #endregion

    /// <summary>
    /// The internal generic method that sets up and starts any tween. It handles tween cancellation, lifecycle management, and invokes the async tweening logic.
    /// </summary>
    private static UniTask StartTween<T>(object owner, uint tweenIdentifierHash, T startValue, T endValue, float duration, Action<T> setter, Func<T, T, float, T> interpolator, Action onComplete, Func<float, float> easeFunction, float onCompleteDelay, bool ignoreTimeScale)
    {
        var key = CreateKey(owner, tweenIdentifierHash);
        StopTweenForKey(key);

        if (duration <= 0)
        {
            setter(endValue);
            onComplete?.Invoke();
            return UniTask.CompletedTask;
        }

        var cts = new CancellationTokenSource();
        IDisposable registration = null;

        CancellationToken ownerDestroyToken = default;
        if (owner is Component ownerAsComponent)
        {
            ownerDestroyToken = ownerAsComponent.GetAsyncDestroyTrigger().CancellationToken;
        }
        else if (owner is GameObject ownerAsGameObject)
        {
            ownerDestroyToken = ownerAsGameObject.GetAsyncDestroyTrigger().CancellationToken;
        }

        if (ownerDestroyToken.CanBeCanceled)
        {
            registration = ownerDestroyToken.Register(() => cts.Cancel());
        }

        _activeTweens[key] = Tuple.Create(cts, registration);
        
        return TweenValueAsync(key, startValue, endValue, duration, setter, interpolator, onComplete, easeFunction, onCompleteDelay, cts, ignoreTimeScale);
    }
    
    private static void StopTweenForKey(Tuple<object, uint> key)
    {
        if (_activeTweens.TryGetValue(key, out var context))
        {
            context.Item1.Cancel();
        }
    }

    /// <summary>
    /// Manually stops a specific tween on a given object, identified by its hash.
    /// </summary>
    /// <param name="owner">The owner object of the tween to stop.</param>
    /// <param name="tweenIdentifierHash">The identifier hash of the tween to stop.</param>
    public static void StopTween(object owner, uint tweenIdentifierHash)
    {
        var key = CreateKey(owner, tweenIdentifierHash);
        StopTweenForKey(key);
    }
    
    /// <summary>
    /// Manually stops a specific tween on a given object, identified by its tag.
    /// </summary>
    /// <param name="owner">The owner object of the tween to stop.</param>
    /// <param name="tweenIdentifierTag">The identifier tag of the tween to stop.</param>
    public static void StopTween(object owner, string tweenIdentifierTag)
    {
        StopTween(owner, StringHash(tweenIdentifierTag));
    }

    /// <summary>
    /// The core asynchronous loop that runs a tween every frame until it completes or is cancelled.
    /// </summary>
    private static async UniTask TweenValueAsync<T>(
        Tuple<object, uint> key, T from, T to, float duration,
        Action<T> setter, Func<T, T, float, T> interpolator,
        Action onComplete, Func<float, float> easeFunction,
        float onCompleteDelay, CancellationTokenSource cts, bool ignoreTimeScale)
    {
        float elapsedTime = 0f;
        easeFunction ??= Ease.Linear;
        var cancellationToken = cts.Token;

        try
        {
            while (elapsedTime < duration)
            {
                cancellationToken.ThrowIfCancellationRequested();
                elapsedTime += ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
                float progress = Mathf.Clamp01(elapsedTime / duration);
                setter(interpolator(from, to, easeFunction(progress)));
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }

            setter(to);

            if (onComplete != null)
            {
                if (onCompleteDelay > 0f)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(onCompleteDelay), ignoreTimeScale: ignoreTimeScale, delayTiming: PlayerLoopTiming.Update, cancellationToken: cancellationToken);
                }
                onComplete.Invoke();
            }

        }
        catch (OperationCanceledException)
        {
            // Expected
        }
        finally
        {
            if (_activeTweens.TryGetValue(key, out var context) && context.Item1 == cts)
            {
                _activeTweens.Remove(key);
                context.Item2?.Dispose();
                context.Item1.Dispose();
            }
        }
    }

    /// <summary>
    /// Stops and clears all tweens currently managed by this system. Useful for scene cleanup.
    /// </summary>
    public static void StopAndClearAllManagedTweens()
    {
        var tweensToStop = new List<Tuple<CancellationTokenSource, IDisposable>>(_activeTweens.Values);
        
        foreach (var context in tweensToStop)
        {
            context.Item1.Cancel();
        }
    }

    /// <summary>
    /// Hashes a string using the FNV-1a algorithm.
    /// </summary>
    public static uint StringHash(string input)
    {
        if (input == null) return 0;
        
        const uint fnvOffset = 2166136261;
        const uint fnvPrime = 16777619;

        uint hash = fnvOffset;
        foreach (char c in input)
        {
            hash ^= c;
            hash *= fnvPrime;
        }
        return hash;
    }

    /// <summary>
    /// A static class containing a collection of common easing functions to control the rate of change of a tween.
    /// </summary>
    public static class Ease
    {
        /// <summary>A linear progression with no acceleration.</summary>
        /// <param name="p">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
        public static float Linear(float p) => p;
        /// <summary>Ease-in with quadratic acceleration.</summary>
        /// <param name="p">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
        public static float InQuad(float p) => p * p;
        /// <summary>Ease-out with quadratic deceleration.</summary>
        /// <param name="p">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
        public static float OutQuad(float p) => p * (2 - p);
        /// <summary>Ease-in and ease-out with quadratic acceleration and deceleration.</summary>
        /// <param name="p">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
        public static float InOutQuad(float p) => p < 0.5f ? 2 * p * p : -1 + (4 - 2 * p) * p;
        /// <summary>Ease-in with cubic acceleration.</summary>
        /// <param name="p">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
        public static float InCubic(float p) => p * p * p;
        /// <summary>Ease-out with cubic deceleration.</summary>
        /// <param name="p">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
        public static float OutCubic(float p) => (--p) * p * p + 1;
        /// <summary>Ease-in and ease-out with cubic acceleration and deceleration.</summary>
        /// <param name="p">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
        public static float InOutCubic(float p) => p < 0.5f ? 4 * p * p * p : (p - 1) * (2 * p - 2) * (2 * p - 2) + 1;
        /// <summary>Ease-in with sinusoidal acceleration.</summary>
        /// <param name="t">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
        public static float InSine(float t) => 1 - (float)Math.Cos(t * Math.PI / 2);
		/// <summary>Ease-out with sinusoidal deceleration.</summary>
        /// <param name="t">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
		public static float OutSine(float t) => (float)Math.Sin(t * Math.PI / 2);
		/// <summary>Ease-in and ease-out with sinusoidal acceleration and deceleration.</summary>
        /// <param name="t">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
		public static float InOutSine(float t) => (float)(Math.Cos(t * Math.PI) - 1) / -2;
        /// <summary>Ease-in with quintic acceleration.</summary>
        /// <param name="t">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
        public static float InQuint(float t) => t * t * t * t * t;
		/// <summary>Ease-out with quintic deceleration.</summary>
        /// <param name="t">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
		public static float OutQuint(float t) => 1 - InQuint(1 - t);
		/// <summary>Ease-in and ease-out with quintic acceleration and deceleration.</summary>
        /// <param name="t">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
		public static float InOutQuint(float t)
		{
			if (t < 0.5) return InQuint(t * 2) / 2;
			return 1 - InQuint((1 - t) * 2) / 2;
		}
        /// <summary>Ease-in with quartic acceleration.</summary>
        /// <param name="t">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
        public static float InQuart(float t) => t * t * t * t;
		/// <summary>Ease-out with quartic deceleration.</summary>
        /// <param name="t">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
		public static float OutQuart(float t) => 1 - InQuart(1 - t);
		/// <summary>Ease-in and ease-out with quartic acceleration and deceleration.</summary>
        /// <param name="t">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
		public static float InOutQuart(float t)
		{
			if (t < 0.5) return InQuart(t * 2) / 2;
			return 1 - InQuart((1 - t) * 2) / 2;
		}
        /// <summary>Ease-in with exponential acceleration.</summary>
        /// <param name="t">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
        public static float InExpo(float t) => (float)Math.Pow(2, 10 * (t - 1));
		/// <summary>Ease-out with exponential deceleration.</summary>
        /// <param name="t">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
		public static float OutExpo(float t) => 1 - InExpo(1 - t);
		/// <summary>Ease-in and ease-out with exponential acceleration and deceleration.</summary>
        /// <param name="t">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
		public static float InOutExpo(float t)
		{
			if (t < 0.5) return InExpo(t * 2) / 2;
			return 1 - InExpo((1 - t) * 2) / 2;
		}
		/// <summary>Ease-in with circular acceleration.</summary>
        /// <param name="t">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
		public static float InCirc(float t) => -((float)Math.Sqrt(1 - t * t) - 1);
		/// <summary>Ease-out with circular deceleration.</summary>
        /// <param name="t">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
		public static float OutCirc(float t) => 1 - InCirc(1 - t);
		/// <summary>Ease-in and ease-out with circular acceleration and deceleration.</summary>
        /// <param name="t">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
		public static float InOutCirc(float t)
		{
			if (t < 0.5) return InCirc(t * 2) / 2;
			return 1 - InCirc((1 - t) * 2) / 2;
		}
		/// <summary>Ease-in with an elastic, spring-like effect.</summary>
        /// <param name="t">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
		public static float InElastic(float t) => 1 - OutElastic(1 - t);
		/// <summary>Ease-out with an elastic, spring-like effect.</summary>
        /// <param name="t">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
		public static float OutElastic(float t)
		{
			float p = 0.3f;
			return (float)Math.Pow(2, -10 * t) * (float)Math.Sin((t - p / 4) * (2 * Math.PI) / p) + 1;
		}
		/// <summary>Ease-in and ease-out with an elastic, spring-like effect.</summary>
        /// <param name="t">The normalized progress of the tween (0 to 1).</param>
        /// <returns>The eased progress.</returns>
		public static float InOutElastic(float t)
		{
			if (t < 0.5) return InElastic(t * 2) / 2;
			return 1 - InElastic((1 - t) * 2) / 2;
		}
    }
}