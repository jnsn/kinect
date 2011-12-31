using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;

namespace KinectResearch.Infrastructure.Controls
{
	/// <summary>
	/// Interaction logic for KinectButton.xaml
	/// </summary>
	public partial class KinectButton
	{
		public static readonly DependencyProperty JointXPositionProperty =
			DependencyProperty.Register("JointXPosition", typeof (double), typeof (KinectButton), new PropertyMetadata(0.0, OnJointXPositionChanged));

		public static readonly DependencyProperty JointYPositionProperty =
			DependencyProperty.Register("JointYPosition", typeof (double), typeof (KinectButton), new PropertyMetadata(0.0, OnJointYPositionChanged));

		public static readonly DependencyProperty ImageProperty =
			DependencyProperty.Register("Image", typeof (ImageSource), typeof (KinectButton), new PropertyMetadata(null));

		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.Register("Command", typeof (DelegateCommand), typeof (KinectButton), new PropertyMetadata(null));

		private readonly Timer _timer = new Timer();

		public KinectButton()
		{
			InitializeComponent();

			_timer = new Timer {Interval = 3000};
			_timer.Elapsed += OnTimerElapsed;
		}

		internal Timer Timer
		{
			get { return _timer; }
		}

		public double JointXPosition
		{
			get { return (double) GetValue(JointXPositionProperty); }
			set { SetValue(JointXPositionProperty, value); }
		}

		public double JointYPosition
		{
			get { return (double) GetValue(JointYPositionProperty); }
			set { SetValue(JointYPositionProperty, value); }
		}

		public ImageSource Image
		{
			get { return GetValue(ImageProperty) as ImageSource; }
			set { SetValue(ImageProperty, value); }
		}

		public DelegateCommand Command
		{
			get { return GetValue(CommandProperty) as DelegateCommand; }
			set { SetValue(CommandProperty, value); }
		}

		private void OnTimerElapsed(object sender, ElapsedEventArgs e)
		{
			Dispatcher.Invoke(
				(Action) (() =>
				{
					Timer.Stop();

					if (Command != null)
					{
						Command.Execute();
					}
				}),
				DispatcherPriority.Normal);
		}

		private static void OnJointXPositionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			var button = o as KinectButton;
			if (button != null)
			{
				if (IsHandOverButton(button, (double) e.NewValue, button.JointYPosition))
				{
					button.Opacity = 0.5;
					if (!button.Timer.Enabled)
					{
						button.Timer.Start();
					}
				}
				else
				{
					button.Opacity = 1.0;
					button.Timer.Stop();
				}
			}
		}

		private static void OnJointYPositionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			var button = o as KinectButton;
			if (button != null)
			{
				if (IsHandOverButton(button, button.JointXPosition, (double) e.NewValue))
				{
					button.Opacity = 0.5;
					if (!button.Timer.Enabled)
					{
						button.Timer.Start();
					}
				}
				else
				{
					button.Opacity = 1.0;
					button.Timer.Stop();
				}
			}
		}

		private static bool IsHandOverButton(FrameworkElement button, double x1, double y1)
		{
			var ancestor = button.FindAncestor<UserControl>();
			var point = button.TransformToAncestor(ancestor).Transform(new Point());

			double x = x1;
			double y = y1;

			double left = point.X;
			double right = left + button.ActualWidth;
			double top = point.Y;
			double bottom = top + button.ActualHeight;

			return ((x > left) && (x < right - 25)) && ((y > top) && (y < bottom - 25));
		}
	}
}