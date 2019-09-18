using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment4
{
    public class ReturnBook
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string BookBarCode { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
