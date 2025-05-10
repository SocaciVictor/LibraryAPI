export const API_BASE = "https://localhost:7088/api";

async function request(path, options = {}) {
  const res = await fetch(`${API_BASE}${path}`, {
    headers: { "Content-Type": "application/json" },
    ...options,
  });
  if (!res.ok) throw new Error(await res.text());
  return res.status === 204 ? null : res.json();
}

export const api = {
  // Authors
  getAuthors: () => request(`/Authors`),
  getAuthor: (id) => request(`/Authors/${id}`),
  createAuthor: (a) =>
    request(`/Authors`, { method: "POST", body: JSON.stringify(a) }),
  updateAuthor: (id, a) =>
    request(`/Authors/${id}`, { method: "PUT", body: JSON.stringify(a) }),
  deleteAuthor: (id) => request(`/Authors/${id}`, { method: "DELETE" }),

  // Books (filtre)
  getBooks: ({ authorId, title, publishedDate }) => {
    const params = new URLSearchParams();
    if (authorId) params.set("authorId", authorId);
    if (title) params.set("title", title);
    if (publishedDate) params.set("publishedDate", publishedDate);
    return request(`/Books?${params.toString()}`);
  },
  getBook: (id) => request(`/Books/${id}`),
  createBook: (b) =>
    request(`/Books`, { method: "POST", body: JSON.stringify(b) }),
  updateBook: (id, b) =>
    request(`/Books/${id}`, { method: "PUT", body: JSON.stringify(b) }),
  deleteBook: (id) => request(`/Books/${id}`, { method: "DELETE" }),

  // Loans
  getLoans: () => request(`/Loans`),
  getLoan: (id) => request(`/Loans/${id}`),
  createLoan: (loanDto, notifyEmail) =>
    request(`/Loans`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "X-Notify-Email": notifyEmail,
      },
      body: JSON.stringify(loanDto),
    }),
  updateLoan: (id, l) =>
    request(`/Loans/${id}`, { method: "PUT", body: JSON.stringify(l) }),
  deleteLoan: (id) => request(`/Loans/${id}`, { method: "DELETE" }),
  returnLoan: (loanId, notifyEmail) =>
    request(`/Loans/${loanId}/return`, {
      method: "POST",
      headers: {
        "X-Notify-Email": notifyEmail,
      },
    }),

  // Users
  getUsers: () => request(`/Users`),
  getUser: (id) => request(`/Users/${id}`),
  createUser: (u) =>
    request(`/Users`, { method: "POST", body: JSON.stringify(u) }),
  updateUser: (id, u) =>
    request(`/Users/${id}`, { method: "PUT", body: JSON.stringify(u) }),
  deleteUser: (id) => request(`/Users/${id}`, { method: "DELETE" }),
};
