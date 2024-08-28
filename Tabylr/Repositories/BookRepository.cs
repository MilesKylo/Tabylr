using Tabylr.Models;

namespace Tabylr.Repositories
{
    public class BookRepository
    {
        private readonly Supabase.Client _supabaseClient;

        public BookRepository(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            var response = await _supabaseClient
                .From<Book>()
                .Get();
            return response.Models;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var response = await _supabaseClient
                .From<Book>()
                .Where(b => b.Id == id)
                .Single();
            return response;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            var response = await _supabaseClient
                .From<Book>()
                .Insert(book);
            return response.Model;
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            var response = await _supabaseClient
                .From<Book>()
                .Update(book);
            return response.Model;
        }

        public async Task DeleteBookAsync(int id)
        {
            await _supabaseClient
                .From<Book>()
                .Where(b => b.Id == id)
                .Delete();
        }
    }
}
