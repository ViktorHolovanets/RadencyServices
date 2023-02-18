using Newtonsoft.Json;
using System;

namespace RadencyWebApplication.Models.Db.Seed
{
    public class SeedDateBase
    {
        public static async Task SeedAsync(ApiContext context)
        {
            Random random = new Random();
            var baseAddress = "https://openlibrary.org/trending/daily.json";
            using (var client = new HttpClient())
            {
                using (var response = client.GetAsync(baseAddress).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var customerJsonString = await response.Content.ReadAsStringAsync();
                        int i = 1;
                        var dynamicObject = JsonConvert.DeserializeObject<dynamic>(customerJsonString)!;

                        foreach (var item in dynamicObject.works)
                        {
                            if (i == 15)
                                break;
                            var book = new Book()
                            {

                                Title = item.title,
                                Cover = item.cover_i,
                                Content = "Палітурка книги тверда, виготовлена з картону. Вона захищає сторінки. Обкладинка кольорова. Я бачу красивий малюнок. На ньому намальовані хлопчик та дівчинка. Вони багато часу присвячують читанню книжок. На обкладинці містяться також найважливіші відомості про неї. Книга називається «Читанка», ми будемо вивчати її на уроці читання. Написав автор О.Я. Савченко. На першому форзаці малюнок розповідає про створення книги, на другому я бачу бібліотеку. А видало цю чудову книжку видавництво «Освіта». Книга має 158 сторінок.",
                                Author = item.author_name[0].ToString() ?? "author",
                                Genre = i % 2 == 0 ? "horror" : i % 3 == 0 ? "romance" : "thrillers"
                            };

                            context.Books.Add(book);
                            await context.SaveChangesAsync();
                            for (int j = 1; j < 6; j++)
                            {
                                context.Reviews.Add(new Review()
                                {
                                    Message = $"message{i}",
                                    Reviewer = $"reviewer{i}",
                                    BookId = book.Id
                                });
                                context.Ratings.Add(new Rating()
                                {
                                    Score = random.Next(1, 5),
                                    BookId = book.Id
                                });
                            }                          
                            i++;
                        }
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
