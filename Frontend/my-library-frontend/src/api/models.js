// src/api/models.ts

export interface Author {
  id: number;
  name: string;
}

export interface Book {
  id: number;
  title: string;
  authorId: number;
  quantity: number;
  publishedDate?: string;
}

export interface Loan {
  id: number;
  bookId: number;
  loanedAt: string;
  returnedAt?: string;
}

export interface User {
  id: number;
  fullName: string;
  email: string;
}
