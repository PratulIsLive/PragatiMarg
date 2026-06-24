# PragatiMarg

PragatiMarg is a Project and Task Management System built using ASP.NET Core and PostgreSQL.

I built this project to improve my backend development skills and get hands-on experience with concepts that are commonly used in real applications such as authentication, authorization, CRUD operations, pagination, soft delete, exception handling, and dashboard analytics.

The system allows users to create projects, manage tasks, track progress, and view project statistics through REST APIs.

---

## Features

### Authentication

* User Registration
* User Login
* JWT Authentication
* Protected Endpoints

### Project Management

* Create Project
* Update Project
* View Project Details
* Soft Delete Project
* Search Projects
* Sorting
* Pagination

### Task Management

* Create Tasks
* Update Task Status
* Update Task Priority
* Soft Delete Tasks
* Due Date Support
* Overdue Task Tracking

### Dashboard

* Total Projects
* Total Tasks
* Todo Tasks
* In Progress Tasks
* Completed Tasks
* Project Completion Percentage

### Backend Concepts Used

* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* JWT Authentication
* Exception Middleware
* DTO Pattern
* Service Layer Architecture
* Soft Delete Implementation

---

## Tech Stack

* C#
* ASP.NET Core
* Entity Framework Core
* PostgreSQL
* JWT Authentication

---

## Database Structure

### User

Stores user account information.

### Project

Stores project details created by users.

### TaskItem

Stores tasks associated with a project.

Relationship:

User → Project → TaskItem

---

## What I Learned

While building this project, I got practical experience with:

* Designing REST APIs
* Database relationships using Entity Framework Core
* JWT based authentication
* Pagination and filtering
* Middleware implementation
* Service layer architecture
* Project and task workflow management

---

## Author

Pratul Kumar

Backend Developer | ASP.NET Core | PostgreSQL
