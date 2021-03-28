using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace App
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public List<Page> Pages { get; set; }
        
        public override string ToString()
        {
            return Name + ": "+ string.Join(", ", Pages.Select(x => x.Content));
        }
    }
}