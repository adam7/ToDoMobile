using System.ComponentModel.DataAnnotations;

namespace ToDoMobile.Web.Models
{
	public class ToDoItem
	{
		public string Id { get; set; }
		[Required]
		public string Name { get; set; }
		public bool Complete { get; set; }
	}
}