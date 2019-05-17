using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Hosting;
using XamlFlair.Extensions;

namespace XamlFlair
{
	internal class AnimationGroupCompletedEventArgs : EventArgs
	{
		internal bool Completed { get; set; }
	}

	internal class AnimationGroup : DependencyObject
	{
		private FrameworkElement _element;
		private readonly Queue<AnimationBase> _fromAnimations = new Queue<AnimationBase>();
		private readonly Queue<AnimationBase> _toAnimations = new Queue<AnimationBase>();

		/// <summary>
		/// Occurs when all animations in the group have completed
		/// </summary>
		internal event EventHandler<AnimationGroupCompletedEventArgs> Completed;

		internal void CreateAnimations(FrameworkElement element, AnimationSettings settings, Func<AnimationGroup, AnimationBase> createAction)
		{
			var animation = createAction(this);

			// If the element is SelectorItem-based, we must check the logging property on its parent ListView
			if (element is SelectorItem
				&& element.FindAscendant<ListViewBase>() is ListViewBase lvb
				&& Animations.GetEnableLogging(lvb))
			{
				// Log for a SelectorItem
				element.LogAnimationInfo(settings, animation.TargetProperty);
			}
			else if (Animations.GetEnableLogging(element))
			{
				// Log for a FrameworkElement
				element.LogAnimationInfo(settings, animation.TargetProperty);
			}
		}

		internal void Add(FrameworkElement element, AnimationBase anim, bool isFrom)
		{
			if (_element == null)
			{
				_element = element;
			}

			if (isFrom)
			{
				_fromAnimations.Enqueue(anim);
			}
			else
			{
				_toAnimations.Enqueue(anim);
			}
		}

		internal void Begin()
		{
			var batch = Window.Current.Compositor.CreateScopedBatch(CompositionBatchTypes.Animation);

			// We don't execute any "to" animations until the "from" animations have completed
			if (_fromAnimations.Count > 0)
			{
				// Start all "from" animations (which are all immediate with a Duration = 1)
				while (_fromAnimations.Count > 0)
				{
					if (_fromAnimations.TryDequeue(out var anim))
					{
						anim.Start(_element, isFrom: true);
					}
				}
			}
			else
			{
				// Kick-off all "to" animations
				while (_toAnimations.Count > 0)
				{
					if (_toAnimations.TryDequeue(out var anim))
					{
						anim.Start(_element);
					}
				}
			}

			batch.Completed += Batch_Completed;
			batch.End();
		}

		internal void Stop()
		{
			while (_toAnimations.Count > 0)
			{
				if (_toAnimations.TryDequeue(out var anim))
				{
					anim.Stop();
				}
			}
		}

		internal void Cleanup()
		{
			Stop();
			_element = null;
		}

		private void Batch_Completed(object sender, CompositionBatchCompletedEventArgs args)
		{
			var batch = sender as CompositionScopedBatch;
			batch.Completed -= Batch_Completed;

			if (_toAnimations.Count > 0)
			{
				Begin();
			}
			else
			{
				Completed?.Invoke(this, new AnimationGroupCompletedEventArgs() { Completed = true });
			}
		}
	}
}