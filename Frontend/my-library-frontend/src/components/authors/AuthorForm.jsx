import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { api } from "../../api/client";

export default function AuthorForm() {
  const { id } = useParams();
  const nav = useNavigate();
  const isNew = !id;
  const [name, setName] = useState("");

  useEffect(() => {
    if (!isNew) api.getAuthor(id).then((a) => setName(a.name));
  }, [id]);

  function onSave() {
    const dto = { name };
    const op = isNew ? api.createAuthor(dto) : api.updateAuthor(id, dto);
    op.then(() => nav("/authors"));
  }

  return (
    <div>
      <h2>{isNew ? "Create" : "Edit"} Author</h2>
      <label>
        Name: <input value={name} onChange={(e) => setName(e.target.value)} />
      </label>
      <button onClick={onSave}>Save</button>
      <button onClick={() => nav(-1)}>Cancel</button>
    </div>
  );
}
