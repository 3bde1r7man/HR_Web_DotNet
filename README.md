# HR_Web

## Project Overview

This project aims to create a Human Resources website using DotNet framework. The website allows users to manage employee data, submit and review vacation requests, and perform various HR-related tasks.


## Functionalities

### 1. Employee Management

- User can **add a new employee** to the system.

- User can **update existing employee data**.

- User can **delete an existing employee data** through a delete button in the edit employee data page with a confirmation dialogue for the action before deletion occurs.

- User can **search for an employee by name** in a search for an employee screen, and employees with similar names should be rendered as a table.

### 2. Vacation Management

- Users can select a specific employee after searching to submit a **vacation form** for the same employee.

- User can **review all "submitted" vacations** of different employees with the ability to approve or reject the vacation submitted.

- If the user clicks on **approve button**:
  - Mark vacation as approved
  - Increment employee's actual number of vacation days
  - Decrement available number of vacation days for that employee
  
- If the user clicks on the **reject button**:
  - Mark vacation as rejected


### 3. Navigation

- Website should have a well-designed **navigation bar** to go through all pages and a home page.

## How to Run

1. Run the development server:
- You need to have .Net Development env downloaded on Microsoft Visual Studio.
- clone this repo using this link:
```bash
https://github.com/3bde1r7man/HR_Web_DotNet.git
```
- run the server
