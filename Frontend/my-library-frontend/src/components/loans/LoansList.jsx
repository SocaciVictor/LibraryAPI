import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { api } from "../../api/client";
import "./LoansList.css"; // importă stilurile

export default function LoansList() {
  const [items, setItems] = useState([]);
  const nav = useNavigate();
  const Email = "socaci.victor@yahoo.com";

  const loadLoans = () => {
    api
      .getLoans()
      .then(setItems)
      .catch((err) => console.error("Error fetching loans:", err));
  };

  useEffect(loadLoans, []);

  function onDelete(id) {
    if (!window.confirm("Ștergi împrumutul?")) return;
    api
      .deleteLoan(id)
      .then(loadLoans)
      .catch((err) => console.error("Delete failed:", err));
  }

  function onReturn(id) {
    if (!window.confirm("Marchezi acest împrumut ca returnat?")) return;
    api
      .returnLoan(id, Email)
      .then(loadLoans)
      .catch((err) => alert("Return failed: " + err));
  }

  return (
    <div className="loans-list">
      <h2>Loans</h2>
      <button onClick={() => nav("/loans/create")}>New Loan</button>

      {items.length === 0 ? (
        <p>No loans to display.</p>
      ) : (
        <ul>
          {items.map((l) => (
            <li key={l.id}>
              {/* 1. Detalii împrumut */}
              <div className="loan-info">
                Loan #{l.id} – User #{l.userId} – Book #{l.bookId}— Loaned:{" "}
                {l.loanedAt.slice(0, 10)}— Returned:{" "}
                {l.returnedAt ? l.returnedAt.slice(0, 10) : "—"}
              </div>
              {/* 2. Edit */}
              <button onClick={() => nav(`/loans/edit/${l.id}`)}>Edit</button>
              {/* 3. Delete și Return */}
              <div className="loan-actions">
                <button onClick={() => onDelete(l.id)}>Delete</button>
                {l.returnedAt == null && (
                  <button onClick={() => onReturn(l.id)}>Return</button>
                )}
              </div>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}
