﻿using CafeOrderManagement;

OrderManagement orderManagement = new OrderManagement();
MenuManagement menuManagement = new MenuManagement();

Console.WriteLine("Order commands:\nAdd order\nUpdate order status\nSorting orders by customer\nSorting orders by status");
Console.WriteLine("Menu commands: \nShow menu\nAdd menu item\nDelete menu item");
Console.WriteLine("To finish enter \"End\".");
bool running = true;
string option;
while (running)
{
    Console.Write("\nEnter command: ");
    option = Console.ReadLine();
    switch (option)
    {
        case "Add order":
            AddOrder();
            break;
        case "Update order status":
            UpdateOrderStatus();
            break;
        case "Sorting orders by customer":
            FilteringByName();
            break;
        case "Sorting orders by status":
            FilteringByStatus();
            break;
        case "Show menu":
            menuManagement.ShowMenu();
            break;
        case "Add menu item":
            AddMenuItem();
            break;
        case "Delete menu item":
            MenuItemDeleting();
            break;
        case "End":
            running = false;
            Console.WriteLine("Exiting the program.");
            break;
        default:
            Console.WriteLine("Invalid option. Please, try again.");
            break;
    }
}

void AddOrder()
{ 
    Order newOrder = new Order();

    Console.WriteLine("Enter the name of customer:");
    string customerName = Console.ReadLine();

    orderManagement.AddOrder(newOrder, menuManagement, customerName);

    Console.WriteLine($"Order for {newOrder.CustomerName} created successfully.");
    Console.WriteLine($"Total amount: ${newOrder.Amount:F2}");
    Console.WriteLine("Order details:");
    foreach (var detail in newOrder.Details)
    {
        Console.WriteLine($"- {detail.Name}: ${detail.Price:F2}");
    }
}

void UpdateOrderStatus()
{
    Console.WriteLine("Enter the order ID:");
    int id;
    while (!int.TryParse(Console.ReadLine(), out id))
    {
        Console.WriteLine("ID must be a positive integer. Enter the order ID:");
    }
    Console.WriteLine("Enter a number of a new order status (1 - Pending; 2 - InProgress; 3 - Completed; 4 - Cancelled) :");
    int status;
    while (!int.TryParse(Console.ReadLine(), out status) || status < 1 || status > 4)
    {
        Console.WriteLine("Order status does not exist. Please try again.");
    }

    OrderStatus newStatus = new OrderStatus();
    switch (status)
    {
        case 1:
            newStatus = OrderStatus.Pending;
            break;
        case 2: 
            newStatus = OrderStatus.InProgress;
            break;
        case 3:
            newStatus = OrderStatus.Completed;
            break;
        case 4:
            newStatus = OrderStatus.Cancelled;
            break;
    }
    orderManagement.UpdateOrderStatus(id, newStatus);
}

void FilteringByName ()
{
    Console.WriteLine("Enter the name of customer:");
    string customerName = Console.ReadLine();
    orderManagement.FilteringByName(customerName);
}

void FilteringByStatus()
{
    Console.WriteLine("Enter a new order status:");
    string input;
    bool flag = false;
    while (!flag)
    {
        input = Console.ReadLine();
        if (Enum.TryParse(typeof(OrderStatus), input, true, out var result) && Enum.IsDefined(typeof(OrderStatus), result))
        {
            flag = true;
            var newStatus = (OrderStatus)result; 
            orderManagement.FilteringByStatus(newStatus);
        }
        else 
        {
            Console.WriteLine("This status does not exist. Try again.");
        }
    }
}

void AddMenuItem()
{
    Console.WriteLine("Enter the name of the menu position:");
    string item = Console.ReadLine();
    menuManagement.AddMenuItem(item);
}

void MenuItemDeleting()
{
    Console.WriteLine("Enter the name of the menu position:");
    string item = Console.ReadLine();
    menuManagement.RemoveMenuItem(item);
}

public enum OrderStatus
{
    Pending,
    InProgress,
    Completed,
    Cancelled
}


