# E-Commerce App

This is an eCommerce app that allows users to browse and purchase products online. It includes the following features:

 - Browse products by category
 - View detailed product information and images
 - Add products to cart
 - Remove products from cart
 - Checkout and pay using Stripe payment gateway
 - Manage categories, products, and product types as an admin user

## Table of Contents

- [Technologies](#technologies)
- [Diagrams](#diagrams)
- [Features](#features)
- [Installation](#installation)
- [Getting Started](#gettingstarted)
- [Deployment](#deployment)
- [Contributing](#contributing)
- [License](#license)

## Technologies

### Architecture Overview

The app is built using a client-server architecture with a Blazor WebAssembly front-end and a .NET Core back-end. The client-side code is written in C# using the Blazor framework, and runs in the browser using WebAssembly. The server-side code is written in C# using .NET Core and provides RESTful APIs for the client-side code to consume.

### Why Blazor WebAssembly?

Blazor WebAssembly is a new front-end framework that allows developers to build web applications using C# and .NET. Here are some benefits of using Blazor WebAssembly for this project:

 - **Familiarity:** If your team already knows C# and .NET, then using Blazor WebAssembly will be an easy transition.
 - **Performance:** Blazor WebAssembly applications are compiled to WebAssembly, which runs at near-native speed in the browser. This means that your app will be fast and responsive, even for complex UIs.
 - **Code sharing:** With Blazor WebAssembly, you can share code between the client and server, which reduces duplication and makes it easier to maintain your codebase.
 - **Easy debugging:** Since you're using C# and .NET on the client-side, you can use your favorite IDE and debugger to debug your front-end code, which makes it easier to find and fix bugs.

### Blazor WebAssembly vs Blazor Server

Blazor Server and Blazor WebAssembly are two different models of Blazor development. While Blazor Server renders the application on the server-side and sends updates to the client, Blazor WebAssembly runs entirely on the client-side, in the browser. Here are some key differences between the two models:

 - Blazor Server requires a constant connection to the server, while Blazor WebAssembly does not. This means that Blazor Server applications may be slower and less responsive than Blazor WebAssembly applications.
 
 - Blazor WebAssembly applications are more suitable for offline scenarios or scenarios with limited network connectivity, while Blazor Server applications are more suitable for scenarios that require real-time updates and communication with the server.
 
 - Blazor Server has lower hardware requirements and can run on less powerful devices than Blazor WebAssembly.
 
 - Blazor Server can use any .NET libraries, while Blazor WebAssembly has limited access to .NET libraries.
 
 - Blazor WebAssembly has better browser compatibility than Blazor Server.

### Blazor WebAssembly vs Other Open-Source Frameworks

Blazor WebAssembly is a relatively new framework, and there are many other open-source frameworks available for building web applications. Here's how Blazor WebAssembly compares to some of the most popular frameworks:

 - **Angular:** Angular is a popular front-end framework that uses TypeScript and is maintained by Google. While Angular is a powerful framework, it has a steeper learning curve than Blazor WebAssembly and requires more boilerplate code.
 - **React:** React is a popular front-end library that uses JavaScript and is maintained by Facebook. React is fast and lightweight, but it can be difficult to learn for developers who are new to JavaScript or functional programming.
 - **Vue.js:** Vue.js is a popular front-end framework that uses JavaScript and is designed to be easy to learn and use. Vue.js is fast and lightweight, but it has a smaller community than Angular and React.

Overall, Blazor WebAssembly is a good choice for developers who are already familiar with .NET and want to build fast, responsive, and maintainable web applications using C#.

### Clean architecture with Blazor WebAssembly

Clean architecture is a software design pattern that separates the code into different layers, with each layer having a specific responsibility. Blazor WebAssembly is compatible with the Clean architecture pattern, which can help to create more maintainable and scalable applications.

Here is a diagram that shows how you can fit Clean architecture with Blazor WebAssembly:


                                    +-----------------+
                                    |     Clients     |
                                    +-----------------+
                                             |
                                             v
                                    +-----------------+
                                    |    Presenters   |
                                    +-----------------+
                                             |
                                             v
                                    +-----------------+
                                    |   Application   |
                                    +-----------------+
                                             |
                                             v
                                    +-----------------+
                                    |    Domain       |
                                    +-----------------+
                                             |
                                             v
                                    +-----------------+
                                    |   Infrastructure|
                                    +-----------------+


 - **Clients:** The client layer contains the Blazor WebAssembly app and handles the user interface and user interactions.
 
 - **Presenters:** The presenter layer contains the view models and presenters that handle the communication between the client and the application layers.
 
 - **Application:** The application layer contains the business logic of the application, including the use cases and services.
 
 - **Domain:** The domain layer contains the entities and business rules of the application.
 
 - **Infrastructure:** The infrastructure layer contains the implementation details of the application, such as data access and external dependencies.

## Diagrams

### Data Flow Diagram

The following diagram shows the high-level data flow of the app:

                   +---------------------+     +---------------------+
                   |                     |     |                     |
                   |  Client Web Browser |     |  Server Application  |
                   |                     |     |                     |
                   +----------+----------+     +----------+----------+
                              |                            |
                              |        HTTP Requests        |
                              |---------------------------->|
                              |                            |
                              |         REST APIs           |
                              |---------------------------->|
                              |                            |
                              |        HTTP Responses       |
                              |<----------------------------|
                              |                            |
                              |      HTML/JS/CSS files      |
                              |<----------------------------|
                              |                            |
                              |    Blazor WebAssembly       |
                              |        Application          |
                              |                            |
                              +----------+----------+      |
                                         |                 |
                                         |  Web API        |
                                         |-----------------+
                                         |                 |
                                         |    Database     |
                                         |<----------------+

In this diagram, the client-side code is running in the user's web browser and is written in C# using the Blazor WebAssembly framework. The server-side code is written in C# using .NET Core and provides RESTful APIs for the client-side code to consume.

When a user interacts with the app, such as browsing products, adding items to their cart, or checking out, the client-side code sends HTTP requests to the server-side code through RESTful APIs. The server-side code processes these requests and sends back HTTP responses with the appropriate data.

The server-side code interacts with a database to store and retrieve data. The database can be any relational database that supports .NET Core, such as Microsoft SQL Server or PostgreSQL.

Overall, this data flow diagram shows how the client-side and server-side components of the eCommerce app work together to provide a seamless and responsive user experience

### Data Flow Diagram with Clean Architecture

                     +---------------------+
                     |    Presentation     |
                     |       Layer         |
                     +----------+----------+
                                |
                                | UI Components
                                |
                                |
                     +----------+----------+
                     |   Application      |
                     |       Layer         |
                     +----------+----------+
                                |
                                | Use Cases
                                |
                                |
                     +----------+----------+
                     |      Domain         |
                     |       Layer         |
                     +----------+----------+
                                |
                                | Entities, Value Objects, Business Rules
                                |
                                |
                     +----------+----------+
                     |   Infrastructure    |
                     |       Layer         |
                     +----------+----------+
                                |
                                | Database, External APIs, Logging
                                |
                                |

In this diagram, the Presentation Layer is responsible for handling user interactions and rendering the UI components using Blazor WebAssembly. It depends on the Application Layer to execute the use cases and retrieve data from the Domain Layer.

The Application Layer coordinates the interactions between the different layers of the system. It implements the use cases by using services provided by the Domain Layer and communicates with the Infrastructure Layer to store and retrieve data.

The Domain Layer contains the core business logic of the eCommerce app. It models the domain concepts and entities, and enforces the business rules.

The Infrastructure Layer provides the necessary services and tools to the upper layers of the system. It communicates with external systems, such as databases and APIs, and provides logging and other infrastructure-related functionalities.

Overall, this layered architecture ensures that the different components of the eCommerce app are well-organized and have clear responsibilities, making it easier to maintain, test, and evolve over time.

## Features

Here are the List the main features and functionalities of this project, ,

    1. User authentication and authorization with login and registration
    2. Product catalog with search and filter functionality
    3. Product detail pages with images and Add to Cart
    4. Shopping cart with ability to add, update, and remove items
    5. Checkout process with order summary and payment options
    6. Admin dashboard with CRUD functionality for products, categories, and orders
    7. Integration with Stripe for secure payment processing
    8. Responsive design for optimal user experience on all devices

Make sure to highlight the features that make your project unique or stand out from similar projects.

## Installation

Provide instructions on how to install and set up your project, including any necessary dependencies or packages.

## Getting Started

To get started with the app, follow these steps:

    1. Clone the repository
    2. Build and run the server-side code using Visual Studio or dotnet run
    3. Build and run the client-side code using Visual Studio or dotnet run
    4 .Navigate to https://localhost:5001 in your web browser

## Deployment

[![.NET](https://github.com/Atharva-System/blazor-ecommerce/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Atharva-System/blazor-ecommerce/actions/workflows/dotnet.yml)

Blazor WebAssembly applications can be easily deployed as a static website, making it easy to host on popular static website hosting platforms such as GitHub Pages or Azure Static Web Apps. This approach eliminates the need for a server to host the application, reducing the hosting cost.

### Easy Deployment on GitHub Pages or Azure Static Web Apps

Deploying a Blazor WebAssembly App to GitHub Pages:

    1. Build the Blazor WebAssembly app using the dotnet publish command. This will generate a set of static files that can be hosted on GitHub Pages.
    2. Create a new repository on GitHub and push the generated files to the repository.
    3. In the repository settings, enable GitHub Pages and set the source to the gh-pages branch.
    4. Your Blazor WebAssembly app is now deployed to GitHub Pages and can be accessed using the URL provided by GitHub Pages.

Deploying a Blazor WebAssembly App to Azure Static Web Apps:

    1. Create a new Static Web App in the Azure Portal and connect it to your GitHub repository containing the Blazor WebAssembly app.
    2. Configure the build settings for the Static Web App to use the Blazor WebAssembly app.
    3. Once the build is complete, your Blazor WebAssembly app will be deployed to Azure Static Web Apps and can be accessed using the URL provided by Azure Static Web Apps.

### Hosting the Server

If your Blazor WebAssembly app requires a server-side component, you'll need to host the server separately. Here are some options for hosting a server:

    1. Use a cloud platform such as Azure, AWS, or Google Cloud to host the server. This provides scalability and reliability, but can be more expensive than hosting on a dedicated server.
    2. Host the server on a dedicated server. This provides more control over the server environment, but requires more maintenance and can be more expensive than cloud hosting.
    3. Use a third-party hosting service such as Heroku or Digital Ocean. These services provide a balance between control and convenience and can be more cost-effective than cloud hosting or dedicated server hosting.

When choosing a hosting option, consider factors such as scalability, reliability, cost, and maintenance requirements.

### Setting up API URL for Web Assembly App with Static Deployment

When deploying a Blazor WebAssembly app to a static hosting environment such as GitHub Pages or Azure Static Web Apps, you need to configure the app to use the correct API URL for the server-side component. Here's how you can set up the API URL for a Blazor WebAssembly app with static deployment:

 1. Define the API URL in the appsettings.json file of your server-side project. For example, you could add the following line to appsettings.json:
        
        {
            "ApiBaseUrl": "https://myapi.com"
        }

 2. In the client-side Blazor WebAssembly project, create a new file named appsettings.json in the wwwroot folder. This file should contain the same API URL that you defined in the server-side appsettings.json file:
        
        {
          "ApiBaseUrl": "https://myapi.com"
        }

 3. In your client-side Blazor WebAssembly app, configure the HttpClient to use the API URL from appsettings.json. You can do this by registering a named HttpClient in the ConfigureServices method of your Startup.cs file:
        
        public void ConfigureServices(IServiceCollection services)
        {
            // Register named HttpClient with the API URL from appsettings.json
            services.AddHttpClient("MyApi", client => {
                client.BaseAddress = new Uri(Configuration["ApiBaseUrl"]);
            });
        }

 4. In your client-side Blazor WebAssembly app, use the named HttpClient to make API requests. You can do this by injecting the named HttpClient into your components or services:
        
        @inject HttpClient MyApiHttpClient

        ...

        // Use the named HttpClient to make API requests
        var response = await MyApiHttpClient.GetAsync("/api/products");

By following these steps, you can set up the API URL for your Blazor WebAssembly app with static deployment.


## Contributing

Provide guidelines on how to contribute to your project, including code standards and contribution workflows.

## License

This project is licensed under the [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/Atharva-System/blazor-ecommerce/blob/main/LICENSE).
