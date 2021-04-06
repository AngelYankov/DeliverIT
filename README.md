<img src="https://webassets.telerikacademy.com/images/default-source/logos/telerik-academy.svg)" alt="logo" width="300px" style="margin-top: 20px;"/>

# Teamwork Assignment <br><br> Freight Forwarding Management System

<p align="center">
[**Click Here**](https://trello.com/b/RK13PeeI/deliverit) for our Trello board

## Overview
Your task is to develop DeliverIT - a web application that serves the needs of a freight forwarding company. 
<br>
DeliverIT's customers can place orders on international shopping sites (like Amazon.de or eBay.com) and have their parcels delivered to the company's warehouses. 
<br>
DeliverIT has two types of users - customers and employees. The customers can see how many parcels they have on the way. The employees have a bit more capabilities. They can add new parcels to the system, modify existing ones, check how many parcels has a given warehouse received and more. 
<br>
Here is an example use case – you’ve order something online from a foreign shopping site and you wish to leave handling, customs fees, and transportation to somebody else. Here is where DeliverIT comes in. Their main warehouse is in Bulgaria, for example, and they have other warehouses in different countries, like Germany, Spain, USA, etc. When placing your order, you address it to a DeliverIT warehouse. When the package arrives, the employees see who it is for and create a parcel in the system. If your parcel appears on the site, then it has arrived at the warehouse successfully and the next step is for it to depart to the main warehouse. 
<br><br>

## Public Part
The public part must be accessible without authentication. 
Users who are not authenticated must be able to see how many customers DeliverIT has and what and where are the available warehouse locations. 
Also, anonymous users must have the ability to register. 
<br><br>

## Private Part
Accessible only if the user is authenticated. 
- Customers 
  - Each customer has a first and last name, email, and address for delivery. An address consists of a country, a city, and a street name. 
- Cities 
  - Each city has a name and a country. 
- Countries 
  - Each country has a name. 
- Warehouses 
  - A warehouse has an address. 
  - Warehouses must be created/modified by employees only. 
- Parcels 
  - Each parcel must have a customer who purchased it, a warehouse to which it would need to be delivered, weight and a category (the categories are predefined and can be Electronics, Clothing, Medical, etc.). 
  - A parcel is created by an employee. Besides creating, employees must be able to modify parcels as well. 
  - Customers must be able to see their past parcels as well as the parcels they have on the way. 
- Shipments 
  - A shipment must have a departure and arrival date, a status (the status is one of preparing, on the way, completed), and a collection of all the parcels that will be delivered with this shipment. A shipment without any parcels cannot depart. 
  - Employees must be able to add/remove parcels from a shipment, as well as see how many shipments are on the way. 
  - Employees should be able to see which the next shipment is to arrive for a given warehouse. 
  - Customers should have to ability to see the status of the shipment that holds a given parcel of theirs. 
<br><br>

## Administrative Part
Available to employees only. 
Employees 
The employees do not have an address, but they still have a first and last name and an email. 
<br><br>

## REST API 
To provide other developers with your service, you need to develop a REST API. It should leverage HTTP as a transport protocol and clear text JSON for the request and response payloads. 
A great API is nothing without a great documentation. The documentation holds the information that is required to successfully consume and integrate with an API. You must Use Swagger to document yours. 
The REST API provides the following capabilities: 

1. Cities 
- Read operations (must) 
2. Countries 
- Read operations (must) 
3. Warehouses 
- Read operations (must) 
- Create, Update, Delete operations (should) 
4. Shipments 
- CRUD operations (must)
- Filter by warehouse (must) 
- Filter by customer (should) 
5. Parcels 
- CRUD operations (must) 
- Filter by weight (must) 
- Filter by customer (must) 
- Filter by warehouse (must) 
- Filter by category (must) 
- Filter by multiple criteria (e.g., customer and category) (should) 
- Sort by weight or arrival date (should) 
- Sort by weight and arrival date (could) 
6. Customers 
- CRUD Operations (must) 
- Search by email (full or part of an email) (must) 
- Search by first/last name (must) 
- Query customer’s incoming parcels (should) • Search by multiple criteria (should)  • Search all fields from one word (e.g., “john” will search in the email, first and last name fields) (could) 
<br><br>

## Technical Requirements 
Your Web application should use the following technologies, frameworks, and development techniques:  
- Use ASP.NET Core, Entity Framework Core, Microsoft SQL Server, and Visual Studio.  
- The service layer (i.e., "business" functionality) must have at least 80%-unit test coverage  
- Apply proper data validation and error handling. 
- Use Git as source control. 
- Documentation of the project and project architecture (as .md file, including screenshots), see Deliverables for more details. 
- Following OOP principles when coding 
- Following KISS, SOLID, DRY principles when coding 
- Following REST API design best practices when designing the REST API (see Appendix) 
- Following BDD when writing tests
- You should implement sensible Exception handling and propagation 
- Try to think ahead. When developing something, think – “How hard would it be to change/modify this later?” 
<br><br>

## Optional Requirements 
- Integrate your project with a Continuous Integration server (e.g., GitLab’s own) and configure your unit tests to run on each commit to your master branch 
- Host your application's backend in a public hosting provider of your choice (e.g., AWS, Azure, Heroku) 
- Use branches while working with Git 
Note: These really are optional.  
<br><br>

## Deliverables
Commits in the GitLab repository should give a good overview of how the project was developed, which features were created first and the people who contributed. Contributions from all team members must be evident through the git commit history! The repository must contain the complete application source code!  
Provide a link to a GitLab repository with the following information in the README.md file: 
- Link to the Trello board (must) 
- Link to the Swagger documentation (must) 
- Link to the hosted project (if hosted online) 
- Documentation describing how to build and run the project (must) 
- Images of the database relations (must) 
<br><br>

## Project Defense
Each team will have a defense of their project with the trainers where they must explain the application structure, major architectural components and selected source code pieces demonstrating the implementation of key features.  
Use a Postman collection to gather your requests for convenience. 
<br><br>

## Expectations
You must understand the system you have created. 
It is OK if your application has flaws or is missing a couple of must's. What's not OK is if you do not know what is working and what is not. That said, you should be aware of any defects or incomplete functionality must be properly documented and secured. 
Some things you need to be able to explain during your project defense: 
- What are the most important things you have learned while working on this project? 
- What are the worst "hacks" in the project, or where do you think it needs improvement? 
- What more would you do if you had another week to work on the system? 
- What would you do differently if you were implementing the system again? 
<br><br>

## Appendix 
- [**Guidelines for designing good REST API**](https://florimond.dev/blog/articles/2018/08/restful-api-design-13-best-practices-to-make-your-users-happy/)
- [**Guidelines for URL encoding**](http://www.talisman.org/~erlkonig/misc/lunatech%5Ewhat-every-webdev-must-know-about-url-encoding/) 
- [**Awlays prefer constructor injection**](https://www.vojtechruzicka.com/field-dependency-injection-considered-harmful/) 
- [**Git commits - an effectife style guide**](https://dev.to/pavlosisaris/git-commits-an-effective-style-guide-2kkn)   
- [**How to Write a Git Commit Message**](https://chris.beams.io/posts/git-commit/) 


