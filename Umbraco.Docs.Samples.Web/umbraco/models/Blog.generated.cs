//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v9.0.0-rc003+36582f2cd2c6ddc797c85dac0a9a6572126bf602
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Infrastructure.ModelsBuilder;
using Umbraco.Cms.Core;
using Umbraco.Extensions;

namespace Umbraco.Cms.Web.Common.PublishedModels
{
	/// <summary>Blog</summary>
	[PublishedModel("blog")]
	public partial class Blog : PublishedContentModel, IContentBase, INavigationBase
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.0.0-rc003+36582f2cd2c6ddc797c85dac0a9a6572126bf602")]
		public new const string ModelTypeAlias = "blog";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.0.0-rc003+36582f2cd2c6ddc797c85dac0a9a6572126bf602")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.0.0-rc003+36582f2cd2c6ddc797c85dac0a9a6572126bf602")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.0.0-rc003+36582f2cd2c6ddc797c85dac0a9a6572126bf602")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<Blog, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public Blog(IPublishedContent content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Disqus Shortname: To use comments, you'll need to sign up for Disqus and enter your shortname here (more info: https://help.disqus.com/customer/portal/articles/472097-universal-embed-code)
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.0.0-rc003+36582f2cd2c6ddc797c85dac0a9a6572126bf602")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("disqusShortname")]
		public virtual string DisqusShortname => this.Value<string>(_publishedValueFallback, "disqusShortname");

		///<summary>
		/// How many posts should be shown?
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.0.0-rc003+36582f2cd2c6ddc797c85dac0a9a6572126bf602")]
		[ImplementPropertyType("howManyPostsShouldBeShown")]
		public virtual decimal HowManyPostsShouldBeShown => this.Value<decimal>(_publishedValueFallback, "howManyPostsShouldBeShown");

		///<summary>
		/// Content
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.0.0-rc003+36582f2cd2c6ddc797c85dac0a9a6572126bf602")]
		[ImplementPropertyType("bodyText")]
		public virtual global::Newtonsoft.Json.Linq.JToken BodyText => global::Umbraco.Cms.Web.Common.PublishedModels.ContentBase.GetBodyText(this, _publishedValueFallback);

		///<summary>
		/// Page Title: The title of the page, this is also the first text in a google search result. The ideal length is between 40 and 60 characters
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.0.0-rc003+36582f2cd2c6ddc797c85dac0a9a6572126bf602")]
		[ImplementPropertyType("pageTitle")]
		public virtual string PageTitle => global::Umbraco.Cms.Web.Common.PublishedModels.ContentBase.GetPageTitle(this, _publishedValueFallback);

		///<summary>
		/// Keywords: Keywords that describe the content of the page. This is considered optional since most modern search engines don't use this anymore
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.0.0-rc003+36582f2cd2c6ddc797c85dac0a9a6572126bf602")]
		[ImplementPropertyType("keywords")]
		public virtual global::System.Collections.Generic.IEnumerable<string> Keywords => global::Umbraco.Cms.Web.Common.PublishedModels.NavigationBase.GetKeywords(this, _publishedValueFallback);

		///<summary>
		/// Description: A brief description of the content on your page. This text is shown below the title in a google search result and also used for Social Sharing Cards. The ideal length is between 130 and 155 characters
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.0.0-rc003+36582f2cd2c6ddc797c85dac0a9a6572126bf602")]
		[ImplementPropertyType("seoMetaDescription")]
		public virtual string SeoMetaDescription => global::Umbraco.Cms.Web.Common.PublishedModels.NavigationBase.GetSeoMetaDescription(this, _publishedValueFallback);

		///<summary>
		/// Hide in Navigation: If you don't want this page to appear in the navigation, check this box
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.0.0-rc003+36582f2cd2c6ddc797c85dac0a9a6572126bf602")]
		[ImplementPropertyType("umbracoNavihide")]
		public virtual bool UmbracoNavihide => global::Umbraco.Cms.Web.Common.PublishedModels.NavigationBase.GetUmbracoNavihide(this, _publishedValueFallback);
	}
}
