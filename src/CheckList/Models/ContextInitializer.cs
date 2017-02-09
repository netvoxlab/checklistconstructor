using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CheckList.Models
{
	public class ContextInitializer : //DropCreateDatabaseAlways<Context>
									  //DropCreateDatabaseIfModelChanges<Context>
									  CreateDatabaseIfNotExists<Context>
	{
		protected override void Seed(Context context)
		{
			#region Users
			var users = new List<User>()
			{
				new User() { Name = "User 1"},
				new User() { Name = "User 2"},
				new User() { Name = "User 3"},
			};

			users.ForEach(u => context.Users.Add(u));
			#endregion

			#region Surveys

			var surveys = new List<Survey>()
			{
				new Survey() { Name = "Чек-лист по итогам тестирования функционала инкассовых поручений с ФСС"},
				new Survey() { Name = "Survey 2"},
				new Survey() { Name = "Survey 3"},
			};

			surveys.ForEach(s => context.Surveys.Add(s));


			#endregion
			
			#region SurveyHeaders

			var surveyHeaders = new List<SurveyHeader>()
			{
				new SurveyHeader() { Name = "Отвественный от УФК", Survey = surveys[0]},
				new SurveyHeader() { Name = "Должность", Survey = surveys[0]},
				new SurveyHeader() { Name = "Дата отчета", Survey = surveys[0]},
				new SurveyHeader() { Name = "Контактный телефон", Survey = surveys[0]},
				new SurveyHeader() { Name = "Email", Survey = surveys[0]},
				new SurveyHeader() { Name = "Код региона", Survey = surveys[0]},

				new SurveyHeader() { Name = "s2 - Question 1", Survey = surveys[1]},
				new SurveyHeader() { Name = "s2 - Question 2", Survey = surveys[1]},
				new SurveyHeader() { Name = "s2 - Question 3", Survey = surveys[1]},

				new SurveyHeader() { Name = "s3 - Question 1", Survey = surveys[2]},
				new SurveyHeader() { Name = "s3 - Question 2", Survey = surveys[2]},
			};

			surveyHeaders.ForEach(s => context.SurveyHeaders.Add(s));


			#endregion

			#region SurveyColumn

			var surveyColumns = new List<SurveyColumn>()
			{
				new SurveyColumn() { Name = "Этапы/Операции", Survey = surveys[0], Order = 0},
				new SurveyColumn() { Name = "Дата начала", Survey = surveys[0], Order = 1},
				new SurveyColumn() { Name = "Дата окончания", Survey = surveys[0], Order = 2},
				new SurveyColumn() { Name = "Статус выполнения операций", Survey = surveys[0], Order = 3, Type = 2},
				new SurveyColumn() { Name = "Перечень обращений", Survey = surveys[0], Order = 4},
				new SurveyColumn() { Name = "Комментарий УФК", Survey = surveys[0], Order = 5}
			};

			surveyColumns.ForEach(s => context.SurveyColumns.Add(s));

			#endregion

			#region SurveyRow

			var surveyRows = new List<SurveyRow>()
			{
				new SurveyRow() { Name = "Проведение сверки справочника", Survey = surveys[0], Order = 0},
				new SurveyRow() { Name = "Проведение проверки настройки", Survey = surveys[0], Order = 1},
				new SurveyRow() { Name = "Проведение в АРМ", Survey = surveys[0], Order = 2},
				new SurveyRow() { Name = "Установка в справочнике", Survey = surveys[0], Order = 3},
				new SurveyRow() { Name = "Проверка поступления", Survey = surveys[0], Order = 4},
				new SurveyRow() { Name = "Проверка поступления СУФД", Survey = surveys[0], Order = 5}
			};

			surveyRows.ForEach(s => context.SurveyRows.Add(s));

			#endregion

			#region SurveyDataHeader

			var surveyDataHeader = new List<SurveyDataHeader>()
			{
				new SurveyDataHeader() { Name = "data 1", Survey = surveys[0], User = users[0], SurveyHeader = surveyHeaders[0]},
				new SurveyDataHeader() { Name = "data 2", Survey = surveys[0], User = users[0], SurveyHeader = surveyHeaders[1]},
				new SurveyDataHeader() { Name = "data 3", Survey = surveys[0], User = users[0], SurveyHeader = surveyHeaders[2]}
			};

			surveyDataHeader.ForEach(s => context.SurveyDataHeaders.Add(s));

			#endregion

			#region SurveyDataDetails

			var surveyDataDetails = new List<SurveyDataDetails>()
			{
				new SurveyDataDetails() { Name = "10", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[3], SurveyRow = surveyRows[0]},
				new SurveyDataDetails() { Name = "20", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[3], SurveyRow = surveyRows[1]},
				new SurveyDataDetails() { Name = "30", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[3], SurveyRow = surveyRows[2]},
				new SurveyDataDetails() { Name = "10", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[3], SurveyRow = surveyRows[3]},
				new SurveyDataDetails() { Name = "20", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[3], SurveyRow = surveyRows[4]},
				new SurveyDataDetails() { Name = "30", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[3], SurveyRow = surveyRows[5]},

				new SurveyDataDetails() { Name = "Detail 4", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[1], SurveyRow = surveyRows[0]},
				new SurveyDataDetails() { Name = "Detail 5", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[1], SurveyRow = surveyRows[1]},
				new SurveyDataDetails() { Name = "Detail 6", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[1], SurveyRow = surveyRows[2]},
				new SurveyDataDetails() { Name = "Detail 7", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[1], SurveyRow = surveyRows[3]},
				new SurveyDataDetails() { Name = "Detail 8", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[1], SurveyRow = surveyRows[4]},
				new SurveyDataDetails() { Name = "Detail 9", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[1], SurveyRow = surveyRows[5]},

				new SurveyDataDetails() { Name = "Detail 4", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[2], SurveyRow = surveyRows[0]},
				new SurveyDataDetails() { Name = "Detail 5", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[2], SurveyRow = surveyRows[1]},
				new SurveyDataDetails() { Name = "Detail 6", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[2], SurveyRow = surveyRows[2]},
				new SurveyDataDetails() { Name = "Detail 7", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[2], SurveyRow = surveyRows[3]},
				new SurveyDataDetails() { Name = "Detail 8", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[2], SurveyRow = surveyRows[4]},
				new SurveyDataDetails() { Name = "Detail 9", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[2], SurveyRow = surveyRows[5]},

				new SurveyDataDetails() { Name = "Detail 4", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[4], SurveyRow = surveyRows[0]},
				new SurveyDataDetails() { Name = "Detail 5", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[4], SurveyRow = surveyRows[1]},
				new SurveyDataDetails() { Name = "Detail 6", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[4], SurveyRow = surveyRows[2]},
				new SurveyDataDetails() { Name = "Detail 7", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[4], SurveyRow = surveyRows[3]},
				new SurveyDataDetails() { Name = "Detail 8", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[4], SurveyRow = surveyRows[4]},
				new SurveyDataDetails() { Name = "Detail 9", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[4], SurveyRow = surveyRows[5]},

				new SurveyDataDetails() { Name = "Detail 4", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[5], SurveyRow = surveyRows[0]},
				new SurveyDataDetails() { Name = "Detail 5", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[5], SurveyRow = surveyRows[1]},
				new SurveyDataDetails() { Name = "Detail 6", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[5], SurveyRow = surveyRows[2]},
				new SurveyDataDetails() { Name = "Detail 7", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[5], SurveyRow = surveyRows[3]},
				new SurveyDataDetails() { Name = "Detail 8", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[5], SurveyRow = surveyRows[4]},
				new SurveyDataDetails() { Name = "Detail 9", Survey = surveys[0], User = users[0], SurveyColumn = surveyColumns[5], SurveyRow = surveyRows[5]},
			};

			surveyDataDetails.ForEach(s => context.SurveyDataDetails.Add(s));

			#endregion

			context.SaveChanges();
		}
	}
}