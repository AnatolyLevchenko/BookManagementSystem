# Book Management System

## Overview

The Book Management System is a Blazor-based web application designed to manage books. The app provides functionalities such as creating, updating, and deleting books, along with paginated lists and search features.

![CI Pipeline](https://github.com/AnatolyLevchenko/BookManagementSystem/actions/workflows/ci.yml/badge.svg)


## Features

- Create, edit, and delete books.
- Paginated book list with search functionality.
- Separate pages for viewing book details.
- Fully responsive design.

## Technologies Used

- **Blazor WebAssembly** (Client-side framework)
- **.NET 8** (Backend framework)
- **Entity Framework Core** (Database access)
- **SQL Inmemory** (Database)
- **Bootstrap** (Styling)

## Screenshots

![Book Management System Screenshot](Screenshot.png)

## Debuggin

To test locally run both projects `BookManagement.API` and `BookManagementSystem.Client`

More info here https://learn.microsoft.com/en-us/visualstudio/ide/how-to-set-multiple-startup-projects?view=vs-2022

## Docker compose

1. Navigate to `src` directory
2. Run `docker-compose up` command
3. Open application at `http://localhost:6432/`
