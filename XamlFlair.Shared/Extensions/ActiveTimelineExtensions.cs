using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if __WPF__
using System.Windows;
using Timeline = System.Windows.Media.Animation.Storyboard;
#elif __UWP__
using Windows.UI.Xaml;
using Timeline = XamlFlair.AnimationGroup;
#else
using Windows.UI.Xaml;
using Timeline = Windows.UI.Xaml.Media.Animation.Storyboard;
#endif

namespace XamlFlair.Extensions
{
	internal static class ActiveTimelineExtensions
	{
		internal static ILogger Logger;

		internal static ActiveTimeline<T> Add<T>(
			this ConcurrentDictionary<Guid, ActiveTimeline<T>> actives,
			T timeline, AnimationSettings settings,
			FrameworkElement element,
			AnimationState state,
			IterationBehavior iterationBehavior,
			int iterationCount,
			bool isSequence,
			int sequenceOrder = 0)
				where T : DependencyObject
		{
			var elementGuid = Animations.GetElementGuid(element);
			var timelineGuid = Guid.NewGuid();

			if (elementGuid.Equals(Guid.Empty))
			{
				elementGuid = Guid.NewGuid();
			}

			Animations.SetElementGuid(element, elementGuid);

			if (timeline != null)
			{
				Animations.SetElementGuid(timeline, elementGuid);
				Animations.SetTimelineGuid(timeline, timelineGuid);
			}

			var active = new ActiveTimeline<T>()
			{
				ElementGuid = elementGuid,
				Timeline = timeline,
				Settings = settings,
				Element = element,
				State = state,
				IterationBehavior = iterationBehavior,
				IterationCount = iterationCount,
				IsSequence = isSequence,
				SequenceOrder = sequenceOrder
			};

			if (actives.TryAdd(timelineGuid, active) && Animations.EnableActiveTimelinesLogging == LogLevel.Debug)
			{
				LogActiveTimelines(actives, "Active timeline added");
			}

			return active;
		}

		internal static void RemoveByID<T>(this ConcurrentDictionary<Guid, ActiveTimeline<T>> actives, Guid timelineGuid)
			where T : DependencyObject
		{
			if (actives.TryRemove(timelineGuid, out var _) && Animations.EnableActiveTimelinesLogging == LogLevel.Debug)
			{
				LogActiveTimelines(actives, "Active timeline removed");
			}
		}

		internal static ActiveTimeline<T> FindActiveTimeline<T>(this ConcurrentDictionary<Guid, ActiveTimeline<T>> actives, Guid timelineGuid)
			where T : DependencyObject
		{
			if (actives.TryGetValue(timelineGuid, out var result))
			{
				return result;
			}

			return default(ActiveTimeline<T>);
		}

		internal static ActiveTimeline<T> FindFirstActiveTimeline<T>(this ConcurrentDictionary<Guid, ActiveTimeline<T>> actives, Guid elementGuid)
			where T : DependencyObject
		{
			return actives
				.OrderBy(kvp => kvp.Value.SequenceOrder)
				.FirstOrDefault(kvp => kvp.Value.ElementGuid.Equals(elementGuid))
				.Value;
		}

		internal static IEnumerable<KeyValuePair<Guid, ActiveTimeline<T>>> GetAllKeyValuePairs<T>(this ConcurrentDictionary<Guid, ActiveTimeline<T>> actives, Guid elementGuid)
			where T : DependencyObject
		{
			return actives
				.Where(kvp => kvp.Value != null
					&& kvp.Value.ElementGuid.Equals(elementGuid));
		}

		internal static IEnumerable<ActiveTimeline<T>> GetAllNonIteratingActiveTimelines<T>(this ConcurrentDictionary<Guid, ActiveTimeline<T>> actives, Guid elementGuid)
			where T : DependencyObject
		{
			return actives
				.Where(kvp => kvp.Value.ElementGuid.Equals(elementGuid) && !kvp.Value.IsIterating)
				.Select(kvp => kvp.Value);
		}

		internal static ActiveTimeline<T> GetNextIdleActiveTimeline<T>(this ConcurrentDictionary<Guid, ActiveTimeline<T>> actives, Guid elementGuid)
			where T : DependencyObject
		{
			return actives.GetNextIdleKeyValuePair(elementGuid).Value;
		}

		private static KeyValuePair<Guid, ActiveTimeline<T>> GetNextIdleKeyValuePair<T>(this ConcurrentDictionary<Guid, ActiveTimeline<T>> actives, Guid elementGuid)
			where T : DependencyObject
		{
			return actives
				.Where(kvp => kvp.Value.State == AnimationState.Idle && kvp.Value.ElementGuid.Equals(elementGuid))
				.OrderBy(kvp => kvp.Value.SequenceOrder)
				.FirstOrDefault();
		}

		internal static FrameworkElement GetElementByTimeline<T>(this ConcurrentDictionary<Guid, ActiveTimeline<T>> actives, T timeline)
			where T : DependencyObject
		{
			FrameworkElement element = null;
			var guid = Animations.GetTimelineGuid(timeline);

			if (actives.TryGetValue(guid, out var active))
			{
				element = active?.Element;

				// Fail-safe code in the case an element isn't retrieved by use of the Guid
				if (element == null)
				{
					element = actives
						.FirstOrDefault(kvp => kvp.Value.Timeline.Equals(timeline))
						.Value?.Element;
				}
			}

			return element;
		}

		internal static void SetAnimationState<T>(this ActiveTimeline<T> active, Guid timelineGuid, AnimationState state)
			where T : DependencyObject
		{
			if (active != null)
			{
				active.State = state;
			}

			if (Animations.EnableActiveTimelinesLogging == LogLevel.Debug)
			{
				LogActiveTimeline(active, timelineGuid, $"Updated state to: {state}");
			}
		}

		internal static void SetAnimationState<T>(this ConcurrentDictionary<Guid, ActiveTimeline<T>> actives, Guid timelineGuid, AnimationState state)
			where T : DependencyObject
		{
			var active = actives
				.FirstOrDefault(kvp => kvp.Key.Equals(timelineGuid)
					&& kvp.Value.State != state
					&& kvp.Value.State != AnimationState.Completed);

			if (active.Value != null)
			{
				active.Value.State = state;
			}

			if (Animations.EnableActiveTimelinesLogging == LogLevel.Debug)
			{
				LogActiveTimeline(active.Value, timelineGuid, $"Updated state to: {state}");
			}
		}

		internal static ActiveTimeline<T> SetTimeline<T>(this ConcurrentDictionary<Guid, ActiveTimeline<T>> actives, Guid elementGuid, T timeline)
			where T : DependencyObject
		{
			var activeKvp = actives.GetNextIdleKeyValuePair(elementGuid);

			if (activeKvp.Value != null)
			{
				// Clear the previous one if it exists
				(activeKvp.Value.Timeline as Timeline)?.Stop();
				activeKvp.Value.Timeline = default(T);

				// Make sure to re-use the existing "timeline" Guid
				Animations.SetTimelineGuid(timeline, activeKvp.Key);
				Animations.SetElementGuid(timeline, elementGuid);

				activeKvp.Value.Timeline = timeline;
			}

			return activeKvp.Value;
		}

		internal static bool AllIteratingCompleted<T>(this ConcurrentDictionary<Guid, ActiveTimeline<T>> actives, Guid elementGuid)
			where T : DependencyObject
		{
			return actives
				.Where(kvp => kvp.Value.ElementGuid.Equals(elementGuid))
				.All(kvp => kvp.Value.State == AnimationState.Completed && kvp.Value.IsIterating);
		}

		internal static void ResetAllIteratingCompletedToIdle<T>(this ConcurrentDictionary<Guid, ActiveTimeline<T>> actives, Guid elementGuid)
			where T : DependencyObject
		{
			foreach(var activeKvp in actives
				.Where(kvp => kvp.Value.ElementGuid.Equals(elementGuid)
					&& kvp.Value.State == AnimationState.Completed
					&& kvp.Value.IsIterating))
			{
				activeKvp.Value.SetAnimationState(activeKvp.Key, AnimationState.Idle);
			}

			if (Animations.EnableActiveTimelinesLogging == LogLevel.Debug)
			{
				LogActiveTimelines(actives, "Reset timeline states to Idle");
			}
		}

		internal static bool IsElementAnimating<T>(this ConcurrentDictionary<Guid, ActiveTimeline<T>> actives, Guid elementGuid)
			where T : DependencyObject
		{
			return actives.Any(kvp => kvp.Value.ElementGuid.Equals(elementGuid) && !kvp.Value.IsIterating);
		}

		private static void LogActiveTimeline<T>(ActiveTimeline<T> active, Guid timelineGuid, string message)
			where T : DependencyObject
		{
			var builder = new StringBuilder();

			builder.AppendLine();
			builder.AppendLine("---------- ACTIVE TIMELINE --------");
			builder.AppendLine($"Guid {timelineGuid} - {message} at {DateTimeOffset.Now.ToString("HH:mm:ss:fffff")}");
			builder.AppendLine("------------------------------------");

			Logger?.LogDebug(builder.ToString());
		}

		private static void LogActiveTimelines<T>(ConcurrentDictionary<Guid, ActiveTimeline<T>> actives, string message)
			where T : DependencyObject
		{
			var builder = new StringBuilder();

			builder.AppendLine();
			builder.AppendLine("---------- ALL ACTIVE TIMELINES --------");
			builder.AppendLine($"{message} at {DateTimeOffset.Now.ToString("HH:mm:ss:fffff")}");
			builder.AppendLine();

			if (actives.Count > 0)
			{
				foreach (var kvp in actives)
				{
					var active = kvp.Value;

					builder.AppendLine(
						$"Element = {active.Element.GetType().Name},  " +
						$"Key = {kvp.Key},  " +
						$"ElementGuid = {active.ElementGuid}");
					builder.AppendLine(
						$"\t State = {active.State},  " +
						$"SequenceOrder = {active.SequenceOrder},  " +
						$"IsSequence = {active.IsSequence},  " +
						$"IsIterating = {active.IsIterating},  " +
						$"IterationBehavior = {active.IterationBehavior},  " +
						$"IterationCount = {active.IterationCount}");
				}
			}
			else
			{
				builder.AppendLine(" NO ACTIVE TIMELINES!");
			}

			builder.AppendLine("------------------------------------");

			Logger?.LogDebug(builder.ToString());
		}
	}
}