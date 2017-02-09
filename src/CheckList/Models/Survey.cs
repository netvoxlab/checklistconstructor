using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckList.Models
{
	public class Survey
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual ICollection<SurveyHeader> SurveyHeaders { get; set; }
		public virtual ICollection<SurveyColumn> SurveyColumns { get; set; }
		public virtual ICollection<SurveyRow> SurveyRows { get; set; }
		public virtual ICollection<SurveyDataHeader> SurveyDataHeaders { get; set; }
		public virtual ICollection<SurveyDataDetails> SurveyDataDetails { get; set; }
	}
}