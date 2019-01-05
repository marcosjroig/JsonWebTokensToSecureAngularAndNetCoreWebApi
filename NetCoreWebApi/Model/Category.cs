using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PtcApi.Model
{
  [Table("Category", Schema = "dbo")]
  public class Category
  {
    public int CategoryId { get; set; }

    [Required()]
    [StringLength(30)]
    public string CategoryName { get; set; }
  }
}
