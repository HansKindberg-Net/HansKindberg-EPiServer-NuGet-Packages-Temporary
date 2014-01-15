<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="Default.aspx.cs" Inherits="WebApplication.Default" %>
<%@ Import Namespace="EPiServer.Configuration" %><!DOCTYPE html>
<html>
	<head>
		<title>Default</title>
	</head>
	<body>
		<h1>Default</h1>
		<p><a href="/Views/Default.aspx">Views/Default</a></p>
		<h2>AppSettings</h2>
		<asp:Repeater id="AppSettingRepeater" DataSource="<%# ConfigurationManager.AppSettings.Keys %>" runat="server">
			<HeaderTemplate><ul></HeaderTemplate>
			<ItemTemplate><li><strong><%# Container.DataItem %>: </strong><%# ConfigurationManager.AppSettings[(string) Container.DataItem] %></li></ItemTemplate>
			<FooterTemplate></ul></FooterTemplate>
		</asp:Repeater>
		<h2>EPiServer site-settings</h2>
		<ul>
			<li><strong>PageFolderVirtualPathProviderName: </strong><%= Settings.Instance.PageFolderVirtualPathProviderName %></li>
			<li><strong>SiteDisplayName: </strong><%= Settings.Instance.SiteDisplayName %></li>
			<li><strong>SiteUrl: </strong><%= Settings.Instance.SiteUrl %></li>			
		</ul>
	</body>
</html>