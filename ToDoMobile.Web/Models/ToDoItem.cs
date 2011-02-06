using System.ComponentModel.DataAnnotations;

namespace ToDoMobile.Web.Models
{
	public class ToDoItem
	{
		public string Id { get; set; }
		[Required]
		public string Name { get; set; }
        [Required]
        [Range(1, 3)]
        public int? Priority { get; set; }
		public bool Complete { get; set; }
	}
}