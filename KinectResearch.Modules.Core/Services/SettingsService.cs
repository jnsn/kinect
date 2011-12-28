using KinectResearch.Infrastructure.Interfaces;
using KinectResearch.Infrastructure.Model;

namespace KinectResearch.Modules.Core.Services
{
	internal class SettingsService : ISettingsService
	{
		#region ISettingsService Members

		public Settings Settings { get; set; }

		#endregion
	}
}