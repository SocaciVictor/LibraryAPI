import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { api } from "../../api/client";

export default function UserForm() {
  const { id } = useParams();
  const isNew = !id;
  const nav = useNavigate();

  // Un singur câmp: name
  const [name, setName] = useState("");

  useEffect(() => {
    if (!isNew) {
      api
        .getUser(id)
        .then((u) => setName(u.name))
        .catch((err) => console.error("Failed to load user", err));
    }
  }, [id]);

  const onSave = () => {
    const dto = { name };
    const op = isNew ? api.createUser(dto) : api.updateUser(id, dto);
    op.then(() => nav("/users")).catch((err) =>
      console.error("Save failed", err)
    );
  };

  return (
    <div>
      <h2>{isNew ? "Create" : "Edit"} User</h2>
      <div>
        <label>
          Name:{" "}
          <input
            type="text"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
        </label>
      </div>
      <button onClick={onSave}>Save</button>{" "}
      <button onClick={() => nav(-1)}>Cancel</button>
    </div>
  );
}
