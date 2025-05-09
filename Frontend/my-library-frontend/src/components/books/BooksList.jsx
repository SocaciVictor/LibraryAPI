import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { api } from "../../api/client";
import "./BooksList.css";

export default function BooksList() {
  const nav = useNavigate();

  // filtre
  const [authorId, setAuthorId] = useState("");
  const [title, setTitle] = useState("");
  const [publishedDate, setPublishedDate] = useState("");

  // datele principale
  const [books, setBooks] = useState([]);
  const [loans, setLoans] = useState([]);

  // încarcă books + loans
  const load = () => {
    const filters = {};
    if (authorId.trim() !== "") filters.authorId = Number(authorId);
    if (title.trim() !== "") filters.title = title;
    if (publishedDate) filters.publishedDate = publishedDate;

    api.getBooks(filters).then(setBooks).catch(console.error);
    api.getLoans().then(setLoans).catch(console.error);
  };

  useEffect(load, []);

  return (
    <div className="books-list">
      <header>
        <h2>Books</h2>
        <button onClick={() => nav("/books/create")}>New Book</button>
      </header>

      <div className="filters">
        <label>
          Author ID:
          <input
            value={authorId}
            onChange={(e) => setAuthorId(e.target.value)}
          />
        </label>
        <label>
          Title:
          <input value={title} onChange={(e) => setTitle(e.target.value)} />
        </label>
        <label>
          Published:
          <input
            type="date"
            value={publishedDate}
            onChange={(e) => setPublishedDate(e.target.value)}
          />
        </label>
        <button onClick={load}>Search</button>
      </div>

      <table className="books-table">
        <thead>
          <tr>
            <th>Title</th>
            <th>Author ID</th>
            <th>Total Quantity</th>
            <th>Active Loans</th>
            <th>Available</th>
            <th>Published</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {books.map((b) => {
            const activeLoans = loans.filter(
              (l) => l.bookId === b.id && l.returnedAt == null
            ).length;
            const available = b.quantity - activeLoans;

            return (
              <tr key={b.id}>
                <td>{b.title}</td>
                <td>{b.authorId}</td>
                <td>{b.quantity}</td>
                <td>{activeLoans}</td>
                <td>{available}</td>
                <td>{b.publishedDate?.slice(0, 10) || "—"}</td>
                <td className="actions">
                  <button onClick={() => nav(`/books/edit/${b.id}`)}>
                    Edit
                  </button>
                  <button
                    onClick={() => {
                      if (!confirm("Ștergi cartea?")) return;
                      api
                        .deleteBook(b.id)
                        .then(() =>
                          setBooks((prev) => prev.filter((x) => x.id !== b.id))
                        );
                    }}
                  >
                    Delete
                  </button>
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}
