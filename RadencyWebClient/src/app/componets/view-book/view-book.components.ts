import {Component} from "@angular/core";

@Component({
  selector: 'app-view-book',
  templateUrl: './view-book.components.html',
})

export class ViewBookComponents {
  book = {
    "id": 1,
    "title": "It Ends With Us",
    "author": "Colleen Hoover",
    "cover": "https://covers.openlibrary.org/b/id/10473609-M.jpg",
    "content": "Палітурка книги тверда, виготовлена з картону. Вона захищає сторінки. Обкладинка кольорова. Я бачу красивий малюнок. На ньому намальовані хлопчик та дівчинка. Вони багато часу присвячують читанню книжок. На обкладинці містяться також найважливіші відомості про неї. Книга називається «Читанка», ми будемо вивчати її на уроці читання. Написав автор О.Я. Савченко. На першому форзаці малюнок розповідає про створення книги, на другому я бачу бібліотеку. А видало цю чудову книжку видавництво «Освіта». Книга має 158 сторінок.Палітурка книги тверда, виготовлена з картону. Вона захищає сторінки. Обкладинка кольорова. Я бачу красивий малюнок. На ньому намальовані хлопчик та дівчинка. Вони багато часу присвячують читанню книжок. Палітурка книги тверда, виготовлена з картону. Вона захищає сторінки. Обкладинка кольорова. Я бачу красивий малюнок. На ньому намальовані хлопчик та дівчинка. Вони багато часу присвячують читанню книжок. ",
    "genre": "thrillers",
    "rating": 3,
    "reviews": [
      {
        "id": 1,
        "message": "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptatibus dignissimos quae id minus sed vero dolor repellat, ullam a ad quod repellendus sunt similique nisi aspernatur, architecto consequatur eaque quas?",
        "reviewer": "reviewer1"
      },
      {
        "id": 2,
        "message": "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptatibus dignissimos quae id minus sed vero dolor repellat, ullam a ad quod repellendus sunt similique nisi aspernatur, architecto consequatur eaque quas?",
        "reviewer": "reviewer1"
      },
      {
        "id": 3,
        "message": "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptatibus dignissimos quae id minus sed vero dolor repellat, ullam a ad quod repellendus sunt similique nisi aspernatur, architecto consequatur eaque quas?",
        "reviewer": "reviewer1"
      },
      {
        "id": 4,
        "message": "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptatibus dignissimos quae id minus sed vero dolor repellat, ullam a ad quod repellendus sunt similique nisi aspernatur, architecto consequatur eaque quas?",
        "reviewer": "reviewer1"
      },
      {
        "id": 5,
        "message": "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptatibus dignissimos quae id minus sed vero dolor repellat, ullam a ad quod repellendus sunt similique nisi aspernatur, architecto consequatur eaque quas?",
        "reviewer": "reviewer1"
      }
    ]
  }
}
