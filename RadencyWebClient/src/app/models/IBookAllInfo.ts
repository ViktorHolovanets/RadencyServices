import {IReview} from "./IReview";

export interface IBookAllInfo{
  id: number,
  title: string,
  content: string,
  genre: string,
  author: string,
  cover: string,
  rating: number,
  reviews: IReview[]
}
