using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckList.Models
{
	public class SurveyDataDetails
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual Survey Survey { get; set; }
		public virtual User User { get; set; }
		public virtual SurveyColumn SurveyColumn { get; set; }
		public virtual SurveyRow SurveyRow { get; set; }
	}
}