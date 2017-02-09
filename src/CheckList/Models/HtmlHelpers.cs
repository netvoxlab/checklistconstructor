using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace CheckList.Models
{
	public static class HtmlHelpers
	{
		public static IHtmlString RemoveLink(this HtmlHelper htmlHelper, string linkText, string container, string deleteElement)
		{
			var js = string.Format("javascript:removeNestedForm(this,'{0}','{1}');return false;", container, deleteElement);
			TagBuilder tb = new TagBuilder("a");
			tb.Attributes.Add("href", "#");
			tb.Attributes.Add("onclick", js);
			tb.InnerHtml = linkText;
			var tag = tb.ToString(TagRenderMode.Normal);
			return MvcHtmlString.Create(tag);
		}

		public static IHtmlString AddLink<TModel>(this HtmlHelper<TModel> htmlHelper, string linkText, string containerElement, string counterElement, string collectionProperty, string name,Type nestedType)
		{
			var items = new List<SelectListItem>
			{
				new SelectListItem() {Text = "Текст", Value = "1", Selected = true},
				new SelectListItem() {Text = "Статус", Value = "2"},
			};

			var ticks = DateTime.UtcNow.Ticks;
			var partial = htmlHelper.TextBox(name).ToHtmlString().JsEncode();
			
			if (name == "columns")
			{
				var str = htmlHelper.DropDownList("status", items);
				partial += str.ToHtmlString().JsEncode();
			}
			var js = string.Format("javascript:addNestedForm('{0}','{1}','{2}','{3}<br/><br/>');return false;", containerElement, counterElement, ticks, partial);
			TagBuilder tb = new TagBuilder("a");
			tb.Attributes.Add("href", "#");
			tb.Attributes.Add("onclick", js);
			tb.InnerHtml = linkText;
			var tag = tb.ToString(TagRenderMode.Normal);
			return MvcHtmlString.Create(tag);
		}

		private static string JsEncode(this string s)
		{
			if (string.IsNullOrEmpty(s)) return "";
			int i;
			int len = s.Length;
			StringBuilder sb = new StringBuilder(len + 4);
			string t;
			for (i = 0; i < len; i += 1)
			{
				char c = s[i];
				switch (c)
				{
					case '>':
					case '"':
					case '\\':
						sb.Append('\\');
						sb.Append(c);
						break;
					case '\b':
						sb.Append("\\b");
						break;
					case '\t':
						sb.Append("\\t");
						break;
					case '\n':
						break;
					case '\f':
						sb.Append("\\f");
						break;
					case '\r':
						break;
					default:
						if (c < ' ')
						{
							string tmp = new string(c, 1);
							t = "000" + int.Parse(tmp, System.Globalization.NumberStyles.HexNumber);
							sb.Append("\\u" + t.Substring(t.Length - 4));
						}
						else
						{
							sb.Append(c);
						}
						break;
				}
			}
			return sb.ToString();
		}


	}
}