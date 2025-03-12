document.getElementById("orderForm").addEventListener("submit", async function (event) {
    event.preventDefault();

    const formData = new FormData(this);

    try {
        const response = await fetch("/Order/Create", {
            method: "POST",
            body: formData
        });

        if (!response.ok) throw new Error(`API error: ${response.status}`);

        const data = await response.json();

        if (data.success) {
            showNotification("Order successfully created! Updating list...", "success");

            setTimeout(() => {
                updateOrderTable(data.order);
            }, 2000);

            event.target.reset();
        }
    } catch (error) { }
});

function updateOrderTable(order) {
    if (!order || !order.id) return;

    const tableBody = document.querySelector("tbody");
    if (!tableBody) return;

    let row = document.querySelector(`tr[data-id="${order.id}"]`);

    if (!row) {
        row = document.createElement("tr");
        row.setAttribute("data-id", order.id);
        tableBody.appendChild(row);
    }

    row.innerHTML = `
        <td>${order.product}</td>
        <td>${order.quantity}</td>
        <td>
            <span class="badge ${order.status === "Completed" ? "bg-success" : "bg-warning"}">
                ${order.status}
            </span>
        </td>
    `;
}
