﻿using System;

namespace WebApplication
{
	public partial class Default : System.Web.UI.Page
	{
		#region Methods

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			this.AppSettingRepeater.DataBind();
		}

		#endregion
	}
}