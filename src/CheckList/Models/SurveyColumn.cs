using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckList.Models
{
	public class SurveyColumn
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual Survey Survey { get; set; }
		public int Order { get; set; }
		public int Type { get; set; }
		public virtual ICollection<SurveyDataDetails> SurveyDataDetails { get; set; }
	}
}