# HR Management Toolbox

## Introduction
This repository contains two projects: a React application for the frontend and an ASP.NET Web API backend. The system serves as an HR Management Portal with features for employee management, leave requests, training schedules, and team information.

## Requirements
To run the application, ensure you have the following installed:
- Node.js and NPM
- .NET Framework and Visual Studio

## Installation and Setup
### React Application
1. Clone the repository or download the zip file.
2. Navigate to the `client` folder.
3. Run `npm install` to install dependencies.
4. Update the `.env` file with the backend API URL.
5. Run `npm start` to start the React application.

### ASP.NET Web API Backend
1. Clone the repository or download the zip file.
2. Open the solution in Visual Studio.
3. Build the solution.
4. Update the `appsettings.json` file with your database connection string.
5. Run the application.


## Dependencies
### Frontend (React App)
- Node.js
- React

### Backend (ASP.NET Web API)
- .NET Core 6
- MySql.Data 8.0.27
- Newtonsoft.Json 13.0.1

## Database
The application uses a MySQL database. You need to create the database schema and tables manually. Configure the connection string in `appsettings.json`.

## Custom Data Context
A custom data context class named `MySqlManager` handles database operations. It can be modified to connect to a different type of database if needed.

## Conclusion
This HR Management System provides basic functionality for managing employees, leaves, and training sessions. It is designed to be easily extendable and customizable for specific use cases.
