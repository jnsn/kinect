using System;
using System.Linq;
using System.Windows;
using Coding4Fun.Kinect.Wpf;
using KinectResearch.Infrastructure.Events;
using KinectResearch.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.Events;
using Microsoft.Research.Kinect.Nui;

namespace KinectResearch.Modules.Core.Services
{
	internal class ConcreteKinectService : IKinectService
	{
		private readonly IEventAggregator _eventAggregator;

		private bool _isInitialized;

		public ConcreteKinectService(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
		}

		#region IKinectService Members

		public Runtime Kinect { get; private set; }

		public void Initialize()
		{
			try
			{
				Runtime.Kinects.StatusChanged += OnKinectStatusChanged;

				foreach (var kinect in Runtime.Kinects.Where(kinect => kinect.Status == KinectStatus.Connected))
				{
					Kinect = kinect;
					break;
				}

				if (Runtime.Kinects.Count == 0)
				{
					MessageBox.Show("No Kinect device connected.");
				}
				else
				{
					Setup();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void Uninitialize()
		{
			if (Kinect != null)
			{
				Kinect.VideoFrameReady -= OnRunTimeVideoFrameReady;
				Kinect.SkeletonFrameReady -= OnRunTimeSkeletonFrameReady;

				Kinect.Uninitialize();
			}

			_isInitialized = false;
		}

		#endregion

		private void OnKinectStatusChanged(object sender, StatusChangedEventArgs e)
		{
			switch (e.Status)
			{
				case KinectStatus.Connected:
					if (Kinect == null)
					{
						Kinect = e.KinectRuntime;
						Setup();
					}
					break;
				case KinectStatus.Disconnected:
					if (Kinect == e.KinectRuntime)
					{
						Uninitialize();
						MessageBox.Show("Kinect was disconnected.");
					}
					break;
				case KinectStatus.NotReady:
					break;
				default:
					MessageBox.Show("Unhandled Status: " + e.Status);
					break;
			}
		}

		private void Setup()
		{
			if (Kinect != null)
			{
				if (_isInitialized)
				{
					Uninitialize();
				}

				Kinect.Initialize(RuntimeOptions.UseSkeletalTracking | RuntimeOptions.UseColor);
				Kinect.SkeletonEngine.TransformSmooth = true;

				Kinect.VideoStream.Open(ImageStreamType.Video, 2, ImageResolution.Resolution640x480, ImageType.Color);

				Kinect.VideoFrameReady += OnRunTimeVideoFrameReady;
				Kinect.SkeletonFrameReady += OnRunTimeSkeletonFrameReady;

				Kinect.SkeletonEngine.TransformSmooth = true;
				//var parameters = new TransformSmoothParameters
				//{
				//    Smoothing = 0.75f,
				//    Correction = 0.0f,
				//    Prediction = 0.0f,
				//    JitterRadius = 0.05f,
				//    MaxDeviationRadius = 0.04f
				//};
				var parameters = new TransformSmoothParameters
				{
					Smoothing = 1.0f,
					Correction = 0.1f,
					Prediction = 0.1f,
					JitterRadius = 0.05f,
					MaxDeviationRadius = 0.05f
				};
				Kinect.SkeletonEngine.SmoothParameters = parameters;

				_isInitialized = true;
			}
		}

		private void OnRunTimeVideoFrameReady(object sender, ImageFrameReadyEventArgs e)
		{
			var image = e.ImageFrame.ToBitmapSource();
			_eventAggregator.GetEvent<VideoFrameUpdate>().Publish(image);
		}

		private void OnRunTimeSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
		{
			try
			{
				var data = e.SkeletonFrame.Skeletons.FirstOrDefault(x => x.TrackingState == SkeletonTrackingState.Tracked);
				if (data != null)
				{
					_eventAggregator.GetEvent<SkeletonFrameUpdate>().Publish(data);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}