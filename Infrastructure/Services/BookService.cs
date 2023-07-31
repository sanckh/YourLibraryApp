using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Enums;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBook_AuthorRepository _book_AuthorRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IAuthorService _authorService;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IPublisherService _publisherService;
        public BookService(IBookRepository bookRepository, IBook_AuthorRepository book_AuthorRepository, IAuthorRepository authorRepository, IAuthorService authorService)
        {
            _bookRepository = bookRepository;
            _book_AuthorRepository = book_AuthorRepository;
            _authorRepository = authorRepository;
            _authorService = authorService;
        }
        public async Task<int> DeleteBookAsync(int bookId, int userId)
        {
            var userBook = await _bookRepository.GetUserBookAsync(bookId, userId);

            if(userBook != null)
            {
                var deletedBook = await _bookRepository.DeleteAsync(userBook.Id);
                return deletedBook.Id;
            }

            //Lazy answer. Fix this later
            return 0;
        }

        public async Task<PagedResult<UserBookModel>> GetAllBooksAsync(
            int pageNumber,
            int pageSize,
            int userId,
            string sortColumn,
            SortDirection sortDirection)
        {
            var userBooks = await _bookRepository.GetAllBooksByUserIdAsync(userId);

            var totalCount = userBooks.Count();

            //Apply sorting
            var sortedBooks = sortDirection == SortDirection.Ascending
                    ? userBooks.OrderBy(b => b.GetType().GetProperty(sortColumn).GetValue(b))
                    : userBooks.OrderByDescending(b => b.GetType().GetProperty(sortColumn).GetValue(b));

            // Apply pagination
            var pagedBooks = sortedBooks.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var userBookModels = pagedBooks.Select(b => new UserBookModel
            {
                UserId = b.UserId,
                BookId = b.BookId,
                Book = new BookModel
                {
                    Id = b.Book.Id,
                    Title = b.Book.Title,
                    Description = b.Book.Description,
                    Genre = b.Book.Genre,
                    CoverUrl = b.Book.CoverUrl,
                    DateAdded = b.Book.DateAdded,
                    Publisher = new PublisherModel
                    {
                        Id = b.Book.Publisher.Id,
                        Name = b.Book.Publisher.Name,
                    },
                    Authors = b.Book.BookAuthors.Select(a => new AuthorModel
                    {
                        Id = a.Author.Id,
                        FullName = a.Author.FullName,
                    }).ToList(),
                },
                isRead = b.isRead,
                DateRead = b.DateRead.HasValue ? b.DateRead.Value : null,
                Rating = b.Rating.HasValue ? b.Rating.Value : 0,
            });

            return new PagedResult<UserBookModel>
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                Results = userBookModels.ToList(),
            };
        }

        public async Task<UserBookModel> UpdateBookAsync(int bookId, int userId, UserBookModel updatedUserBook)
        {
            var userBook = await _bookRepository.GetUserBookAsync(bookId, userId);

            if (userBook == null)
            {
                throw new ArgumentException("Book not found or not owned by the user!");
            }

            // Update only the allowed fields
            userBook.isRead = updatedUserBook.isRead;
            userBook.DateRead = updatedUserBook.DateRead;
            userBook.Rating = updatedUserBook.Rating;

            await _bookRepository.SaveChangesAsync();

            // Map the updated UserBook to a UserBookModel and return
            var userBookModel = new UserBookModel
            {
                UserId = userBook.UserId,
                BookId = userBook.BookId,
                isRead = userBook.isRead,
                DateRead = userBook.DateRead,
                Rating = userBook.Rating,
                DateAdded = userBook.DateAdded,
                Book = new BookModel
                {
                    Id = userBook.Book.Id,
                    Title = userBook.Book.Title,
                    Description = userBook.Book.Description,
                    Genre = userBook.Book.Genre,
                    CoverUrl = userBook.Book.CoverUrl,
                    DateAdded = userBook.Book.DateAdded,
                    Publisher = new PublisherModel
                    {
                        Id = userBook.Book.Publisher.Id,
                        Name = userBook.Book.Publisher.Name,
                    },
                    Authors = userBook.Book.BookAuthors.Select(a => new AuthorModel
                    {
                        Id = a.Author.Id,
                        FullName = a.Author.FullName,
                    }).ToList(),
                }
            };

            return userBookModel;
        }


        public async Task<List<UserBookModel>> GetRecentlyAddedBooksAsync(int days, int userId)
        {
            var startDate = DateTime.UtcNow.AddDays(-days);
            var userBooks = await _bookRepository.GetAllBooksByUserIdAsync(userId);

            var recentlyAddedUserBooks = userBooks
                .Where(b => b.DateAdded >= startDate)
                .Select(b => new UserBookModel
                {
                    UserId = b.UserId,
                    BookId = b.BookId,
                    isRead = b.isRead,
                    DateRead = b.DateRead,
                    Rating = b.Rating,
                    DateAdded = b.DateAdded,
                    Book = new BookModel
                    {
                        Id = b.Book.Id,
                        Title = b.Book.Title,
                        Description = b.Book.Description,
                        Genre = b.Book.Genre,
                        CoverUrl = b.Book.CoverUrl,
                        DateAdded = b.Book.DateAdded,
                        Publisher = new PublisherModel
                        {
                            Id = b.Book.Publisher.Id,
                            Name = b.Book.Publisher.Name,
                        },
                        Authors = b.Book.BookAuthors.Select(a => new AuthorModel
                        {
                            Id = a.Author.Id,
                            FullName = a.Author.FullName,
                        }).ToList(),
                    }
                })
                .ToList();

            return recentlyAddedUserBooks;
        }

        public async Task<UserBookModel> GetBookByIdAsync(int bookId, int userId)
        {
            var userBook = await _bookRepository.GetUserBookAsync(bookId, userId);

            if (userBook == null)
            {
                throw new ArgumentException("Book not found or not owned by the user!");
            }

            var userBookModel = new UserBookModel
            {
                UserId = userBook.UserId,
                BookId = userBook.BookId,
                isRead = userBook.isRead,
                DateRead = userBook.DateRead,
                Rating = userBook.Rating,
                DateAdded = userBook.DateAdded,
                Book = new BookModel
                {
                    Id = userBook.Book.Id,
                    Title = userBook.Book.Title,
                    Description = userBook.Book.Description,
                    Genre = userBook.Book.Genre,
                    CoverUrl = userBook.Book.CoverUrl,
                    DateAdded = userBook.Book.DateAdded,
                    Publisher = new PublisherModel
                    {
                        Id = userBook.Book.Publisher.Id,
                        Name = userBook.Book.Publisher.Name,
                    },
                    Authors = userBook.Book.BookAuthors.Select(a => new AuthorModel
                    {
                        Id = a.Author.Id,
                        FullName = a.Author.FullName,
                    }).ToList(),
                }
            };

            return userBookModel;
        }

        public async Task<PagedResult<UserBookModel>> SearchUserBooksAsync(int pageNumber, int pageSize, int userId, string searchQuery, string sortColumn, SortDirection sortDirection)
        {
            var userBooks = await _bookRepository.GetAllBooksByUserIdAsync(userId);

            var filteredUserBooks = userBooks
                .Where(ub => ub.Book.Title.Contains(searchQuery) ||
                             ub.Book.Description.Contains(searchQuery) ||
                             ub.Book.BookAuthors.Any(a => a.Author.FullName.Contains(searchQuery)) ||
                             ub.Book.Publisher.Name.Contains(searchQuery));

            var totalCount = filteredUserBooks.Count();

            // Apply sorting
            var sortedUserBooks = sortDirection == SortDirection.Ascending
                    ? filteredUserBooks.OrderBy(ub => ub.Book.GetType().GetProperty(sortColumn).GetValue(ub.Book))
                    : filteredUserBooks.OrderByDescending(ub => ub.Book.GetType().GetProperty(sortColumn).GetValue(ub.Book));

            // Apply pagination
            var pagedUserBooks = sortedUserBooks.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var userBookModels = pagedUserBooks.Select(ub => new UserBookModel
            {
                Id = ub.Id,
                UserId = ub.UserId,
                BookId = ub.BookId,
                isRead = ub.isRead,
                Rating = ub.Rating,
                DateAdded = ub.DateAdded,
                DateRead = ub.DateRead,
                Book = new BookModel
                {
                    Id = ub.Book.Id,
                    Title = ub.Book.Title,
                    Description = ub.Book.Description,
                    Genre = ub.Book.Genre,
                    CoverUrl = ub.Book.CoverUrl,
                    DateAdded = ub.Book.DateAdded,
                    Publisher = new PublisherModel
                    {
                        Id = ub.Book.Publisher.Id,
                        Name = ub.Book.Publisher.Name,
                    },
                    Authors = ub.Book.BookAuthors.Select(a => new AuthorModel
                    {
                        Id = a.Author.Id,
                        FullName = a.Author.FullName,
                    }).ToList(),
                }
            });

            return new PagedResult<UserBookModel>
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                Results = userBookModels.ToList(),
            };
        }

        public async Task<List<UserBookModel>> GetUnreadBooksAsync(int userId)
        {
            // Get all user books
            var userBooks = await _bookRepository.GetAllBooksByUserIdAsync(userId);

            // Filter the books that are not read
            var unreadBooks = userBooks.Where(ub => !ub.isRead).ToList();

            // Map to UserBookModel and return
            var unreadBookModels = unreadBooks.Select(ub => new UserBookModel
            {
                Id = ub.Id,
                UserId = ub.UserId,
                BookId = ub.BookId,
                Book = new BookModel
                {
                    Id = ub.Book.Id,
                    Title = ub.Book.Title,
                    Description = ub.Book.Description,
                    Genre = ub.Book.Genre,
                    CoverUrl = ub.Book.CoverUrl,
                    DateAdded = ub.Book.DateAdded,
                    Publisher = new PublisherModel
                    {
                        Id = ub.Book.Publisher.Id,
                        Name = ub.Book.Publisher.Name,
                    },
                    // If you have added Author info in BookModel, you can map it here as well
                },
                isRead = ub.isRead,
                DateRead = ub.DateRead,
                Rating = ub.Rating,
            }).ToList();

            return unreadBookModels;
        }

        public async Task<UserBookModel> AddBookToUserLibraryAsync(int userId, BookModel newBook)
        {
            // Check if the book already exists in the database
            var book = await _bookRepository.GetBookAsync(newBook.Id);
            var userBook = await _bookRepository.GetUserBookAsync(userId, newBook.Id);

            // If the book doesn't exist, add it to the database
            if (book == null)
            {
                // If authors don't exist in the database, add them
                foreach (var author in newBook.Authors)
                {
                    var existingAuthor = await _authorRepository.GetAuthorAsync(author.Id);
                    if (existingAuthor == null)
                    {
                        var newAuthor = new AuthorModel {
                            FullName = author.FullName
                        };  // You will need to fetch the full author data here
                        await _authorService.AddAuthorAsync(newAuthor);
                    }
                }

                // If the publisher doesn't exist in the database, add it
                var existingPublisher = await _publisherRepository.GetPublisherAsync(newBook.Publisher.Id);
                if (existingPublisher == null)
                {
                    var newPublisher = new PublisherModel {
                        Name = newBook.Publisher.Name
                    };  // You will need to fetch the full publisher data here
                    await _publisherService.AddPublisherAsync(newPublisher);
                }

                // Add the book to the database
                book = await _bookRepository.AddBookAsync(newBook);
            }
            else if (userBook != null)
            {
                // If user already has this book in the library, throw an exception
                throw new InvalidOperationException("The user already has this book in their library.");
            }

            // Add the book to the user's library
            userBook = await _bookRepository.AddBookToUserLibraryAsync(userId, book.Id, book.Title);

            // Map to UserBookModel and return
            var userBookModel = new UserBookModel
            {
                UserId = userBook.UserId,
                BookId = userBook.BookId,
                isRead = userBook.isRead,
                DateRead = userBook.DateRead,
                Rating = userBook.Rating,
                DateAdded = userBook.DateAdded,
                Book = new BookModel
                {
                    Id = userBook.Book.Id,
                    Title = userBook.Book.Title,
                    Description = userBook.Book.Description,
                    Genre = userBook.Book.Genre,
                    CoverUrl = userBook.Book.CoverUrl,
                    DateAdded = userBook.Book.DateAdded,
                    Publisher = new PublisherModel { Id = userBook.Book.Publisher.Id, Name = userBook.Book.Publisher.Name },
                    Authors = userBook.Book.BookAuthors.Select(a => new AuthorModel { Id = a.Author.Id, FullName = a.Author.FullName }).ToList()
                }
            };

            return userBookModel;
        }


        public async Task<int> AddBookAsync(BookModel newBook)
        {
            var book = await _bookRepository.GetBookAsync(newBook.Id);

            // If the book doesn't exist, add it to the database
            if (book == null)
            {
                // If authors don't exist in the database, add them
                foreach (var author in newBook.Authors)
                {
                    var existingAuthor = await _authorRepository.GetAuthorAsync(author.Id);
                    if (existingAuthor == null)
                    {
                        await _authorService.AddAuthorAsync(author);
                    }

                    //add relation between author and book
                    // this should be done after the book is added, so moving it down

                }

                //if publisher doesn't exist in the db, add it
                var existingPublisher = await _publisherRepository.GetPublisherAsync(newBook.Publisher.Id);
                if (existingPublisher == null)
                {
                    await _publisherService.AddPublisherAsync(newBook.Publisher);
                }

                //add book
                book = await _bookRepository.AddBookAsync(newBook);

                //now we have the book id, add relations

                foreach(var author in newBook.Authors)
                {
                    await _book_AuthorRepository.AddBookAuthorAsync(author.Id, book.Id);
                }
            }
            return book.Id;
        }

    }
}
