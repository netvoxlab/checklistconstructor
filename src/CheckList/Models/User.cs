using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckList.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int SharepointUserId { get; set; }
		public virtual ICollection<SurveyDataDetails> SurveyDataDetails { get; set; }
	}
}