import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { api } from "../../api/client";

export default function BooksList() {
  const nav = useNavigate();

  // filtre
  const [authorId, setAuthorId] = useState("");
  const [title, setTitle] = useState("");
  const [publishedDate, setPublishedDate] = useState("");

  const [items, setItems] = useState([]);

  function load() {
    const filters = {};

    if (authorId.trim() !== "") {
      filters.authorId = Number(authorId);
    }
    if (title.trim() !== "") {
      filters.title = title;
    }
    if (publishedDate) {
      filters.publishedDate = publishedDate;
    }

    api
      .getBooks(filters)
      .then(setItems)
      .catch((err) => console.error(err));
  }

  useEffect(load, []); // la mount, fără filtre

  return (
    <div>
      <h2>Books</h2>
      <button onClick={() => nav("/books/create")}>New Book</button>
      <h4>Filter</h4>
      <label>
        Author ID:{" "}
        <input value={authorId} onChange={(e) => setAuthorId(e.target.value)} />
      </label>{" "}
      <label>
        Title:{" "}
        <input value={title} onChange={(e) => setTitle(e.target.value)} />
      </label>{" "}
      <label>
        Published Date:{" "}
        <input
          type="date"
          value={publishedDate}
          onChange={(e) => setPublishedDate(e.target.value)}
        />
      </label>{" "}
      <button onClick={load}>Search</button>
      <ul>
        {items.map((b) => (
          <li key={b.id}>
            <strong>{b.title}</strong> (Author #{b.authorId}) —{" "}
            <span>Quantity: {b.quantity}</span> —{" "}
            <span>Published: {b.publishedDate?.slice(0, 10) || "—"}</span>{" "}
            <button onClick={() => nav(`/books/edit/${b.id}`)}>Edit</button>{" "}
            <button
              onClick={() => {
                if (!confirm("Ștergi cartea?")) return;
                api
                  .deleteBook(b.id)
                  .then(() =>
                    setItems((prev) => prev.filter((x) => x.id !== b.id))
                  );
              }}
            >
              Delete
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}
