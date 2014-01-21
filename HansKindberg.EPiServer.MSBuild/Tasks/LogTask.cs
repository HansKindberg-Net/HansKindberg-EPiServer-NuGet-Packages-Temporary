using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace HansKindberg.EPiServer.MSBuild.Tasks
{
	public abstract class LogTask : Task
	{
		#region Fields

		private MessageImportance _messageImportance = MessageImportance.Normal;
		private string _messagePrefix;
		private string _targetName;

		#endregion

		#region Properties

		public virtual string LogImportance
		{
			get { return this._messageImportance.ToString(); }
			set
			{
				if(string.IsNullOrEmpty(value))
				{
					this._messageImportance = MessageImportance.Normal;
				}
				else
				{
					try
					{
						this._messageImportance = (MessageImportance) Enum.Parse(typeof(MessageImportance), value, true);
					}
					catch(ArgumentException argumentException)
					{
						throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "\"{0}\" is an invalid value for the \"LogImportance\" parameter. Valid values are: High, Normal and Low.", value), "value", argumentException);
					}
				}
			}
		}

		protected internal virtual MessageImportance MessageImportance
		{
			get { return this._messageImportance; }
		}

		protected internal virtual string MessagePrefix
		{
			get { return this._messagePrefix ?? (this._messagePrefix = !string.IsNullOrEmpty(this.TargetName) ? "Target '" + this.TargetName + "'" : string.Empty); }
		}

		public virtual string TargetName
		{
			get { return this._targetName; }
			set
			{
				this._targetName = value;
				this._messagePrefix = null;
			}
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Microsoft.Build.Utilities.TaskLoggingHelper.LogMessage(Microsoft.Build.Framework.MessageImportance,System.String,System.Object[])")]
		protected internal virtual void LogMessage(string message)
		{
			string messagePrefix = this.MessagePrefix + (!string.IsNullOrEmpty(message) ? ": " : ".");

			this.Log.LogMessage(this.MessageImportance, "{0}", new object[] {messagePrefix + message});
		}

		#endregion
	}
}