import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { api } from "../../api/client";

export default function LoanForm() {
  const { id } = useParams();
  const isNew = !id;
  const nav = useNavigate();
  const notifyEmail = "socaci.victor@yahoo.com";

  const [bookId, setBookId] = useState("");
  const [userId, setUserId] = useState("");
  const [loanedAt, setLoanedAt] = useState(
    new Date().toISOString().slice(0, 10)
  );
  const [returnedAt, setReturnedAt] = useState("");
  const [books, setBooks] = useState([]);
  const [users, setUsers] = useState([]);

  // încărcări inițiale și la edit
  useEffect(() => {
    api.getBooks({}).then(setBooks);
    api.getUsers().then(setUsers);

    if (!isNew) {
      api.getLoan(id).then((l) => {
        setBookId(String(l.bookId));
        setUserId(String(l.userId));
        setLoanedAt(l.loanedAt.slice(0, 10));
        setReturnedAt(l.returnedAt?.slice(0, 10) || "");
      });
    }
  }, [id]);

  function onSave() {
    const dto = {
      bookId: +bookId,
      userId: +userId,
      loanedAt: new Date(loanedAt),
      returnedAt: returnedAt ? new Date(returnedAt) : null,
    };

    const op = isNew
      ? api.createLoan(dto, notifyEmail)
      : api.updateLoan(id, dto, notifyEmail);

    op.then(() => nav("/loans")).catch((err) => {
      console.error("Save failed", err);
      alert("Operațiunea a eșuat");
    });
  }

  return (
    <div style={{ maxWidth: 500, margin: "2rem auto", textAlign: "left" }}>
      <h2>{isNew ? "Borrow a Book" : `Edit Loan #${id}`}</h2>
      {/* User */}
      <div>
        <label>User:</label>
        <br />
        <select
          value={userId}
          onChange={(e) => setUserId(e.target.value)}
          style={{ width: "100%", padding: "0.5rem", margin: "0.5rem 0" }}
        >
          <option value="">-- select user --</option>
          {users.map((u) => (
            <option key={u.id} value={u.id}>
              {u.name}
            </option>
          ))}
        </select>
      </div>
      {/* Book */}
      <div>
        <label>Book:</label>
        <br />
        <select
          value={bookId}
          onChange={(e) => setBookId(e.target.value)}
          style={{ width: "100%", padding: "0.5rem", margin: "0.5rem 0" }}
        >
          <option value="">-- select book --</option>
          {books.map((b) => (
            <option key={b.id} value={b.id}>
              {b.title}
            </option>
          ))}
        </select>
      </div>
      {/* LoanedAt */}
      <div>
        <label>Loaned At:</label>
        <br />
        <input
          type="date"
          value={loanedAt}
          onChange={(e) => setLoanedAt(e.target.value)}
          style={{ width: "100%", padding: "0.5rem", margin: "0.5rem 0" }}
        />
      </div>
      {/* ReturnedAt */}
      <div>
        <label>Returned At:</label>
        <br />
        <input
          type="date"
          value={returnedAt}
          onChange={(e) => setReturnedAt(e.target.value)}
          style={{ width: "100%", padding: "0.5rem", margin: "0.5rem 0" }}
        />
      </div>
      <button
        onClick={onSave}
        disabled={!bookId || !userId || !loanedAt}
        style={{ padding: "0.6rem 1.2rem" }}
      >
        {isNew ? "Borrow" : "Save"}
      </button>{" "}
      <button onClick={() => nav(-1)} style={{ padding: "0.6rem 1.2rem" }}>
        Cancel
      </button>
    </div>
  );
}
