# Ecommerce API
A web api that serves as the backend for an Ecommerce app

<h2>Description</h2>
This web-api features create, read, update, and delete (CRUD) endpoints for an ecommerce application, and is connected to a local sql server for data persistance. The api allows for the storage and retrieval 
of products, categories, and user account information, and also features specific endpoints that allow for data filtering such as sorting by price and filtering by category. It also features 
authentication for specific endpoints such as creating, updating, and deleting products and is reserved for administrative roles.

<h2>Technical Description</h2>
This web api is built using the ASP.NET Web API framework and C#. It leverages ASP.NET Core's Entity framework to establish an Object Relational Mapper (ORM) for seamless connectivity with a local 
SQL Server, and utilizes ASP.NET Core's Identity framework to establish authentication and authorization on specific endpoints. The SQL Server is managed and debugged using SQL Server Management Studio 
(SSMS). LINQ was used to query data and perform filtering and sorting for specific endpoints, and Fluent API was utilized to establish entity relationships such as one to many, and many to many for
database normalization. The application's archtecture was built following SOLID design principles and the repository design pattern for scalability and maintainability.

<h2>Technologies Used</h2>
<ul>
  <li>ASP.NET Web API</li>
  <li>Entity</li>
  <li>Identity</li>
  <li>C#</li>
  <li>LINQ</li>
  <li>Fluent API</li>
</ul>

<h2>Environments Used</h2>
<ul>
  <li>Windows 10</li>
  <li>Visual Studio 2022</li>
  <li>SQL Server Management Studio</li>
</ul>
