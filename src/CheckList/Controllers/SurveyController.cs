using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CheckList.Models;
using WebGrease.Css.Extensions;

namespace CheckList.Controllers
{
	public class SurveyController : Controller
	{
		private List<User> _users;
		private static User _currentUser = new User() {Id = 1, Name = "User 1", SharepointUserId = 0};
		private readonly Context _db = new Context();
		
		public ActionResult Index()
		{
			_users = _db.Users.ToList();

			var items = new List<SelectListItem>
			{
				new SelectListItem {Text = _users[0].Name, Value = "0", Selected = true},
				new SelectListItem {Text = _users[1].Name, Value = "1"},
				new SelectListItem {Text = _users[2].Name, Value = "2"},
			};
			
			ViewBag.Users = items;
			ViewBag.CurrentUser = _currentUser.Name;

			return View(_db.Surveys.ToList());
		}

		[HttpPost]
		public ActionResult Index(string users)
		{
			_users = _db.Users.ToList();

			var items = new List<SelectListItem>
			{
				new SelectListItem {Text = _users[0].Name, Value = "0", Selected = users == "0"},
				new SelectListItem {Text = _users[1].Name, Value = "1", Selected = users == "1"},
				new SelectListItem {Text = _users[2].Name, Value = "2", Selected = users == "2"},
			};

			int id = Convert.ToInt32(users);

			_currentUser = _db.Users.FirstOrDefault(u => u.Id == id + 1);
			ViewBag.Users = items;
			ViewBag.CurrentUser = _currentUser.Name;
			return View(_db.Surveys.ToList());
		}

		public ActionResult Create()
		{
			ViewBag.CurrentUser = _currentUser.Name;
			return View();
		}

		[HttpPost]
		public ActionResult Create(Survey survey, List<string> headers, List<string> columns, List<string> rows, List<string> status)
		{
			_db.Surveys.Add(survey);

			foreach (var header in headers) {
				_db.SurveyHeaders.Add(new SurveyHeader() { Name = header, Survey = survey });
			}
			foreach (var column in columns)
			{
				var type = Convert.ToInt32(status[columns.IndexOf(column)]);
				_db.SurveyColumns.Add(new SurveyColumn() { Name = column, Survey = survey, Order = columns.IndexOf(column), Type = type });
			}
			foreach (var row in rows){
				_db.SurveyRows.Add(new SurveyRow() { Name = row, Survey = survey, Order = rows.IndexOf(row) });
			}
			
			_db.SaveChanges();

			ViewBag.CurrentUser = _currentUser.Name;
			return View("Success");
		}
		
		public ActionResult Remove(Survey survey)
		{
			var headers = _db.SurveyHeaders.Where(d => d.Survey.Id == survey.Id).ToList();
			_db.SurveyHeaders.RemoveRange(headers);
			var headersData = _db.SurveyDataHeaders.Where(d => d.Survey.Id == survey.Id).ToList();
			_db.SurveyDataHeaders.RemoveRange(headersData);
			var columns = _db.SurveyColumns.Where(c => c.Survey.Id == survey.Id).ToList();
			_db.SurveyColumns.RemoveRange(columns);
			var rows = _db.SurveyRows.Where(r => r.Survey.Id == survey.Id).ToList();
			_db.SurveyRows.RemoveRange(rows);
			var details = _db.SurveyDataDetails.Where(d => d.Survey.Id == survey.Id).ToList();
			_db.SurveyDataDetails.RemoveRange(details);

			_db.SaveChanges();
			
			_db.Entry(survey).State = EntityState.Deleted;
			_db.SaveChanges();

			return Redirect("~/Survey/Index");
		}
		
		public ActionResult Pass(Survey survey)
		{
			ViewBag.UserId = _currentUser.Id;

			var surv = _db.Surveys.Where(s => s.Id == survey.Id)
				.Include(s => s.SurveyHeaders)
				.Include(s => s.SurveyDataHeaders)
				.Include(s => s.SurveyDataDetails)
				.Include(s => s.SurveyColumns)
				.ToList();

			ViewBag.CurrentUser = _currentUser.Name;
			return View(surv.FirstOrDefault());
		}
		
		public ActionResult Save(Survey survey, IDictionary<int, string> names, IDictionary<string, string> details)
		{
			foreach (var name in names)
			{
				var data = new SurveyDataHeader()
				{
					Name = name.Value,
					Survey = _db.Surveys.FirstOrDefault(s => s.Id == survey.Id),
					SurveyHeader = _db.SurveyHeaders.FirstOrDefault(h => h.Id == name.Key),
					User = _db.Users.FirstOrDefault(u => u.Id == _currentUser.Id)
				};

				var entity =
					_db.SurveyDataHeaders.FirstOrDefault(d => d.Survey.Id == survey.Id 
					&& d.SurveyHeader.Id == data.SurveyHeader.Id
					&& d.User.Id == _currentUser.Id);

				if (entity != null) {
					entity.Name = data.Name;
				} else {
					_db.SurveyDataHeaders.Add(data);
				}
			}
			
			foreach (var detail in details)
			{
				var ind = detail.Key.Split(':');
				int row = Convert.ToInt32(ind[0]);
				int col = Convert.ToInt32(ind[1]);

				var dt = new SurveyDataDetails()
				{
					Name = detail.Value,
					Survey = _db.Surveys.FirstOrDefault(s => s.Id == survey.Id),
					SurveyRow = _db.SurveyRows.FirstOrDefault(r => r.Id == row),
					SurveyColumn = _db.SurveyColumns.FirstOrDefault(c => c.Id == col),
					User = _db.Users.FirstOrDefault(u => u.Id == _currentUser.Id)
				};
				
				var entity = _db.SurveyDataDetails.FirstOrDefault(
					d => d.Survey.Id == survey.Id &&
					d.SurveyColumn.Id == dt.SurveyColumn.Id &&
					d.SurveyRow.Id == dt.SurveyRow.Id &&
					d.User.Id == _currentUser.Id
				);
				
				if (entity != null) {
					entity.Name = dt.Name;
				} else {
					_db.SurveyDataDetails.Add(dt);
				}
			}

			_db.SaveChanges();

			return Redirect("~/Survey/Index");
		}

		public ActionResult Success()
		{
			return View();
		}
		
		public ActionResult Result(Survey survey)
		{
			var data = _db.SurveyDataDetails.Where(d => d.Survey.Id == survey.Id).ToList();
			return View(data);
		}
	}
}