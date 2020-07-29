using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiPrue.Models;

namespace WebApiPrue.Controllers
{
    [DisableCors]
    public class BookController : ApiController
    {
        static HttpClient client = new HttpClient();
        public BookController()
         
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("https://fakerestapi.azurewebsites.net");

            }

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));


        }
        [HttpGet]
        public async Task<Book[]> GetAllBooks()
        {
           Book[] books = null;
            HttpResponseMessage response = await client.GetAsync($"/api/Books");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpResponseException(
                Request.CreateResponse(response.StatusCode, response.ReasonPhrase));

            }
            else
            {
                books = await response.Content.ReadAsAsync<Book[]>();
            }

            return books;
        }
        [HttpGet]
        public async Task<Book> GetBook(int id)
        {
            Book book = null;
            HttpResponseMessage response = await client.GetAsync($"/api/Books/{id}");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpResponseException(
                Request.CreateResponse(response.StatusCode, response.ReasonPhrase));
                
            }else
            {
                book = await response.Content.ReadAsAsync<Book>();
            }

            return book;
        }
        [HttpPost]
        public async Task<Book> CreateBook(Book book)

        {
            if (book == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, "Book object is required"));

            }
            HttpResponseMessage response = await client.PostAsJsonAsync("/api/Books", book);
             
            if(response.IsSuccessStatusCode == false)
            {
                throw new HttpResponseException(Request.CreateResponse(response.StatusCode, response.ReasonPhrase));
            }
           return book;

        }
        [HttpPut]
        public async Task<Book> UpdateBook(Book book)
        {
            if(book == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, "Book object is required"));
               
            }
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/Books/{book.ID}", book);

            if(response.IsSuccessStatusCode== false)
            {
                throw new HttpResponseException(Request.CreateResponse(response.StatusCode, response.ReasonPhrase));
            }
            else
            {

                book = await response.Content.ReadAsAsync<Book>();
            }

            return book;
        }
        [HttpDelete]
        public async Task<Book> DeleteBook(int id)
        {
            Book book = null;

            if (id < 0)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, "Book id is required"));

            }

            HttpResponseMessage response = await client.DeleteAsync(
                $"api/Books/{id}");

            if (response.IsSuccessStatusCode == false)
            {
                throw new HttpResponseException(Request.CreateResponse(response.StatusCode, response.ReasonPhrase));
            }
            else
            {
                book = await response.Content.ReadAsAsync<Book>();
            }

            
            return book;
        }


    }




}

