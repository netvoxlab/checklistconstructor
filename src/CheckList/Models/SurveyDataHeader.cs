using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckList.Models
{
	public class SurveyDataHeader
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual Survey Survey { get; set; }
		public virtual User User { get; set; }
		public virtual SurveyHeader SurveyHeader { get; set; }
	}
}