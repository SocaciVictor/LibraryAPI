import { NavLink, Route, Routes } from "react-router-dom";
import AuthorForm from "./components/authors/AuthorForm";
import AuthorsList from "./components/authors/AuthorList";
import BookForm from "./components/books/BookForm";
import BooksList from "./components/books/BooksList";
import LoanForm from "./components/loans/LoanForm";
import LoansList from "./components/loans/LoansList";
import UserForm from "./components/users/UserForm";
import UsersList from "./components/users/UserList";

function Home() {
  return (
    <div style={{ padding: 20 }}>
      <h1>Bine ai venit în Library Frontend!</h1>
      <p>
        Foloseşte meniul de mai sus ca să navighezi prin CRUD-ul aplicației.
      </p>
    </div>
  );
}

export default function App() {
  return (
    <div>
      <nav style={{ padding: 10, borderBottom: "1px solid #ccc" }}>
        <NavLink to="/" style={{ margin: 5 }}>
          Home
        </NavLink>
        |
        <NavLink to="/authors" style={{ margin: 5 }}>
          Authors
        </NavLink>
        |
        <NavLink to="/books" style={{ margin: 5 }}>
          Books
        </NavLink>
        |
        <NavLink to="/loans" style={{ margin: 5 }}>
          Loans
        </NavLink>
        |
        <NavLink to="/users" style={{ margin: 5 }}>
          Users
        </NavLink>
      </nav>

      <Routes>
        {/* Pagina principală */}
        <Route path="/" element={<Home />} />

        {/* Authors */}
        <Route path="/authors" element={<AuthorsList />} />
        <Route path="/authors/create" element={<AuthorForm />} />
        <Route path="/authors/edit/:id" element={<AuthorForm />} />

        {/* Books */}
        <Route path="/books" element={<BooksList />} />
        <Route path="/books/create" element={<BookForm />} />
        <Route path="/books/edit/:id" element={<BookForm />} />

        {/* Loans */}
        <Route path="/loans" element={<LoansList />} />
        <Route path="/loans/create" element={<LoanForm />} />
        <Route path="/loans/edit/:id" element={<LoanForm />} />

        {/* Users */}
        <Route path="/users" element={<UsersList />} />
        <Route path="/users/create" element={<UserForm />} />
        <Route path="/users/edit/:id" element={<UserForm />} />
      </Routes>
    </div>
  );
}
