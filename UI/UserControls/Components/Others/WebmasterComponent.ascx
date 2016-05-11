<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebmasterComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Others.WebmasterComponent" %>

<%= string.Format("<meta name='msvalidate.01' content='{0}' />", OWDARO.AppConfig.BingWebmasterCenter) %>
<%= string.Format("<meta name='google-site-verification' content='{0}' />", OWDARO.AppConfig.GoogleWebmasterTool) %>
<%= string.Format("<meta name='p:domain_verify' content='{0}' />", OWDARO.AppConfig.PinterestSiteVerification) %>
<%= string.Format("<meta name='twitter:account_id' content='{0}' />", OWDARO.AppConfig.TwitterWebsite) %>
<%= string.Format("<meta name='yandex-verification' content='{0}' />", OWDARO.AppConfig.YandexWebmaster) %>
