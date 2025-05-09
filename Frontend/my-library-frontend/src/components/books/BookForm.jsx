import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { api } from "../../api/client";

export default function BookForm() {
  const { id } = useParams();
  const isNew = !id;
  const nav = useNavigate();

  // stări pentru formular
  const [title, setTitle] = useState("");
  const [authorId, setAuthorId] = useState("");
  const [quantity, setQuantity] = useState(1);
  const [publishedDate, setPublishedDate] = useState("");

  // stări pentru listă + filtrare autori
  const [authors, setAuthors] = useState([]);
  const [authorFilter, setAuthorFilter] = useState("");

  useEffect(() => {
    // încărcăm toți autorii o singură dată
    api.getAuthors().then(setAuthors).catch(console.error);

    if (!isNew) {
      // la edit, încărcăm datele cărții
      api
        .getBook(id)
        .then((b) => {
          setTitle(b.title);
          setAuthorId(String(b.authorId));
          setQuantity(b.quantity);
          setPublishedDate(b.publishedDate?.slice(0, 10) || "");
        })
        .catch(console.error);
    }
  }, [id]);

  function onSave() {
    const dto = {
      title,
      authorId: +authorId, // trimitem ID-ul numeric
      quantity: +quantity,
      publishedDate,
    };
    const op = isNew ? api.createBook(dto) : api.updateBook(id, dto);

    op.then(() => nav("/books")).catch((err) => {
      console.error("Save failed", err);
      alert("Eroare la salvarea cărții.");
    });
  }

  // filtrăm autorii după textul introdus
  const filteredAuthors = authors.filter((a) =>
    a.name.toLowerCase().includes(authorFilter.toLowerCase())
  );

  return (
    <div style={{ maxWidth: 600, margin: "0 auto" }}>
      <h2>{isNew ? "Create" : "Edit"} Book</h2>

      <div style={{ marginBottom: "1rem", textAlign: "left" }}>
        <label>
          Title:
          <input
            type="text"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            style={{ width: "100%", padding: "0.5rem" }}
          />
        </label>
      </div>

      <div style={{ marginBottom: "1rem", textAlign: "left" }}>
        <label>
          Search Author:
          <input
            type="text"
            value={authorFilter}
            onChange={(e) => setAuthorFilter(e.target.value)}
            placeholder="Start typing a name…"
            style={{ width: "100%", padding: "0.5rem", margin: "0.5rem 0" }}
          />
        </label>
        <label>
          Select Author:
          <select
            value={authorId}
            onChange={(e) => setAuthorId(e.target.value)}
            style={{ width: "100%", padding: "0.5rem" }}
          >
            <option value="">-- select author --</option>
            {filteredAuthors.map((a) => (
              <option key={a.id} value={a.id}>
                {a.name}
              </option>
            ))}
          </select>
        </label>
      </div>

      <div style={{ marginBottom: "1rem", textAlign: "left" }}>
        <label>
          Quantity:
          <input
            type="number"
            value={quantity}
            onChange={(e) => setQuantity(e.target.value)}
            style={{ width: "100%", padding: "0.5rem" }}
          />
        </label>
      </div>

      <div style={{ marginBottom: "1rem", textAlign: "left" }}>
        <label>
          Published Date:
          <input
            type="date"
            value={publishedDate}
            onChange={(e) => setPublishedDate(e.target.value)}
            style={{ width: "100%", padding: "0.5rem" }}
          />
        </label>
      </div>

      <button onClick={onSave} disabled={!title || !authorId}>
        Save
      </button>
      <button onClick={() => nav(-1)} style={{ marginLeft: 8 }}>
        Cancel
      </button>
    </div>
  );
}
