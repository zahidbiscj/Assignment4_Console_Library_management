﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment4
{
    public class IssueAndReturnBook
    {
        public  void IssueBook(LibraryContext context)
        {
            Student student = new Student();
            Book book = new Book();
            
            Console.WriteLine("Issue a book to student");
            Console.WriteLine("===============================");
            Console.Write("Please Enter Student Id : ");
            student.Id = int.Parse(Console.ReadLine());

            Console.Write("Please Enter Book Barcode : ");
            book.BarCode = Console.ReadLine();
            Console.WriteLine("===============================");

            try
            {
                //book id anar jonno lagbe eta
                var b = context.Books.Where(x => x.BarCode == book.BarCode).SingleOrDefault();

                if (context.Students.Any(db => db.Id == student.Id) &&
                    context.Books.Any(db => db.BarCode == book.BarCode) &&
                    context.Books.Any(db => db.CopyCount >= 0))
                {
    
                    context.IssueBook.Add(new IssueBook()
                    {
                        StudentId = student.Id,
                        BookBarCode = book.BarCode,
                        bookId = b.Id,
                        IssueDate = DateTime.UtcNow
                    });
       //Issue korle ekta boi niye gese tai copyCount 1 Decrement
                    var bookAvailableCopyCount = context.Books.Where(x => x.BarCode == book.BarCode).SingleOrDefault();
                    bookAvailableCopyCount.CopyCount -= 1;
                    context.SaveChanges();
                    Console.WriteLine("Book Issues Successfully");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }
        public  void ReturnBook(LibraryContext context)
        {
            Student student = new Student();
            Book book = new Book();

            Console.WriteLine("Return a book ");
            Console.WriteLine("===============================");
            Console.Write("Please Enter Student Id : ");
            student.Id = int.Parse(Console.ReadLine());

            Console.Write("Please Enter Book Barcode : ");
            book.BarCode = Console.ReadLine();

            var b = context.IssueBook.Where(x => x.BookBarCode == book.BarCode && x.StudentId == student.Id).FirstOrDefault();
            var c = ((DateTime.UtcNow - b.IssueDate).Days)-1;
            var WeekDays = 7;
            var StudentUpdate = context.Students.Where(x => x.Id == student.Id).SingleOrDefault();
            if (c > WeekDays)
            { 
                var daysDelay = c - WeekDays;
                var fine = daysDelay * 10;
                StudentUpdate.FineAmount = fine;
                Console.WriteLine("Fine Updated Successfully");
            }
            //Receive korle ekta boi ferot paise tai copyCount 1 increment
            var bookAvailableCopyCount = context.Books.Where(x => x.BarCode == book.BarCode).SingleOrDefault();
            bookAvailableCopyCount.CopyCount += 1;

            context.ReturnBooks.Add(new ReturnBook()
            {
                StudentId = student.Id,
                BookId = book.Id,
                BookBarCode = book.BarCode,
                ReturnDate = DateTime.UtcNow
            });

            context.SaveChanges();
            Console.WriteLine("\n\n Books Received Successfuly...");
            Console.WriteLine("===============================");
        }
    }
}
