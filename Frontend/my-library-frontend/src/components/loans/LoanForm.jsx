import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { api } from "../../api/client";

export default function LoanForm() {
  const { id } = useParams();
  const isNew = !id; // creăm doar (nu edităm)
  const nav = useNavigate();

  // stări pentru dropdown-uri
  const [bookId, setBookId] = useState("");
  const [userId, setUserId] = useState("");
  const [books, setBooks] = useState([]);
  const [users, setUsers] = useState([]);

  useEffect(() => {
    // încărcăm lista de cărți și utilizatori odată
    api.getBooks({}).then(setBooks).catch(console.error);
    api.getUsers().then(setUsers).catch(console.error);
  }, []);

  function onSave() {
    // creează numai DTO-ul așa cum e definit backend:
    // record LoanRequestDto(int BookId, int UserId)
    const dto = {
      bookId: +bookId,
      userId: +userId,
    };

    api
      .createLoan(dto, "socaci.victor@yahoo.com")
      .then(() => nav("/loans"))
      .catch((err) => {
        console.error("Borrow failed", err);
        alert("Nu s-a putut împrumuta cartea.");
      });
  }

  return (
    <div>
      <h2>Borrow a Book</h2>
      <div>
        <label>
          User:{" "}
          <select value={userId} onChange={(e) => setUserId(e.target.value)}>
            <option value="">-- select user --</option>
            {users.map((u) => (
              <option key={u.id} value={u.id}>
                {u.name}
              </option>
            ))}
          </select>
        </label>
      </div>
      <div>
        <label>
          Book:{" "}
          <select value={bookId} onChange={(e) => setBookId(e.target.value)}>
            <option value="">-- select book --</option>
            {books.map((b) => (
              <option key={b.id} value={b.id}>
                {b.title}
              </option>
            ))}
          </select>
        </label>
      </div>
      <button onClick={onSave} disabled={!userId || !bookId}>
        Borrow
      </button>{" "}
      <button onClick={() => nav(-1)}>Cancel</button>
    </div>
  );
}
