// AuthorList.jsx
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { api } from "../../api/client";
import "./AuthorList.css";

export default function AuthorList() {
  const [items, setItems] = useState([]);
  const nav = useNavigate();

  useEffect(() => {
    api.getAuthors().then(setItems);
  }, []);

  function onDelete(id) {
    if (!confirm("Ștergi autorul?")) return;
    api
      .deleteAuthor(id)
      .then(() => setItems((prev) => prev.filter((a) => a.id !== id)));
  }

  return (
    <div className="author-list">
      <h2>Authors</h2>
      <button onClick={() => nav("/authors/create")}>New Author</button>
      <ul>
        {items.map((a) => (
          <li key={a.id}>
            {/* 1. copil: span cu numele */}
            <span>{a.name}</span>
            {/* 2. copil: buton Edit */}
            <button onClick={() => nav(`/authors/edit/${a.id}`)}>Edit</button>
            {/* 3. copil: buton Delete */}
            <button onClick={() => onDelete(a.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
}
