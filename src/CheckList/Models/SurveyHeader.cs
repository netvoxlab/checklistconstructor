using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckList.Models
{
	public class SurveyHeader
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual Survey Survey { get; set; }
		public virtual ICollection<SurveyDataHeader> SurveyDataHeaders { get; set; }
	}
}