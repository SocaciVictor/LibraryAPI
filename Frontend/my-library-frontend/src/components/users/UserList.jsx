import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { api } from "../../api/client";

export default function UsersList() {
  const [users, setUsers] = useState([]);
  const nav = useNavigate();

  useEffect(() => {
    api
      .getUsers()
      .then(setUsers)
      .catch((err) => console.error("Failed to load users", err));
  }, []);

  const onDelete = (id) => {
    if (!window.confirm("Ștergi utilizatorul?")) return;
    api
      .deleteUser(id)
      .then(() => setUsers((prev) => prev.filter((u) => u.id !== id)))
      .catch((err) => console.error("Delete failed", err));
  };

  return (
    <div>
      <h2>Users</h2>
      <button onClick={() => nav("/users/create")}>New User</button>
      <ul>
        {users.map((u) => (
          <li key={u.id}>
            {u.name}{" "}
            <button onClick={() => nav(`/users/edit/${u.id}`)}>Edit</button>{" "}
            <button onClick={() => onDelete(u.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
}
