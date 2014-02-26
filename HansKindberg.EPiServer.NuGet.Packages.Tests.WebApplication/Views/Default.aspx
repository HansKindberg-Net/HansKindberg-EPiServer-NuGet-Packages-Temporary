<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="Default.aspx.cs" Inherits="HansKindberg.EPiServer.NuGet.Packages.Tests.WebApplication.Views.Default" %>
<%@ Import Namespace="EPiServer.Configuration" %>
<%@ Import Namespace="EPiServer.Web" %><!DOCTYPE html>
<html>
	<head>
		<title>Views/Default</title>
	</head>
	<body>
		<h1>Views/Default</h1>
		<p><a href="/">Home</a></p>
		<h2>AppSettings</h2>
		<asp:Repeater id="AppSettingRepeater" DataSource="<%# ConfigurationManager.AppSettings.Keys %>" runat="server">
			<HeaderTemplate><ul></HeaderTemplate>
			<ItemTemplate><li><strong><%# Container.DataItem %>: </strong><%# ConfigurationManager.AppSettings[(string) Container.DataItem] %></li></ItemTemplate>
			<FooterTemplate></ul></FooterTemplate>
		</asp:Repeater>
		<h2>EPiServer site-settings</h2>
		<ul>
			<li><strong>EPiServer.Configuration.Settings.Instance.PageFolderVirtualPathProviderName: </strong><%= Settings.Instance.PageFolderVirtualPathProviderName %></li>
			<li><strong>EPiServer.Configuration.EPiServerSection.Instance.ApplicationSettings.UIUrl: </strong><%= EPiServerSection.Instance.ApplicationSettings.UIUrl %></li>
			<li><strong>EPiServer.Web.SiteDefinition.Current.SiteUrl: </strong><%= SiteDefinition.Current.SiteUrl %></li>
		</ul>
	</body>
</html>