const API_BASE = "https://localhost:7158/api";
const VET_API = `${API_BASE}/veterinarians`;

let deleteVetId = null;

document.addEventListener("DOMContentLoaded", loadVets);

function loadVets() {
    fetch(VET_API)
        .then(res => res.json())
        .then(data => {
            const table = document.getElementById("vetTable");
            table.innerHTML = "";

            data.forEach(v => {
                table.innerHTML += `
                    <tr>
                        <td>${v.fullName}</td>
                        <td>${v.specialization}</td>
                        <td>
                            <button class="btn btn-sm btn-warning me-1"
                                onclick="editVet(${v.id})">✏️</button>

                            <button class="btn btn-sm btn-danger"
                                onclick="deleteVet(${v.id})">🗑️</button>
                        </td>
                    </tr>
                `;
            });
        });
}

function addVet() {
    const vet = {
        fullName: document.getElementById("fullName").value,
        specialization: document.getElementById("specialization").value
    };

    fetch(VET_API, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(vet)
    }).then(() => loadVets());
}

function editVet(id) {
    fetch(`${VET_API}/${id}`)
        .then(res => res.json())
        .then(v => {
            document.getElementById("editVetId").value = v.id;
            document.getElementById("editVetFullName").value = v.fullName;
            document.getElementById("editVetSpecialization").value = v.specialization;

            new bootstrap.Modal(
                document.getElementById("editVetModal")
            ).show();
        });
}

function updateVet() {
    const id = document.getElementById("editVetId").value;

    const vet = {
        fullName: document.getElementById("editVetFullName").value,
        specialization: document.getElementById("editVetSpecialization").value
    };

    fetch(`${VET_API}/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(vet)
    }).then(() => {
        bootstrap.Modal.getInstance(
            document.getElementById("editVetModal")
        ).hide();
        loadVets();
    });
}

function deleteVet(id) {
    deleteVetId = id;
    new bootstrap.Modal(
        document.getElementById("deleteVetModal")
    ).show();
}

function confirmDeleteVet() {
    fetch(`${VET_API}/${deleteVetId}`, {
        method: "DELETE"
    }).then(() => {
        bootstrap.Modal.getInstance(
            document.getElementById("deleteVetModal")
        ).hide();
        loadVets();
    });
}
