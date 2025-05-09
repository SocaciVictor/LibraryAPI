import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { api } from "../../api/client";

export default function AuthorList() {
  const [items, setItems] = useState([]);
  const nav = useNavigate();

  useEffect(() => {
    api.getAuthors().then(setItems);
  }, []);

  function onDelete(id) {
    if (!confirm("Ștergi autorul?")) return;
    api.deleteAuthor(id).then(() => setItems(items.filter((a) => a.id !== id)));
  }

  return (
    <div>
      <h2>Authors</h2>
      <button onClick={() => nav("/authors/create")}>New Author</button>
      <ul>
        {items.map((a) => (
          <li key={a.id}>
            {a.name}{" "}
            <button onClick={() => nav(`/authors/edit/${a.id}`)}>Edit</button>{" "}
            <button onClick={() => onDelete(a.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
}
