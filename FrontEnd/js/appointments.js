const API_BASE = "https://localhost:7158/api";
const APPOINTMENT_API = `${API_BASE}/appointments`;
const ANIMAL_API = `${API_BASE}/animals`;
const VET_API = `${API_BASE}/veterinarians`;

document.addEventListener("DOMContentLoaded", () => {
    loadAppointments();
    loadAnimals();
    loadVets();
});

// LIST
function loadAppointments() {
    fetch(APPOINTMENT_API)
        .then(r => r.json())
        .then(data => {
            const tbody = document.getElementById("appointmentTable");
            tbody.innerHTML = "";

            data.forEach(a => {
                tbody.innerHTML += `
                    <tr>
                        <td>${a.animalName}</td>
                        <td>${a.veterinarianName}</td>
                        <td>${new Date(a.appointmentDate).toLocaleString()}</td>
                        <td>${a.description ?? "-"}</td>
                        <td>
                            <button class="btn btn-sm btn-warning"
                                onclick="editAppointment(${a.id})">✏️</button>
                            <button class="btn btn-sm btn-danger"
                                onclick="deleteAppointment(${a.id})">🗑️</button>
                        </td>
                    </tr>
                `;
            });
        });
}

// CREATE
function addAppointment() {
    const model = {
        animalId: Number(animalId.value),
        veterinarianId: Number(vetId.value),
        appointmentDate: appointmentDate.value,
        description: description.value
    };

    fetch(APPOINTMENT_API, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(model)
    }).then(() => loadAppointments());
}

// EDIT LOAD
function editAppointment(id) {
    fetch(`${APPOINTMENT_API}/${id}`)
        .then(r => r.json())
        .then(a => {
            editAppointmentId.value = a.id;
            editAppointmentDate.value = a.appointmentDate.slice(0, 16);
            editDescription.value = a.description ?? "";
            loadAnimalsForEdit(a.animalId);
            loadVetsForEdit(a.veterinarianId);
            new bootstrap.Modal(editAppointmentModal).show();
        });
}

// UPDATE
function updateAppointment() {
    const id = editAppointmentId.value;

    const model = {
        animalId: Number(editAnimalId.value),
        veterinarianId: Number(editVetId.value),
        appointmentDate: editAppointmentDate.value,
        description: editDescription.value
    };

    fetch(`${APPOINTMENT_API}/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(model)
    }).then(() => {
        bootstrap.Modal.getInstance(editAppointmentModal).hide();
        loadAppointments();
    });
}

// DELETE
let deleteId = null;
function deleteAppointment(id) {
    deleteId = id;
    new bootstrap.Modal(deleteAppointmentModal).show();
}

function confirmDeleteAppointment() {
    fetch(`${APPOINTMENT_API}/${deleteId}`, { method: "DELETE" })
        .then(() => {
            bootstrap.Modal.getInstance(deleteAppointmentModal).hide();
            loadAppointments();
        });
}

// DROPDOWNS
function loadAnimals() {
    fetch(ANIMAL_API)
        .then(r => r.json())
        .then(d => animalId.innerHTML =
            d.map(x => `<option value="${x.id}">${x.name}</option>`).join("")
        );
}

function loadVets() {
    fetch(VET_API)
        .then(r => r.json())
        .then(d => vetId.innerHTML =
            d.map(x => `<option value="${x.id}">${x.fullName}</option>`).join("")
        );
}

function loadAnimalsForEdit(id) {
    fetch(ANIMAL_API)
        .then(r => r.json())
        .then(d => editAnimalId.innerHTML =
            d.map(x => `<option value="${x.id}" ${x.id === id ? "selected" : ""}>${x.name}</option>`).join("")
        );
}

function loadVetsForEdit(id) {
    fetch(VET_API)
        .then(r => r.json())
        .then(d => editVetId.innerHTML =
            d.map(x => `<option value="${x.id}" ${x.id === id ? "selected" : ""}>${x.fullName}</option>`).join("")
        );
}
