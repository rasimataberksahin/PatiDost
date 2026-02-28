const API_BASE = "https://localhost:7158/api";
const OWNER_API = `${API_BASE}/Owners`;

document.addEventListener("DOMContentLoaded", () => {
    loadOwners();
});

function loadOwners() {
    fetch(OWNER_API)
        .then(res => res.json())
        .then(data => {
            const table = document.getElementById("ownerTable");
            table.innerHTML = "";

            data.forEach(o => {
                table.innerHTML += `
                    <tr>
                        <td>${o.fullName}</td>
                        <td>${o.phone}</td>
                        <td>${o.email}</td>
                        <td>
                            <button class="btn btn-sm btn-warning me-1"
                                onclick="editOwner(${o.id})">✏️</button>

                            <button class="btn btn-sm btn-danger"
                                onclick="deleteOwner(${o.id})">🗑️</button>
                        </td>
                    </tr>
                `;
            });
        });
}


function addOwner() {
    const owner = {
        fullName: document.getElementById("fullName").value.trim(),
        phone: document.getElementById("phone").value.trim(),
        email: document.getElementById("email").value.trim()
    };

    fetch(OWNER_API, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(owner)
    })
        .then(res => {
            if (!res.ok) throw new Error("Ekleme başarısız");
            return res.json();
        })
        .then(() => {
            loadOwners();
            alert("Sahip eklendi");
        })
        .catch(err => alert(err.message));
}


function filterTable() {
    const input = document.getElementById("tableSearch");
    const filter = input.value.toLowerCase();
    const rows = document.querySelectorAll("tbody tr");

    rows.forEach(row => {
        const text = row.innerText.toLowerCase();
        row.style.display = text.includes(filter) ? "" : "none";
    });
}

function editOwner(id) {
    fetch(`${OWNER_API}/${id}`)
        .then(res => res.json())
        .then(o => {
            document.getElementById("editOwnerId").value = o.id;
            document.getElementById("editFullName").value = o.fullName;
            document.getElementById("editPhone").value = o.phone;
            document.getElementById("editEmail").value = o.email;

            const modal = new bootstrap.Modal(
                document.getElementById("editOwnerModal")
            );
            modal.show();
        });
}

function updateOwner() {
    const id = document.getElementById("editOwnerId").value;

    const owner = {
        fullName: document.getElementById("editFullName").value,
        phone: document.getElementById("editPhone").value,
        email: document.getElementById("editEmail").value
    };

    fetch(`${OWNER_API}/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(owner)
    }).then(() => {
        bootstrap.Modal.getInstance(
            document.getElementById("editOwnerModal")
        ).hide();
        loadOwners();
    });
}


let deleteOwnerId = null;

function deleteOwner(id) {
    deleteOwnerId = id;

    const modal = new bootstrap.Modal(
        document.getElementById("deleteOwnerModal")
    );
    modal.show();
}

function confirmDeleteOwner() {
    fetch(`${OWNER_API}/${deleteOwnerId}`, {
        method: "DELETE"
    }).then(() => {
        bootstrap.Modal.getInstance(
            document.getElementById("deleteOwnerModal")
        ).hide();
        loadOwners();
    });
}
