const connection = new signalR.HubConnectionBuilder()
    .withUrl("/orderHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.start().catch(error => console.error("Error connecting to SignalR:", error));

connection.on("ReceiveMessage", (message, order) => {
    showNotification(message, "info");

    if (order) {
        updateOrderTable(order);
    }
});

function showNotification(message, type) {
    const notificationDiv = document.getElementById("notifications");
    if (!notificationDiv) return;

    const newNotification = document.createElement("div");
    newNotification.textContent = message;
    newNotification.classList.add("alert", `alert-${type}`, "mt-2");

    notificationDiv.appendChild(newNotification);

    setTimeout(() => {
        newNotification.remove();
    }, 3000);
}
