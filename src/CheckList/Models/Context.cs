using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web.Configuration;

namespace CheckList.Models
{
	public class Context : DbContext
	{
		public Context()
			: base(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
			//: base(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Work\CheckList\CheckList\App_Data\Database.mdf;Integrated Security=True")
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Survey> Surveys { get; set; }
		public DbSet<SurveyHeader> SurveyHeaders { get; set; }
		public DbSet<SurveyColumn> SurveyColumns { get; set; }
		public DbSet<SurveyRow> SurveyRows { get; set; }
		public DbSet<SurveyDataHeader> SurveyDataHeaders { get; set; }
		public DbSet<SurveyDataDetails> SurveyDataDetails { get; set; }
		
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}