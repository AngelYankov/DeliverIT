# Freight Forwarding Management System

Click here to see our [Trello](https://trello.com/b/RK13PeeI/deliverit) board

Developing "DeliverIt" - a web application that serves the needs of a freight forwarding company.

## **Usage**

"DeliverIt" support two types of users - customers and employees. Customers can place orders and have their parcels delivered to the company's warehouses.
Customer can see how many parcels they have on the way.
The employees can add new parcel to the system, modify existing ones and more.

There is a public and private part.
Public part can be accessed without authentication.
Users can see how many customers DeliverIt has and what are the available warehouses. 
You have the ability also to register. 
If you are authenticated by providing an userName, containing your first and last name with a dot between them, you will have access to create, read, update, delete operations.

## **Technology**
Using **ASP.NET Core**, **Microsoft SQL Server**, **Visual Studio** and **Entity Framework** with code first approach to migrate a database. Developed **REST API**. Using **HTTP** as a transport protocol and **JSON** for request and response action. **Unit tests** for folder "Services" having 80% code-coverage. Using **GitLab** repository as source control. Following principles - **OOP, SOLID, KISS, DRY**.

**[Swagger Link](http://localhost:5000/swagger/)**
